
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTE) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTE = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTE.RIBBON) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTE.RIBBON = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTE.RIBBON.FORM) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTE.RIBBON.FORM = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTE.RIBBON.SUBGRID) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTE.RIBBON.SUBGRID = {};
}

if (typeof (RSMNG.TAUMEDIKA.QUOTE.RIBBON.HOME) == "undefined") {
    RSMNG.TAUMEDIKA.QUOTE.RIBBON.HOME = {};
}

(function () {

    var _self = this;

    _self.STATUS = {
        BOZZA: 1,
        IN_APPROVAZIONE: 2,
        APPROVATA: 3,
        ACQUISITA: 4,
        NON_APPROVATA: 5,
        PERSA: 6,
        AGGIORNATA: 7
    }

    _self.Agent = undefined;

    _self.getAgent = function () {
        return new Promise(function (resolve, reject) {
            Xrm.WebApi.retrieveRecord("systemuser", Xrm.Utility.getGlobalContext().userSettings.userId, "?$select=res_isagente").then(
                function success(result) {
                    console.log(result);
                    // Columns
                    resolve(result["res_isagente"]); // Boolean
                },
                function (error) {
                    reject(null);
                    console.log(error.message);
                }
            );
        });
    };

    //--------------------------------------------------
    _self.UPDATESTATUS = {
        canExecute: async function (formContext, status) {
            let currentStatus = formContext.getAttribute("statuscode").getValue();
            let visible = false;
            if (_self.Agent === undefined) {
                _self.Agent = await _self.getAgent();
            }
            switch (status) {
                //in approvazione
                case "APPROVAL":
                    if (currentStatus === _self.STATUS.BOZZA) {
                        if (_self.Agent === 1) { visible = true; }
                    }
                    break;
                //approvata
                case "APPROVED":
                    if (currentStatus === _self.STATUS.BOZZA ||
                        (currentStatus === _self.STATUS.IN_APPROVAZIONE && _self.Agent === 0)) {
                        visible = true;
                    }
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE) {
                        visible = true;
                    }
                    break;
                //non approvata
                case "NOT_APPROVED":
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE && _self.Agent === 0) {
                        visible = true;
                    }
                    break;
                //chiudi offerta (persa)
                case "CLOSE_QUOTE":
                    if (currentStatus === _self.STATUS.APPROVATA) {
                        visible = true;
                    }
                    break;
                //acquisisci offerta (acquisita)
                case "CREATE_ORDER":
                    if (currentStatus === _self.STATUS.APPROVATA) {
                        visible = true;
                    }
                    break;
                //aggiorna
                case "REVISE":
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE ||
                        currentStatus === _self.STATUS.APPROVATA) {
                        visible = true;
                    }
                    break;
                //
                case "ACTIVATE_QUOTE":

                    break;
            }
            return visible;
        },
        execute: async function (formContext, status) {

            await import('../res_scripts/res_global.js');

            /**
             * recupero l'id del record su cui sto operando il cambio di stato
             * recupero l'attributo statuscode
             * in base allo statuscode attuale (switch) effettuo un update 
             * dallo status attuale a quello successivo tramite cloud flow
             */
            var quoteId = formContext.data.entity.getId();
            var currentStatus = formContext.getAttribute("statuscode").getValue();

            console.log(`Statuscode attuale: ${currentStatus}`);

            switch (currentStatus) {
                case _self.STATUS.BOZZA:
                    break;
                case _self.STATUS.IN_APPROVAZIONE:
                    break;
                case _self.STATUS.APPROVATA:
                    break;
                case _self.STATUS.ACQUISITA:
                    break;
                case _self.STATUS.NON_APPROVATA:
                    break;
                case _self.STATUS.PERSA:
                    break;
                case _self.STATUS.AGGIORNATA:
                    break;
            }
        }
    };
}).call(RSMNG.TAUMEDIKA.QUOTE.RIBBON.FORM);

/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

