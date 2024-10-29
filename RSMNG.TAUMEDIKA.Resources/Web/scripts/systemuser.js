//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.SYSTEMUSER) == "undefined") {
    RSMNG.TAUMEDIKA.SYSTEMUSER = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///Utente constants.
            logicalName: "systemuser",
            displayName: "Utente",
        },
        fields: {
            ///Codice agente
            res_agentnumber: "res_agentnumber",
            ///Agente
            res_isagente: "res_isagente",
            ///Disabilita calcolo provvigioni
            res_iscommissioncalculationdisabled: "res_iscommissioncalculationdisabled",
            // % Commissione
            res_commissionpercentage: "res_commissionpercentage"
        },
        tabs: {

        },
        sections: {

        }
    };
    //---------------------------------------------------
    _self.onChangeIsAgente = function (executionContext, isEvent) {
        var formContext = executionContext.getFormContext();

        let isAgente = formContext.getAttribute(_self.formModel.fields.res_isagente).getValue();

        formContext.getControl(_self.formModel.fields.res_agentnumber).setDisabled(isAgente == true ? false : true);
        formContext.getAttribute(_self.formModel.fields.res_agentnumber).setRequiredLevel(isAgente == true ? "required" : "none");

        formContext.getControl(_self.formModel.fields.res_iscommissioncalculationdisabled).setVisible(isAgente == true ? true : false);
        formContext.getControl(_self.formModel.fields.res_iscommissioncalculationdisabled).setDisabled(isAgente == true ? false : true);


        if (isEvent == true && isAgente == false) {
            formContext.getAttribute(_self.formModel.fields.res_agentnumber).setValue(null);
            formContext.getAttribute(_self.formModel.fields.res_iscommissioncalculationdisabled).setValue(false);

        }
    }
    //---------------------------------------------------
    _self.handleAgentRelatedFieldsProperties = executionContext => {
        const formContext = executionContext.getFormContext();

        const campoAgente = formContext.getControl(_self.formModel.fields.res_isagente);
        const campoCodiceAgente = formContext.getControl(_self.formModel.fields.res_agentnumber);
        const campoPercentualeCommissione = formContext.getControl(_self.formModel.fields.res_commissionpercentage);

        const isAgente = campoAgente.getAttribute().getValue() == 1;

        campoCodiceAgente.setVisible(isAgente);
        campoCodiceAgente.setDisabled(!isAgente);
        campoCodiceAgente.getAttribute().setRequiredLevel(isAgente ? "required" : "none");

        campoPercentualeCommissione.setVisible(isAgente);
        campoPercentualeCommissione.setDisabled(!isAgente);
        campoPercentualeCommissione.getAttribute().setRequiredLevel(isAgente ? "required" : "none");

        if (!isAgente) {
            campoCodiceAgente.getAttribute().setValue(null);
            campoPercentualeCommissione.getAttribute().setValue(null);
        }
    }
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

        var formContext = executionContext.getFormContext();


    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onLoadForm = async function (executionContext) {


        await import('../res_scripts/res_global.js');

        //init formContext
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.getAttribute(_self.formModel.fields.res_isagente).addOnChange(() => { _self.onChangeIsAgente(executionContext, true); });
        formContext.getAttribute(_self.formModel.fields.res_isagente).addOnChange(_self.handleAgentRelatedFieldsProperties);

        //Init function
        _self.onChangeIsAgente(executionContext, false);

        _self.handleAgentRelatedFieldsProperties(executionContext);

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
).call(RSMNG.TAUMEDIKA.SYSTEMUSER);