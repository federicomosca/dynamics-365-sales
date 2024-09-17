
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

            let scopeTypeCodes =  formContext.getControl("res_scopetypecodes").getAttribute().getOptions();

            

            jsonDataInput = {
                scopeValues: scopeTypeCodes
            }

            pageInput = {
                pageType: 'webresource',
                webresourceName: '/res_pages/copyPriceLevel.html',
                data: JSON.stringify(jsonDataInput)
            }

            navigationOptions = {
                target: 2,
                width: { value: 40, unit: "%" },
                height: { value: 50, unit: "%" },
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
/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

 