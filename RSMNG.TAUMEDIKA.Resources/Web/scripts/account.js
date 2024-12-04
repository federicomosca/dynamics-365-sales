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
            ///SDI
            res_sdi: "res_sdi",
            ///Partita IVA
            res_vatnumber: "res_vatnumber",
            ///Indirizzo
            address1_line1: "address1_line1",
            ///CAP
            address1_postalcode: "address1_postalcode",
            ///Città
            address1_city: "address1_city",
            ///Località
            res_location: "res_location",
            ///Provincia
            address1_stateorprovince: "address1_stateorprovince",
            ///Nazione
            res_countryid: "res_countryid",
            ///Nazione (Testo)
            address1_country: "address1_country",
            //Telefono 
            telephone1: "telephone1",
            //Cellulare
            res_mobilenumber: "res_mobilenumber",

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
        let res_vatnumber = formContext.data.entity.attributes.get(_self.formModel.fields.res_vatnumber);

        if (res_vatnumber != null) {
            let res_vatnumber_value = res_vatnumber.getValue();
            formContext.getControl(_self.formModel.fields.res_vatnumber).clearNotification("01");
            if (res_vatnumber_value != null && res_vatnumber_value.length != 11) {
                formContext.getControl(_self.formModel.fields.res_vatnumber).addNotification({
                    messages: ["La Partita IVA non può essere maggiore di 11 caratteri."],
                    notificationLevel: "RECOMMENDATION",
                    uniqueId: "01",
                    actions: null
                });
            } else {
                Xrm.WebApi.retrieveMultipleRecords(_self.formModel.entity.logicalName, `?$filter=(${_self.formModel.fields.res_vatnumber} eq '${res_vatnumber_value}' and accountid ne ${formContext.data.entity.getId()})`).then(
                    function success(results) {
                        console.log(results);
                        if (results.entities.length > 0) {
                            formContext.getControl(_self.formModel.fields.res_vatnumber).addNotification({
                                messages: [`Ci sono n° ${results.entities.length} con la stessa la Partita IVA '${res_vatnumber_value}'.`],
                                notificationLevel: "RECOMMENDATION",
                                uniqueId: "01",
                                actions: null
                            });
                        }
                    },
                    function (error) {
                        console.log(error.message);
                    }
                );
            }
        }
    };
    //---------------------------------------------------
    _self.onChangeAddress = function (executionContext) {
        //Controllo i campi obbligatori
        let formContext = executionContext.getFormContext();
        let address1_line1 = formContext.data.entity.attributes.get(_self.formModel.fields.address1_line1);
        let address1_postalcode = formContext.data.entity.attributes.get(_self.formModel.fields.address1_postalcode);
        let address1_city = formContext.data.entity.attributes.get(_self.formModel.fields.address1_city);
        let address1_city_obj = formContext.ui.controls.get(_self.formModel.fields.address1_city);
        let res_location_obj = formContext.ui.controls.get(_self.formModel.fields.res_location);
        let address1_stateorprovince_obj = formContext.ui.controls.get(_self.formModel.fields.address1_stateorprovince);
        let address1_country_obj = formContext.ui.controls.get(_self.formModel.fields.address1_country);
        let res_countryid_obj = formContext.ui.controls.get(_self.formModel.fields.res_countryid);

        address1_line1.setRequiredLevel(address1_city.getValue() != null || address1_postalcode.getValue() != null ? "required" : "none");
        address1_postalcode.setRequiredLevel(address1_line1.getValue() != null ? "required" : "none");
        address1_city_obj.setDisabled(address1_postalcode.getValue() != null ? false : true);
        address1_city.setRequiredLevel(address1_postalcode.getValue() != null ? "required" : "none");
        res_location_obj.setDisabled(address1_city.getValue() != null ? false : true);
        address1_stateorprovince_obj.setDisabled(address1_city.getValue() != null ? false : true);
        address1_country_obj.setDisabled(true);
        address1_country_obj.setVisible(false);
        res_countryid_obj.setDisabled(address1_city.getValue() != null ? false : true);

    };
    //---------------------------------------------------
    _self.onChangeCountry = function (executionContext) {
        let formContext = executionContext.getFormContext();
        let res_countryid = formContext.data.entity.attributes.get(_self.formModel.fields.res_countryid);
        let address1_country = formContext.data.entity.attributes.get(_self.formModel.fields.address1_country);

        address1_country.setValue(res_countryid.getValue() != null ? res_countryid.getValue()[0].name : null);
    };
    //---------------------------------------------------
    _self.checkCodiceFiscale = function (executionContext) {
        const formContext = executionContext.getFormContext();

        //clear della notifica
        const campoCodiceFiscale = formContext.getControl(_self.formModel.fields.res_taxcode);
        campoCodiceFiscale.clearNotification('CODICE_FISCALE_NOT_16');

        //recupero il codice fiscale e controllo la lunghezza
        const inputCodiceFiscale = campoCodiceFiscale.getAttribute().getValue();
        const isCodiceFiscale16 = inputCodiceFiscale ? inputCodiceFiscale.length === 16 : null;

        //verifico che sia stato compilato altrimenti non proseguo
        if (isCodiceFiscale16 === null) return;

        //se non è esattamente 16 caratteri, notifico
        if (!isCodiceFiscale16) {

            const notification = {
                messages: ['Il codice fiscale inserito non è composto da 16 caratteri.'],
                notificationLevel: 'RECOMMENDATION',
                uniqueId: 'CODICE_FISCALE_NOT_16'
            }

            campoCodiceFiscale.addNotification(notification);
        }
    }
    //---------------------------------------------------
    _self.checkSDI = function (executionContext) {
        const formContext = executionContext.getFormContext();

        //clear della notifica
        const campoSDI = formContext.getControl(_self.formModel.fields.res_sdi);
        campoSDI.clearNotification('SDI_NOT_7');

        //recupero il codice SDI e controllo la lunghezza
        const inputSDI = campoSDI.getAttribute().getValue();
        const isSdi7 = inputSDI ? inputSDI.length === 7 : null;

        //verifico che sia stato compilato altrimenti non proseguo
        if (isSdi7 === null) return;

        //se non è esattamente 7 caratteri, notifico
        if (!isSdi7) {

            const notification = {
                messages: ['Il codice SDI inserito non è composto da 7 caratteri.'],
                notificationLevel: 'RECOMMENDATION',
                uniqueId: 'SDI_NOT_7'
            }

            campoSDI.addNotification(notification);
        }
    }
    //---------------------------------------------------
    _self.gestioneObbligatorietàCampiTelefono = function (executionContext) {
        const formContext = executionContext.getFormContext();

        const campoTelefono = formContext.getControl(_self.formModel.fields.telephone1);
        const campoCellulare = formContext.getControl(_self.formModel.fields.res_mobilenumber);

        const hasValueTelefono = campoTelefono.getAttribute().getValue() != null;
        const hasValueCellulare = campoCellulare.getAttribute().getValue() != null;

        campoCellulare.getAttribute().setRequiredLevel(hasValueTelefono ? "none" : "required");
        campoTelefono.getAttribute().setRequiredLevel(hasValueCellulare ? "none" : "required");
    };
    //---------------------------------------------------
    _self.setContextCapIframe = function (executionContext) {
        let formContext = executionContext.getFormContext();
        var wrControl = formContext.getControl("WebResource_postalcode");

        var fields = {
            cap: _self.formModel.fields.address1_postalcode,
            city: _self.formModel.fields.address1_city,
            province: _self.formModel.fields.address1_stateorprovince,
            nation: _self.formModel.fields.address1_country,
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
        } else {
            formContext.getControl(_self.formModel.fields.res_accountnaturecode).setDisabled(false);
        }
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
        formContext.getControl("WebResource_postalcode").setVisible(false);
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
        formContext.getAttribute(_self.formModel.fields.address1_line1).addOnChange(_self.onChangeAddress);
        formContext.getAttribute(_self.formModel.fields.address1_postalcode).addOnChange(_self.onChangeAddress);
        formContext.getAttribute(_self.formModel.fields.address1_city).addOnChange(_self.onChangeAddress);
        formContext.getAttribute(_self.formModel.fields.res_countryid).addOnChange(_self.onChangeCountry);
        formContext.getAttribute(_self.formModel.fields.res_taxcode).addOnChange(_self.checkCodiceFiscale);
        formContext.getAttribute(_self.formModel.fields.res_sdi).addOnChange(_self.checkSDI);
        formContext.getAttribute(_self.formModel.fields.telephone1).addOnChange(_self.gestioneObbligatorietàCampiTelefono);
        formContext.getAttribute(_self.formModel.fields.res_mobilenumber).addOnChange(_self.gestioneObbligatorietàCampiTelefono);

        //Init function
        _self.onChangeVatNumber(executionContext);
        _self.onChangeAddress(executionContext);
        _self.checkCodiceFiscale(executionContext);
        _self.checkSDI(executionContext);
        _self.gestioneObbligatorietàCampiTelefono(executionContext);

        //Init IFrame
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
    }
}
).call(RSMNG.TAUMEDIKA.ACCOUNT);