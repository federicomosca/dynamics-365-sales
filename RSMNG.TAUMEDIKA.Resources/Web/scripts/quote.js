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
            ///Condizioni di pagamento (campo lookup)
            res_paymenttermid: "res_paymenttermid",
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
            ///Sconto totale
            totaldiscountamount: "totaldiscountamount",
            ///Totale righe
            totallineitemamount: "totallineitemamount",
            ///Importo totale sconto per voce
            totallineitemdiscountamount: "totallineitemdiscountamount",
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
            /// statuscode
            statuscode: "statuscode",
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
    // stati che prevedono il blocco di campi 
    _self.lockFieldStatus = [
        _self.formModel.fields.statuscodeValues.Inapprovazione_StateAttiva,
        _self.formModel.fields.statuscodeValues.Approvata_StateAttiva,
        _self.formModel.fields.statuscodeValues.Nonapprovata_StateChiusa,
        _self.formModel.fields.statuscodeValues.Acquisita_StateAcquisita,
        _self.formModel.fields.statuscodeValues.Persa_StateChiusa,
        _self.formModel.fields.statuscodeValues.Aggiornata_StateChiusa
    ];
    _self.readOnlyFields = [
        _self.formModel.fields.ordernumber,
        _self.formModel.fields.res_origincode,
        _self.formModel.fields.datefulfilled,
        _self.formModel.fields.totallineitemamount,
        _self.formModel.fields.totaldiscountamount,
        _self.formModel.fields.totalamountlessfreight,
        _self.formModel.fields.totaltax,
        _self.formModel.fields.totalamount,
        _self.formModel.fields.quoteid
    ];

    //--------------------------------< CONDIZIONI DI PAGAMENTO >-------------------------------//
    _self.onChangeCondizioniPagamento = function (executionContext) {
        const formContext = executionContext.getFormContext();

        const condizioniPagamento = formContext.getAttribute(_self.formModel.fields.res_paymenttermid).getValue();
        const campoBanca = formContext.getControl(_self.formModel.fields.res_bankdetailsid);

        //il campo Banca può essere visibile solo se è stata selezionata una Condizione di pagamento
        if (condizioniPagamento) {
            Xrm.WebApi.retrieveRecord("res_paymentterm", condizioniPagamento[0].id, "?$select=res_isbankvisible").then(
                result => {
                    //dopodiché si verifica il flag impostato nella condizione di pagamento: 'abilita visibilità banca'
                    const isBankVisible = result.res_isbankvisible;

                    campoBanca.setVisible(isBankVisible);

                    //se il flag è impostato su false, il campo Banca viene nascosto e svuotato del valore
                    if (!isBankVisible) { campoBanca.getAttribute().setValue(null); }
                },
                error => {
                    console.log(error.message);
                }
            );
        }
        //se la Condizione di pagamento non è stata selezionata
        else {

            //imposto a null l'eventuale valore e nascondo il campo
            campoBanca.getAttribute().setValue(null);
            campoBanca.setVisible(false);
        }
    };

    //-----------------------------------< SPESE ACCESSORIE >-----------------------------------//
    //
    //
    //
    //
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
    //---------------------------------------------------
    _self.retrieveAggregatiRighe = async parentId => {

        var fetchAggregati = [
            "?fetchXml=<fetch aggregate='true'>",
            "  <entity name='quotedetail'>",
            "    <attribute name='res_taxableamount' alias='TotaleImponibile' aggregate='sum'/>",
            "    <attribute name='tax' alias='TotaleIva' aggregate='sum'/>",
            "    <filter>",
            "      <condition attribute='quoteid' operator='eq' value='", parentId, "'/>",
            "    </filter>",
            "  </entity>",
            "</fetch>"
        ].join("");

        try {
            const result = await Xrm.WebApi.retrieveMultipleRecords("quotedetail", fetchAggregati);
            const aggregati = result.entities[0];
            const totaleImponibile = aggregati.TotaleImponibile;
            const totaleIva = aggregati.TotaleIva;

            // ritorno un oggetto JSON con i valori aggregati
            return {
                righeTotaleImponibile: totaleImponibile,
                righeTotaleIva: totaleIva
            }
        } catch (error) {
            console.error("Errore nel recupero degli aggregati:", error);
        }
    };
    //---------------------------------------------------
    _self.onChangeSpesaAccessoria = async executionContext => {
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
            _self.onChangeImportoSpesaAccessoria(executionContext);

        } else {
            //se ho svuotato Spesa accessoria...

            //rendo facoltativo il codice iva e svuoto il campo importo spesa accessoria
            formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel("none");
            formContext.getAttribute(_self.formModel.fields.freightamount).setValue(null);

            //e li rendo non editabili
            formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(true);
            formContext.getControl(_self.formModel.fields.freightamount).setDisabled(true);

            //svuoto i campi Totale imponibile, Totale IVA e Importo totale
            formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(null);
            formContext.getAttribute(_self.formModel.fields.totaltax).setValue(null);
            formContext.getAttribute(_self.formModel.fields.totalamount).setValue(null);
        }
    };
    //---------------------------------------------------
    _self.onChangeCodiceIvaSpesaAccessoria = async executionContext => {
        const formContext = executionContext.getFormContext();

        const codiceIvaSpesaAccessoriaControl = formContext.getControl(_self.formModel.fields.res_vatnumberid);
        const spesaAccessoriaControl = formContext.getControl(_self.formModel.fields.res_additionalexpenseid);
        const totalTaxControl = formContext.getControl(_self.formModel.fields.totaltax);
        const importoTotaleControl = formContext.getControl(_self.formModel.fields.totalamount);
        const importoSpesaAccessoriaControl = formContext.getControl(_self.formModel.fields.freightamount);
        const totaleImponibileControl = formContext.getControl(_self.formModel.fields.totalamountlessfreight);

        /**
         * calcolo la spesa accessoria con applicata l'aliquota
         * recupero il totale iva di tutte le righe offerta associate
         * e imposto il risultato nel campo totale iva dell'offerta
         */
        const codiceIvaLookup = codiceIvaSpesaAccessoriaControl.getAttribute().getValue();
        const spesaAccessoriaLookup = spesaAccessoriaControl.getAttribute().getValue();

        const codiceIvaId = codiceIvaLookup ? codiceIvaLookup[0].id : null;
        const spesaAccessoriaId = spesaAccessoriaLookup ? spesaAccessoriaLookup[0].id : null;

        //retrieve della somma del Totale IVA di tutte le righe offerta
        const parentId = formContext.data.entity.getId();

        const { righeTotaleImponibile, righeTotaleIva } = await _self.retrieveAggregatiRighe(parentId);
        let importoSpesaAccessoria = importoSpesaAccessoriaControl.getAttribute().getValue() ?? 0;

        //se è stato selezionato il codice iva spesa accessoria
        if (codiceIvaId) {
            const codiceIvaSpesaAccessoria = await Xrm.WebApi.retrieveRecord("res_vatnumber", codiceIvaId, "?$select=res_rate");
            const aliquotaCodiceIVA = codiceIvaSpesaAccessoria?.res_rate ?? 0;

            if (!importoSpesaAccessoria) {
                const spesaAccessoria = await Xrm.WebApi.retrieveRecord("res_additionalexpense", spesaAccessoriaId, "?$select=res_amount")
                importoSpesaAccessoria = spesaAccessoria ? spesaAccessoria.res_amount : 0;
            }

            //calcolo l'iva sulla spesa accessoria
            const ivaSpesaAccessoria = importoSpesaAccessoria * (aliquotaCodiceIVA / 100);

            const totaleImponibile = righeTotaleImponibile ?? 0 + importoSpesaAccessoria;
            const totaleIVA = righeTotaleIva ?? 0 + ivaSpesaAccessoria;
            const importoTotale = totaleImponibile + totaleIVA;

            totaleImponibileControl.getAttribute().setValue(totaleImponibile);
            totalTaxControl.getAttribute().setValue(totaleIVA);
            importoTotaleControl.getAttribute().setValue(importoTotale);

        } else {

            const totaleImponibile = righeTotaleImponibile ?? 0 + importoSpesaAccessoria;
            const importoTotale = righeTotaleIva ?? 0 + totaleImponibile;

            totalTaxControl.getAttribute().setValue(righeTotaleIva);
            totaleImponibileControl.getAttribute().setValue(totaleImponibile);
            importoTotaleControl.getAttribute().setValue(importoTotale);
        }
    };
    //---------------------------------------------------
    _self.onChangeImportoSpesaAccessoria = async (executionContext) => {

        const formContext = executionContext.getFormContext();
        const importoSpesaAccessoria = formContext.getAttribute(_self.formModel.fields.freightamount).getValue() ?? 0;

        //--------------------------------< RECUPERO I VALORI PER CALCOLARE IL TOTALE IMPONIBILE >--------------------------------//
        const parentId = formContext.data.entity.getId();
        const { righeTotaleImponibile, righeTotaleIva } = await _self.retrieveAggregatiRighe(parentId);

        //--------------------------------< RECUPERO L'ALIQUOTA DEL CODICE IVA SPESA ACCESSORIA >--------------------------------//
        const codiceIVASpesaAccessoria = formContext.getAttribute(_self.formModel.fields.res_vatnumberid).getValue(); //Codice IVA Spesa Accessoria
        let totaleIva = 0;
        let totaleImponibile = 0;

        if (codiceIVASpesaAccessoria) {
            const codiceIvaId = codiceIVASpesaAccessoria[0].id;

            const aliquota = await _self.retrieveAliquotaCodiceIVA(codiceIvaId);
            totaleImponibile = righeTotaleImponibile /* aka totale prodotti */ ?? 0 + importoSpesaAccessoria;
            totaleIva = righeTotaleIva ?? 0 + (importoSpesaAccessoria * (aliquota / 100));
        }
        totaleImponibile = importoSpesaAccessoria;
        const importoTotale = totaleImponibile + totaleIva;

        formContext.getAttribute(_self.formModel.fields.totaltax).setValue(totaleIva != 0 ? totaleIva : null);
        formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(totaleImponibile != 0 ? totaleImponibile : null);
        formContext.getAttribute(_self.formModel.fields.totalamount).setValue(importoTotale != 0 ? importoTotale : null);
    };
    //
    //
    //
    //
    //------------------------------------------------------------------------------------------//
    //-----------------------------------< DATI SPEDIZIONE >------------------------------------//
    //
    //
    //
    //
    _self.onChangeSpedizione = function (executionContext, isEvent) {
        const formContext = executionContext.getFormContext();
        const spedizione = formContext.getAttribute(_self.formModel.fields.willcall).getValue();

        //se spedizione == presso cliente, è valorizzato con true, altrimenti false
        const isPressoCliente = spedizione == _self.formModel.fields.willcallValues.Indirizzo;

        //campi spedizione di cui gestire visibilità e obbligatorietà
        const campiSpedizione = [
            _self.formModel.fields.res_shippingreference,
            _self.formModel.fields.shipto_line1,
            _self.formModel.fields.shipto_postalcode,
            _self.formModel.fields.res_location,
            _self.formModel.fields.shipto_city,
            _self.formModel.fields.shipto_stateorprovince,
            _self.formModel.fields.res_countryid
        ];

        //gestisco la visibilità dei campi a seconda del valore di 'spedizione'
        campiSpedizione.forEach(campo => {
            const control = formContext.getControl(campo);
            control.setVisible(isPressoCliente);
            if (!isPressoCliente) {
                const attribute = control.getAttribute();
                attribute.setValue(null);
                attribute.setRequiredLevel("none");
            }
        });

        if (!isPressoCliente) {
            formContext.getControl("WebResource_postalcode").setVisible(false);
            if (isEvent) { _self.onChangeCliente(executionContext); }
        }
    };
    //---------------------------------------------------
    _self.onChangeIndirizzo = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToLine1 = formContext.getAttribute(_self.formModel.fields.shipto_line1).getValue();

        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setRequiredLevel(shipToLine1 !== null ? "required" : "none");
        formContext.getControl(_self.formModel.fields.shipto_postalcode).setVisible(shipToLine1 !== null ? true : false);
        formContext.getControl("WebResource_postalcode").setVisible(shipToLine1 !== null ? true : false);

        if (shipToLine1 !== null) {

            _self.setContextCapIframe(executionContext);
        } else {

            formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setValue(null);
        }

    };
    //---------------------------------------------------
    _self.onChangeCAP = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToPostalCode = formContext.getAttribute(_self.formModel.fields.shipto_postalcode).getValue();

        formContext.getAttribute(_self.formModel.fields.shipto_city).setRequiredLevel(shipToPostalCode !== null ? "required" : "none");
        formContext.getControl(_self.formModel.fields.shipto_city).setDisabled(shipToPostalCode !== null ? false : true);

    };
    //---------------------------------------------------
    _self.onChangeCittà = function (executionContext) {
        var formContext = executionContext.getFormContext();
        let shipToCity = formContext.getAttribute(_self.formModel.fields.shipto_city).getValue();

        formContext.getControl(_self.formModel.fields.res_location).setDisabled(shipToCity !== null ? false : true);
        formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setDisabled(shipToCity !== null ? false : true);
        formContext.getControl(_self.formModel.fields.res_countryid).setDisabled(shipToCity !== null ? false : true);
    };
    //
    //
    //
    //
    //-----------------------------------------------------------------------------------------//

    //---------------------------------------< CLIENTE >---------------------------------------//
    //
    //
    //
    //
    _self.filterCliente = executionContext => {
        const formContext = executionContext.getFormContext();
        const campoCliente = formContext.getControl(_self.formModel.fields.customerid);
        if (!campoCliente) { console.error(`Controllo ${campoCliente} non trovato`); return; }

        //  filtro gli account
        const accountFilter = "<filter><condition attribute='statecode' operator='eq' value='0' /></filter>";
        campoCliente.addCustomFilter(accountFilter, "account");

        //  filtro i contatti
        const contactFilter = "<filter><condition attribute='contactid' operator='eq' value='00000000-0000-0000-0000-000000000000' /></filter>";
        campoCliente.addCustomFilter(contactFilter, "contact");

    };
    //---------------------------------------------------
    _self.onChangeCliente = async function (executionContext) {
        const formContext = executionContext.getFormContext();

        const cliente = formContext.getAttribute(_self.formModel.fields.customerid).getValue();
        const campiSpedizione = {
            indirizzo: formContext.getAttribute(_self.formModel.fields.shipto_line1),
            CAP: formContext.getAttribute(_self.formModel.fields.shipto_postalcode),
            città: formContext.getAttribute(_self.formModel.fields.shipto_city),
            località: formContext.getAttribute(_self.formModel.fields.res_location),
            provincia: formContext.getAttribute(_self.formModel.fields.shipto_stateorprovince),
            nazione: formContext.getAttribute(_self.formModel.fields.res_countryid),
            paese: formContext.getAttribute(_self.formModel.fields.shipto_country)
        }

        if (cliente !== null) { // willcall è un bool. richiede == per fare la conversione implicita
            const indirizzi = await RSMNG.TAUMEDIKA.GLOBAL.getCustomerAddresses(cliente[0].id, true);

            if (indirizzi != null && indirizzi.entities.length > 0) {

                const indirizzoCliente = indirizzi.entities[0];

                formContext.getAttribute(_self.formModel.fields.willcall).setValue(Boolean(_self.formModel.fields.willcallValues.Indirizzo));

                if (indirizzoCliente._res_countryid_value != null) {

                    const countryLookup = [{
                        id: indirizzoCliente["_res_countryid_value"],
                        entityType: 'res_country',
                        name: indirizzoCliente["_res_countryid_value@OData.Community.Display.V1.FormattedValue"]
                    }];

                    //una volta recuperato l'indirizzo del cliente, valorizzo i campi spedizione dell'offerta
                    campiSpedizione.indirizzo.setValue(indirizzoCliente.res_address);
                    campiSpedizione.CAP.setValue(indirizzoCliente.res_postalcode);
                    campiSpedizione.città.setValue(indirizzoCliente.res_city);
                    campiSpedizione.località.setValue(indirizzoCliente.res_location);
                    campiSpedizione.provincia.setValue(indirizzoCliente.res_province);
                    campiSpedizione.nazione.setValue(countryLookup);
                    campiSpedizione.paese.setValue(indirizzoCliente["_res_countryid_value@OData.Community.Display.V1.FormattedValue"]);
                }
                _self.updateAddressFieldsRequirements(executionContext);
            }
        } else {

            //svuoto tutti i campi
            Object.values(campiSpedizione).forEach(campo => {
                campo.setValue(null);
            });
            _self.updateAddressFieldsRequirements(executionContext);
        }
    };
    //
    //
    //
    //
    //-----------------------------------------------------------------------------------------//

    //---------------------------------------------------
    _self.addCustomViewListinoPrezzi = function (executionContext) {

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
    };
    //---------------------------------------------------
    _self.setValueData = function (executionContext) {
        const formContext = executionContext.getFormContext();

        formContext.getAttribute(_self.formModel.fields.res_date).setValue(new Date());
    };
    //---------------------------------------------------
    _self.updateAddressFieldsRequirements = function (executionContext) {
        const formContext = executionContext.getFormContext();
        const campoCittà = formContext.getControl(_self.formModel.fields.shipto_city);

        const CAP = formContext.getAttribute(_self.formModel.fields.shipto_postalcode).getValue();
        const città = campoCittà.getAttribute().getValue();

        _self.onChangeIndirizzo(executionContext);

        //se il cap è valorizzato rendo il campo Città obbligatorio e modificabile
        campoCittà.getAttribute().setRequiredLevel(CAP ? "required" : "none");
        campoCittà.setDisabled(CAP ? false : true);

        //gestisco i campi relativi al campo Città in base al fatto che sia valorizzato o meno
        _self.disableCityRelated(executionContext, città ? false : true);
    };
    //---------------------------------------------------
    _self.disableCityRelated = function (executionContext, isDisable) {
        var formContext = executionContext.getFormContext();

        formContext.getControl(_self.formModel.fields.res_location).setDisabled(isDisable);
        formContext.getControl(_self.formModel.fields.shipto_stateorprovince).setDisabled(isDisable);
        formContext.getControl(_self.formModel.fields.res_countryid).setDisabled(isDisable);
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

        _self.setValueData(executionContext);

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

        //--- Blocca/Sblocca i campi in base a Utente-Agente e Motivo Stato---
        let currStatus = formContext.getAttribute(_self.formModel.fields.statuscode).getValue();

        if (currStatus == _self.formModel.fields.statuscodeValues.Inapprovazione_StateAttivo || currStatus == _self.formModel.fields.statuscodeValues.Approvato_StateAttivo) {

            let isAgent = await RSMNG.TAUMEDIKA.GLOBAL.getAgent();
            RSMNG.TAUMEDIKA.GLOBAL.setAllFieldsReadOnly(formContext, isAgent, _self.readOnlyFields);
        }

        //----------------------------------------------
        _self.onChangeSpedizione(executionContext, false);
        _self.onChangeIndirizzo(executionContext);
        _self.onChangeCAP(executionContext);
        _self.onChangeCittà(executionContext);
        //-----
        let additionlExpenseId = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();

        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).setRequiredLevel(additionlExpenseId !== null ? "required" : "none");

        const spesaAccessoria = formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).getValue();
        formContext.getControl(_self.formModel.fields.freightamount).setDisabled(spesaAccessoria ? false : true);
        formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(spesaAccessoria ? false : true);
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
        formContext.getAttribute(_self.formModel.fields.res_paymenttermid).addOnChange(_self.onChangeCondizioniPagamento);

        //-----------------------------------< SPESE ACCESSORIE >-----------------------------------//
        formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).addOnChange(_self.onChangeSpesaAccessoria);
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).addOnChange(_self.onChangeCodiceIvaSpesaAccessoria);
        formContext.getAttribute(_self.formModel.fields.freightamount).addOnChange(_self.onChangeImportoSpesaAccessoria);

        //-----------------------------------< DATI SPEDIZIONE >-----------------------------------//
        formContext.getAttribute(_self.formModel.fields.willcall).addOnChange(() => { _self.onChangeSpedizione(executionContext, true); });
        formContext.getAttribute(_self.formModel.fields.shipto_line1).addOnChange(_self.onChangeIndirizzo);
        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).addOnChange(_self.onChangeCAP);
        formContext.getAttribute(_self.formModel.fields.shipto_city).addOnChange(_self.onChangeCittà);

        //---------------------------------------< CLIENTE >---------------------------------------//
        formContext.getControl(_self.formModel.fields.customerid).addPreSearch(_self.filterCliente);
        formContext.getAttribute(_self.formModel.fields.customerid).addOnChange(_self.onChangeCliente);

        //Init function
        _self.addCustomViewListinoPrezzi(executionContext);
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
).call(RSMNG.TAUMEDIKA.QUOTE);