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
    //---------------------------------------------------
    _self.setPrice = executionContext => {
        const formContext = executionContext.getFormContext();

        const controls = {
            priceperunit: formContext.getControl(_self.formModel.fields.priceperunit),
            quantity: formContext.getControl(_self.formModel.fields.quantity),
            baseamount: formContext.getControl(_self.formModel.fields.baseamount),
            discountpercent1: formContext.getControl(_self.formModel.fields.res_discountpercent1),
            discountpercent2: formContext.getControl(_self.formModel.fields.res_discountpercent2),
            discountpercent3: formContext.getControl(_self.formModel.fields.res_discountpercent3),
            manualdiscountamount: formContext.getControl(_self.formModel.fields.manualdiscountamount),
            extendedamount: formContext.getControl(_self.formModel.fields.extendedamount),
            taxableamount: formContext.getControl(_self.formModel.fields.res_taxableamount),
            itemcode: formContext.getControl(_self.formModel.fields.res_itemcode),
            vatnumberid: formContext.getControl(_self.formModel.fields.res_vatnumberid),
            vatrate: formContext.getControl(_self.formModel.fields.res_vatrate),
            productid: formContext.getControl(_self.formModel.fields.productid)
        };

        const priceperunit = controls.priceperunit.getAttribute().getValue() ?? 0;                              //prezzo unitario
        const quantity = controls.quantity.getAttribute().getValue() ?? 0;                                      //quantità
        const baseamount = _self.getBaseAmountValue(priceperunit, quantity);                                    //importo
        controls.baseamount.getAttribute().setValue(baseamount);

        const dp1 = controls.discountpercent1.getAttribute().getValue() ?? 0;                                   //sconto%1
        const dp2 = controls.discountpercent2.getAttribute().getValue() ?? 0;                                   //sconto%2
        const dp3 = controls.discountpercent3.getAttribute().getValue() ?? 0;                                   //sconto%3
        const manualdiscountamount = _self.getManualDiscountAmount(baseamount, dp1, dp2, dp3, executionContext);     //sconto totale
        controls.manualdiscountamount.getAttribute().setValue(manualdiscountamount);

        _self.setTaxableAmount(executionContext);
        _self.setExtendedAmount(executionContext);
    }
    //---------------------------------------------------
    _self.getProductDetails = function (productId) {
        return new Promise(function (resolve, reject) {

            var fetchData = {
                "productid": productId
            };
            var fetchXml = [
                "?fetchXml=<fetch>",
                "  <entity name='product'>",
                "    <attribute name='productnumber'/>",
                "    <filter>",
                "      <condition attribute='productid' operator='eq' value='", fetchData.productid, "' />",
                "    </filter>",
                "    <link-entity name='res_vatnumber' from='res_vatnumberid' to='res_vatnumberid' link-type='inner' alias='CodiceIva'>",
                "      <attribute name='res_rate'/>",
                "      <attribute name='res_name'/>",
                "      <attribute name='res_vatnumberid'/>",
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
    _self.onChangeProductId = async executionContext => {
        const formContext = executionContext.getFormContext();

        const productIdControl = formContext.getControl(_self.formModel.fields.productid);
        const productLookup = productIdControl.getAttribute().getValue() ?? null;

        const fields = [
            _self.formModel.fields.priceperunit,
            _self.formModel.fields.quantity,
            _self.formModel.fields.baseamount,
            _self.formModel.fields.res_discountpercent1,
            _self.formModel.fields.res_discountpercent2,
            _self.formModel.fields.res_discountpercent3,
            _self.formModel.fields.manualdiscountamount,
            _self.formModel.fields.extendedamount,
            _self.formModel.fields.res_taxableamount
        ];

        if (!productLookup) {
            //all'onChange di productid, vengono azzerati tutti i campi connessi
            formContext.getControl(_self.formModel.fields.res_itemcode).getAttribute().setValue(null);
            formContext.getControl(_self.formModel.fields.res_vatnumberid).getAttribute().setValue(null);
            formContext.getControl(_self.formModel.fields.res_vatrate).getAttribute().setValue(null);

            fields.forEach(field => {
                const control = formContext.getControl(field);
                control.getAttribute().setValue(0);
            });
        } else {
            let product = await _self.getProductDetails(productLookup[0].id);

            let codiceIva = [{
                id: product["CodiceIva.res_vatnumberid"],
                entityType: 'res_vatnumber',
                name: product["CodiceIva.res_name"]
            }];

            formContext.getAttribute(_self.formModel.fields.res_itemcode).setValue(product.productnumber);
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(codiceIva);
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(product["CodiceIva.res_rate"]);
        }

    };
    //---------------------------------------------------
    _self.getBaseAmountValue = (priceperunit, quantity) => (priceperunit && quantity) ? priceperunit * quantity : 0;
    //---------------------------------------------------
    _self.getManualDiscountAmount = (baseamount, dp1, dp2, dp3, executionContext) => {
        const formContext = executionContext.getFormContext();
        const eventSourceAttribute = executionContext.getEventSource();
        const eventSourceControl = formContext.getControl(eventSourceAttribute.getName());
        let manualDiscountAmount;

        const notificationFields = [
            _self.formModel.fields.res_discountpercent1,
            _self.formModel.fields.res_discountpercent2,
            _self.formModel.fields.res_discountpercent3];
        const totalDiscountPercentage = dp1 + dp2 + dp3;

        if (totalDiscountPercentage <= 100) {

            notificationFields.forEach(field => {
                const control = formContext.getControl(field);
                control.clearNotification();
            });

            manualDiscountAmount = baseamount * (totalDiscountPercentage / 100);

        } else {

            eventSourceControl.setNotification("Lo sconto percentuale complessivo non pu\u00f2 essere maggiore di 100.");
            manualDiscountAmount = 0;
        }

        return manualDiscountAmount;
    };
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
    };
    //---------------------------------------------------
    _self.onChangeVatNumber = function (executionContext) {
        var formContext = executionContext.getFormContext();

        let vatNumberLookup = formContext.getAttribute(_self.formModel.fields.res_vatnumberid).getValue();

        if (vatNumberLookup != null) {
            let queryOptions = "?$select=res_rate";

            Xrm.WebApi.retrieveRecord("res_vatnumber", vatNumberLookup[0].id, queryOptions).then(
                function success(result) {

                    formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(result.res_rate);
                },
                function error(error) {

                    console.log(error);
                }
            );
        }
        else {
            formContext.getAttribute(_self.formModel.fields.res_vatrate).setValue(null);
        }
    };
    //---------------------------------------------------
    /*
    Utilizzare la keyword async se si utilizza uno o più metodi await dentro la funzione onSaveForm
    per rendere il salvataggio asincrono (da attivare sull'app dynamics!)
    */
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
    _self.onLoadReadyOnlyForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        _self.checkPotentialCustomerData(executionContext);
    };
    /* 
    Ricordare di aggiungere la keyword anche ai metodi richiamati dall'onLoadForm se l'await avviene dentro di essi\
    */
    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        const formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        const setPriceFields = [
            _self.formModel.fields.priceperunit,
            _self.formModel.fields.quantity,
            _self.formModel.fields.res_discountpercent1,
            _self.formModel.fields.res_discountpercent2,
            _self.formModel.fields.res_discountpercent3
        ];
        setPriceFields.forEach(field => {
            formContext.getAttribute(field).addOnChange(_self.setPrice);
        });

        formContext.getAttribute(_self.formModel.fields.productid).addOnChange(_self.onChangeProductId);
        formContext.getAttribute(_self.formModel.fields.res_taxableamount).addOnChange(_self.setExtendedAmount);
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).addOnChange(_self.onChangeVatNumber);

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