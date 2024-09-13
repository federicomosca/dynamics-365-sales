//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

RSMNG.Global = {

    CRM_FORM_TYPE_CREATE: 1,
    CRM_FORM_TYPE_UPDATE: 2,
    CRM_FORM_TYPE_READONLY: 3,
    CRM_FORM_TYPE_DISABLED: 4,
    CRM_FORM_TYPE_QUICKCREATE: 5,
    CRM_FORM_TYPE_BULKEDIT: 6,

};

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.GLOBAL) == "undefined") {
    RSMNG.TAUMEDIKA.GLOBAL = {};
}

(async function () {

    /*
    file global.js utilizzato per definire variabili globali al PROGETTO
    */
    var _self = this;

    /*
    esempio di definizione di un option set
    */
    _self.STATE = {
        ACTIVE: 0,
        INACTIVE: 1
    };

    /*
    Funzione per la visualizzazione a video dell'errore imprevisto
    */
    _self.errorMessage = function (ex) {

        var errorOptions = {
            errorCode: ex.errorCode,
            message: ex.message,
            details: ex.raw
        };

        Xrm.Navigation.openErrorDialog(errorOptions);
    };


}).call(RSMNG.TAUMEDIKA.GLOBAL);