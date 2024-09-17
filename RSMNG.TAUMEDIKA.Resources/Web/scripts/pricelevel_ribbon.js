
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
        execute: async function (formContext) {

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
        execute: async function (formContext, SelectedControlSelectedItemIds) {

            await import('../res_scripts/res_global.js');
            let optMetadata = await RSMNG.TAUMEDIKA.GLOBAL.getGlobalOptionSetMetadata("res_opt_scopetype");
            let selectedScope = [];
            let scopeTypeCodes = optMetadata.map(item => ({
                text: item.Label.UserLocalizedLabel.Label,
                value: item.Value
            }));

            let pricelevelId = SelectedControlSelectedItemIds[0];
            let queryOptions = "?$select=begindate,enddate,transactioncurrencyid,res_scopetypecodes,res_isdefaultforwebsite,res_isdefaultforagents,description";


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


            //Xrm.WebApi.retrieveRecord("pricelevel", pricelevelId, queryOptions).then(
            //    function success(result) {

                    
                   
            //    },
            //    function error(error) {

            //        console.log(error);
            //    }
            //);
            

            
        }
    };
}).call(RSMNG.TAUMEDIKA.PRICELEVEL.RIBBON.HOME);



/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

 