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


    //---------------------------------------------------
    _self.setDate = executionContext => {
        const formContext = executionContext.getFormContext();

        const dateControl = formContext.getControl(_self.formModel.fields.res_date);

        if (dateControl) {
            const date = new Date();
            if (dateControl.getAttribute().getValue() == null) {
                dateControl.getAttribute().setValue(date);
            }
        }
    };
    //---------------------------------------------------
    _self.setPriceLevelLookup = executionContext => {
        const formContext = executionContext.getFormContext();

        const priceLevelControl = formContext.getControl(_self.formModel.fields.pricelevelid);

        if (priceLevelControl) {
            priceLevelControl.getAttribute().setRequiredLevel("required");

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

                    priceLevelControl.getAttribute().setValue(priceLevelLookUp);
                },
                error => {
                    console.log(error.message);
                }
            );
        }
    };

    //---------------------< CAMPI SPESA ACCESSORIA >---------------------
    //
    //
    //
    //
    //------------------------< SPESA ACCESSORIA >------------------------ 
    _self.onChangeSpesaAccessoria = async (executionContext, flag) => {
        const formContext = executionContext.getFormContext();

        let importoSpesaAccessoria;
        let totaleImponibile;

        const spesaAccessoriaControl = formContext.getControl(_self.formModel.fields.res_additionalexpenseid);
        const totaleProdottiControl = formContext.getControl(_self.formModel.fields.totallineitemamount);
        const totaleImponibileControl = formContext.getControl(_self.formModel.fields.totalamountlessfreight);
        const codiceIvaControl = formContext.getControl(_self.formModel.fields.res_vatnumberid);
        const importoSpesaAccessoriaControl = formContext.getControl(_self.formModel.fields.freightamount);

        const spesaAccessoriaLookup = spesaAccessoriaControl.getAttribute().getValue() ?? null;
        const totaleProdotti = totaleProdottiControl.getAttribute().getValue() ?? null;

        //se viene selezionata una spesa accessoria
        if (spesaAccessoriaLookup) {

            //abilito il campo importo spesa accessoria
            importoSpesaAccessoriaControl.setDisabled(false);

            //abilito il campo codice IVA spesa accessoria e lo imposto come obbligatorio
            codiceIvaControl.setDisabled(false);
            codiceIvaControl.getAttribute().setRequiredLevel("required");

            //gestisco totale iva e importo totale (se il metodo viene chiamato alla modifica di un valore)
            if (flag) {
                const spesaAccessoria = await Xrm.WebApi.retrieveRecord("res_additionalexpense", spesaAccessoriaLookup[0].id, "?$select=res_amount");
                importoSpesaAccessoria = spesaAccessoria.res_amount;

                //imposto il suo ammontare al campo importo spesa accessoria
                formContext.getAttribute(_self.formModel.fields.freightamount).setValue(importoSpesaAccessoria ?? null);

                _self.onChangeCodiceIva(executionContext);

                // ricalcolo totale imponibile: imp tot + spesa acc
                totaleImponibile = totaleProdotti + importoSpesaAccessoria;
                totaleImponibileControl.getAttribute().setValue(totaleImponibile != 0 ? totaleImponibile : null);
            }

            //se non è stata selezionata spesa accessoria
        } else {

            //rendo il campo facoltativo e non editabile
            codiceIvaControl.getAttribute().setRequiredLevel("none");
            codiceIvaControl.setDisabled(true);
            importoSpesaAccessoriaControl.setDisabled(true);

            //se sono onChange
            if (flag) {
                //svuoto il campo codice iva spesa accessoria
                codiceIvaControl.getAttribute().setValue(null);

                //svuoto il campo importo spesa accessoria
                importoSpesaAccessoriaControl.getAttribute().setValue(null);

                //e sottraggo l'iva calcolata sulla spesa accessoria al totale iva e all'importo totale
                _self.onChangeCodiceIva(executionContext);

                // ricalcolo totale imponibile: imp tot + spesa acc
                totaleImponibile = totaleProdotti + importoSpesaAccessoria;
                totaleImponibileControl.getAttribute().setValue(totaleImponibile != 0 ? totaleImponibile : null);
            }
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
    //------------------< CODICE IVA SPESA ACCESSORIA >-------------------
    _self.onChangeCodiceIva = async executionContext => {
        const formContext = executionContext.getFormContext();

        const codiceIvaControl = formContext.getControl(_self.formModel.fields.res_vatnumberid);                    //codice iva spesa accessoria
        const totalTaxControl = formContext.getControl(_self.formModel.fields.totaltax);                            //totale iva
        const totalAmountControl = formContext.getControl(_self.formModel.fields.totalamount);                      //importo totale
        let importoSpesaAccessoria = formContext.getAttribute(_self.formModel.fields.freightamount).getValue() ?? 0;
        let totaleImponibile;
        let importoTotale;

        /**
         * all'onChange del campo codice iva
         * calcolo la spesa accessoria con applicata l'aliquota
         * recupero il totale iva di tutte le righe offerta associate
         * e imposto il risultato nel campo totale iva dell'offerta
         */
        const vatNumberLookup = codiceIvaControl.getAttribute().getValue();
        const vatNumberId = vatNumberLookup ? vatNumberLookup[0].id : null;

        const parentId = formContext.data.entity.getId();

        //retrieve della somma del Totale IVA di tutte le righe offerta
        const { righeTotaleImponibile, righeTotaleIva } = await _self.retrieveAggregatiRighe(parentId);

        //se è stato selezionato il codice iva spesa accessoria
        if (vatNumberId) {
            let vatNumber = await Xrm.WebApi.retrieveRecord("res_vatnumber", vatNumberId, "?$select=res_rate")
            const aliquotaCodiceIVA = vatNumber ? vatNumber.res_rate : 0;

            //recupero l'importo della spesa accessoria
            //let additionalExpense = await Xrm.WebApi.retrieveRecord("res_additionalexpense", additionalExpenseId, "?$select=res_amount");
            //importoSpesaAccessoria = additionalExpense ? additionalExpense.res_amount : 0;

            //calcolo l'iva sulla spesa accessoria
            if (importoSpesaAccessoria && aliquotaCodiceIVA) {
                const ivaSpesaAccessoria = importoSpesaAccessoria * (aliquotaCodiceIVA / 100);

                //totale iva offerta (iva della spesa accessoria + totale righe offerta)
                const totaleIVA = righeTotaleIva + ivaSpesaAccessoria;

                //imposto il valore calcolato nel campo Totale IVA
                totalTaxControl.getAttribute().setValue(totaleIVA != 0 ? totaleIVA : null);

                totaleImponibile = righeTotaleImponibile + importoSpesaAccessoria;
                formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(totaleImponibile != 0 ? totaleImponibile : null);

                importoTotale = totaleImponibile + totaleIVA;
                totalAmountControl.getAttribute().setValue(importoTotale != 0 ? importoTotale : null);
            } else throw console.error("additional expense amount or vat number are missing");
        } else {
            /**
             * se non è stato selezionato il codice iva spesa accessoria
             * imposto il totale iva delle righe offerta senza l'iva sulla spesa accessoria
             */
            totalTaxControl.getAttribute().setValue(righeTotaleIva != 0 ? righeTotaleIva : null);

            /**
             * sottraggo l'eventuale iva della spesa accessoria all'importo totale
             */
            totaleImponibile = righeTotaleImponibile + importoSpesaAccessoria;

            importoTotale = righeTotaleIva + totaleImponibile;

            totalAmountControl.getAttribute().setValue(importoTotale != 0 ? importoTotale : null);

            // ricalcolo Totale Imponibile  (imp tot + imp spesa acc)
            formContext.getAttribute(_self.formModel.fields.totalamountlessfreight).setValue(totaleImponibile != 0 ? totaleImponibile : null);
        }
    };
    //--------------------< IMPORTO SPESA ACCESSORIA >--------------------
    _self.onChangeImportoSpesaAccessoria = async function (executionContext) {
        const formContext = executionContext.getFormContext();
        const importoSpesaAccessoria = formContext.getAttribute(_self.formModel.fields.freightamount).getValue() ?? 0;

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
    //---------------------------------------------------
    _self.setPostalCodeRelatedFieldsRequirement = executionContext => {
        const formContext = executionContext.getFormContext();

        const postalCodeControl = formContext.getControl(_self.formModel.fields.shipto_postalcode);
        const cityControl = formContext.getControl(_self.formModel.fields.shipto_city);

        if (postalCodeControl) {
            if (!postalCodeControl.getAttribute().getValue()) {
                cityControl.getAttribute().setRequiredLevel("none");
                cityControl.setDisabled(true);
            } else {
                cityControl.getAttribute().setRequiredLevel("required");
                cityControl.setDisabled(false);
            }
        }
    };
    //---------------------------------------------------
    _self.handleWillCallRelatedFields = executionContext => {
        const formContext = executionContext.getFormContext();

        const willCallControl = formContext.getControl(_self.formModel.fields.willcall);

        /**
         * mostro/nascondo tutti i campi relativi al campo Spedizione
         * e gestisco obbligatorietà e read-only
         */
        const willCallControlsVisibility = [
            _self.formModel.fields.res_shippingreference,
            _self.formModel.fields.shipto_line1,
            _self.formModel.fields.shipto_postalcode,
            _self.formModel.fields.res_location,
            _self.formModel.fields.shipto_city,
            _self.formModel.fields.shipto_stateorprovince,
            _self.formModel.fields.res_countryid,
        ];

        const willCallControlsRequirement = [
            _self.formModel.fields.shipto_line1,
            _self.formModel.fields.shipto_postalcode,
            _self.formModel.fields.shipto_city,
        ];

        willCallControlsVisibility.forEach(field => {
            const control = formContext.getControl(field);
            if (!control) throw new Error(`${field} field is missing`);

            if (willCallControl.getAttribute().getValue() == _self.formModel.fields.willcallValues.Indirizzo) {
                formContext.getControl("WebResource_postalcode").setVisible(true);
                control.setVisible(true);
                _self.setContextCapIframe(executionContext);
            }

            if (willCallControl.getAttribute().getValue() == _self.formModel.fields.willcallValues.Spedizioneacaricodelcliente) {
                formContext.getControl("WebResource_postalcode").setVisible(false);
                control.setVisible(false);
                control.getAttribute().setValue(null)
            }
        });

        willCallControlsRequirement.forEach(field => {
            const control = formContext.getControl(field);

            if (!control) throw new Error(`${field} field is missing`);

            if (willCallControl.getAttribute().getValue() == _self.formModel.fields.willcallValues.Indirizzo) {
                control.getAttribute().setRequiredLevel("required");
            }

            if (willCallControl.getAttribute().getValue() == _self.formModel.fields.willcallValues.Spedizioneacaricodelcliente) {
                control.getAttribute().setRequiredLevel("none");
            }

        });

        this.setCityRelatedFieldsEditability(executionContext);
    };
    //---------------------------------------------------
    _self.setCityRelatedFieldsEditability = executionContext => {
        const formContext = executionContext.getFormContext();

        const shipToCityControl = formContext.getControl(_self.formModel.fields.shipto_city);
        const shipToCityValue = shipToCityControl ? shipToCityControl.getAttribute().getValue() ?? null : null;

        /**
         * se Città spedizione è valorizzato, i campi correlati (Località, Provincia, Nazione spedizione) sono editabili
         */
        const shipToCityRelatedFields = [
            _self.formModel.fields.res_location,
            _self.formModel.fields.shipto_stateorprovince,
            _self.formModel.fields.res_countryid
        ];

        shipToCityRelatedFields.forEach(field => {
            const control = formContext.getControl(field);
            if (!control) { throw new Error(`${field} field is missing`); }

            if (shipToCityValue) { control.setDisabled(false); } else { control.setDisabled(true); }
        });

    };
    //---------------------------------------------------
    _self.setBankVisibility = executionContext => {
        const formContext = executionContext.getFormContext();
        const bankControl = formContext.getControl(_self.formModel.fields.res_bankdetailsid);

        /**
         * controllo visibilità campo Banca
         */
        if (bankControl) {
            const paymentTermControl = formContext.getControl("res_paymenttermid");
            if (paymentTermControl) {
                const paymentTermId = paymentTermControl.getAttribute().getValue() ? paymentTermControl.getAttribute().getValue()[0].id : null;
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
                } else {
                    bankControl.setVisible(false);
                }
            }
        }
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
                        const notification = "Per acquisire l'offerta è necessario compilare i seguenti campi del potenziale cliente: " + missingDataString;
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
    _self.onChangeAddress = executionContext => {
        _self.handleWillCallRelatedFields(executionContext);
    };
    //---------------------------------------------------
    _self.filterPotentialCustomer = executionContext => {
        const formContext = executionContext.getFormContext();
        const potentialCustomerControl = formContext.getControl(_self.formModel.fields.customerid);
        if (!potentialCustomerControl) { console.error(`Controllo ${potentialCustomerControl} non trovato`); return; }

        //  filtro gli account
        const accountFilter = "<filter><condition attribute='statecode' operator='eq' value='0' /></filter>";
        potentialCustomerControl.addCustomFilter(accountFilter, "account");
        console.log("Filtro account applicato");

        //  filtro i contatti
        const contactFilter = "<filter><condition attribute='contactid' operator='eq' value='00000000-0000-0000-0000-000000000000' /></filter>";
        potentialCustomerControl.addCustomFilter(contactFilter, "contact");
        console.log("Filtro contatti applicato");
    };
    //---------------------------------------------------
    _self.onChangeCustomer = async function (executionContext) {
        var formContext = executionContext.getFormContext();

        console.log("on change customer");
        let customerLookup = formContext.getAttribute(_self.formModel.fields.customerid).getValue();
        //let tipoSpedizione = formContext.getAttribute(_self.formModel.fields.willcall).getValue();

        if (customerLookup !== null) { // && tipoSpedizione == _self.formModel.fields.willcallValues.Indirizzo

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


            }
        } else {
            formContext.getAttribute(_self.formModel.fields.shipto_line1).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_postalcode).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_city).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_location).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_stateorprovince).setValue(null);
            formContext.getAttribute(_self.formModel.fields.shipto_country).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_countryid).setValue(null);


        }
        _self.setPostalCodeRelatedFieldsRequirement(executionContext);
        _self.setCityRelatedFieldsEditability(executionContext);
        _self.handleWillCallRelatedFields(executionContext);
    };
    //---------------------------------------------------
    _self.onChangeWillCall = function (executionContext) {
        var formContext = executionContext.getFormContext();

        let willCall = formContext.getAttribute(_self.formModel.fields.willcall).getValue();

        if (willCall == _self.formModel.fields.willcallValues.Indirizzo) {
            _self.onChangeCustomer(executionContext);
        }

        _self.handleWillCallRelatedFields(executionContext);
    };
    //---------------------------------------------------
    _self.hasQuoteDetails = formContext => {
        const subgrid = formContext.getControl("quotedetailsGrid");
        if (subgrid && subgrid.getGrid()) {
            return subgrid.getGrid().getTotalRecordCount() > 0 ? true : false;
        }
    }
    //---------------------------------------------------
    _self.addSubgridEventListener = executionContext => {
        const formContext = executionContext.getFormContext();
        const gridContext = formContext.getControl("quotedetailsGrid");

        if (!gridContext) {
            setTimeout(() => { this.addSubgridEventListener(executionContext); }, 500);
            return;
        }
        gridContext.addOnLoad(_self.subgridEventListener);
    };
    //---------------------------------------------------
    _self.subgridEventListener = executionContext => {
        const formContext = executionContext.getFormContext();
        formContext.data.refresh();
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

        _self.onChangeSpesaAccessoria(executionContext, false);

        _self.handleWillCallRelatedFields(executionContext);
        _self.setCityRelatedFieldsEditability(executionContext);
        _self.setPostalCodeRelatedFieldsRequirement(executionContext);
    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {
        var formContext = executionContext.getFormContext();

        formContext.ui.clearFormNotification("HAS_QUOTE_DETAILS");

        _self.onChangeSpesaAccessoria(executionContext, false);

        _self.handleWillCallRelatedFields(executionContext);
        _self.setCityRelatedFieldsEditability(executionContext);
        _self.setPostalCodeRelatedFieldsRequirement(executionContext);

        let statuscodeValue = formContext.getAttribute(_self.formModel.fields.statuscode).getValue();

        if (_self.lockFieldStatus.includes(statuscodeValue)) {

            formContext.getControl(_self.formModel.fields.res_countryid).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(true);
            formContext.getControl("WebResource_postalcode").setVisible(false);
        }

        const hasQuoteDetails = _self.hasQuoteDetails(formContext);
        if (!hasQuoteDetails) {

            const notification = {
                message: "Per mandare in approvazione, approvare o acquisire l'offerta è necessario aggiungere almeno un prodotto.",
                level: "INFO",
                uniqueId: "HAS_QUOTE_DETAILS"
            };

            formContext.ui.setFormNotification(notification.message, notification.level, notification.uniqueId);
        }

        _self.addSubgridEventListener(executionContext);
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        _self.checkPotentialCustomerData(executionContext);

        let statuscodeValue = formContext.getAttribute(_self.formModel.fields.statuscode).getValue();

        if (_self.lockFieldStatus.includes(statuscodeValue)) {

            formContext.getControl(_self.formModel.fields.res_countryid).setDisabled(true);
            formContext.getControl(_self.formModel.fields.res_vatnumberid).setDisabled(true);
            formContext.getControl("WebResource_postalcode").setVisible(false);
        }
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

        //--------------------------------< CONDIZIONI DI PAGAMENTO >--------------------------------//
        formContext.getAttribute(_self.formModel.fields.res_paymenttermid).addOnChange(_self.setBankVisibility);

        //-----------------------------------< SPESE ACCESSORIE >-----------------------------------//
        formContext.getAttribute(_self.formModel.fields.res_additionalexpenseid).addOnChange(() => { _self.onChangeSpesaAccessoria(executionContext, true) });
        formContext.getAttribute(_self.formModel.fields.res_vatnumberid).addOnChange(_self.onChangeCodiceIva); //codice IVA spesa accessoria
        formContext.getAttribute(_self.formModel.fields.freightamount).addOnChange(_self.onChangeImportoSpesaAccessoria);

        //-----------------------------------< DATI SPEDIZIONE >-----------------------------------//
        formContext.getAttribute(_self.formModel.fields.shipto_postalcode).addOnChange(_self.setPostalCodeRelatedFieldsRequirement);
        formContext.getAttribute(_self.formModel.fields.shipto_city).addOnChange(_self.setCityRelatedFieldsEditability);
        //formContext.getAttribute(_self.formModel.fields.willcall).addOnChange(_self.handleWillCallRelatedFields);
        formContext.getAttribute(_self.formModel.fields.willcall).addOnChange(_self.onChangeWillCall);
        formContext.getAttribute(_self.formModel.fields.customerid).addOnChange(_self.onChangeCustomer);

        formContext.getAttribute(_self.formModel.fields.res_isinvoicerequested).addOnChange(_self.checkPotentialCustomerData);
        formContext.getAttribute(_self.formModel.fields.customerid).addOnChange(_self.checkPotentialCustomerData);

        formContext.getControl(_self.formModel.fields.customerid).addPreSearch(_self.filterPotentialCustomer);

        //Init function
        _self.setDate(executionContext);
        _self.setPriceLevelLookup(executionContext);

        _self.setBankVisibility(executionContext);
        _self.checkPotentialCustomerData(executionContext);
        _self.setContextCapIframe(executionContext);

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
).call(RSMNG.TAUMEDIKA.QUOTE);