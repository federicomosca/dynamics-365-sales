
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRICELEVEL) == "undefined") {
    RSMNG.TAUMEDIKA.PRICELEVEL = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON) == "undefined") {
    RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.FORM) == "undefined") {
    RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.FORM = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.SUBGRID) == "undefined") {
    RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.SUBGRID = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.HOME) == "undefined") {
    RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.HOME = {};
}

(function () {

    var _self = this;

    //--------------------------------------------------
    _self.COPY = {
        canExecute: async function (formContext) {
            return true;
        },
        execute: function (formContext) {

            await import('../res_scripts/res_global.js');

            let scopeTypeCodes =  formContext.getControl("res_scopetypecodes").getAttribute().getOptions();
            let selectedScope = formContext.getAttribute("res_scopetypecodes").getValue();
            

            jsonDataInput = {
                scopeValues: scopeTypeCodes,
                selectedScope: selectedScope
            }

            pageInput = {
                pageType: 'webresource',
                webresourceName: '/res_pages/copyPriceLevel.html',
                data: JSON.stringify(jsonDataInput)
            }

            navigationOptions = {
                target: 2,
                width: { value: 35, unit: "%" },
                height: { value: 360, unit: "px" },
                position: 1,
                title: 'Copia Listino'
            }

            Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                function (result) {

                    if (result.returnValue != null) {

                        console.log("navigate ok");

                    }
                },

                function (error) {
                    console.log(error.message);
                });
        }
    };
}).call(RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.FORM);

(function () {

    var _self = this;

    //--------------------------------------------------
    _self.COPY = {
        canExecute: async function (formContext) {

            // abilito bottone solo se è selezionato un record

            return true;
        },
        execute: function (formContext, SelectedControlSelectedItemIds) {

            await import('../res_scripts/res_global.js');
            let scopeValues = await RSMNG.TAUMEDIKA.GLOBAL.getOptionSetMetadata("pricelevel", "res_scopetypecodes");
            let pricelevelId = SelectedControlSelectedItemIds[0];
            let queryOptions = "?$select=begindate,enddate,transactioncurrencyid,res_scopetypecodes,res_isdefaultforwebsite,res_isdefaultforagents,description";

            Xrm.WebApi.retrieveRecord("pricelevel", pricelevelId, queryOptions).then(
                function success(result) {

                    jsonDataInput = {
                        scopeValues: scopeTypeCodes,
                        selectedScope: selectedScope
                    }

                    pageInput = {
                        pageType: 'webresource',
                        webresourceName: '/res_pages/copyPriceLevel.html',
                        data: JSON.stringify(jsonDataInput)
                    }

                    navigationOptions = {
                        target: 2,
                        width: { value: 35, unit: "%" },
                        height: { value: 360, unit: "px" },
                        position: 1,
                        title: 'Copia Listino'
                    }

                    Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                        function (result) {

                            if (result.returnValue != null) {

                                console.log("navigate ok");

                            }
                        },

                        function (error) {
                            console.log(error.message);
                        });
                   
                },
                function error(error) {

                    console.log(error);
                }
            );
            

            
        }
    };
}).call(RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.HOME);



/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

 