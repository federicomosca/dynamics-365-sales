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
    _self.isCustomerAddress = formContext => {

        const isCustomerAddressAttribute = formContext.getAttribute(_self.formModel.fields.res_iscustomeraddress);
        const isCustomerAddress = isCustomerAddressAttribute ? isCustomerAddressAttribute.getValue() : null;

        if (isCustomerAddress) {
            Object.values(_self.formModel.fields).forEach(field => {
                const control = formContext.getControl(_self.formModel.fields[field]);

                if (control) {
                    if (field === "res_isdefault") { control.setDisabled(false); }
                    else { control.setDisabled(true); }
                }
            });
        }
    };
    //---------------------------------------------------
    _self.hasPostalCode = formContext => {

        const postalCodeAttribute = formContext.getAttribute(_self.formModel.fields.res_postalcode);
        const hasPostalCode = postalCodeAttribute ? postalCodeAttribute.getValue() : null;

        const cityControl = formContext.getControl(_self.formModel.fields.res_city);

        if (cityControl) { cityControl.setDisabled(!hasPostalCode); }
    };
    //---------------------------------------------------
    _self.hasCity = formContext => {

        const cityAttribute = formContext.getAttribute(_self.formModel.fields.res_city);
        const city = cityAttribute ? cityAttribute.getValue() : null;

        const fieldsToEnable = [
            _self.formModel.fields.res_location,
            _self.formModel.fields.res_province,
            _self.formModel.fields.res_countryid
        ];

        fieldsToEnable.forEach(field => {
            const control = formContext.getControl(field);
            if (control) {
                if (city) { control.setDisabled(false); } else { control.setDisabled(true); }
            }
        });
    };
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
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        //Init function
        _self.hasPostalCode(formContext);
        _self.hasCity(formContext);
        _self.isCustomerAddress(formContext);

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