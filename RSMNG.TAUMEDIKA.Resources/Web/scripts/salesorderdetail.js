//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDERDETAIL) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDERDETAIL = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///costanti entità
            logicalName: "salesorderdetail",
            displayName: "Riga ordine",
        },
        fields: {
            ///Importo
            baseamount: "baseamount",
            ///Descrizione
            description: "description",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Importo totale
            extendedamount: "extendedamount",
            ///Prezzi
            ispriceoverridden: "ispriceoverridden",
            ///Numero voce
            lineitemnumber: "lineitemnumber",
            ///Sconto totale
            manualdiscountamount: "manualdiscountamount",
            ///Prezzo unitario
            priceperunit: "priceperunit",
            ///Associazione elemento aggregazione
            productassociationid: "productassociationid",
            ///Prodotto fuori catalogo
            productdescription: "productdescription",
            ///Prodotto esistente
            productid: "productid",
            ///Nome prodotto
            productname: "productname",
            ///Numero prodotto
            productnumber: "productnumber",
            ///Tipo di prodotto
            producttypecode: "producttypecode",
            ///Quantità
            quantity: "quantity",
            ///Quantità inevasa
            quantitybackordered: "quantitybackordered",
            ///Quantità annullata
            quantitycancelled: "quantitycancelled",
            ///Quantità consegnata
            quantityshipped: "quantityshipped",
            ///ID prodotto offerta
            quotedetailid: "quotedetailid",
            ///Sconto % 1
            res_discountpercentage1: "res_discountpercentage1",
            ///Sconto % 2
            res_discountpercentage2: "res_discountpercentage2",
            ///Sconto % 3
            res_discountpercentage3: "res_discountpercentage3",
            ///Omaggio
            res_ishomage: "res_ishomage",
            ///Codice Articolo
            res_itemcode: "res_itemcode",
            ///Totale imponibile
            res_taxableamount: "res_taxableamount",
            ///Codice IVA
            res_vatnumberid: "res_vatnumberid",
            ///Aliquota IVA
            res_vatrate: "res_vatrate",
            ///Prodotto ordine
            salesorderdetailid: "salesorderdetailid",
            ///Nome
            salesorderdetailname: "salesorderdetailname",
            ///Ordine
            salesorderid: "salesorderid",
            ///Totale IVA
            tax: "tax",
            ///Valuta
            transactioncurrencyid: "transactioncurrencyid",
            ///Unità
            uomid: "uomid",
            ///Sconto per volume
            volumediscountamount: "volumediscountamount",
            ///Sconto per volume (Base)
            volumediscountamount_base: "volumediscountamount_base",
            ///Spedizione
            willcall: "willcall",
        },
        tabs: {

        },
        sections: {

        }
    };

    _self.onSaveForm = function (executionContext) {
        if (executionContext.getEventArgs().getSaveMode() == 70) {
            executionContext.getEventArgs().preventDefault();
            return;
        }
    };
    //---------------------------------------------------
    _self.onLoadCreateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onChangeIsPriceOverridden = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let isPriceOverriden = formContext.getAttribute(_self.formModel.fields.ispriceoverridden).getValue();
        let productId = formContext.getAttribute(_self.formModel.fields.productid).getValue();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let amount = null;

        if (!isPriceOverriden) {
            if (productId !== null && productId !== undefined) {

                // Recupero importo presente su Voce Listino
                var fetchData = {
                    "productid": productId[0].id
                };
                var fetchXml = [
                    "?fetchXml=<fetch>",
                    "  <entity name='product'>",
                    "    <filter>",
                    "      <condition attribute='productid' operator='eq' value='", fetchData.productid, "' />",
                    "    </filter>",
                    "    <link-entity name='pricelevel' from='pricelevelid' to='pricelevelid' link-type='inner' alias='Listino'>",
                    "      <link-entity name='productpricelevel' from='pricelevelid' to='pricelevelid' alias='VoceListino'>",
                    "        <attribute name='amount'/>",
                    "      </link-entity>",
                    "    </link-entity>",
                    "  </entity>",
                    "</fetch>"
                ].join("");

                Xrm.WebApi.retrieveMultipleRecords("product", fetchXml).then(
                    results => {

                        amount = results.entities[0]["VoceListino.amount"] != undefined ? results.entities[0]["VoceListino.amount"] : null;

                        formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(amount);
                        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(_self.setBaseAmount(amount, quantity));
                    },
                    error => {
                        console.log(error.message);
                    }
                );
            }
            else {
                formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(null);
                formContext.getAttribute(_self.formModel.fields.baseamount).setValue(_self.setBaseAmount(amount, quantity));
            }
        }
    }
    //---------------------------------------------------
    _self.onChangeQuantity = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let pricePerUnit = formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();

        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(_self.setBaseAmount(pricePerUnit, quantity));
    }
    //---------------------------------------------------
    _self.onChangePricePerUnit = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let pricePerUnit = formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();

        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(_self.setBaseAmount(pricePerUnit, quantity));
    }
    //---------------------------------------------------
    _self.setBaseAmount = function (pricePerUnit, quantity) {

        let q = quantity != null ? quantity : 0;
        let p = pricePerUnit != null ? pricePerUnit : 0;
        let result = p * q;

        return result;

    }
    //---------------------------------------------------

    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        var formContext = executionContext.getFormContext();


        //Init event
        formContext.getAttribute(_self.formModel.fields.ispriceoverridden).addOnChange(_self.onChangeIsPriceOverridden);
        formContext.getAttribute(_self.formModel.fields.quantity).addOnChange(_self.onChangeQuantity);
        formContext.getAttribute(_self.formModel.fields.priceperunit).addOnChange(_self.onChangePricePerUnit);


        //Init function

        switch (formContext.ui.getFormType()) {
            case RSMNG.Global.CRM_FORM_TYPE_CREATE:
                _self.onLoadCreateForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_UPDATE:
                _self.onLoadUpdateForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_READONLY:
                _self.onLoadReadyOnlyForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_QUICKCREATE:
                _self.onLoadCreateForm(executionContext);
                break;
        }
    }
}
).call(RSMNG.TAUMEDIKA.SALESORDERDETAIL);