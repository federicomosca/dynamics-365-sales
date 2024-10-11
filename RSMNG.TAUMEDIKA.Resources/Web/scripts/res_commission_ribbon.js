
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.COMMISSION) == "undefined") {
    RSMNG.TAUMEDIKA.COMMISSION = {};
}

if (typeof (RSMNG.TAUMEDIKA.COMMISSION.RIBBON) == "undefined") {
    RSMNG.TAUMEDIKA.COMMISSION.RIBBON = {};
}

if (typeof (RSMNG.TAUMEDIKA.COMMISSION.RIBBON.FORM) == "undefined") {
    RSMNG.TAUMEDIKA.COMMISSION.RIBBON.FORM = {};
}

if (typeof (RSMNG.TAUMEDIKA.COMMISSION.RIBBON.SUBGRID) == "undefined") {
    RSMNG.TAUMEDIKA.COMMISSION.RIBBON.SUBGRID = {};
}

if (typeof (RSMNG.TAUMEDIKA.COMMISSION.RIBBON.HOME) == "undefined") {
    RSMNG.TAUMEDIKA.COMMISSION.RIBBON.HOME = {};
}

(function () {

    var _self = this;

	_self.formModel = {
		entity: {
			///Provvigione constants.
			logicalName: "res_commission",
			displayName: "Provvigione",
		},
		fields: {

			///Provvigione
			res_commissionid: "res_commissionid",
			///Data fine
			res_enddate: "res_enddate",
			///Nome
			res_name: "res_name",
			///Data inizio
			res_startdate: "res_startdate",

			/// Values for field Stato
			statecodeValues: {
				Attivo: 0,
				Inattivo: 1
			},

			/// Values for field Motivo stato
			statuscodeValues: {
				Bozza_StateAttivo: 1,
				Calcolata_StateAttivo: 100000002,
				Calcolataerrori_StateAttivo: 100000003,
				Calcoloincorso_StateAttivo: 100000001,
				Inattivo_StateInattivo: 2
			}
		}
	};


    //--------------------------------------------------
    _self.STARTCALC = {
        canExecute: async function (formContext) {

            let statuscode = formContext.getAttribute("statuscode").getValue();

            return statuscode != _self.formModel.fields.statuscodeValues.Calcoloincorso_StateAttivo ? true : false;
        },
        execute: async function (formContext) {

            //await import('../res_scripts/res_global.js');

            pageInput = {
                pageType: 'webresource',
                webresourceName: '/res_pages/selectAgents.html'
            }

            navigationOptions = {
                target: 2,
                width: { value: 600, unit: "px" },
                height: { value: 660, unit: "px" },
                position: 1,
                title: 'Seleziona Agenti'
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
    };
}).call(RSMNG.TAUMEDIKA.COMMISSION.RIBBON.FORM);





/*
Alla call puoi aggiungere i namespace se hai necessità di estendere le funzionalità
- RSMNG.PROGETTO.ENTITY.RIBBON.SUBGRID
- RSMNG.PROGETTO.ENTITY.RIBBON.HOME
*/

