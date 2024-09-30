
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
    /**
     * sviluppare qui la retrieve per recuperare i dati del potenziale cliente
     * invocare questa funzione nel caso "CREATE_ORDER" per determinare la visibilità del 
     */
    //_self.getCustomer = () => {

    //    const accountId = null;

    //    return new Promise((resolve, reject) => {
    //        Xrm.WebApi.retrieveRecord("account", accountId, "?$select=attributes").then(
    //            result => { }, error => { }
    //        );
    //    });
    //};
    //--------------------------------------------------
    _self.UPDATESTATUS = {
        canExecute: async function (formContext, status) {
            let currentStatus = formContext.getAttribute("statuscode").getValue();

            console.log(`Current Status: ${currentStatus}`);

            let visible = false;

            if (_self.Agent === undefined) {
                _self.Agent = await _self.getAgent();
            }
            switch (status) {

                case "APPROVAL": //in approvazione
                    if (currentStatus === _self.STATUS.BOZZA && _self.Agent === true) { visible = true; } break;

                case "APPROVED": //approvata
                    if (currentStatus === _self.STATUS.BOZZA && (_self.Agent === false || _self.Agent === null)) { visible = true; }
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE && (_self.Agent === false || _self.Agent === null)) { visible = true; } break;

                case "NOT_APPROVED": //non approvata
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE && (_self.Agent === false || _self.Agent === null)) { visible = true; } break;

                case "CREATE_ORDER": //acquisisci offerta (acquisita)
                    //da qui invocare il metodo getCustomer() per effettuare verifica sui dati
                    //e determinare la visiblità del button
                    if (currentStatus === _self.STATUS.APPROVATA) { visible = true; } break;

                case "CLOSE_QUOTE": //chiudi offerta (persa)
                    if (currentStatus === _self.STATUS.APPROVATA) { visible = true; } break;

                case "REVISE": //aggiorna
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE) { visible = true; }
                    if (currentStatus === _self.STATUS.APPROVATA) { visible = true; } break;
            }
            return visible;
        },
        execute: async function (formContext, status) {

            await import('../res_scripts/res_global.js');

            const quoteId = formContext.data.entity.getId().replace(/[{}]/g, "");
            console.log(`Quote ID: ${quoteId}`);

            const quoteStatus = formContext.getAttribute("statuscode").getValue();

            switch (status) {
                case "APPROVAL":
                    debugger;
                    console.log(`case: ${status}`)
                    console.log(`statuscode: ${quoteStatus}`);

                    Sales.QuoteRibbonActions.Instance.activateQuote();

                    break;

                case "APPROVED":
                    console.log(`case: ${status}`)
                    console.log(`statuscode: ${quoteStatus}`);

                    const actionName = "UPDATE_QUOTE_STATUS";

                    RSMNG.TAUMEDIKA.GLOBAL.invokeClientAction(quoteId, status, actionName)
                        .then(result => {
                            console.log("Action executed successfully:", result);
                            // Qui puoi gestire il successo dell'azione, ad esempio aggiornando l'UI
                        })
                        .catch(error => {
                            console.error("Error executing action:", error);
                            // Qui puoi gestire l'errore, ad esempio mostrando una notifica all'utente
                            formContext.ui.setFormNotification("Impossibile aggiornare lo stato del record", "ERROR", "01");
                        });
                    break;

                case "NOT_APPROVED":
                    console.log(`case: ${status}`)
                    console.log(`statuscode: ${quoteStatus}`);

                    //const actionName = "UPDATE_QUOTE_STATUS";

                    //const success = RSMNG.TAUMEDIKA.GLOBAL.invokeClientAction(quoteId, status, actionName);
                    //if (!success) {

                        //formNotification
                        //formContext.ui.setFormNotification("Impossibile aggiornare lo stato del record", "ERROR", "01");
                    //}
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

