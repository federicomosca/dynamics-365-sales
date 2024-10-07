
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
    
    _self.STATUS = {
        Annullato: 4,
        Approvato: 100005,
        Bozza: 1,
        Completato: 100001,
        Fatturato: 100003,
        Inapprovazione: 2,
        Incorso: 3,
        Inlavorazione: 100006,
        Nonapprovato: 100004,
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
            let isVisible = false;

            if (_self.Agent === undefined) {
                _self.Agent = await _self.getAgent();
            }
            switch (status) {

                case "APPROVAL": //in approvazione 
                    if (formContext.ui.getFormType() != 1) {
                        if (currentStatus === _self.STATUS.Bozza && _self.Agent === true) {
                            isVisible = true;
                        }
                    }                    
                    break;

                case "APPROVED": //approvata
                    if (currentStatus === _self.STATUS.Bozza && (_self.Agent === false || _self.Agent === null)) { isVisible = true; }
                    if (currentStatus === _self.STATUS.Inapprovazione && (_self.Agent === false || _self.Agent === null)) { isVisible = true; } break;

                case "NOT_APPROVED": //non approvata
                    if (currentStatus === _self.STATUS.Inapprovazione && (_self.Agent === false || _self.Agent === null)) { isVisible = true; } break;

                case "IN_LAVORAZIONE":
                    if (currentStatus === _self.STATUS.Approvato) { isVisible = true; }
                    break;
                case "SPEDITO":
                    if (currentStatus === _self.STATUS.Approvato || currentStatus === _self.STATUS.Inlavorazione) { isVisible = true; }

                    break;
            }
            return isVisible;
        },
        execute: async function (formContext, status) {

            await import('../res_scripts/res_global.js');

            const salesOrderId = formContext.data.entity.getId().replace(/[{}]/g, "");

            let statuscode = null;
            let statecode = 0;

            switch (status) {
                case "APPROVAL":
                    statuscode = _self.STATUS.Inapprovazione;
                    break;
                case "APPROVED":
                    statuscode = _self.STATUS.Approvato;
                    break;
                case "NOT_APPROVED":
                    statuscode = _self.STATUS.Nonapprovato;
                    statecode = 2; // Annullato
                    break;
                case "IN_LAVORAZIONE":
                    statuscode = _self.STATUS.Inlavorazione;
                    break;
                case "SPEDITO":
                    statuscode = _self.STATUS.Spedito_StateAttivo;
                    break;
            }

            Xrm.Utility.showProgressIndicator("Cambio stato...");

            if (statuscode != null) {

                const json = {
                    EntityId: salesOrderId,
                    StateCode: statecode,
                    StatusCode: statuscode
                }

                var execute_res_ClientAction_Request = {

                    // Parameters
                    actionName: "UPDATE_SALESORDER_STATUS", // Edm.String
                    jsonDataInput: JSON.stringify(json), // Edm.String

                    getMetadata: function () {
                        return {
                            boundParameter: null,
                            parameterTypes: {
                                actionName: { typeName: "Edm.String", structuralProperty: 1 },
                                jsonDataInput: { typeName: "Edm.String", structuralProperty: 1 }
                            },
                            operationType: 0, operationName: "res_ClientAction"
                        };
                    }
                };

                parent.Xrm.WebApi.execute(execute_res_ClientAction_Request).then(
                    response => {
                        if (response.ok) { return response.json(); }
                    }
                ).then(responseBody => {
                    const result = JSON.parse(responseBody.jsonDataOutput);

                    if (result === 0) {
                        console.log("Client action executed successfully (returned 0)");


                    } else if (typeof result === 'object' && result !== null) {
                        console.log("Client action returned object:", result);


                        if (result.message) {
                            console.log("Message:", result.message);


                        }
                    } else {
                        console.warn("Client action returned unexpected value:", result);
                    }
                    Xrm.Utility.closeProgressIndicator();
                    RSMNG.TAUMEDIKA.GLOBAL.refreshFormAndRibbon();

                }).catch(error => {
                    Xrm.Utility.closeProgressIndicator();
                    console.error("Error in invokeClientAction:", error);
                });

            }
            else {
                console.log("Errore: Bottone non riconosciuto o configurato male");
            }
            

            
        }
    };
}).call(RSMNG.TAUMEDIKA.SALESORDER.RIBBON.FORM);

/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

