//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDER) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDER = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///costanti entità
            logicalName: "salesorder",
            displayName: "Ordine"
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
            ///Contatto
            contactid: "contactid",
            ///Cliente
            customerid: "customerid",
            ///Tipo di cliente
            customeridtype: "customeridtype",
            ///Data evasione
            datefulfilled: "datefulfilled",
            ///Descrizione
            description: "description",
            ///Importo sconto ordine
            discountamount: "discountamount",
            ///Importo sconto ordine (Base)
            discountamount_base: "discountamount_base",
            ///Sconto ordine (%)
            discountpercentage: "discountpercentage",
            ///Email Address
            emailaddress: "emailaddress",
            ///Immagine entità
            entityimage: "entityimage",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Importo spesa accessoria
            freightamount: "freightamount",
            ///Spese di spedizione (Base)
            freightamount_base: "freightamount_base",
            ///Condizioni di spedizione
            freighttermscode: "freighttermscode",
            ///Prezzi bloccati
            ispricelocked: "ispricelocked",
            ///Ultimo invio al back office
            lastbackofficesubmit: "lastbackofficesubmit",
            ///Ultimo periodo sospensione
            lastonholdtime: "lastonholdtime",
            ///Nome
            name: "name",
            ///Periodo di sospensione (minuti)
            onholdtime: "onholdtime",
            ///Opportunità
            opportunityid: "opportunityid",
            ///Metodo di creazione
            ordercreationmethod: "ordercreationmethod",
            ///Nr. Ordine
            ordernumber: "ordernumber",
            ///Condizioni di pagamento
            paymenttermscode: "paymenttermscode",
            ///Listino prezzi
            pricelevelid: "pricelevelid",
            ///Errore di determinazione dei prezzi 
            pricingerrorcode: "pricingerrorcode",
            ///Priorità
            prioritycode: "prioritycode",
            ///Process Id
            processid: "processid",
            ///Offerta
            quoteid: "quoteid",
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
            ///Origine
            res_origincode: "res_origincode",
            ///Condizione di pagamento
            res_paymenttermid: "res_paymenttermid",
            ///Riferimento spedizione
            res_shippingreference: "res_shippingreference",
            ///Codice IVA spesa accessoria
            res_vatnumberid: "res_vatnumberid",
            ///Ordine
            salesorderid: "salesorderid",
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
            ///Via spedizione
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
            ///Data invio
            submitdate: "submitdate",
            ///Stato invio
            submitstatus: "submitstatus",
            ///Descrizione stato invio
            submitstatusdescription: "submitstatusdescription",
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
            ///Spedizione
            willcall: "willcall",

            /// Values for field Stato
            statecodeValues: {
                Annullato: 2,
                Attivo: 0,
                Evaso: 3,
                Fatturato: 4,
                Inviato: 1
            },

            /// Values for field Motivo stato
            statuscodeValues: {
                Annullato_StateAnnullato: 4,
                Approvato_StateAttivo: 100005,
                Bozza_StateAttivo: 1,
                Completato_StateEvaso: 100001,
                Fatturato_StateFatturato: 100003,
                Inapprovazione_StateAttivo: 2,
                Incorso_StateInviato: 3,
                Inlavorazione_StateAttivo: 100006,
                Nonapprovato_StateAnnullato: 100004,
                Parziale_StateEvaso: 100002,
                Spedito_StateAttivo: 100007
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

            /// Values for field Prezzi bloccati
            ispricelockedValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Metodo di creazione
            ordercreationmethodValues: {
                Acquisisciofferta: 776160001,
                Sconosciuto: 776160000
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

            /// Values for field Priorità
            prioritycodeValues: {
                Valorepredefinito: 1
            },

            /// Values for field Richiesta fattura
            res_isinvoicerequestedValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Origine
            res_origincodeValues: {
                Dynamics: 100000000,
                ERP: 100000001
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


    _self.onSaveForm = function (executionContext) {
        if (executionContext.getEventArgs().getSaveMode() == 70) {
            executionContext.getEventArgs().preventDefault();
            return;
        }
    };
    //-----------Listino-prezzi-Custom-View--------------   
    _self.addPriceLevelCustomView = function (executionContext) {

        let formContext = executionContext.getFormContext();

        let viewDisplayName = "Listino prezzi agenti";
        const viewId = "00000000-0000-0000-0000-000000000001";

        var fetchData = {
            "statecode": "0",
            "res_scopetypecodes": "100000000",
            "res_scopetypecodes2": "100000000"
        };
        var fetchXml = [
            "<fetch>",
            "  <entity name='pricelevel'>",
            "    <attribute name='pricelevelid'/>",
            "    <attribute name='name'/>",
            "    <filter type='and'>",
            "      <condition attribute='statecode' operator='eq' value='", fetchData.statecode/*0*/, "'/>",
            "      <filter type='or'>",
            "        <condition attribute='res_scopetypecodes' operator='eq' value='", fetchData.res_scopetypecodes/*100000000*/, "'/>",
            "        <condition attribute='res_scopetypecodes' operator='contain-values'>",
            "          <value>", fetchData.res_scopetypecodes2/*100000000*/, "</value>",
            "        </condition>",
            "      </filter>",
            "    </filter>",
            "  </entity>",
            "</fetch>"
        ].join("");

        var layoutXml = "<grid name='resultset' object='1' jump='pricelevelid' select='1' icon='1' preview='1'>" +
            "  <row name='result' id='pricelevelid'>" +
            "    <cell name='name' width='300' />" +
            "  </row>" +
            "</grid>";

        formContext.getControl(_self.formModel.fields.pricelevelid).addCustomView(viewId, "pricelevel", viewDisplayName, fetchXml, layoutXml, true);
    };
    //---------------------------------------------------
    _self.setValueDate = function (executionContext) {

        var formContext = executionContext.getFormContext();

        formContext.getAttribute(_self.formModel.fields.res_date).setValue(new Date());
    };

    //-----------Condizione-di-pagamento-----------------
    _self.onChangePaymentTermId = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let paymentLookup = formContext.getAttribute(_self.formModel.fields.res_paymenttermid).getValue();

        if (paymentLookup !== null) {

            Xrm.WebApi.retrieveRecord("res_paymentterm", paymentLookup[0].id, "?$select=res_isbankvisible").then(
                function success(result) {
                    if (result.res_isbankvisible === true) {

                        formContext.getControl(_self.formModel.fields.res_bankdetailsid).setVisible(true);
                    } else {
                        formContext.getAttribute(_self.formModel.fields.res_bankdetailsid).setValue(null);
                        formContext.getControl(_self.formModel.fields.res_bankdetailsid).setVisible(false);
                    }


                },
                function (error) {
                    console.log(error.message);
                    // handle error conditions
                }
            );

        }
        else {
            formContext.getAttribute(_self.formModel.fields.res_bankdetailsid).setValue(null);
            formContext.getControl(_self.formModel.fields.res_bankdetailsid).setVisible(false);
        }


    };

    //-----------Spesa-accessoria------------------------
    _self.onChangeAdditionalExpenseId = function (executionContext) {

        var formContext = executionContext.getFormContext();

        let additionlExpenseId = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(null);

        if (additionlExpenseId !== null) {

            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel("required");
            formContext.getControl(_self.formModel.fields.freightamount).setDisabled(false);

            Xrm.WebApi.retrieveRecord("res_additionalexpense", additionlExpenseId[0].id, "?$select=res_amount").then(
                function success(result) {

                    formContext.getAttribute(_self.formModel.fields.freightamount).setValue(result.res_amount);
                },
                function (error) {
                    console.log(error.message);
                }
            );

        } else {

            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel("none");
            formContext.getAttribute(_self.formModel.fields.freightamount).setValue(null);
            formContext.getControl(_self.formModel.fields.freightamount).setDisabled(true);
        }



    };

    //-----------Spedizione------------------------------
    _self.onChangeWillCall = function (executionContext, isEvent) {

        var formContext = executionContext.getFormContext();
        let willCall = formContext.getAttribute(_self.formModel.fields.willcall).getValue();

        if (willCall == _self.formModel.fields.willcallValues.Indirizzo) {

            formContext.getControl(_self.formModel.fields.shipto_postalcode).getAttribute().setRequiredLevel("required");
            formContext.getControl(_self.formModel.fields.shipto_line1).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_shippingreference).setVisible(true);
            formContext.getControl(_self.formModel.fields.shipto_postalcode).setVisible(true);
            formContext.getControl(_self.formModel.fields.shipto_city).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_location).setVisible(true);
            formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_countryid).setVisible(true);
            formContext.getControl("WebResource_postalcode").setVisible(true);
        } else {
            if (isEvent) {
                formContext.getAttribute(_self.formModel.fields.shipto_line1).setValue(null);
                formContext.getAttribute(_self.formModel.fields.res_shippingreference).setValue(null);
                formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setValue(null);
                formContext.getAttribute(_self.formModel.fields.shipto_city).setValue(null);
                formContext.getAttribute(_self.formModel.fields.res_location).setValue(null);
                formContext.getAttribute(_self.formModel.fields.shipto_stateorprovince).setValue(null);
                formContext.getAttribute(_self.formModel.fields.res_countryid).setValue(null);

                formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setRequiredLevel("none");
                formContext.getAttribute(_self.formModel.fields.shipto_city).setRequiredLevel("none");
            }

            formContext.getControl(_self.formModel.fields.shipto_postalcode).getAttribute().setRequiredLevel("none");
            formContext.getControl(_self.formModel.fields.shipto_line1).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_shippingreference).setVisible(false);
            formContext.getControl(_self.formModel.fields.shipto_postalcode).setVisible(false);
            formContext.getControl(_self.formModel.fields.shipto_city).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_location).setVisible(false);
            formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_countryid).setVisible(false);
            formContext.getControl("WebResource_postalcode").setVisible(false);
          
        }
    };

    //-----------Indirizzo-Spedizione--------------------
    _self.onChangeShipToLine1 = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToLine1 = formContext.getAttribute(_self.formModel.fields.shipto_line1).getValue();

        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setRequiredLevel(shipToLine1 !== null ? "required" : "none");

    };

    //-----------CAP-Spedizione--------------------------
    _self.onChangeShipToPostalCode = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToPostalCode = formContext.getAttribute(_self.formModel.fields.shipto_postalcode).getValue();

        formContext.getAttribute(_self.formModel.fields.shipto_city).setRequiredLevel(shipToPostalCode !== null ? "required" : "none");
        formContext.getControl(_self.formModel.fields.shipto_city).setDisabled(shipToPostalCode !== null ? false : true);

    };

    //----------Citta-Spedizione-------------------------
    _self.onChangeShipToCity = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToCity = formContext.getAttribute(_self.formModel.fields.shipto_city).getValue();

        formContext.getControl(_self.formModel.fields.res_location).setDisabled(shipToCity !== null ? false : true);
        formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setDisabled(shipToCity !== null ? false : true);
        formContext.getControl(_self.formModel.fields.res_countryid).setDisabled(shipToCity !== null ? false : true);

    };
    //---------------------------------------------------
    _self.setContextCapIframe = function (executionContext) {
        let formContext = executionContext.getFormContext();
        var wrControl = formContext.getControl("WebResource_postalcode");
        if (wrControl) {
            wrControl.getContentWindow().then(
                function (contentWindow) {
                    contentWindow.setContext(Xrm, formContext, _self, executionContext);
                }
            )
        }
    }
    //---------------------------------------------------
    _self.onLoadCreateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        _self.setValueDate(executionContext);

        // Valorizza lookup in auto
        var fetchData = {
            "res_isdefaultforagents": "1",
            "statecode": "0"
        };
        var fetchXml = [
            "?fetchXml=<fetch top='1'>",
            "  <entity name='pricelevel'>",
            "    <attribute name='pricelevelid'/>",
            "    <attribute name='name' />",
            "    <filter>",
            "      <condition attribute='res_isdefaultforagents' operator='eq' value='", fetchData.res_isdefaultforagents/*1*/, "'/>",
            "      <condition attribute='statecode' operator='eq' value='", fetchData.statecode/*0*/, "'/>",
            "    </filter>",
            "  </entity>",
            "</fetch>"
        ].join("");

        Xrm.WebApi.retrieveMultipleRecords("pricelevel", fetchXml).then(
            function success(results) {
                console.log(results);
                if (results.entities.length > 0) {

                    var lookupValue = [{
                        id: results.entities[0].pricelevelid,
                        name: results.entities[0].name,
                        entityType: "pricelevel"
                    }];

                    formContext.getAttribute(_self.formModel.fields.pricelevelid).setValue(lookupValue);
                }

            },
            function (error) {
                console.log(error);
            }
        );
    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();
        //-----
        _self.onChangeWillCall(executionContext, false);
        _self.onChangeShipToLine1(executionContext);
        _self.onChangeShipToPostalCode(executionContext);
        _self.onChangeShipToCity(executionContext);
        //-----
        let additionlExpenseId = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();

        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel(additionlExpenseId !== null ? "required" : "none");
        formContext.getControl(_self.formModel.fields.freightamount).setDisabled(additionlExpenseId !== null ? false : true);
        //-----

        let bankdetailsid = formContext.getAttribute(_self.formModel.fields.res_bankdetailsid).getValue();
        let paymenttermid = formContext.getAttribute(_self.formModel.fields.res_paymenttermid).getValue();

        if (bankdetailsid !== null) {
            formContext.getControl(_self.formModel.fields.res_bankdetailsid).setVisible(true);
        }
        else if (bankdetailsid === null && paymenttermid === null) {
            formContext.getControl(_self.formModel.fields.res_bankdetailsid).setVisible(false);
        }
        else if (bankdetailsid === null && paymenttermid !== null) {
            // bankdetails non è obbligatorio, quindi faccio controllo in caso in cui l'utente non inserisce nessun valore

            Xrm.WebApi.retrieveRecord("res_paymentterm", paymenttermid[0].id, "?$select=res_isbankvisible").then(
                function success(result) {
                    formContext.getControl(_self.formModel.fields.res_bankdetailsid).setVisible(result.res_isbankvisible);
                },
                function (error) {
                    console.log(error.message);
                }
            );
        }

    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------

    _self.onLoadForm = async function (executionContext) {

        //init lib
        /*await import('../res_scripts/res_global.js');*/

        //init formContext
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        formContext.getAttribute(_self.formModel.fields.res_paymenttermid).addOnChange(_self.onChangePaymentTermId);
        formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).addOnChange(_self.onChangeAdditionalExpenseId);
        formContext.getAttribute(_self.formModel.fields.shipto_line1).addOnChange(_self.onChangeShipToLine1);
        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).addOnChange(_self.onChangeShipToPostalCode);
        formContext.getAttribute(_self.formModel.fields.shipto_city).addOnChange(_self.onChangeShipToCity);
        formContext.getAttribute(_self.formModel.fields.willcall).addOnChange(() => { _self.onChangeWillCall(executionContext, true); });
        //Init function
        _self.addPriceLevelCustomView(executionContext);
        _self.setContextCapIframe(executionContext);

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
    };
}
).call(RSMNG.TAUMEDIKA.SALESORDER);