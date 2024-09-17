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
    _self.parseDataValue = function (datavalue) {
        var ret = null;
        if (datavalue != "") {
            var vals = new Array();
            vals = decodeURIComponent(datavalue).split("&");
            for (var i in vals) {
                vals[i] = vals[i].replace(/\+/g, " ").split("=");
            }
            ret = vals;

        }
        return ret;
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
    _self.retrieveAllRecords = async function (entityName, topCount, queryOptions = "") {
        let allRecords = [];
        let fetchMore = true;
        let fetchXmlPagingCookie = null;

        while (fetchMore) {
            let response;
            try {
                // Costruisci l'URL di richiesta con il cookie di paginazione se presente
                let fetchXml = `<fetch mapping="logical" count="${topCount}" page="${allRecords.length / topCount + 1}">`;
                if (fetchXmlPagingCookie) {
                    fetchXml += `<cookie>${fetchXmlPagingCookie}</cookie>`;
                }
                fetchXml += queryOptions;
                fetchXml += `</fetch>`;
                response = await Xrm.WebApi.retrieveMultipleRecords(entityName, `?fetchXml=${encodeURIComponent(fetchXml)}`);
            } catch (error) {
                console.error("Error retrieving records:", error);
                fetchMore = false;
                continue;
            }

            // Aggiungi i record recuperati alla lista completa
            allRecords = allRecords.concat(response.entities);

            // Controlla se ci sono più pagine da recuperare
            fetchMore = response["fetchXmlPagingCookie"] ? true : false;
            if (fetchMore) {
                // Estrai il cookie di paginazione per la richiesta successiva
                fetchXmlPagingCookie = response["fetchXmlPagingCookie"];
            }
        }

        return allRecords;
    };
}).call(RSMNG.TAUMEDIKA.GLOBAL);