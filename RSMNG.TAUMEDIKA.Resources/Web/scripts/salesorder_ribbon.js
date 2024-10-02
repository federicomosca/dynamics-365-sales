
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDER) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDER = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDER.RIBBON) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDER.RIBBON = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDER.RIBBON.FORM) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDER.RIBBON.FORM = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDER.RIBBON.SUBGRID) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDER.RIBBON.SUBGRID = {};
}

if (typeof (RSMNG.TAUMEDIKA.SALESORDER.RIBBON.HOME) == "undefined") {
    RSMNG.TAUMEDIKA.SALESORDER.RIBBON.HOME = {};
}

(function () {

    var _self = this;


    _self.statuscodeValues = {
        Annullato_StateAnnullato: 4,
        Approvato_StateAttivo: 100005,
        Bozza_StateAttivo: 1,
        Completato_StateEvaso: 100001,
        Fatturato_StateFatturato: 100003,
        Inapprovazione_StateAttivo: 2,
        Incorso_StateInviato: 3,
        Inlavorazione_StateAttivo: 100006,
        Nonapprovato_StateAnnullato: 100004,
        Parziale_StateEvaso: 100002,
        Spedito_StateAttivo: 100007
    }


    _self.Agent = undefined;

    _self.getAgent = function () {
        return new Promise(function (resolve, reject) {
            Xrm.WebApi.retrieveRecord("systemuser", Xrm.Utility.getGlobalContext().userSettings.userId, "?$select=res_isagente").then(
                function success(result) {

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

    //--------------------------------------------------
    _self.UPDATESTATUS = {
        canExecute: async function (formContext, status) {
            let currentStatus = formContext.getAttribute("statuscode").getValue();
            console.log(formContext);
            console.log(`Current Status: ${currentStatus}`);
            console.log(status);
            let isVisible = true;

            if (_self.Agent === undefined) {
                _self.Agent = await _self.getAgent();
            }
            //switch (status) {

            //    case "APPROVAL": //in approvazione
            //        if (currentStatus === _self.STATUS.BOZZA && _self.Agent === true) { visible = true; } break;

            //    case "APPROVED": //approvata
            //        if (currentStatus === _self.STATUS.BOZZA && (_self.Agent === false || _self.Agent === null)) { visible = true; }
            //        if (currentStatus === _self.STATUS.IN_APPROVAZIONE && (_self.Agent === false || _self.Agent === null)) { visible = true; } break;

            //    case "NOT_APPROVED": //non approvata
            //        if (currentStatus === _self.STATUS.IN_APPROVAZIONE && (_self.Agent === false || _self.Agent === null)) { visible = true; } break;

            //    case "CREATE_ORDER": //acquisisci offerta (acquisita)
            //        //da qui invocare il metodo getCustomer() per effettuare verifica sui dati
            //        //e determinare la visiblità del button
            //        if (currentStatus === _self.STATUS.APPROVATA) { visible = true; } break;

            //    case "CLOSE_QUOTE": //chiudi offerta (persa)
            //        if (currentStatus === _self.STATUS.APPROVATA) { visible = true; } break;

            //    case "REVISE": //aggiorna
            //        if (currentStatus === _self.STATUS.IN_APPROVAZIONE) { visible = true; }
            //        if (currentStatus === _self.STATUS.APPROVATA) { visible = true; } break;
            //}
            return isVisible;
        },
        execute: async function (formContext, status) {

            await import('../res_scripts/res_global.js');

            const salesOrderId = formContext.data.entity.getId().replace(/[{}]/g, "");
            console.log(`SalesOrder ID: ${salesOrderId}`);
            console.log(status);
            const salesOrderStatus = formContext.getAttribute("statuscode").getValue();

            //switch (status) {
            //    case "APPROVAL":
            //        debugger;
            //        console.log(`case: ${status}`)
            //        console.log(`statuscode: ${quoteStatus}`);

            //        Sales.QuoteRibbonActions.Instance.activateQuote();

            //        break;

            //    case "APPROVED":
            //        console.log(`case: ${status}`)
            //        console.log(`statuscode: ${quoteStatus}`);

            //        const actionName = "UPDATE_QUOTE_STATUS";

            //        RSMNG.TAUMEDIKA.GLOBAL.invokeClientAction(quoteId, status, actionName)
            //            .then(result => {
            //                console.log("Action executed successfully");
            //                // Qui puoi gestire il successo dell'azione, ad esempio aggiornando l'UI
            //            })
            //            .catch(error => {
            //                console.error("Error executing action");
            //                // Qui puoi gestire l'errore, ad esempio mostrando una notifica all'utente
            //            });
            //        break;

            //    case "NOT_APPROVED":
            //        console.log(`case: ${status}`)
            //        console.log(`statuscode: ${quoteStatus}`);

            //        //const actionName = "UPDATE_QUOTE_STATUS";

            //        //const success = RSMNG.TAUMEDIKA.GLOBAL.invokeClientAction(quoteId, status, actionName);
            //        //if (!success) {

            //        //formNotification
            //        //formContext.ui.setFormNotification("Impossibile aggiornare lo stato del record", "ERROR", "01");
            //        //}
            //        break;
            //}
        }
    };
}).call(RSMNG.TAUMEDIKA.SALESORDER.RIBBON.FORM);

/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

