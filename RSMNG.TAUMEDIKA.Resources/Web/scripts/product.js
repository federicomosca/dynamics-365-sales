//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRODUCT) == "undefined") {
    RSMNG.TAUMEDIKA.PRODUCT = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            logicalName: "product",
            displayName: "Prodotto"
        },
        fields: {
            ////Created By (External Party)
            createdbyexternalparty: "createdbyexternalparty",
            ///Costo corrente
            currentcost: "currentcost",
            ///Costo corrente (Base)
            currentcost_base: "currentcost_base",
            ///Unità predefinita
            defaultuomid: "defaultuomid",
            ///Unità di vendita
            defaultuomscheduleid: "defaultuomscheduleid",
            ///Descrizione
            description: "description",
            ///Solo per uso interno
            dmtimportstate: "dmtimportstate",
            ///Famiglia associata
            editableParentControl: "editableParentControl",
            ///Immagine entità
            entityimage: "entityimage",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Percorso gerarchia
            hierarchypath: "hierarchypath",
            ///Kit
            iskit: "iskit",
            ///Associato con nuovo elemento padre
            isreparented: "isreparented",
            ///Disponibile in magazzino
            isstockitem: "isstockitem",
            ///Modified By (External Party)
            modifiedbyexternalparty: "modifiedbyexternalparty",
            ///Rifiuto esplicito RGPD
            msdyn_gdproptout: "msdyn_gdproptout",
            ///Nome
            name: "name",
            ///Organization Id
            organizationid: "organizationid",
            ///Padre
            parentproductid: "parentproductid",
            ///Prezzo di listino
            price: "price",
            ///Prezzo di listino (Base)
            price_base: "price_base",
            ///Listino prezzi predefinito
            pricelevelid: "pricelevelid",
            ///Process Id
            processid: "processid",
            ///Prodotto
            productid: "productid",
            ///Codice
            productnumber: "productnumber",
            ///Struttura prodotto
            productstructure: "productstructure",
            ///Tipo di prodotto
            producttypecode: "producttypecode",
            ///URL
            producturl: "producturl",
            ///Decimali supportati
            quantitydecimal: "quantitydecimal",
            ///Disponibilità
            quantityonhand: "quantityonhand",
            ///Codice a barre
            res_barcode: "res_barcode",
            ///Peso lordo
            res_grossweight: "res_grossweight",
            ///Produttore
            res_manufacturer: "res_manufacturer",
            ///Origine
            res_origincode: "res_origincode",
            ///Categoria Principale
            res_parentcategoryid: "res_parentcategoryid",
            ///Unità di misura (peso)
            res_uomweightid: "res_uomweightid",
            ///Codice IVA
            res_vatnumberid: "res_vatnumberid",
            ///Dimensioni
            size: "size",
            ///(Deprecated) Stage Id
            stageid: "stageid",
            ///Costo medio
            standardcost: "standardcost",
            ///Costo medio (Base)
            standardcost_base: "standardcost_base",
            ///Volume (cm3)
            stockvolume: "stockvolume",
            ///Peso netto
            stockweight: "stockweight",
            ///Argomento
            subjectid: "subjectid",
            ///Fornitore
            suppliername: "suppliername",
            ///Valuta
            transactioncurrencyid: "transactioncurrencyid",
            ///(Deprecated) Traversed Path
            traversedpath: "traversedpath",
            ///Valido da
            validfromdate: "validfromdate",
            ///Valido fino a
            validtodate: "validtodate",
            ///ID fornitore
            vendorid: "vendorid",
            ///Fornitore
            vendorname: "vendorname",
            ///Nome fornitore
            vendorpartnumber: "vendorpartnumber"
        },
        tabs: {

        },
        sections: {

        },
        options: {
            /// Values for field Stato
            statecodeValues: {
                Attivo: 0,
                Bozza: 2,
                Inaggiornamento: 3,
                Ritirato: 1
            },

            /// Values for field Motivo stato
            statuscodeValues: {
                Attivo_StateAttivo: 1,
                Bozza_StateBozza: 0,
                Inaggiornamento_StateInaggiornamento: 3,
                Ritirato_StateRitirato: 2
            },

            /// Values for field Kit
            iskitValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Associato con nuovo elemento padre
            isreparentedValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Disponibile in magazzino
            isstockitemValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Rifiuto esplicito RGPD
            msdyn_gdproptoutValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Struttura prodotto
            productstructureValues: {
                Aggregazioneprodotti: 3,
                Famigliadiprodotti: 2,
                Prodotto: 1
            },

            /// Values for field Tipo di prodotto
            producttypecodeValues: {
                Articolo: 3,
                Articoloconmagazzinolotti: 8,
                Articoloconmagazzinolottiscadenze: 9,
                Articoloconmagazzinoseriali: 100000001,
                Articoloinmagazzino: 7,
                Servizio: 100000002
            },

            /// Values for field Origine
            res_origincodeValues: {
                Dynamics: 100000000,
                ERP: 100000001
            }
        }
    };
    //---------------------------------------------------
    _self.disabilitaCampiERP = executionContext => {
        const formContext = executionContext.getFormContext();
        const originCodeControl = formContext.getControl("header_res_origincode");

        const origine = originCodeControl ? originCodeControl.getAttribute().getValue() : null;
        const ERP = _self.formModel.options.res_origincodeValues.ERP;

        if (origine && origine == ERP) {

            //disabilito tutti i campi
            Object.values(_self.formModel.fields).forEach(field => {
                const control = formContext.getControl(_self.formModel.fields[field]);
                if (control) { control.setDisabled(true); }
            });
        }
    }
    //---------------------------------------------------
    _self.onChangeFamigliaAssociata = async executionContext => {
        const formContext = executionContext.getFormContext();

        const famigliaAssociataAttribute = formContext.getAttribute(_self.formModel.fields.parentproductid);
        const categoriaPrincipaleAttribute = formContext.getAttribute(_self.formModel.fields.res_parentcategoryid);

        const famigliaAssociata = famigliaAssociataAttribute.getValue();

        if (!famigliaAssociata) {
            categoriaPrincipaleAttribute.setValue(null);
        }
        else {
            //id del parent
            const famigliaAssociataId = famigliaAssociata[0].id

            //retrieve del parent & $expand = parent del parent
            const referenceFamigliaAssociata = await Xrm.WebApi.retrieveRecord("product", famigliaAssociataId, "?$select=name&$expand=parentproductid($select=productid,name)");

            //reference del parent del parent
            const parentReference = referenceFamigliaAssociata["parentproductid"] ?? null;

            //se la categoria ha una categoria padre, valorizzo il campo lookup
            if (parentReference) {
                const lookup = [{
                    id: parentReference["productid"],
                    name: parentReference["name"],
                    entityType: "product"
                }];
                categoriaPrincipaleAttribute.setValue(lookup);
            }
        }
    };
    //---------------------------------------------------
    _self.filtroFamigliaAssociata = executionContext => {
        const formContext = executionContext.getFormContext();
        const famigliaAssociataControl = formContext.getControl(_self.formModel.fields.parentproductid);

        if (!famigliaAssociataControl) { console.error(`Campo Famiglia associata non trovato`); return; }

        var fetchData = {
            "statecode": _self.formModel.fields.statecodeValues.Attivo,
            "Famigliadiprodotti": _self.formModel.fields.productstructureValues.Famigliadiprodotti,
            "Aggregazioneprodotti": _self.formModel.fields.productstructureValues.Aggregazioneprodotti
        };
        var filtro = [
            "    <filter>",
            "      <condition attribute='statecode' operator='eq' value='", fetchData.statecode/*0*/, "'/>",
            "      <condition attribute='productstructure' operator='in'>",
            "        <value>", fetchData.Famigliadiprodotti/*2*/, "</value>",
            "        <value>", fetchData.Aggregazioneprodotti/*3*/, "</value>",
            "      </condition>",
            "    </filter>"
        ].join("");

        famigliaAssociataControl.addCustomFilter(filtro, "product");
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
        formContext.data.entity.addOnSave(_self.onSaveForm);
        formContext.getAttribute(_self.formModel.fields.parentproductid).addOnChange(_self.onChangeFamigliaAssociata);

        //Init function
        _self.disabilitaCampiERP(executionContext);
        formContext.getControl(_self.formModel.fields.parentproductid).addPreSearch(_self.filtroFamigliaAssociata);

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
).call(RSMNG.TAUMEDIKA.PRODUCT);