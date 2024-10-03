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

        _self.onChangeProduct(executionContext);



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

                        let importo = _self.getBaseAmount(amount, quantity);
                        let scontoTotale = formContext.getAttribute(_self.formModel.fields.manualdiscountamount).getValue();
                        let totaleImponibile = _self.getTaxableAmount(importo, scontoTotale);

                        formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(amount);
                        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(importo);
                        formContext.getAttribute(_self.formModel.fields.res_taxableamount).setValue(totaleImponibile);
                    },
                    error => {
                        console.log(error.message);
                    }
                );
            }
            else {
                formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(null);
                formContext.getAttribute(_self.formModel.fields.baseamount).setValue(_self.getBaseAmount(amount, quantity));
                formContext.getAttribute(_self.formModel.fields.res_taxableamount).setValue(0);
            }
        }
    }
    //---------------------------------------------------
    _self.onChangeQuantity = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let pricePerUnit = formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();
        let importo = _self.getBaseAmount(pricePerUnit, quantity);
        let scontoTotale = formContext.getAttribute(_self.formModel.fields.manualdiscountamount).getValue();
        let totaleImponibile = _self.getTaxableAmount(importo, scontoTotale);

        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(importo);
        formContext.getAttribute(_self.formModel.fields.res_taxableamount).setValue(totaleImponibile);
    }
    //---------------------------------------------------
    _self.onChangePricePerUnit = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let prezzoUnitario = formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();
        let importo = _self.getBaseAmount(prezzoUnitario, quantity);
        let scontoTotale = formContext.getAttribute(_self.formModel.fields.manualdiscountamount).getValue();
        let totaleImponibile = _self.getTaxableAmount(importo, scontoTotale);

        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(importo);
        formContext.getAttribute(_self.formModel.fields.res_taxableamount).setValue(totaleImponibile);
    };
    //---------------------------------------------------
    _self.getBaseAmount = function (pricePerUnit, quantity) {

        let q = quantity != null ? quantity : 0;
        let p = pricePerUnit != null ? pricePerUnit : 0;
        let result = p * q;

        return result;

    };
    //---------------------------------------------------
    _self.getTax = function (imponibile, aliquota) {
        let i = imponibile;
        let a = aliquota;
        let tax = null;

        if (i != null && a != null && i >= 0) {

            tax = (i * a) / 100;
        }

        return tax;
    };
    //---------------------------------------------------
    _self.getTaxableAmount = function (importo, scontoTot) {

        let i = importo;
        let s = scontoTot != null ? scontoTot : 0;

        return i != null && i != 0 ? i - s : null;
    };

    //---------------------------------------------------
    _self.onChangeProduct = async function (executionContext) {
        var formContext = executionContext.getFormContext();

        let productLookup = formContext.getAttribute(_self.formModel.fields.productid).getValue();

        if (productLookup != null) {

            let product = await _self.getProductDetails(productLookup[0].id);

            let codiceIva = product._res_vatnumberid_value == null ? null : [{
                id: product._res_vatnumberid_value,
                entityType: 'res_vatnumber',
                name: product["CodiceIva.res_name"]
            }];

            let rate = product.res_vatnumberid == null ? null : product["CodiceIva.res_rate"];



            formContext.getAttribute(_self.formModel.fields.res_itemcode).setValue(product.productnumber);
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(codiceIva);
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(rate);
        }
        else {
            formContext.getAttribute(_self.formModel.fields.res_itemcode).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(null);
        }

    }
    //---------------------------------------------------
    _self.onChangeVatNumber = function (executionContext) {
        var formContext = executionContext.getFormContext();

        let vatNumberLookup = formContext.getAttribute(_self.formModel.fields.res_vatnumberid).getValue();


        if (vatNumberLookup != null) {
            let queryOptions = "?$select=res_rate";

            Xrm.WebApi.retrieveRecord("res_vatnumber", vatNumberLookup[0].id, queryOptions).then(
                function success(result) {
                    let imponibile = formContext.getAttribute(_self.formModel.fields.res_taxableamount).getValue();

                    formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(result.res_rate);
                    formContext.getAttribute(_self.formModel.fields.tax).setValue(_self.getTax(imponibile, result.res_rate));
                },
                function error(error) {

                    console.log(error);
                }
            );
        }
        else {
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(null);
            formContext.getAttribute(_self.formModel.fields.tax).setValue(null);
        }
    };
    //---------------------------------------------------
    _self.getProductDetails = function (productId) {
        return new Promise(function (resolve, reject) {

            var fetchData = {
                "productid": productId
            };
            var fetchXml = [
                "?fetchXml=<fetch>",
                "  <entity name='product'>",
                "    <attribute name='res_vatnumberid'/>",
                "    <attribute name='productnumber'/>",
                "    <filter>",
                "      <condition attribute='productid' operator='eq' value='", fetchData.productid, "' />",
                "    </filter>",
                "    <link-entity name='res_vatnumber' from='res_vatnumberid' to='res_vatnumberid' link-type='outer' alias='CodiceIva'>",
                "      <attribute name='res_rate'/>",
                "      <attribute name='res_name'/>",
                "    </link-entity>",
                "  </entity>",
                "</fetch>"
            ].join("");

            Xrm.WebApi.retrieveMultipleRecords("product", fetchXml).then(
                results => {
                    if (results.entities.length > 0) {
                        resolve(results.entities[0]);
                    } else {
                        reject(new Error("No product found"));
                    }
                },
                error => {
                    reject(error);
                }
            );
        });
    };
    //---------------------------------------------------
    _self.setManualDiscountAmount = executionContext => {
        const formContext = executionContext.getFormContext();
        const eventSourceAttribute = executionContext.getEventSource();
        const eventSourceControl = formContext.getControl(eventSourceAttribute.getName());


        const manualDiscountAmountControl = formContext.getControl(_self.formModel.fields.manualdiscountamount);
        const taxableAmountControl = formContext.getControl(_self.formModel.fields.res_taxableamount);
        const baseAmountControl = formContext.getControl(_self.formModel.fields.baseamount);

        const discountPercent1Control = formContext.getControl(_self.formModel.fields.res_discountpercentage1);
        const discountPercent2Control = formContext.getControl(_self.formModel.fields.res_discountpercentage2);
        const discountPercent3Control = formContext.getControl(_self.formModel.fields.res_discountpercentage3);

        const baseAmount = baseAmountControl.getAttribute().getValue() ?? 0;                                        //importo
        const discountPercent1 = discountPercent1Control.getAttribute().getValue() ?? 0;
        const discountPercent2 = discountPercent2Control.getAttribute().getValue() ?? 0;
        const discountPercent3 = discountPercent3Control.getAttribute().getValue() ?? 0;
        const discountPercentages = [discountPercent1, discountPercent2, discountPercent3];

        const totalDiscountPercentage = discountPercentages.reduce((sum, percent) => sum + percent, 0);             //totale sconto %

        if (totalDiscountPercentage <= 100) {

            discountPercent1Control.clearNotification();
            discountPercent2Control.clearNotification();
            discountPercent3Control.clearNotification();

            const manualDiscountAmount = baseAmount * (totalDiscountPercentage / 100);                                  //sconto applicato

            manualDiscountAmountControl.getAttribute().setValue(manualDiscountAmount);

            _self.setTaxableAmount(executionContext);
            _self.setExtendedAmount(executionContext);

        } else {
            eventSourceControl.setNotification("Lo sconto percentuale complessivo non pu\u00f2 essere maggiore di 100.");
        }
    }
    //---------------------------------------------------
    _self.setTaxableAmount = executionContext => {
        const formContext = executionContext.getFormContext();

        const taxableAmountControl = formContext.getControl(_self.formModel.fields.res_taxableamount);
        const manualDiscountAmountControl = formContext.getControl(_self.formModel.fields.manualdiscountamount);
        const baseAmountControl = formContext.getControl(_self.formModel.fields.baseamount);

        const manualDiscountAmount = manualDiscountAmountControl.getAttribute().getValue() ?? 0;                 //sconto totale
        const baseAmount = baseAmountControl.getAttribute().getValue() ?? 0;                                     //importo
        const taxableAmount = baseAmount - manualDiscountAmount;           //totale imponibile
        taxableAmountControl.getAttribute().setValue(taxableAmount);

        let aliquota = formContext.getAttribute(_self.formModel.fields.res_vatrate).getValue();
        formContext.getAttribute(_self.formModel.fields.tax).setValue(_self.getTax(taxableAmount, aliquota));
    };
    //---------------------------------------------------
    _self.setExtendedAmount = executionContext => {
        const formContext = executionContext.getFormContext();

        const extendedAmountControl = formContext.getControl(_self.formModel.fields.extendedamount);
        const taxableAmountControl = formContext.getControl(_self.formModel.fields.res_taxableamount);
        const taxControl = formContext.getControl(_self.formModel.fields.tax);

        const taxableAmount = taxableAmountControl.getAttribute().getValue() ?? 0;                                  //totale imponibile        
        const tax = taxControl.getAttribute().getValue() ?? 0;                                                      //totale iva

        const extendedAmount = taxableAmount + tax;                                                                 //importo totale
        extendedAmountControl.getAttribute().setValue(extendedAmount);
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
        formContext.getAttribute(_self.formModel.fields.productid).addOnChange(_self.onChangeProduct);
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).addOnChange(_self.onChangeVatNumber);

        formContext.getAttribute(_self.formModel.fields.res_discountpercentage1).addOnChange(_self.setManualDiscountAmount);
        formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).addOnChange(_self.setManualDiscountAmount);
        formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).addOnChange(_self.setManualDiscountAmount);


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