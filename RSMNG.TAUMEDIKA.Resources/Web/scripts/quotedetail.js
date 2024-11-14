//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTEDETAIL) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTEDETAIL = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            logicalName: "quotedetail",
            displayName: "Riga offerta"
        },
        fields: {
            ///Importo
            baseamount: "baseamount",
            ///Importo (Base)
            baseamount_base: "baseamount_base",
            ///Descrizione
            description: "description",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Importo totale
            extendedamount: "extendedamount",
            ///Importo totale (Base)
            extendedamount_base: "extendedamount_base",
            ///Prezzi
            ispriceoverridden: "ispriceoverridden",
            ///Seleziona prodotto
            isproductoverridden: "isproductoverridden",
            ///Numero voce
            lineitemnumber: "lineitemnumber",
            ///Sconto totale
            manualdiscountamount: "manualdiscountamount",
            ///Sconto manuale (Base)
            manualdiscountamount_base: "manualdiscountamount_base",
            ///Aggregazione padre
            parentbundleid: "parentbundleid",
            ///Prodotto di aggregazione prodotti
            parentbundleidref: "parentbundleidref",
            ///Prezzo unitario
            priceperunit: "priceperunit",
            ///Prezzo unitario (Base)
            priceperunit_base: "priceperunit_base",
            ///Errore di determinazione dei prezzi 
            pricingerrorcode: "pricingerrorcode",
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
            ///Configurazione proprietà
            propertyconfigurationstatus: "propertyconfigurationstatus",
            ///Quantità
            quantity: "quantity",
            ///Metodo di creazione
            quotecreationmethod: "quotecreationmethod",
            ///Prodotto offerta
            quotedetailid: "quotedetailid",
            ///Nome
            quotedetailname: "quotedetailname",
            ///Offerta
            quoteid: "quoteid",
            ///Stato offerta
            quotestatecode: "quotestatecode",
            ///Data consegna richiesta
            requestdeliveryby: "requestdeliveryby",
            ///Sconto % 1
            res_discountpercent1: "res_discountpercent1",
            ///Sconto % 2
            res_discountpercent2: "res_discountpercent2",
            ///Sconto % 3
            res_discountpercent3: "res_discountpercent3",
            ///Omaggio
            res_ishomage: "res_ishomage",
            ///Codice Articolo
            res_itemcode: "res_itemcode",
            ///Totale imponibile
            res_taxableamount: "res_taxableamount",
            ///Totale imponibile (base)
            res_taxableamount_base: "res_taxableamount_base",
            ///Codice IVA
            res_vatnumberid: "res_vatnumberid",
            ///Aliquota IVA
            res_vatrate: "res_vatrate",
            ///Venditore
            salesrepid: "salesrepid",
            ///Numero sequenza
            sequencenumber: "sequencenumber",
            ///ID indirizzo di spedizione
            shipto_addressid: "shipto_addressid",
            ///Città spedizione
            shipto_city: "shipto_city",
            ///Nome contatto spedizione
            shipto_contactname: "shipto_contactname",
            ///Paese spedizione
            shipto_country: "shipto_country",
            ///Fax spedizione
            shipto_fax: "shipto_fax",
            ///Condizioni di spedizione
            shipto_freighttermscode: "shipto_freighttermscode",
            ///Via 1 spedizione
            shipto_line1: "shipto_line1",
            ///Via 2 spedizione
            shipto_line2: "shipto_line2",
            ///Via 3 spedizione
            shipto_line3: "shipto_line3",
            ///Nome spedizione
            shipto_name: "shipto_name",
            ///CAP spedizione
            shipto_postalcode: "shipto_postalcode",
            ///Provincia spedizione
            shipto_stateorprovince: "shipto_stateorprovince",
            ///Telefono spedizione
            shipto_telephone: "shipto_telephone",
            ///Ignora calcolo prezzo
            skippricecalculation: "skippricecalculation",
            ///Totale IVA
            tax: "tax",
            ///Imposte (Base)
            tax_base: "tax_base",
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

            /// Values for field Prezzi
            ispriceoverriddenValues: {
                Sostituisciprezzo: 1,
                Usapredefinito: 0
            },

            /// Values for field Seleziona prodotto
            isproductoverriddenValues: {
                Esistente: 0,
                Fuoricatalogo: 1
            },

            /// Values for field Errore di determinazione dei prezzi 
            pricingerrorcodeValues: {
                Attributodelprezzofuoriintervallo: 35,
                Codicedideterminazionedeiprezzimancante: 8,
                Codicedideterminazionedeiprezzinonvalido: 9,
                Costocorrentemancante: 15,
                Costocorrentenonvalido: 20,
                Costomediomancante: 16,
                Costomediononvalido: 21,
                Dettaglierrore: 1,
                Erroredicalcolodelprezzo: 25,
                Importodellistinoprezzimancante: 12,
                Importodellistinoprezzinonvalido: 17,
                Lavalutadellatransazionenoneimpostataperlavocedilistinodelprodotto: 38,
                Listinoprezziinattivo: 3,
                Listinoprezzimancante: 2,
                Nessuno: 0,
                Opzionediarrotondamentononvalida: 23,
                Overflowdellattributodellavalutadibase: 36,
                Percentualedellistinoprezzimancante: 13,
                Percentualedellistinoprezzinonvalida: 18,
                Precisionedideterminazionedeiprezzinonvalida: 30,
                Prezzomancante: 14,
                Prezzononvalido: 19,
                Prezzounitariomancante: 5,
                Prodottomancante: 6,
                Prodottononinclusonellistinoprezzi: 11,
                Prodottononvalido: 7,
                Quantitamancante: 4,
                Quantitanonvalida: 29,
                Regoladiarrotondamentononvalida: 22,
                Scontononvalido: 28,
                Statononvalidotipodisconto: 27,
                Tipodiscontoinattivo: 33,
                Tipodiscontononvalido: 26,
                Underflowdellattributodellavalutadibase: 37,
                Unitadimisuramancante: 10,
                Unitadimisurapredefinitadelprodottomancante: 31,
                Unitadivenditadelprodottomancante: 32,
                Valorediarrotondamentononvalido: 24,
                Valutadellistinoprezzinonvalida: 34
            },

            /// Values for field Tipo di prodotto
            producttypecodeValues: {
                Aggregazione: 2,
                Prodotto: 1,
                Prodottoaggregazionefacoltativo: 4,
                Prodottoaggregazioneobbligatorio: 3,
                Serviziobasatosulprogetto: 5
            },

            /// Values for field Configurazione proprietà
            propertyconfigurationstatusValues: {
                Modifica: 0,
                Nonconfigurato: 2,
                Rettifica: 1
            },

            /// Values for field Metodo di creazione
            quotecreationmethodValues: {
                Revisione: 776160001,
                Sconosciuto: 776160000
            },

            /// Values for field Omaggio
            res_ishomageValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Condizioni di spedizione
            shipto_freighttermscodeValues: {
                CFRCostoenolo: 3,
                CIFCostoassenolo: 4,
                CIPTraspeasspagatifinoa: 5,
                CPTTrasportopagatofinoa: 6,
                DAFResofrontiera: 7,
                DDPResosdoganato: 10,
                DDUResononsdoganato: 11,
                DEQResobanchina: 8,
                DESResoexship: 9,
                EXWFrancofabbrica: 12,
                FASFrancolungobordo: 13,
                FCAFrancovettore: 14,
                FOBFrancoabordo: 1,
                Gratis: 2
            },

            /// Values for field Ignora calcolo prezzo
            skippricecalculationValues: {
                DoPriceCalcAlways: 0,
                SkipPriceCalcOnCreate: 1,
                SkipPriceCalcOnUpdate: 2,
                SkipPriceCalcOnUpSert: 3
            },

            /// Values for field Spedizione
            willcallValues: {
                Indirizzo: 0,
                Ritiroacaricodelcliente: 1
            }
        },
        tabs: {

        },
        sections: {

        }
    };
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
    _self.getTotalDiscountPercentage = function (executionContext) {
        const formContext = executionContext.getFormContext();

        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercent1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercent2).getValue() ?? 0;
        let disc3 = formContext.getAttribute(_self.formModel.fields.res_discountpercent3).getValue() ?? 0;

        return (disc1 + disc2 + disc3);
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
    //-------------------------------------------------------
    _self.onChangeQuantity = function (executionContext) {

        var formContext = executionContext.getFormContext();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();

        _self.setBaseAmount(executionContext, { quantita: quantity, prezzoUnitario: null });
    };
    //---------------------------------------------------
    _self.onChangePricePerUnit = function (executionContext) {

        var formContext = executionContext.getFormContext();
        let pricePerUnit = formContext.getAttribute(_self.formModel.fields.priceperunit).getValue();

        _self.setBaseAmount(executionContext, { quantita: null, prezzoUnitario: pricePerUnit });
    };
    //---------------------------------------------------
    _self.onChangeProduct = async function (executionContext) {
        var formContext = executionContext.getFormContext();

        let productLookup = formContext.getAttribute(_self.formModel.fields.productid).getValue();
        let scontoTot = formContext.getAttribute(_self.formModel.fields.manualdiscountamount).getValue();
        let quantity = formContext.getAttribute(_self.formModel.fields.quantity).getValue();
        let importo = null;

        if (productLookup != null) {
            let productId = RSMNG.TAUMEDIKA.GLOBAL.convertGuid(productLookup[0].id);
            let product = await _self.getProductDetails(productId);
            //let amount = await _self.getProductPriceLevelAmount(productId);

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
        formContext.getAttribute(_self.formModel.fields.res_discountpercent1).setValue(disc1);
        formContext.getControl(_self.formModel.fields.res_discountpercent1).setDisabled(isHomage);

        //sconto%2 e sconto%3 not editable e valorizzati a 0
        if (isHomage || !disc1) {
            formContext.getControl(_self.formModel.fields.res_discountpercent2).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(true);

            formContext.getAttribute(_self.formModel.fields.res_discountpercent2).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_discountpercent3).setValue(null);
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
    //_self.getProductPriceLevelAmount = function (productId) {
    //    return new Promise(function (resolve, reject) {

    //        // Recupero importo presente su Voce Listino
    //        var fetchData = {
    //            "productid": productId
    //        };
    //        var fetchXml = [
    //            "?fetchXml=<fetch>",
    //            "  <entity name='product'>",
    //            "    <filter>",
    //            "      <condition attribute='productid' operator='eq' value='", fetchData.productid, "' />",
    //            "    </filter>",
    //            "    <link-entity name='pricelevel' from='pricelevelid' to='pricelevelid' link-type='inner' alias='Listino'>",
    //            "      <link-entity name='productpricelevel' from='pricelevelid' to='pricelevelid' alias='VoceListino'>",
    //            "        <attribute name='amount'/>",
    //            "        <filter>",
    //            "          <condition attribute='productid' operator='eq' value='", fetchData.productid, "' />",
    //            "        </filter>",
    //            "      </link-entity>",
    //            "    </link-entity>",
    //            "  </entity>",
    //            "</fetch>"
    //        ].join("");
    //        console.log(fetchXml);
    //        Xrm.WebApi.retrieveMultipleRecords("product", fetchXml).then(
    //            results => {

    //                let amount = results.entities.length > 0 && results.entities[0]["VoceListino.amount"] != undefined ? results.entities[0]["VoceListino.amount"] : null;
    //                resolve(amount);

    //            },
    //            error => {
    //                reject(error);
    //            }
    //        );
    //    });
    //};
    //---------------------------------------------------
    _self.onChangeDiscountPercent1 = function (executionContext) {
        const formContext = executionContext.getFormContext();

        _self.setManualDiscountAmount(executionContext, { importo: undefined, aliquota: undefined });
    };
    _self.onChangeDiscountPercent2 = function (executionContext) {
        const formContext = executionContext.getFormContext();

        _self.setManualDiscountAmount(executionContext, { importo: undefined, aliquota: undefined });
    };
    _self.onChangeDiscountPercent3 = function (executionContext) {
        const formContext = executionContext.getFormContext();

        _self.setManualDiscountAmount(executionContext, { importo: undefined, aliquota: undefined });
    };
    //---------------------------------------------------
    _self.setManualDiscountAmount = (executionContext, data) => {
        const formContext = executionContext.getFormContext();
        let eventSourceAttribute = executionContext.getEventSource();
        let eventSourceControl = formContext.getControl(eventSourceAttribute.getName());
        let manualDiscountAmount = null;

        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercent1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercent2).getValue() ?? 0;
        let disc3 = formContext.getAttribute(_self.formModel.fields.res_discountpercent3).getValue() ?? 0;


        //-----------------------
        if (disc1 == 0 || disc1 == 100) {
            formContext.getControl(_self.formModel.fields.res_discountpercent2).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(true);

            formContext.getAttribute(_self.formModel.fields.res_discountpercent2).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_discountpercent3).setValue(null);
            disc2 = 0;
            disc3 = 0;
        }
        else {
            if (disc2 == 0 || disc2 == 100) {
                formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(true);
                formContext.getAttribute(_self.formModel.fields.res_discountpercent3).setValue(null);
                disc3 = 0;
            }
            else {
                formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(false);
            }
            formContext.getControl(_self.formModel.fields.res_discountpercent2).setDisabled(false);
        }
        //----------------------

        let baseAmount = data.importo != undefined ? data.importo : formContext.getAttribute(_self.formModel.fields.baseamount).getValue();
        let vatRate = data.aliquota != undefined ? data.aliquota : formContext.getAttribute(_self.formModel.fields.res_vatrate).getValue();
        let totDiscount = _self.getManualDiscountAmount(baseAmount, [disc1, disc2, disc3]);


        formContext.getControl(_self.formModel.fields.res_discountpercent1).clearNotification();
        formContext.getControl(_self.formModel.fields.res_discountpercent2).clearNotification();
        formContext.getControl(_self.formModel.fields.res_discountpercent3).clearNotification();

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
        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercent1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercent2).getValue() ?? 0;
        let disc3 = formContext.getAttribute(_self.formModel.fields.res_discountpercent3).getValue() ?? 0;
        formContext.getAttribute(_self.formModel.fields.manualdiscountamount).setValue(_self.getManualDiscountAmount(baseAmount, [disc1, disc2, disc3]));

        let imponibileTot = _self.setTaxableAmount(executionContext, { importo: baseAmount, scontoTot: null });
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
    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {
        debugger;
        var formContext = executionContext.getFormContext();

        let disc1 = formContext.getAttribute(_self.formModel.fields.res_discountpercent1).getValue() ?? 0;
        let disc2 = formContext.getAttribute(_self.formModel.fields.res_discountpercent2).getValue() ?? 0;

        if (disc1 == 0) {
            formContext.getControl(_self.formModel.fields.res_discountpercent2).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(true);

            formContext.getAttribute(_self.formModel.fields.res_discountpercent2).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_discountpercent3).setValue(null);
        }
        else {
            if (disc2 == 0) {
                formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(true);
                formContext.getAttribute(_self.formModel.fields.res_discountpercent3).setValue(null);
            }
            else {
                formContext.getControl(_self.formModel.fields.res_discountpercent3).setDisabled(false);
            }
            formContext.getControl(_self.formModel.fields.res_discountpercent2).setDisabled(false);
        }
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        _self.checkPotentialCustomerData(executionContext);
    };

    _self.onLoadForm = async function (executionContext) {
        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        const formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        formContext.getAttribute(_self.formModel.fields.ispriceoverridden).addOnChange(_self.onChangeIsPriceOverridden);
        formContext.getAttribute(_self.formModel.fields.quantity).addOnChange(_self.onChangeQuantity);
        formContext.getAttribute(_self.formModel.fields.priceperunit).addOnChange(_self.onChangePricePerUnit);
        formContext.getAttribute(_self.formModel.fields.productid).addOnChange(_self.onChangeProduct);
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).addOnChange(_self.onChangeVatNumber);
        formContext.getAttribute(_self.formModel.fields.uomid).addOnChange(_self.onChangeUomId);

        formContext.getAttribute(_self.formModel.fields.res_discountpercent1).addOnChange(_self.onChangeDiscountPercent1);
        formContext.getAttribute(_self.formModel.fields.res_discountpercent2).addOnChange(_self.onChangeDiscountPercent2);
        formContext.getAttribute(_self.formModel.fields.res_discountpercent3).addOnChange(_self.onChangeDiscountPercent3);
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
            case RSMNG.Global.CRM_FORM_TYPE_DISABLED:
                _self.onLoadReadyOnlyForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_QUICKCREATE:
                _self.onLoadCreateForm(executionContext);
                break;
        }
    };
}
).call(RSMNG.TAUMEDIKA.QUOTEDETAIL);