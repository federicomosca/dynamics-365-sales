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

    //---------------------------------------------------
    _self.onChangeUomId = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let unita = formContext.getAttribute(_self.formModel.fields.uomid).getValue();

        if (unita != null) {
            Xrm.Utility.showProgressIndicator("Ricalcolo...");
            setTimeout(function () {
                _self.setBaseAmount(executionContext, { quantita: quantity, prezzoUnitario: undefined, aliquota: undefined });
                formContext.data.entity.save();
                Xrm.Utility.closeProgressIndicator();
            }, 2000);

        } else {
            formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(null);
            formContext.getAttribute(_self.formModel.fields.ispriceoverridden).setValue(false);
            formContext.getControl(_self.formModel.fields.priceperunit).setDisabled(true);

            _self.setBaseAmount(executionContext, { quantita: quantity, prezzoUnitario: null });
        }
    };
    //---------------------------------------------------
    _self.onChangeIsPriceOverridden = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        let isPriceOverriden = formContext.getAttribute(_self.formModel.fields.ispriceoverridden).getValue();
        let productId = formContext.getAttribute(_self.formModel.fields.productid).getValue();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let baseAmountOld = formContext.getAttribute(_self.formModel.fields.baseamount).getValue();

        if (!isPriceOverriden) {
            if (productId !== null && productId !== undefined) {


                Xrm.Utility.showProgressIndicator("Ricalcolo...");
                setTimeout(function () {

                    _self.delayRecalcProduct(executionContext, baseAmountOld);

                }, 2000);

            }
            else {
                formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(null);

                _self.setBaseAmount(executionContext, { quantita: quantity, prezzoUnitario: null });
            }
        }
    };
    //---------------------------------------------------
    _self.onChangeQuantity = function (executionContext) {

        var formContext = executionContext.getFormContext();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();

        _self.setBaseAmount(executionContext, { quantita: quantity, prezzoUnitario: null });
    }
    //---------------------------------------------------
    _self.onChangePricePerUnit = function (executionContext) {

        var formContext = executionContext.getFormContext();
        let pricePerUnit = formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();

        _self.setBaseAmount(executionContext, { quantita: null, prezzoUnitario: pricePerUnit });
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

        if (i != null && i >= 0) {
            a = a === 0 || a == null ? a = 1 : a;
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
    _self.getExtendedAmount = function (imponibile, totIva) {

        let i = imponibile != null ? imponibile : 0;
        let v = totIva != null ? totIva : 0;

        return (i + v);
    };
    //---------------------------------------------------
    _self.getManualDiscountAmount = function (importo, arrDisc = []) {
        let manualDiscount = 0;
        let importoModificato = importo;

        if (arrDisc.length > 0) {

            for (let i = 0; i < arrDisc.length; i++) {

                if (Number.isFinite(arrDisc[i])) {

                    let sconto = importoModificato * arrDisc[i] / 100;

                    importoModificato -= sconto;
                    manualDiscount += sconto;
                } else {
                    console.log("passed not a finite number to the getManualDiscountAmount");
                }
            }

        }


        return manualDiscount;

    };

    //---------------------------------------------------
    _self.onChangeProduct = async function (executionContext) {
        var formContext = executionContext.getFormContext();

        let productLookup = formContext.getAttribute(_self.formModel.fields.productid).getValue();
        let scontoTot = formContext.getAttribute(_self.formModel.fields.manualdiscountamount).getValue();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let importo = null;
        let imponibile;

        if (productLookup != null) {
            let productId = RSMNG.TAUMEDIKA.GLOBAL.convertGuid(productLookup[0].id);
            let product = await _self.getProductDetails(productId);

            let codiceIva = product._res_vatnumberid_value == null ? null : [{
                id: product._res_vatnumberid_value,
                entityType: 'res_vatnumber',
                name: product["CodiceIva.res_name"]
            }];

            let rate = product._res_vatnumberid_value == null ? null : product["CodiceIva.res_rate"];

            formContext.getAttribute(_self.formModel.fields.res_itemcode).setValue(product.productnumber);
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(codiceIva); // codice Iva
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(rate); // aliquota

            Xrm.Utility.showProgressIndicator("Ricalcolo...");
            setTimeout(function () {
                _self.setBaseAmount(executionContext, { quantita: quantity, prezzoUnitario: undefined, aliquota: rate });
                formContext.data.entity.save();
                Xrm.Utility.closeProgressIndicator();
            }, 2000);

        }
        else {
            imponibile = _self.getTaxableAmount(importo, scontoTot);

            formContext.getAttribute(_self.formModel.fields.priceperunit).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_itemcode).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(null);
            formContext.getAttribute(_self.formModel.fields.baseamount).setValue(null);

            _self.setManualDiscountAmount(executionContext, { importo: null, aliquota: null });
        }

    };
    //---------------------------------------------------
    _self.delayRecalcProduct = function (executionContext, oldBaseAmount) {

        var formContext = executionContext.getFormContext();

        if (formContext.getAttribute(_self.formModel.fields.baseamount).getValue() == oldBaseAmount) {
            setTimeout(function () {
                _self.delayRecalcProduct(executionContext);
            }, 2000);
        } else {

            _self.setBaseAmount(executionContext, { quantita: undefined, prezzoUnitario: undefined, aliquota: undefined });
            formContext.data.entity.save();
            Xrm.Utility.closeProgressIndicator();
        }

    };
    //---------------------------------------------------
    _self.onChangeVatNumber = function (executionContext) {
        var formContext = executionContext.getFormContext();

        let vatNumberLookup = formContext.getAttribute(_self.formModel.fields.res_vatnumberid).getValue();
        let imponibileTot = formContext.getAttribute(_self.formModel.fields.res_taxableamount).getValue();

        if (vatNumberLookup != null) {
            let queryOptions = "?$select=res_rate";

            Xrm.WebApi.retrieveRecord("res_vatnumber", vatNumberLookup[0].id, queryOptions).then(
                function success(result) {

                    formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(result.res_rate);  // aliquota
                    let totIva = _self.setTax(executionContext, { imponibile: imponibileTot, aliquota: result.res_rate }); // totale iva
                    _self.setExtendedAmount(executionContext, { imponibile: imponibileTot, totaleIva: totIva }); // importo totale

                },
                function error(error) {

                    console.log(error);
                }
            );
        }
        else {
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(null);
            formContext.getAttribute(_self.formModel.fields.tax).setValue(null);
            formContext.getAttribute(_self.formModel.fields.extendedamount).setValue(_self.getExtendedAmount(imponibileTot, 0));
        }
    };
    //---------------------------------------------------
    _self.onChangeIsHomage = executionContext => {
        const formContext = executionContext.getFormContext();

        let isHomage = formContext.getAttribute(_self.formModel.fields.res_ishomage).getValue();

        //sconto%1 not editable e valorizzato a 100
        let disc1 = isHomage ? 100 : null;
        formContext.getAttribute(_self.formModel.fields.res_discountpercentage1).setValue(disc1);
        formContext.getControl(_self.formModel.fields.res_discountpercentage1).setDisabled(isHomage);

        //sconto%2 e sconto%3 not editable e valorizzati a 0
        if (isHomage || !disc1) {
            formContext.getControl(_self.formModel.fields.res_discountpercentage2).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(true);

            formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).setValue(null);
        }

        // ricalcolo Tot Imponibile -> Totale Iva-> Importo Totale
        let importo = formContext.getAttribute(_self.formModel.fields.baseamount).getValue();
        let scontoTot = _self.getManualDiscountAmount(importo, [disc1]);
        let imponibile = _self.getTaxableAmount(importo, scontoTot);
        let aliquota = formContext.getAttribute(_self.formModel.fields.res_vatrate).getValue();
        let totIva = _self.getTax(imponibile, aliquota);
        let importoTot = _self.getExtendedAmount(imponibile, totIva);

        formContext.getAttribute(_self.formModel.fields.manualdiscountamount).setValue(scontoTot);
        formContext.getAttribute(_self.formModel.fields.res_taxableamount).setValue(imponibile);
        formContext.getAttribute(_self.formModel.fields.tax).setValue(totIva);
        formContext.getAttribute(_self.formModel.fields.extendedamount).setValue(importoTot);
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
    _self.onChangeDiscountPercent1 = function (executionContext) {
        _self.setManualDiscountAmount(executionContext, { importo: undefined, aliquota: undefined });

    };
    _self.onChangeDiscountPercent2 = function (executionContext) {
        _self.setManualDiscountAmount(executionContext, { importo: undefined, aliquota: undefined });
    };
    _self.onChangeDiscountPercent3 = function (executionContext) {
        _self.setManualDiscountAmount(executionContext, { importo: undefined, aliquota: undefined });
    };
    //---------------------------------------------------
    _self.setManualDiscountAmount = (executionContext, data) => {
        const formContext = executionContext.getFormContext();
        let eventSourceAttribute = executionContext.getEventSource();
        let manualDiscountAmount = null;

        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).getValue() ?? 0;
        let disc3 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).getValue() ?? 0;

        //-----------------------
        if (disc1 == 0 || disc1 == 100) {
            formContext.getControl(_self.formModel.fields.res_discountpercentage2).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(true);

            formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).setValue(null);
            disc2 = 0;
            disc3 = 0;
        }
        else {
            if (disc2 == 0 || disc2 == 100) {
                formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(true);
                formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).setValue(null);
                disc3 = 0;
            }
            else {
                formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(false);
            }
            formContext.getControl(_self.formModel.fields.res_discountpercentage2).setDisabled(false);
        }
        //----------------------

        let baseAmount = data.importo != undefined ? data.importo : formContext.getAttribute(_self.formModel.fields.baseamount).getValue();
        let vatRate = data.aliquota != undefined ? data.aliquota : formContext.getAttribute(_self.formModel.fields.res_vatrate).getValue();

        formContext.getControl(_self.formModel.fields.res_discountpercentage1).clearNotification();
        formContext.getControl(_self.formModel.fields.res_discountpercentage2).clearNotification();
        formContext.getControl(_self.formModel.fields.res_discountpercentage3).clearNotification();

        formContext.getAttribute(_self.formModel.fields.manualdiscountamount).setValue(_self.getManualDiscountAmount(baseAmount, [disc1, disc2, disc3]));

        let imponibileTot = _self.setTaxableAmount(executionContext, { importo: baseAmount, scontoTot: null });
        let totIva = _self.setTax(executionContext, { imponibile: imponibileTot, aliquota: vatRate });
        _self.setExtendedAmount(executionContext, { imponibile: imponibileTot, totaleIva: totIva });

        return manualDiscountAmount;
    };
    //---------Totale-Imponibile------------------------
    _self.setTaxableAmount = (executionContext, data) => {
        const formContext = executionContext.getFormContext();

        let baseAmount = data.importo != null ? data.importo : formContext.getAttribute(_self.formModel.fields.baseamount).getValue();
        let manualDiscountAmount = data.scontoTot != null ? data.scontoTot : formContext.getAttribute(_self.formModel.fields.manualdiscountamount).getValue();

        let taxableAmount = _self.getTaxableAmount(baseAmount, manualDiscountAmount);

        formContext.getAttribute(_self.formModel.fields.res_taxableamount).setValue(taxableAmount);

        return taxableAmount;
    };
    //---------Totale Iva-------------------------------
    _self.setTax = (executionContext, data) => {
        const formContext = executionContext.getFormContext();

        let imponibile = data.imponibile != undefined ? data.imponibile : formContext.getAttribute(_self.formModel.fields.res_taxableamount).getValue();
        let aliquota = data.aliquota != undefined ? data.aliquota : formContext.getAttribute(_self.formModel.fields.res_vatrate).getValue();

        let totaleIva = _self.getTax(imponibile, aliquota);

        formContext.getAttribute(_self.formModel.fields.tax).setValue(totaleIva);

        return totaleIva;
    };
    //---------Importo Totale---------------------------
    _self.setExtendedAmount = (executionContext, data) => {
        const formContext = executionContext.getFormContext();

        let imponibile = data.imponibile != null ? data.imponibile : formContext.getAttribute(_self.formModel.fields.res_taxableamount).getValue();
        let totaleIva = data.totaleIva != null ? data.totaleIva : formContext.getAttribute(_self.formModel.fields.tax).getValue();

        let importoTotale = _self.getExtendedAmount(imponibile, totaleIva);

        formContext.getAttribute(_self.formModel.fields.extendedamount).setValue(importoTotale);
    };
    //---------Importo----------------------------------
    _self.setBaseAmount = (executionContext, data) => {
        var formContext = executionContext.getFormContext();

        let quantita = data.quantita != null ? data.quantita : formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let prezzoUnitario = data.prezzoUnitario != null ? data.prezzoUnitario : formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();
        let baseAmount = _self.getBaseAmount(prezzoUnitario, quantita);

        formContext.getAttribute(_self.formModel.fields.baseamount).setValue(baseAmount);

        // Ricalcolo sconto totale
        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).getValue() ?? 0;
        let disc3 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).getValue() ?? 0;
        let scontoTotale = _self.getManualDiscountAmount(baseAmount, [disc1, disc2, disc3]);
        formContext.getAttribute(_self.formModel.fields.manualdiscountamount).setValue(scontoTotale);

        let imponibileTot = _self.setTaxableAmount(executionContext, { importo: baseAmount, scontoTot: scontoTotale });
        let totIva = _self.setTax(executionContext, { imponibile: imponibileTot, aliquota: data.aliquota != undefined ? data.aliquota : null });
        _self.setExtendedAmount(executionContext, { imponibile: imponibileTot, totaleIva: totIva });


        return baseAmount;
    };
    //---------------------------------------------------
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

        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).getValue() ?? 0;

        if (disc1 == 0) {
            formContext.getControl(_self.formModel.fields.res_discountpercentage2).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(true);

            formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).setValue(null);
        }
        else {
            if (disc2 == 0) {
                formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(true);
                formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).setValue(null);
            }
            else {
                formContext.getControl(_self.formModel.fields.res_discountpercentage3).setDisabled(false);
            }
            formContext.getControl(_self.formModel.fields.res_discountpercentage2).setDisabled(false);
        }
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
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
        formContext.getAttribute(_self.formModel.fields.uomid).addOnChange(_self.onChangeUomId);

        formContext.getAttribute(_self.formModel.fields.res_discountpercentage1).addOnChange(_self.onChangeDiscountPercent1);
        formContext.getAttribute(_self.formModel.fields.res_discountpercentage2).addOnChange(_self.onChangeDiscountPercent2);
        formContext.getAttribute(_self.formModel.fields.res_discountpercentage3).addOnChange(_self.onChangeDiscountPercent3);
        formContext.getAttribute(_self.formModel.fields.res_ishomage).addOnChange(_self.onChangeIsHomage);

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