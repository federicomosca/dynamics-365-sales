
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

    //--------------------------------------------------
    _self.UPDATESTATUS = {
        canExecute: async function (formContext) {
            return true;
        },
        execute: async function (formContext) {

            await import('../res_scripts/res_global.js');

            /**
             * recupero l'id del record su cui sto operando il cambio di stato
             * recupero l'attributo statuscode
             * in base allo statuscode attuale (switch) effettuo un update 
             * dallo status attuale a quello successivo tramite cloud flow
             */
            var quoteId = Xrm.Page.data.entity.getId();
            var currentStatus = Xrm.Page.getAttribute("statuscode").getValue();

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

(function () {

    var _self = this;

    //--------------------------------------------------
    _self.UPDATESTATUS = {
        canExecute: async function (formContext) {

            // abilito bottone solo se è selezionato un record

            return true;
        },
        execute: async function (formContext, SelectedControlSelectedItemIds) {

            await import('../res_scripts/res_global.js');

            /**
             * recupero l'id del record su cui sto operando il cambio di stato
             * recupero l'attributo statuscode
             * in base allo statuscode attuale (switch) effettuo un update 
             * dallo status attuale a quello successivo tramite cloud flow
             */
            var quoteId = Xrm.Page.data.entity.getId();
            var currentStatus = Xrm.Page.getAttribute("statuscode").getValue();

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
}).call(RSMNG.TAUMEDIKA.QUOTE.RIBBON.HOME);



/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

