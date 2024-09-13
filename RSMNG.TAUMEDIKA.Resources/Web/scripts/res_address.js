//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.RES_ADDRESS) == "undefined") {
    RSMNG.TAUMEDIKA.RES_ADDRESS = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///costanti entità
            logicalName: "res_address",
            displayName: "Indirizzo"
        },
        fields: {
            ///Indirizzo
            res_address: "res_address",
            ///Indirizzo
            res_addressid: "res_addressid",
            ///Città
            res_city: "res_city",
            ///Nazione
            res_countryid: "res_countryid",
            ///Cliente
            res_customerid: "res_customerid",
            ///Indirizzo scheda cliente
            res_iscustomeraddress: "res_iscustomeraddress",
            ///Default
            res_isdefault: "res_isdefault",
            ///Località
            res_location: "res_location",
            ///Nome
            res_name: "res_name",
            ///CAP
            res_postalcode: "res_postalcode",
            ///Provincia
            res_province: "res_province"
        },
        tabs: {

        },
        sections: {

        }
    };

    /*
    Esempio di stringa interpolata
    */
    _self._interpolateString = `${null} example`;

    /*
    Esempio di fetch lato client 
        - solo dimostrativo per far vedere la modalità di sviluppo ed esecuzione della fetch
    */
    _self.getName = async function (accountId) {

        return new Promise(function (resolve, reject) {

            var fetchXML = `?fetchXml=<fetch distinct='false' mapping='logical'>
			<entity name='account'>
			<attribute name='name'/>
			<filter>
			<condition attribute='account' operator='eq' value='${accountId}'/>
			</filter>
			</entity>
			</fetch>`;

            Xrm.WebApi.retrieveMultipleRecords("account", fetchXML).then(
                function success(result) {

                    resolve(result.entities.length == 1 && result.entities[0].name != undefined && result.entities[0].name != null ? result.entities[0].name : null);

                },
                function (error) {

                    reject(error);

                }
            );
        });
    };

    /*
    Esempio di chiamata della funzione asincrona
    */
    _self.onChange = async function (executionContext) {

        try {
            let name = await _self.getName(formContext.data.entity.getId());

        } catch (ex) {
            RSMNG.PROGETTO.GLOBAL.errorMessage(ex);
        }

    };

    /*
    Esempio di una funzione di filtro di una LookUp
    */
    _self.addFilter = function (executionContext) {

        var formContext = executionContext.getFormContext();

        var status = "0";

        var filterXml = '';
        filterXml = `<filter><condition attribute='statuscode' operator='eq' value='${status}'/></filter>`;
        formContext.getControl("res_accountid").addCustomFilter(filterXml, 'account');

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
    /* 
    Utilizzare la keyword async se si utilizza uno o più metodi await dentro la funzione l'onLoadForm
    per rendere l'onload asincrono asincrono (da attivare sull'app dynamics!)
    Ricordare di aggiungere la keyword anche ai metodi richiamati dall'onLoadForm se l'await avviene dentro di essi
    */
    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_lib/RSMNG.CORE.js');
        await import('../res_scripts/res_global.js');

        //init formContext
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);
        console.log("i'm here")

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
).call(RSMNG.TAUMEDIKA.RES_ADDRESS);