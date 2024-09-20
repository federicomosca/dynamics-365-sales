//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTE) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTE = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            logicalName: "quote",
            displayName: "Offerta"
        },
        fields: {
            ///Account
            accountid: "accountid",
            ///ID indirizzo di fatturazione
            billto_addressid: "billto_addressid",
            ///Città fatturazione
            billto_city: "billto_city",
            ///Indirizzo di fatturazione
            billto_composite: "billto_composite",
            ///Nome contatto fatturazione
            billto_contactname: "billto_contactname",
            ///Paese fatturazione
            billto_country: "billto_country",
            ///Fax fatturazione
            billto_fax: "billto_fax",
            ///Via 1 fatturazione
            billto_line1: "billto_line1",
            ///Via 2 fatturazione
            billto_line2: "billto_line2",
            ///Via 3 fatturazione
            billto_line3: "billto_line3",
            ///Nome fatturazione
            billto_name: "billto_name",
            ///CAP fatturazione
            billto_postalcode: "billto_postalcode",
            ///Provincia di fatturazione
            billto_stateorprovince: "billto_stateorprovince",
            ///Telefono fatturazione
            billto_telephone: "billto_telephone",
            ///Campagna di origine
            campaignid: "campaignid",
            ///Data chiusura
            closedon: "closedon",
            ///Contatto
            contactid: "contactid",
            ///Potenziale cliente
            customerid: "customerid",
            ///Tipo di potenziale cliente
            customeridtype: "customeridtype",
            ///Descrizione
            description: "description",
            ///Importo sconto offerta
            discountamount: "discountamount",
            ///Importo sconto offerta (Base)
            discountamount_base: "discountamount_base",
            ///Sconto offerta (%)
            discountpercentage: "discountpercentage",
            ///Data inizio validità
            effectivefrom: "effectivefrom",
            ///Data fine validità
            effectiveto: "effectiveto",
            ///Email Address
            emailaddress: "emailaddress",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Scadenza
            expireson: "expireson",
            ///Importo spesa accessoria
            freightamount: "freightamount",
            ///Spese di spedizione (Base)
            freightamount_base: "freightamount_base",
            ///Condizioni di spedizione
            freighttermscode: "freighttermscode",
            ///Ultimo periodo sospensione
            lastonholdtime: "lastonholdtime",
            ///Nome
            name: "name",
            ///Periodo di sospensione (minuti)
            onholdtime: "onholdtime",
            ///Opportunità
            opportunityid: "opportunityid",
            ///Condizioni di pagamento
            paymenttermscode: "paymenttermscode",
            ///Listino prezzi
            pricelevelid: "pricelevelid",
            ///Errore di determinazione dei prezzi 
            pricingerrorcode: "pricingerrorcode",
            ///Process Id
            processid: "processid",
            ///Metodo di creazione
            quotecreationmethod: "quotecreationmethod",
            ///Offerta
            quoteid: "quoteid",
            ///Nr. Offerta
            quotenumber: "quotenumber",
            ///Data consegna richiesta
            requestdeliveryby: "requestdeliveryby",
            ///Spesa accessoria
            res_additionalexpenseid: "res_additionalexpenseid",
            ///Banca
            res_bankdetailsid: "res_bankdetailsid",
            ///Nazione spedizione
            res_countryid: "res_countryid",
            ///Data
            res_date: "res_date",
            ///Acconto
            res_deposit: "res_deposit",
            ///Acconto (base)
            res_deposit_base: "res_deposit_base",
            ///Commento uso interno
            res_internalusecomment: "res_internalusecomment",
            ///Richiesta fattura
            res_isinvoicerequested: "res_isinvoicerequested",
            ///Località
            res_location: "res_location",
            ///Riferimento spedizione
            res_shippingreference: "res_shippingreference",
            ///Codice IVA spesa accessoria
            res_vatnumberid: "res_vatnumberid",
            ///ID aggiornamento
            revisionnumber: "revisionnumber",
            ///Metodo di spedizione
            shippingmethodcode: "shippingmethodcode",
            ///ID indirizzo di spedizione
            shipto_addressid: "shipto_addressid",
            ///Città spedizione
            shipto_city: "shipto_city",
            ///Indirizzo di spedizione
            shipto_composite: "shipto_composite",
            ///Nome contatto spedizione
            shipto_contactname: "shipto_contactname",
            ///Paese spedizione
            shipto_country: "shipto_country",
            ///Fax spedizione
            shipto_fax: "shipto_fax",
            ///Condizioni di spedizione per indirizzo spedizione
            shipto_freighttermscode: "shipto_freighttermscode",
            ///Indirizzo
            shipto_line1: "shipto_line1",
            ///Via 2 spedizione
            shipto_line2: "shipto_line2",
            ///Via 3 spedizione
            shipto_line3: "shipto_line3",
            ///Nome spedizione
            shipto_name: "shipto_name",
            ///CAP spedizione
            shipto_postalcode: "shipto_postalcode",
            ///Provincia
            shipto_stateorprovince: "shipto_stateorprovince",
            ///Telefono spedizione
            shipto_telephone: "shipto_telephone",
            ///Ignora calcolo prezzo
            skippricecalculation: "skippricecalculation",
            ///CONTRATTO DI SERVIZIO
            slaid: "slaid",
            ///Ultimo contratto di servizio applicato
            slainvokedid: "slainvokedid",
            ///(Deprecated) Stage Id
            stageid: "stageid",
            ///Importo totale
            totalamount: "totalamount",
            ///Importo totale (Base)
            totalamount_base: "totalamount_base",
            ///Totale imponibile
            totalamountlessfreight: "totalamountlessfreight",
            ///Totale senza spedizione (Base)
            totalamountlessfreight_base: "totalamountlessfreight_base",
            ///Sconto totale
            totaldiscountamount: "totaldiscountamount",
            ///Importo totale sconto (Base)
            totaldiscountamount_base: "totaldiscountamount_base",
            ///Totale righe
            totallineitemamount: "totallineitemamount",
            ///Totale dettagli (Base)
            totallineitemamount_base: "totallineitemamount_base",
            ///Importo totale sconto per voce
            totallineitemdiscountamount: "totallineitemdiscountamount",
            ///Importo totale sconto per voce (Base)
            totallineitemdiscountamount_base: "totallineitemdiscountamount_base",
            ///Totale IVA
            totaltax: "totaltax",
            ///Totale imposte (Base)
            totaltax_base: "totaltax_base",
            ///Valuta
            transactioncurrencyid: "transactioncurrencyid",
            ///(Deprecated) Traversed Path
            traversedpath: "traversedpath",
            ///ID univoco descrizione
            uniquedscid: "uniquedscid",
            ///Spedizione
            willcall: "willcall",

            /// Values for field Stato
            statecodeValues: {
                Acquisita: 2,
                Attiva: 1,
                Bozza: 0,
                Chiusa: 3
            },

            /// Values for field Motivo stato
            statuscodeValues: {
                Acquisita_StateAcquisita: 4,
                Aggiornata_StateChiusa: 7,
                Approvata_StateAttiva: 3,
                Bozza_StateBozza: 1,
                Inapprovazione_StateAttiva: 2,
                Nonapprovata_StateChiusa: 5,
                Persa_StateChiusa: 6
            },

            /// Values for field Condizioni di spedizione
            freighttermscodeValues: {
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

            /// Values for field Condizioni di pagamento
            paymenttermscodeValues: {
                _30gg: 1,
                _60gg: 4,
                _90gg: 9,
                Pagamentoincontanti: 10
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

            /// Values for field Metodo di creazione
            quotecreationmethodValues: {
                Revisione: 776160001,
                Sconosciuto: 776160000
            },

            /// Values for field Richiesta fattura
            res_isinvoicerequestedValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Metodo di spedizione
            shippingmethodcodeValues: {
                Corriereespresso: 28,
                SpedizionePostale: 5,
                Trasportoaereo: 1,
                Trasportoferroviario: 10,
                Trasportointermodale: 18,
                Trasportomarittimo: 11,
                Trasportostradale: 9
            },

            /// Values for field Condizioni di spedizione per indirizzo spedizione
            shipto_freighttermscodeValues: {
                Valorepredefinito: 1
            },

            /// Values for field Ignora calcolo prezzo
            skippricecalculationValues: {
                DoPriceCalcAlways: 0,
                SkipPriceCalcOnRetrieve: 1
            },

            /// Values for field Spedizione
            willcallValues: {
                Indirizzo: 0,
                Spedizioneacaricodelcliente: 1
            }
        },
        tabs: {

        },
        sections: {

        }
    };

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
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.fillDateField = formContext => {

        const dateAttribute = formContext.getAttribute(_self.formModel.fields.res_date);

        if (dateAttribute) {
            const date = new Date();
            if (dateAttribute.getValue() == null) {
                dateAttribute.setValue(date);
            }
        }
    }
    //---------------------------------------------------
    _self.fillPriceLevelField = executionContext => {
        const formContext = executionContext.getFormContext();

        const priceLevelAttribute = formContext.getAttribute(_self.formModel.fields.pricelevelid);

        if (priceLevelAttribute) {

            var fetchData = {
                "res_isdefaultforagents": "1",
                "statecode": "0"
            };
            var fetchXml = [
                "?fetchXml=<fetch top='1'>",
                "  <entity name='pricelevel'>",
                "    <attribute name='pricelevelid'/>",
                "    <attribute name='name'/>",
                "    <filter>",
                "      <condition attribute='res_isdefaultforagents' operator='eq' value='", fetchData.res_isdefaultforagents/*1*/, "'/>",
                "      <condition attribute='statecode' operator='eq' value='", fetchData.statecode/*0*/, "'/>",
                "    </filter>",
                "  </entity>",
                "</fetch>"
            ].join("");

            Xrm.WebApi.retrieveMultipleRecords("pricelevel", fetchXml).then(
                priceLevel => {
                    if (priceLevel.entities.length < 0) return;
                    const priceLevelId = priceLevel.entities[0].pricelevelid ?? null;
                    const priceLevelName = priceLevel.entities[0].name ?? null;

                    if (!priceLevelId || !priceLevelName) {
                        console.log("id or name missing")
                        return;
                    }

                    const priceLevelLookUp = [{
                        id: priceLevelId,
                        name: priceLevelName,
                        entityType: "pricelevel"
                    }];

                    priceLevelAttribute.setValue(priceLevelLookUp);
                },
                error => {
                    console.log(error.message);
                }
            );
        }
    }
    //---------------------------------------------------
    _self.handleFieldsVisibility = executionContext => {
        const formContext = executionContext.getFormContext();
        const bankControl = formContext.getControl(_self.formModel.fields.res_bankdetailsid);
        const additionalExpenseAttribute = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid);
        const vatNumberAttribute = formContext.getAttribute(_self.formModel.fields.res_vatnumberid);
        const freightAmountControl = formContext.getControl(_self.formModel.fields.freightamount);
        /**
         * controllo visibilità campo Banca
         */
        if (bankControl) {
            const paymentTermAttribute = formContext.getAttribute("res_paymenttermid");
            if (paymentTermAttribute) {
                const paymentTermId = paymentTermAttribute.getValue() ? paymentTermAttribute.getValue()[0].id : null;
                if (paymentTermId) {
                    const paymentTermIdCleaned = paymentTermId.replace(/[{}]/g, "");
                    Xrm.WebApi.retrieveRecord("res_paymentterm", paymentTermIdCleaned, "?$select=res_isbankvisible").then(
                        paymentTerm => {
                            const flag = paymentTerm.res_isbankvisible ?? null;
                            bankControl.setVisible(flag);
                        },
                        error => {
                            console.log(error.message)
                        }
                    );
                }
            }
        }

        if (additionalExpenseAttribute) {
            const additionalExpenseValue = additionalExpenseAttribute.getValue() ?? null;
            if (additionalExpenseValue) {
                /**
                 * controllo visibilità campo "Codice IVA spesa accessoria"
                 */
                if (vatNumberAttribute) {
                    vatNumberAttribute.setRequiredLevel("required");
                } else {
                    vatNumberAttribute.setRequiredLevel("none");
                }

                /**
                 * controllo visibilità campo "Importo spesa accessoria"
                 */
                if (freightAmountControl) {
                    freightAmountControl.setDisabled(false);
                } else {
                    freightAmountControl.setDisabled(true);
                }
            }
        }
    }
    //---------------------------------------------------
    _self.onChangeAdditionalExpenseId = executionContext => {
        const formContext = executionContext.getFormContext();

        const vatNumberAttribute = formContext.getAttribute(_self.formModel.fields.res_vatnumberid);

        const additionalExpenseAttribute = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid);
        const additionalExpenseValue = additionalExpenseAttribute ? additionalExpenseAttribute.getValue() ?? null : null;

        if (vatNumberAttribute) {
            vatNumberAttribute.setValue(null);
            if (additionalExpenseValue) {
                vatNumberAttribute.setRequiredLevel("required");
            } else {
                vatNumberAttribute.setRequiredLevel("none");
            }
            // potrei gestirla con una notification invece che col required level
            //oppure potrei mettere un flag (spesa accessoria: si/no), sull'onChange del flag cambio il setRequiredLevel
        }
    }
    //---------------------------------------------------
    /* 
    Utilizzare la keyword async se si utilizza uno o più metodi await dentro la funzione l'onLoadForm
    per rendere l'onload asincrono asincrono (da attivare sull'app dynamics!)
    Ricordare di aggiungere la keyword anche ai metodi richiamati dall'onLoadForm se l'await avviene dentro di essi
    */
    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        const formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);
        formContext.getAttribute("res_paymenttermid").addOnChange(_self.handleFieldsVisibility);
        formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).addOnChange(_self.onChangeAdditionalExpenseId);

        //Init function
        _self.fillDateField(formContext);
        _self.fillPriceLevelField(executionContext);
        _self.handleFieldsVisibility(executionContext);

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
).call(RSMNG.TAUMEDIKA.QUOTE);