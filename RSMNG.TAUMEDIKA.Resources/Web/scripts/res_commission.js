//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.RES_COMMISSION) == "undefined") {
    RSMNG.TAUMEDIKA.RES_COMMISSION = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            ///Provvigione constants.
            logicalName: "res_commission",
            displayName: "Provvigione"
        },
        fields: {
            ///Dettaglio calcolo provvigioni
            res_commissioncalculationdetail: "res_commissioncalculationdetail",
            ///Provvigione
            res_commissionid: "res_commissionid",
            ///Data fine
            res_enddate: "res_enddate",
            ///Nome
            res_name: "res_name",
            ///Data inizio
            res_startdate: "res_startdate",
            ///Stato
            statecode: "statecode",
            ///Motivo stato
            statuscode: "statuscode",

            /// Values for field Stato
            statecodeValues: {
                Attivo: 0,
                Inattivo: 1
            },

            /// Values for field Motivo stato
            statuscodeValues: {
                Bozza_StateAttivo: 1,
                Calcolata_StateAttivo: 100000002,
                Calcolataerrori_StateAttivo: 100000003,
                Calcoloincorso_StateAttivo: 100000001,
                Inattivo_StateInattivo: 2
            }
        },
        tabs: {

        },
        sections: {

        }
    };
    //---------------------------------------------------
    _self.initFields = function (executionContext) {
        var formContext = executionContext.getFormContext();

        let statuscode = formContext.getAttribute(_self.formModel.fields.statuscode).getValue();
        formContext.getControl(_self.formModel.fields.res_commissioncalculationdetail).setVisible(statuscode == _self.formModel.fields.statuscodeValues.Calcolataerrori_StateAttivo);
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
    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();
    };

    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        const formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);
        formContext.getAttribute(_self.formModel.fields.res_startdate).addOnChange(() => { RSMNG.TAUMEDIKA.GLOBAL.checkDates(executionContext, _self.formModel.fields.res_startdate, _self.formModel.fields.res_enddate,"La data di inizio deve essere inferiore alla data di fine.","01"); });
        formContext.getAttribute(_self.formModel.fields.res_enddate).addOnChange(() => { RSMNG.TAUMEDIKA.GLOBAL.checkDates(executionContext, _self.formModel.fields.res_startdate, _self.formModel.fields.res_enddate, "La data di inizio deve essere inferiore alla data di fine.", "01"); });

        //Init function
        _self.initFields(executionContext);

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
).call(RSMNG.TAUMEDIKA.RES_COMMISSION);