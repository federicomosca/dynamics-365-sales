//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRICELEVEL) == "undefined") {
    RSMNG.TAUMEDIKA.PRICELEVEL = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///costanti entità
            logicalName: "pricelevel", // esempio logical Name
            displayName: "Listino Prezzi", // esempio display Name
        },
        fields: {
            ///Data di inizio
            begindate: "begindate",
            ///Descrizione
            description: "description",
            ///Data di fine
            enddate: "enddate",
            ///Tasso di cambio
            exchangerate: "exchangerate",
            ///Condizioni di spedizione
            freighttermscode: "freighttermscode",
            ///Nome
            name: "name",
            ///Organization Id
            organizationid: "organizationid",
            ///Modalità di pagamento 
            paymentmethodcode: "paymentmethodcode",
            ///Listino prezzi
            pricelevelid: "pricelevelid",
            ///Default per agenti
            res_isdefaultforagents: "res_isdefaultforagents",
            ///Default per sito web
            res_isdefaultforwebsite: "res_isdefaultforwebsite",
            ///Import ERP
            res_iserpimport: "res_iserpimport",
            ///Ambito
            res_scopetypecodes: "res_scopetypecodes",
            ///Metodo di spedizione
            shippingmethodcode: "shippingmethodcode",
            ///Valuta
            transactioncurrencyid: "transactioncurrencyid",

            /// Values for field Stato 
            statecodeValues: {
                Attivo: 0,
                Inattivo: 1
            },

            /// Values for field Motivo stato
            statuscodeValues: {
                Attivo_StateAttivo: 100001,
                Inattivo_StateInattivo: 100002
            },

            /// Values for field Condizioni di spedizione
            freighttermscodeValues: {
                Valorepredefinito: 1
            },

            /// Values for field Modalità di pagamento 
            paymentmethodcodeValues: {
                Valorepredefinito: 1
            },

            /// Values for field Default per agenti
            res_isdefaultforagentsValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Default per sito web
            res_isdefaultforwebsiteValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Import ERP
            res_iserpimportValues: {
                No: 0,
                Si: 1
            },

            /// Values for field Ambito
            res_scopetypecodesValues: {
                Agenti: 100000000,
                SitoWeb: 100000001
            },

            /// Values for field Metodo di spedizione
            shippingmethodcodeValues: {
                Valorepredefinito: 1
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


    _self.onChangeScopeTypeCodes = function (executionContext) {
        var formContext = executionContext.getFormContext();

        let scopeCodes = formContext.getAttribute(_self.formModel.fields.res_scopetypecodes).getValue();
     
        if (scopeCodes !== null && scopeCodes.includes(_self.formModel.fields.res_scopetypecodesValues.Agenti)) {

            formContext.getControl(_self.formModel.fields.res_isdefaultforagents).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_isdefaultforagents).setDisabled(false);
        }
        else {
            formContext.getControl(_self.formModel.fields.res_isdefaultforagents).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_isdefaultforagents).setDisabled(true);
            formContext.getAttribute(_self.formModel.fields.res_isdefaultforagents).setValue(false);
        }

        if (scopeCodes !== null && scopeCodes.includes(_self.formModel.fields.res_scopetypecodesValues.SitoWeb)) {

            formContext.getControl(_self.formModel.fields.res_isdefaultforwebsite).setVisible(true);
            formContext.getControl(_self.formModel.fields.res_isdefaultforwebsite).setDisabled(false);

        } else {
            formContext.getControl(_self.formModel.fields.res_isdefaultforwebsite).setVisible(false);
            formContext.getControl(_self.formModel.fields.res_isdefaultforwebsite).setDisabled(true);
            formContext.getAttribute(_self.formModel.fields.res_isdefaultforwebsite).setValue(false);
        }
        
    };
    //----------------------------------------------------
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
    /* 
    Utilizzare la keyword async se si utilizza uno o più metodi await dentro la funzione l'onLoadForm
    per rendere l'onload asincrono asincrono (da attivare sull'app dynamics!)
    Ricordare di aggiungere la keyword anche ai metodi richiamati dall'onLoadForm se l'await avviene dentro di essi
    */
    _self.onLoadForm = async function (executionContext) {

        //init lib
        //await import('../res_lib/RSMNG.CORE.js');
        await import('../res_scripts/res_global.js');

        //init formContext
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        formContext.getAttribute(_self.formModel.fields.res_scopetypecodes).addOnChange(_self.onChangeScopeTypeCodes);

        //Init function
        _self.onChangeScopeTypeCodes(executionContext);

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
).call(RSMNG.TAUMEDIKA.PRICELEVEL);