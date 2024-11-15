
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

    let agentPromise = null;

    //--------------------------------------------------
    /**
     * invocare questa funzione nel caso "CREATE_ORDER" per determinare la visibilità del 
     */
    _self.getPotentialCustomerId = formContext => {
        let potentialCustomerId = null;
        const potentialCustomerControl = formContext.getControl("customerid");
        if (potentialCustomerControl) {
            potentialCustomerId = potentialCustomerControl.getAttribute().getValue() ? potentialCustomerControl.getAttribute().getValue()[0].id : null;
        }
        return potentialCustomerId ?? null;
    };
    //--------------------------------------------------
    _self.isInvoiceRequested = formContext => {
        return formContext.getAttribute("res_isinvoicerequested").getValue();
    }
    //--------------------------------------------------
    _self.hasQuoteDetails = formContext => {
        const subgrid = formContext.getControl("quotedetailsGrid");
        if (subgrid && subgrid.getGrid()) {
            return subgrid.getGrid().getTotalRecordCount() > 0 ? true : false;
        }
    }
    //--------------------------------------------------
    _self.UPDATESTATUS = {
        canExecute: async function (formContext, status) {

            await import('../res_scripts/res_global.js');

            let currentStatus = formContext.getAttribute("statuscode").getValue();

            let visible = false;

            const agent = await RSMNG.TAUMEDIKA.GLOBAL.getAgent();
            const hasQuoteDetails = _self.hasQuoteDetails(formContext);

            switch (status) {

                case "APPROVAL": //in approvazione
                    if (currentStatus === _self.STATUS.BOZZA && agent === true && hasQuoteDetails) { visible = true; } break;

                case "APPROVED": //approvata
                    if (currentStatus === _self.STATUS.BOZZA && (agent === false || agent === null) && hasQuoteDetails) { visible = true; }
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE && (agent === false || agent === null) && hasQuoteDetails) { visible = true; } break;

                case "NOT_APPROVED": //non approvata
                    if (currentStatus === _self.STATUS.IN_APPROVAZIONE && (agent === false || agent === null)) { visible = true; } break;

                case "CREATE_ORDER": //crea ordine
                    if (currentStatus === _self.STATUS.APPROVATA && hasQuoteDetails) {
                        visible = true;
                        if (_self.isInvoiceRequested(formContext) == 1) {
                            try {
                                //controllo se mancano dati nell'anagrafica del potenziale cliente
                                let missingData = null;
                                const potentialCustomerId = _self.getPotentialCustomerId(formContext);

                                if (potentialCustomerId) {
                                    missingData = await RSMNG.TAUMEDIKA.GLOBAL.retrievePotentialCustomerMissingData(formContext, potentialCustomerId);

                                    if (missingData.length > 0) {
                                        visible = false;    //se mancano dati nascondo il button
                                    }
                                }
                            } catch (error) {
                                console.error("Error checking customer data:", error);
                                visible = false;
                            }
                        }
                    }
                    break;

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
            const quoteStatus = formContext.getAttribute("statuscode").getValue();
            const actionName = "UPDATE_QUOTE_STATUS";

            switch (status) {
                case "APPROVAL":

                    Sales.QuoteRibbonActions.Instance.activateQuote();
                    formContext.getControl("WebResource_postalcode").setVisible(false);
                    break;

                case "APPROVED":
                case "NOT_APPROVED":
                    RSMNG.TAUMEDIKA.GLOBAL.invokeClientActionFromButton(actionName, quoteId, statecode = status == "APPROVED" ? 1 : 3, statuscode = status == "APPROVED" ? 3 : 5);
                    formContext.getControl("WebResource_postalcode").setVisible(false);
                    break;
            }
        }
    };
    //--------------------------------------------------
    _self.INDIRIZZOCLIENTE = {
        canExecute: function (formContext) {

            let isVisible = true;


            return isVisible;
        },
        execute: async function (formContext) {

            await import('../res_scripts/res_global.js');

            let customerLookup = formContext.getAttribute("customerid").getValue();

            // gestire visibilita bottone se manca customer

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
                        }
                    },

                    function (error) {
                        console.log(error.message);
                    }
                );
            }
        }
    };
    //--------------------------------------------------
    _self.MOBILEAPP = {
        canExecute() { return true; },
        execute() {
            var recordId = Xrm.Page.data.entity.getId(); // This retrieves the ID of the current record
            const source = Xrm.Page.data.entity.getEntityName()
            recordId = recordId.replace('{', '').replace('}', ''); // Clean up the ID format

            function isMobileDevice() {
                return /Mobi|Android|iPhone/i.test(navigator.userAgent);
            }
          
            var pageInput = {
                pageType: "custom",
                name: "res_app_a0afc",  // Use the unique name of your custom page
                entityName: "quote",
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
    

    //--------------------------------------------------
}).call(RSMNG.TAUMEDIKA.QUOTE.RIBBON.FORM);

/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

