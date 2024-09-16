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
    _self.getDataParam = function (queryString) {
        var vals = new Array();
        var retParam = new Array();
        if (queryString != "") {
            vals = queryString.substr(1).split("&");
            for (var i in vals) {
                vals[i] = vals[i].replace(/\+/g, " ").split("=");
            }
            for (var i in vals) {
                if (vals[i][0].toLowerCase() == "data") {
                    retParam = _self.parseDataValue(vals[i][1]);
                    break;
                }
            }
        }
        return retParam;

    };


}).call(RSMNG.TAUMEDIKA.GLOBAL);