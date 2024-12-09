
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
    };

    _self.STATECODE = {
        Annullato: 2,
        Attivo: 0,
        Evaso: 3,
        Fatturato: 4,
        Inviato: 1
    }

    _self.readOnlyFields = [
        "ordernumber",
        "res_origincode",
        "datefulfilled",
        "totallineitemamount",
        "totaldiscountamount",
        "totalamountlessfreight",
        "totaltax",
        "totalamount",
        "quoteid"
    ];

    _self.Agent = undefined;
    _self.hasSalesOrderDetails = null;

    //--------------------------------------------------
    _self.getSalesOrderDetails = function (salesorderid) {
        return new Promise(function (resolve, reject) {

            var fetchData = {
                "salesorderid": salesorderid
            };
            var fetchXml = [
                "?fetchXml=<fetch>",
                "  <entity name='salesorderdetail'>",
                "    <attribute name='salesorderdetailname'/>",
                "    <filter>",
                "      <condition attribute='salesorderid' operator='eq' value='", fetchData.salesorderid, "' />",
                "    </filter>",
                "  </entity>",
                "</fetch>"
            ].join("");


            Xrm.WebApi.retrieveMultipleRecords("salesorderdetail", fetchXml)
                .then(function (result) {
                    var count = result.entities.length;

                    resolve(count > 0 ? true : false);
                })
                .catch(function (error) {
                    console.log(error);
                    reject(error.message);
                });
        });
    };
    //--------------------------------------------------
    _self.isInvoiceRequested = formContext => {
        return formContext.getAttribute("res_isinvoicerequested").getValue();
    }
    //--------------------------------------------------
    _self.getPotentialCustomerId = formContext => {
        let potentialCustomerId = null;
        const potentialCustomerControl = formContext.getControl("customerid");
        if (potentialCustomerControl) {
            potentialCustomerId = potentialCustomerControl.getAttribute().getValue() ? potentialCustomerControl.getAttribute().getValue()[0].id : null;
        }
        return potentialCustomerId ?? null;
    };
    //--------------------------------------------------

    _self.UPDATESTATUS = {
        canExecute: async function (formContext, status) {
            let currentStatus = formContext.getAttribute("statuscode").getValue();
            let salesorderId = formContext.data.entity.getId().replace("{", "").replace("}", "");
            let isVisible = false;



            if (_self.Agent === undefined) {
                _self.Agent = await RSMNG.TAUMEDIKA.GLOBAL.getAgent();
            }


            switch (status) {

                case "APPROVAL": //in approvazione 
                    _self.hasSalesOrderDetails = await _self.getSalesOrderDetails(salesorderId);
                    if (formContext.ui.getFormType() != 1) {
                        if (currentStatus === _self.STATUS.Bozza &&
                            _self.Agent === true &&
                            _self.hasSalesOrderDetails) {
                            try {
                                if (_self.isInvoiceRequested(formContext) == 1) {

                                    //controllo se mancano dati nell'anagrafica del potenziale cliente
                                    let missingData = null;
                                    const potentialCustomerId = _self.getPotentialCustomerId(formContext);

                                    if (potentialCustomerId) {
                                        missingData = await RSMNG.TAUMEDIKA.GLOBAL.retrievePotentialCustomerMissingData(formContext, potentialCustomerId);

                                        if (missingData.length > 0) {
                                            isVisible = false;    //se mancano dati nascondo il button
                                        } else {
                                            isVisible = true;
                                        }
                                    }
                                } else {
                                    isVisible = true;
                                }
                            } catch (error) {
                                console.error("Error checking customer data:", error);
                                visible = false;
                            }
                        }
                    }
                    break;

                case "APPROVED": //approvata
                    _self.hasSalesOrderDetails = await _self.getSalesOrderDetails(salesorderId);
                    if (formContext.ui.getFormType() != 1 && _self.hasSalesOrderDetails) {

                        if (currentStatus === _self.STATUS.Bozza && (_self.Agent === false || _self.Agent === null)) {
                            isVisible = true;
                        }
                        if (currentStatus === _self.STATUS.Inapprovazione && (_self.Agent === false || _self.Agent === null)) {
                            isVisible = true;
                        }
                        break;
                    }
                    break;
                case "NOT_APPROVED": //non approvata
                    if (currentStatus === _self.STATUS.Inapprovazione && (_self.Agent === false || _self.Agent === null)) { isVisible = true; } break;

                case "IN_LAVORAZIONE":
                    if (currentStatus === _self.STATUS.Approvato && (_self.Agent === false || _self.Agent === null)) { isVisible = true; }
                    break;
                case "SPEDITO":
                    if (currentStatus === _self.STATUS.Approvato || currentStatus === _self.STATUS.Inlavorazione) {
                        if (_self.Agent === false || _self.Agent === null) { isVisible = true; }

                    }


                    break;

                case "ANNULLA_ORDINE":
                    if (_self.Agent === false) {
                        isVisible = true;
                    }
                    else if (currentStatus === _self.STATUS.Bozza && _self.Agent === true) {
                        isVisible = true;
                    }
                    break;
            }
            return isVisible;
        },
        execute: async function (formContext, status) {

            await import('../res_scripts/res_global.js');

            const salesOrderId = formContext.data.entity.getId().replace(/[{}]/g, "");

            let statuscode = null;
            let statecode = 0;

            let setAllReadOnly = false;

            switch (status) {
                case "APPROVAL": // in approvazione

                    statuscode = _self.STATUS.Inapprovazione;
                    setAllReadOnly = _self.Agent;

                    break;
                case "APPROVED":

                    statuscode = _self.STATUS.Approvato;
                    setAllReadOnly = _self.Agent;

                    break;
                case "NOT_APPROVED":
                    statuscode = _self.STATUS.Nonapprovato;
                    statecode = _self.STATECODE.Annullato;
                    break;
                case "IN_LAVORAZIONE":
                    statuscode = _self.STATUS.Inlavorazione;
                    break;
                case "SPEDITO":
                    statuscode = _self.STATUS.Spedito_StateAttivo;
                    setAllReadOnly = true;
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

                    // imposto read only tutti i campi
                    if (setAllReadOnly === true) {

                        RSMNG.TAUMEDIKA.GLOBAL.setAllFieldsReadOnly(formContext, true);
                    }

                    if (result == 0) {



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
    //-----------------------------------------------------------
    _self.INDIRIZZOCLIENTE = {
        canExecute: async function (formContext) {

            const motivoStatoAttuale = formContext.getAttribute("statuscode").getValue();
            const clienteLookup = formContext.getAttribute("customerid").getValue();

            if (clienteLookup) {
                if (motivoStatoAttuale === _self.STATUS.Bozza) {
                    return true;
                } else {
                    if (_self.Agent === undefined) {
                        _self.Agent = await RSMNG.TAUMEDIKA.GLOBAL.getAgent();
                    }

                    if (_self.Agent === false) {
                        return true;
                    }
                }
            }
            return false;
        },
        execute: async function (formContext) {

            await import('../res_scripts/res_global.js');

            let customerLookup = formContext.getAttribute("customerid").getValue();



            if (customerLookup != null) {

                jsonDataInput = {
                    customerId: customerLookup[0].id
                }

                pageInput = {
                    pageType: 'webresource',
                    webresourceName: '/res_pages/clientAddress.html',
                    data: JSON.stringify(jsonDataInput)
                }

                navigationOptions = {
                    target: 2,
                    width: { value: 850, unit: "px" },
                    height: { value: 560, unit: "px" },
                    position: 1,
                    title: 'Indirizzi Cliente'
                }
                window._formContext = formContext;
                Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                    function (result) {

                        if (result.returnValue != null) {

                            console.log("navigate ok");

                        }
                    },

                    function (error) {
                        console.log(error.message);
                    }
                );
            }
        }
    };
    //-----------------------------------------------------------
    _self.MOBILEAPP = {
        canExecute: function (formContext) {
            const currentStatusCode = formContext.getAttribute("statuscode").getValue();
            if (currentStatusCode == _self.STATUS.Bozza) { return true; }
        },
        execute: async function (formContext) {

            var recordId = Xrm.Page.data.entity.getId(); // This retrieves the ID of the current record
            const source = Xrm.Page.data.entity.getEntityName()
            recordId = recordId.replace('{', '').replace('}', ''); // Clean up the ID format
            let isMobile = /Mobi|Android|iPhone/i.test(navigator.userAgent);

            var pageInput = {
                pageType: "custom",
                name: isMobile ? "res_app_a0afc" : "res_appcopia_7db7b",  // Use the unique name of your custom page
                entityName: "salesorder",
                recordId: recordId,
                parameters: {
                    id: recordId
                }
            };
            var navigationOptions = {
                target: 1, // Opens as a dialog
                //position: 1,  // usato solo con target 1
                height: { value: 100, unit: "%" },
                width: { value: 100, unit: "%" },
                title: "Prodotti"
            };
            Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                function () {

                },
                function (error) {
                    console.error("Error opening custom page:", error.message);
                }
            );
        }
    };
    //-----------------------------------------------------------
}).call(RSMNG.TAUMEDIKA.SALESORDER.RIBBON.FORM);


/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

