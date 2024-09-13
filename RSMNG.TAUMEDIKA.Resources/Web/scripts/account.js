if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.ACCOUNT) == "undefined") {
    RSMNG.TAUMEDIKA.ACCOUNT = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///costanti entità
            logicalName: "account",
            displayName: "Account"
        },
        fields: {
            ///Tipologia
            res_accounttypecodes: "res_accounttypecodes",
            ///Natura giuridica
            res_accountnaturecode: "res_accountnaturecode",
            ///Codice fiscale
            res_taxcode: "res_taxcode",
            ///Partita IVA
            res_vatnumber: "res_vatnumber",

            /// Values for field Natura giuridica
            res_accountnaturecodeValues: {
                Personafisica: 100000000,
                Personagiuridica: 100000001
            },

            /// Values for field Tipologia
            res_accounttypecodesValues: {
                Cliente: 100000000,
                Fornitore: 100000001
            },
        },
        tabs: {

        },
        sections: {

        }
    };

    //---------------------------------------------------
    _self.onChangeVatNumber = function (executionContext) {

        let formContext = executionContext.getFormContext();

        if (formContext.getAttribute(_self.formModel.fields.res_vatnumber) != null) {
            let res_vatnumber = formContext.getAttribute(_self.formModel.fields.res_vatnumber).getValue();
            formContext.getControl(_self.formModel.fields.res_vatnumber).clearNotification("01");
            if (res_vatnumber != null && res_vatnumber.length() > 11) {
                let actionCollection = {
                    message: 'cancellare il campo'
                };
                actionCollection.actions = [function () {
                    formContext.getAttribute(_self.formModel.fields.res_vatnumber).setValue(null);
                }];
                formContext.getControl(_self.formModel.fields.res_vatnumber).addNotification({
                    messages: [message],
                    notificationLevel: "RECOMMENDATION",
                    uniqueId: "01",
                    actions: [actionCollection]
                });
            }
        }
    };
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

        if (formContext.getAttribute(_self.formModel.fields.res_accounttypecodes) != null) {
            formContext.getAttribute(_self.formModel.fields.res_accounttypecodes).setValue([_self.formModel.fields.res_accounttypecodesValues.Cliente]);
        }
    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();

        if (formContext.getAttribute(_self.formModel.fields.res_accountnaturecode) != null) {
            formContext.getControl(_self.formModel.fields.res_accountnaturecode).setDisabled(true);
        }
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
        formContext.getAttribute(_self.formModel.fields.res_vatnumber).addOnChange(_self.onChangeVatNumber);


        //Init function
        _self.onChangeVatNumber(executionContext);

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
).call(RSMNG.TAUMEDIKA.ACCOUNT);