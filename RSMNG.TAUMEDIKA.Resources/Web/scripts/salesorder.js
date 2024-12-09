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
            ///Sconto ordine (%)
            discountpercentage: "discountpercentage",
            ///Email Address
            emailaddress: "emailaddress",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Importo spesa accessoria
            freightamount: "freightamount",
            ///Condizioni di spedizione
            freighttermscode: "freighttermscode",
            ///Prezzi bloccati
            ispricelocked: "ispricelocked",
            ///Nome
            name: "name",
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
            ///Offerta
            quoteid: "quoteid",
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
            ///Segnacollo
            res_segnacollo: "res_segnacollo",
            ///Metodo di spedizione
            shippingmethodcode: "shippingmethodcode",
            ///ID indirizzo di spedizione
            shipto_addressid: "shipto_addressid",
            ///Città spedizione
            shipto_city: "shipto_city",
            ///Nome contatto spedizione
            shipto_contactname: "shipto_contactname",
            ///Paese spedizione
            shipto_country: "shipto_country",
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
            ///Data invio
            submitdate: "submitdate",
            ///Stato invio
            submitstatus: "submitstatus",
            ///Descrizione stato invio
            submitstatusdescription: "submitstatusdescription",
            ///Importo totale
            totalamount: "totalamount",
            ///Totale imponibile
            totalamountlessfreight: "totalamountlessfreight",
            ///Sconto totale
            totaldiscountamount: "totaldiscountamount",
            ///Totale righe
            totallineitemamount: "totallineitemamount",
            ///Importo totale sconto per voce
            totallineitemdiscountamount: "totallineitemdiscountamount",
            ///Totale IVA
            totaltax: "totaltax",
            ///Valuta
            transactioncurrencyid: "transactioncurrencyid",
            ///Spedizione
            willcall: "willcall",
            // Motivo Stato
            statuscode: "statuscode",

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

    _self.readOnlyFields = [
        //_self.formModel.fields.ordernumber,
        _self.formModel.fields.res_origincode,
        _self.formModel.fields.datefulfilled,
        _self.formModel.fields.totallineitemamount,
        _self.formModel.fields.totaldiscountamount,
        _self.formModel.fields.totalamountlessfreight,
        _self.formModel.fields.totaltax,
        _self.formModel.fields.totalamount,
        _self.formModel.fields.quoteid,
        _self.formModel.fields.shipto_city,
        _self.formModel.fields.res_location,
        _self.formModel.fields.shipto_stateorprovince,
        _self.formModel.fields.res_countryid

    ];

    //---------------------< CAMPI SPESA ACCESSORIA >---------------------
    //
    //
    //
    //
    //------------------------< SPESA ACCESSORIA >------------------------ 
    _self.onChangeAdditionalExpenseId = async executionContext => {
        const formContext = executionContext.getFormContext();

        //svuoto il campo Codice IVA Spesa Accessoria
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setValue(null);

        const spesaAccessoria = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();

        if (spesaAccessoria !== null) {

            const result = await Xrm.WebApi.retrieveRecord("res_additionalexpense", spesaAccessoria[0].id, "?$select=res_amount");
            const importoSpesaAccessoria = result.res_amount;

            //rendo modificabili i campi Codice IVA Spesa Accessoria e Importo spesa accessoria
            formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(false);
            formContext.getControl(_self.formModel.fields.freightamount).setDisabled(false);

            //rendo obbligatorio il campo Codice IVA Spesa Accessoria
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel("required");

            //valorizzo il campo importo spesa accessoria e invoco il metodo per gestire i campi correlati al cambio dell'importo
            formContext.getAttribute(_self.formModel.fields.freightamount).setValue(importoSpesaAccessoria);
            _self.onChangeFreightAmount(executionContext);

        } else {
            //se ho svuotato Spesa accessoria...

            //rendo facoltativo il codice iva e svuoto il campo importo spesa accessoria
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel("none");
            formContext.getAttribute(_self.formModel.fields.freightamount).setValue(null);

            //e li rendo non editabili
            formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(true);
            formContext.getControl(_self.formModel.fields.freightamount).setDisabled(true);
        }
    };

    //------------------< RETRIEVE ALIQUOTA CODICE IVA >------------------   
    _self.retrieveAliquotaCodiceIVA = async codiceIvaId => {

        const fetchCodiceIVASpesaAccessoria = [
            "?fetchXml=<fetch>",
            "  <entity name='res_vatnumber'>",
            "    <attribute name='res_rate'/>",
            "    <filter>",
            "      <condition attribute='statecode' operator='eq' value='0'/>",
            "      <condition attribute='res_vatnumberid' operator='eq' value='", codiceIvaId, "'/>",
            "    </filter>",
            "  </entity>",
            "</fetch>"
        ].join("");

        try {
            const result = await Xrm.WebApi.retrieveMultipleRecords("res_vatnumber", fetchCodiceIVASpesaAccessoria);
            return result.entities[0].res_rate;
        } catch (error) {
            console.error("Errore nel recupero dell'aliquota codice IVA", error);
        }
    };

    //--------------------< RETRIEVE AGGREGATI RIGHE >--------------------   
    _self.retrieveAggregatiRighe = async parentId => {

        var fetchAggregati = [
            "?fetchXml=<fetch aggregate='true'>",
            "  <entity name='salesorderdetail'>",
            "    <attribute name='res_taxableamount' alias='TotaleImponibile' aggregate='sum'/>",
            "    <attribute name='tax' alias='TotaleIva' aggregate='sum'/>",
            "    <filter>",
            "      <condition attribute='salesorderid' operator='eq' value='", parentId, "'/>",
            "    </filter>",
            "  </entity>",
            "</fetch>"
        ].join("");

        try {
            const result = await Xrm.WebApi.retrieveMultipleRecords("salesorderdetail", fetchAggregati);
            const aggregati = result.entities[0];
            const totaleImponibile = aggregati.TotaleImponibile ?? 0;
            const totaleIva = aggregati.TotaleIva ?? 0;

            // ritorno un oggetto JSON con i valori aggregati
            return {
                righeTotaleImponibile: totaleImponibile,
                righeTotaleIva: totaleIva
            }
        } catch (error) {
            console.error("Errore nel recupero degli aggregati:", error);
        }
    };

    //------------------< CODICE IVA SPESA ACCESSORIA >-------------------
    _self.onChangeVatNumber = async executionContext => {
        const formContext = executionContext.getFormContext();

        const vatNumberControl = formContext.getControl(_self.formModel.fields.res_vatnumberid);                    //codice iva spesa accessoria
        const spesaAccessoriaControl = formContext.getControl(_self.formModel.fields.res_additionalexpenseid);    //spesa accessoria
        const totalTaxControl = formContext.getControl(_self.formModel.fields.totaltax);                            //totale iva
        const totalAmountControl = formContext.getControl(_self.formModel.fields.totalamount);                      //importo totale
        const importoSpesaAccessoriaControl = formContext.getAttribute(_self.formModel.fields.freightamount);

        /**
         * calcolo la spesa accessoria con applicata l'aliquota
         * recupero il totale iva di tutte le righe offerta associate
         * e imposto il risultato nel campo totale iva dell'offerta
         */
        const vatNumberLookup = vatNumberControl.getAttribute().getValue();
        const spesaAccessoriaLookup = spesaAccessoriaControl.getAttribute().getValue();

        const codiceIvaId = vatNumberLookup ? vatNumberLookup[0].id : null;
        const spesaAccessoriaId = spesaAccessoriaLookup ? spesaAccessoriaLookup[0].id.replace(/[{}]/g, "") : null;

        //retrieve della somma del Totale IVA di tutte le righe offerta
        const parentId = formContext.data.entity.getId();

        const { righeTotaleImponibile, righeTotaleIva } = await _self.retrieveAggregatiRighe(parentId);
        let importoSpesaAccessoria = importoSpesaAccessoriaControl.getValue();

        //se è stato selezionato il codice iva spesa accessoria
        if (codiceIvaId) {
            const codiceIvaSpesaAccessoria = await Xrm.WebApi.retrieveRecord("res_vatnumber", codiceIvaId, "?$select=res_rate");
            let aliquotaCodiceIVA = codiceIvaSpesaAccessoria ? codiceIvaSpesaAccessoria.res_rate : 0;

            if (!importoSpesaAccessoria) {
                const spesaAccessoria = await Xrm.WebApi.retrieveRecord("res_additionalexpense", spesaAccessoriaId, "?$select=res_amount")
                importoSpesaAccessoria = spesaAccessoria ? spesaAccessoria.res_amount : 0;
            }

            //calcolo l'iva sulla spesa 
            importoSpesaAccessoria = !importoSpesaAccessoria || importoSpesaAccessoria === 0 ? 1 : importoSpesaAccessoria;
            aliquotaCodiceIVA = !aliquotaCodiceIVA || aliquotaCodiceIVA === 0 ? 1 : aliquotaCodiceIVA;
            const ivaSpesaAccessoria = importoSpesaAccessoria * (aliquotaCodiceIVA / 100);

            const totaleIVA = righeTotaleIva + ivaSpesaAccessoria;
            const totaleImponibile = righeTotaleImponibile + importoSpesaAccessoria;
            const importoTotale = totaleImponibile + totaleIVA;

            totalTaxControl.getAttribute().setValue(totaleIVA);
            formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(totaleImponibile);
            totalAmountControl.getAttribute().setValue(importoTotale);

        } else {

            totalTaxControl.getAttribute().setValue(righeTotaleIva);

            const totaleImponibile = righeTotaleImponibile + importoSpesaAccessoria;
            const importototale = totaleImponibile + righeTotaleIva;

            totalAmountControl.getAttribute().setValue(importototale);
            formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(totaleImponibile);

        }
    };

    //--------------------< IMPORTO SPESA ACCESSORIA >--------------------
    _self.onChangeFreightAmount = async (executionContext) => {
        const formContext = executionContext.getFormContext();
        const importoSpesaAccessoria = formContext.getAttribute(_self.formModel.fields.freightamount).getValue();

        //--------------------------------< RECUPERO I VALORI PER CALCOLARE IL TOTALE IMPONIBILE >--------------------------------//
        const parentId = formContext.data.entity.getId();
        const { righeTotaleImponibile, righeTotaleIva } = await _self.retrieveAggregatiRighe(parentId);

        //--------------------------------< RECUPERO L'ALIQUOTA DEL CODICE IVA SPESA ACCESSORIA >--------------------------------//
        const codiceIVASpesaAccessoria = formContext.getAttribute(_self.formModel.fields.res_vatnumberid).getValue(); //Codice IVA Spesa Accessoria
        const totaleImponibile = righeTotaleImponibile /* aka totale prodotti */ + importoSpesaAccessoria;
        let totaleIva = righeTotaleIva;

        if (codiceIVASpesaAccessoria) {
            const codiceIvaId = codiceIVASpesaAccessoria[0].id;
            const aliquota = await _self.retrieveAliquotaCodiceIVA(codiceIvaId);

            totaleIva += (importoSpesaAccessoria * (aliquota / 100));
        }

        const importoTotale = totaleImponibile + totaleIva;

        formContext.getAttribute(_self.formModel.fields.totaltax).setValue(totaleIva != 0 ? totaleIva : null);
        formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(totaleImponibile != 0 ? totaleImponibile : null);
        formContext.getAttribute(_self.formModel.fields.totalamount).setValue(importoTotale != 0 ? importoTotale : null);
    };
    //
    //
    //
    //
    //
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
    //-----------Condizione_di_pagamento-----------------
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

    //-----------Spedizione------------------------------
    _self.onChangeWillCall = function (executionContext, isEvent) {

        var formContext = executionContext.getFormContext();
        let willCall = formContext.getAttribute(_self.formModel.fields.willcall).getValue();

        if (willCall == _self.formModel.fields.willcallValues.Indirizzo) {

            formContext.getControl(_self.formModel.fields.shipto_line1).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_shippingreference).setVisible(true);
            //formContext.getControl(_self.formModel.fields.shipto_postalcode).setVisible(true);
            formContext.getControl(_self.formModel.fields.shipto_city).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_location).setVisible(true);
            formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_countryid).setVisible(true);
            //formContext.getControl("WebResource_postalcode").setVisible(true);
            //_self.setContextCapIframe(executionContext); 

            if (isEvent) { _self.onChangeCustomer(executionContext); } // controlla se cliente ha un indirizzo default per settare in auto i campi spedizione
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

            formContext.getControl(_self.formModel.fields.shipto_line1).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_shippingreference).setVisible(false);
            formContext.getControl(_self.formModel.fields.shipto_postalcode).setVisible(false);
            formContext.getControl(_self.formModel.fields.shipto_city).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_location).setVisible(false);
            formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_countryid).setVisible(false);
            formContext.getControl("WebResource_postalcode")?.setVisible(false);

        }
    };
    //-----------Indirizzo-Spedizione--------------------
    _self.onChangeShipToLine1 = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToLine1 = formContext.getAttribute(_self.formModel.fields.shipto_line1).getValue();

        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setRequiredLevel(shipToLine1 !== null ? "required" : "none");
        formContext.getControl(_self.formModel.fields.shipto_postalcode).setVisible(shipToLine1 !== null ? true : false);
        formContext.getControl("WebResource_postalcode")?.setVisible(shipToLine1 !== null ? true : false);

        if (shipToLine1 !== null) {

            _self.setContextCapIframe(executionContext);
        } else {

            formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setValue(null);
        }

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
    _self.onChangeAddress = function (executionContext) {
        _self.onChangeShipToPostalCode(executionContext);
        _self.onChangeShipToCity(executionContext);
    };
    //---------------------------------------------------
    _self.setContextCapIframe = function (executionContext) {
        let formContext = executionContext.getFormContext();
        var wrControl = formContext.getControl("WebResource_postalcode");

        var fields = {
            cap: _self.formModel.fields.shipto_postalcode,
            city: _self.formModel.fields.shipto_city,
            province: _self.formModel.fields.shipto_stateorprovince,
            nation: _self.formModel.fields.shipto_country,
            country: _self.formModel.fields.res_countryid
        }


        if (wrControl) {
            wrControl.getContentWindow().then(
                function (contentWindow) {
                    contentWindow.setContext(Xrm, formContext, _self, executionContext, fields);
                }
            )
        }
    }
    //---------------------------------------------------
    _self.filterPotentialCustomer = executionContext => {
        const formContext = executionContext.getFormContext();
        const potentialCustomerControl = formContext.getControl(_self.formModel.fields.customerid);
        if (!potentialCustomerControl) { console.error(`Controllo ${potentialCustomerControl} non trovato`); return; }

        //  filtro gli account
        const accountFilter = "<filter><condition attribute='statecode' operator='eq' value='0' /></filter>";
        potentialCustomerControl.addCustomFilter(accountFilter, "account");

        //  filtro i contatti
        const contactFilter = "<filter><condition attribute='contactid' operator='eq' value='00000000-0000-0000-0000-000000000000' /></filter>";
        potentialCustomerControl.addCustomFilter(contactFilter, "contact");

    };
    //---------------------------------------------------
    _self.onChangeCustomer = async function (executionContext) {
        var formContext = executionContext.getFormContext();
        console.log("on change customer");
        let customerLookup = formContext.getAttribute(_self.formModel.fields.customerid).getValue();
        let tipoSpedizione = formContext.getAttribute(_self.formModel.fields.willcall).getValue();

        if (customerLookup !== null) { // willcall è un bool. richiede == per fare la conversione implicita

            let addresses = await RSMNG.TAUMEDIKA.GLOBAL.getCustomerAddresses(customerLookup[0].id, true);

            if (addresses != null && addresses.entities.length > 0) {



                let address = addresses.entities[0];

                formContext.getAttribute(_self.formModel.fields.shipto_line1).setValue(address.res_address);
                formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setValue(address.res_postalcode);
                formContext.getAttribute(_self.formModel.fields.shipto_city).setValue(address.res_city);
                formContext.getAttribute(_self.formModel.fields.res_location).setValue(address.res_location);
                formContext.getAttribute(_self.formModel.fields.shipto_stateorprovince).setValue(address.res_province);


                formContext.getAttribute(_self.formModel.fields.willcall).setValue(Boolean(_self.formModel.fields.willcallValues.Indirizzo));

                if (address._res_countryid_value != null) {


                    let countryLookup = [{
                        id: address["_res_countryid_value"],
                        entityType: 'res_country',
                        name: address["_res_countryid_value@OData.Community.Display.V1.FormattedValue"]
                    }];

                    formContext.getAttribute(_self.formModel.fields.shipto_country).setValue(address["_res_countryid_value@OData.Community.Display.V1.FormattedValue"]);
                    formContext.getAttribute(_self.formModel.fields.res_countryid).setValue(countryLookup);


                }

                _self.updateAddressFieldsRequirements(executionContext);
            }
        } else {
            formContext.getAttribute(_self.formModel.fields.shipto_line1).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_city).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_location).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_stateorprovince).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_country).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_countryid).setValue(null);

            _self.updateAddressFieldsRequirements(executionContext);
        }
    };
    //---------------------------------------------------
    _self.updateAddressFieldsRequirements = function (executionContext) {
        var formContext = executionContext.getFormContext();


        _self.onChangeShipToLine1(executionContext);

        let cap = formContext.getAttribute(_self.formModel.fields.shipto_postalcode).getValue();
        formContext.getAttribute(_self.formModel.fields.shipto_city).setRequiredLevel(cap !== null ? "required" : "none");
        formContext.getControl(_self.formModel.fields.shipto_city).setDisabled(cap != null ? false : true);

        let citta = formContext.getAttribute(_self.formModel.fields.shipto_city).getValue();
        _self.disableCityRelated(executionContext, citta != null ? false : true);

    };
    //---------------------------------------------------
    _self.disableCityRelated = function (executionContext, isDisable) {
        var formContext = executionContext.getFormContext();

        formContext.getControl(_self.formModel.fields.res_location).setDisabled(isDisable);
        formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setDisabled(isDisable);
        formContext.getControl(_self.formModel.fields.res_countryid).setDisabled(isDisable);
    };
    //---------------------------------------------------
    _self.checkPotentialCustomerData = async executionContext => {
        const formContext = executionContext.getFormContext();

        const isInvoiceRequestedControl = formContext.getControl(_self.formModel.fields.res_isinvoicerequested);
        const isInvoiceRequested = isInvoiceRequestedControl ? isInvoiceRequestedControl.getAttribute().getValue() ?? null : null;

        if (isInvoiceRequested) {
            const potentialCustomerControl = formContext.getControl(_self.formModel.fields.customerid);
            const potentialCustomerId = potentialCustomerControl ? potentialCustomerControl.getAttribute().getValue() ? potentialCustomerControl.getAttribute().getValue()[0].id : null : null;

            if (potentialCustomerId) {
                try {
                    const missingData = await RSMNG.TAUMEDIKA.GLOBAL.retrievePotentialCustomerMissingData(formContext, potentialCustomerId);
                    //se mancano dei dati
                    if (missingData.length > 0) {
                        const missingDataString = missingData.join(", ");
                        const notification = "Per acquisire l'ordine è necessario compilare i seguenti campi del potenziale cliente: " + missingDataString;
                        formContext.ui.setFormNotification(notification, "WARNING", "missingDataNotification");
                    } else {
                        formContext.ui.clearFormNotification("missingDataNotification");
                    }
                } catch (error) {
                    console.error("Error checking potential customer data:", error);
                    formContext.ui.setFormNotification("Si è verificato un errore durante il controllo dei dati del cliente.", "ERROR", "errorNotification");
                }
            }
        } else {
            formContext.ui.clearFormNotification("missingDataNotification");
        }
    };
    //---------------------------------------------------
    _self.getQuoteDetailsCount = gridContext => {
        return new Promise((resolve, reject) => {
            if (!gridContext) {
                reject("Subgrid not found");
                return;
            }
            gridContext.addOnLoad(() => {
                const subgrid = gridContext.getGrid();
                if (subgrid) {
                    const count = subgrid.getTotalRecordCount();
                    resolve(count);  // Restituisce il conteggio dei record quando disponibile
                } else {
                    reject("subgrid not loaded.");
                }
            });
        });
    };
    //---------------------------------------------------
    _self.addSubgridEventListener = executionContext => {
        const formContext = executionContext.getFormContext();
        const gridContext = formContext.getControl("salesorderdetailsGrid");

        if (!gridContext) {
            setTimeout(() => { _self.addSubgridEventListener(executionContext); }, 500);
            return;
        }
        gridContext.addOnLoad(_self.subgridEventListener);
    };
    //---------------------------------------------------
    _self.subgridEventListener = executionContext => {
        const formContext = executionContext.getFormContext();
        formContext.ui.clearFormNotification("HAS_SALESORDER_DETAILS");
        var subgridControl = formContext.getControl("salesorderdetailsGrid");

        setTimeout(() => {
            if (subgridControl.getGrid().getTotalRecordCount() == 0) {
                const notification = {
                    message: "Per mandare in approvazione, approvare o acquisire l'ordine è necessario aggiungere almeno un prodotto.",
                    level: "INFO",
                    uniqueId: "HAS_SALESORDER_DETAILS"
                };
                formContext.ui.setFormNotification(notification.message, notification.level, notification.uniqueId);
                formContext.ui.refreshRibbon();
            } else { formContext.ui.refreshRibbon(); }
        }, 500);
    };
    //---------------------------------------------------
    _self.gestioneVisibilitàCampoSegnacollo = async function (executionContext) {
        const formContext = executionContext.getFormContext();
        const isAgente = await RSMNG.TAUMEDIKA.GLOBAL.getAgent();

        /**
         * il campo è visibile solo se motivo stato = "Spedito" oppure "In lavorazione"
         */
        const motivoStato = formContext.getAttribute(_self.formModel.fields.statuscode).getValue();
        const motivoStatoSpedito = _self.formModel.fields.statuscodeValues.Spedito_StateAttivo;
        const motivoStatoInLavorazione = _self.formModel.fields.statuscodeValues.Inlavorazione_StateAttivo;

        if (motivoStato == motivoStatoSpedito || motivoStato == motivoStatoInLavorazione) {
            const campoSegnacollo = formContext.getControl(_self.formModel.fields.res_segnacollo);

            campoSegnacollo.setVisible(true);

            /**
             * se Agente = No, è anche editabile
             */
            if (!isAgente) {
                campoSegnacollo.setDisabled(false);
            }
        }
    };
    //---------------------------------------------------
    _self.onLoadCreateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        /**
         * se Spedizione == Indirizzo, il campo Indirizzo è required
         */
        const shipToLine1Control = formContext.getControl(_self.formModel.fields.shipto_line1);
        const willCallControl = formContext.getControl(_self.formModel.fields.willcall);
        if (willCallControl) {
            if (willCallControl.getAttribute().getValue() == _self.formModel.fields.willcallValues.Indirizzo) {
                if (shipToLine1Control) {
                    shipToLine1Control.getAttribute().setRequiredLevel("required");
                }
            }
        }

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

        _self.addSubgridEventListener(executionContext);


        //----------------------------------------------
        _self.onChangeWillCall(executionContext, false);
        _self.onChangeShipToLine1(executionContext);
        _self.onChangeShipToPostalCode(executionContext);
        _self.onChangeShipToCity(executionContext);
        //-----
        let additionlExpenseId = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();

        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel(additionlExpenseId !== null ? "required" : "none");

        const spesaAccessoria = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();
        formContext.getControl(_self.formModel.fields.freightamount).setDisabled(spesaAccessoria ? false : true);
        formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(spesaAccessoria ? false : true);

        //--- Blocca/Sblocca i campi in base a Utente-Agente e Motivo Stato---

        let currStatus = formContext.getAttribute(_self.formModel.fields.statuscode).getValue();

        if (currStatus == _self.formModel.fields.statuscodeValues.Inapprovazione_StateAttivo ||
            currStatus == _self.formModel.fields.statuscodeValues.Approvato_StateAttivo ||
            currStatus == _self.formModel.fields.statuscodeValues.Annullato_StateAnnullato ||
            currStatus == _self.formModel.fields.statuscodeValues.Spedito_StateAttivo ||
            currStatus == _self.formModel.fields.statuscodeValues.Inlavorazione_StateAttivo

        ) {

            let isAgent = await RSMNG.TAUMEDIKA.GLOBAL.getAgent();
            RSMNG.TAUMEDIKA.GLOBAL.setAllFieldsReadOnly(formContext, isAgent, _self.readOnlyFields);

            if (isAgent) {
                // nascono l'iframe che può modificare i valori di certi campi
                formContext.getControl("WebResource_postalcode")?.setVisible(false);
            }
        }
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
        _self.checkPotentialCustomerData(executionContext);

        formContext.getControl("WebResource_postalcode")?.setVisible(false);

    };
    //---------------------------------------------------
    _self.onSaveForm = function (executionContext) {
        if (executionContext.getEventArgs().getSaveMode() == 70) {
            executionContext.getEventArgs().preventDefault();
            return;
        }
    };
    //---------------------------------------------------
    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        //--------------------------------< CONDIZIONI DI PAGAMENTO >--------------------------------//
        formContext.getAttribute(_self.formModel.fields.res_paymenttermid).addOnChange(_self.onChangePaymentTermId);

        //-----------------------------------< SPESE ACCESSORIE >-----------------------------------//
        formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).addOnChange(_self.onChangeAdditionalExpenseId);
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).addOnChange(_self.onChangeVatNumber);
        formContext.getAttribute(_self.formModel.fields.freightamount).addOnChange(_self.onChangeFreightAmount);

        //-----------------------------------< DATI SPEDIZIONE >-----------------------------------//
        formContext.getAttribute(_self.formModel.fields.shipto_line1).addOnChange(_self.onChangeShipToLine1);
        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).addOnChange(_self.onChangeShipToPostalCode);
        formContext.getAttribute(_self.formModel.fields.shipto_city).addOnChange(_self.onChangeShipToCity);
        formContext.getAttribute(_self.formModel.fields.willcall).addOnChange(() => { _self.onChangeWillCall(executionContext, true); });
        formContext.getAttribute(_self.formModel.fields.customerid).addOnChange(_self.onChangeCustomer);

        formContext.getAttribute(_self.formModel.fields.res_isinvoicerequested).addOnChange(_self.checkPotentialCustomerData);
        formContext.getAttribute(_self.formModel.fields.customerid).addOnChange(_self.checkPotentialCustomerData);

        formContext.getControl(_self.formModel.fields.customerid).addPreSearch(_self.filterPotentialCustomer);

        //Init function
        _self.addPriceLevelCustomView(executionContext);
        _self.setContextCapIframe(executionContext);
        _self.checkPotentialCustomerData(executionContext);
        _self.gestioneVisibilitàCampoSegnacollo(executionContext);

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