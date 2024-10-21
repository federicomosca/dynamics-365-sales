
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
            };

            navigationOptions = {
                target: 2,
                width: { value: 600, unit: "px" },
                height: { value: 660, unit: "px" },
                position: 1,
                title: 'Seleziona Agenti'
            };
            formContext.ui.clearFormNotification();
            window._formContext = formContext;
            Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                async function (result) {

                    if (result.returnValue != null) {
                        try {
                            debugger;
                            selectedAgentsCommission = JSON.parse(result.returnValue);
                            var processedAgentCommission = 0;
                            var processPercentage = 0;
                            var agentsCommissionWithErrors = [];
                            var statusData = {};
                            if (selectedAgentsCommission.length > 0) {
                                Xrm.Utility.showProgressIndicator(`Calcolo della provvigione in corso con Agenti ${processedAgentCommission} di ${selectedAgentsCommission.length}...`);
                                for (let selectedItem of selectedAgentsCommission) {
                                    try {
                                        var jsonInput = {
                                            "DeleteAgentCommission": processedAgentCommission == 0 ? true : false,
                                            "CommissionId": RSMNG.TAUMEDIKA.GLOBAL.convertGuid(formContext.data.entity.getId()),
                                            "AgentId": selectedItem["systemuserid"],
                                            "LastAgent": processedAgentCommission == selectedAgentsCommission.length - 1
                                        };
                                        var resposeMsg = {
                                            message: "",
                                            id: selectedItem["fullname"]
                                        };
                                        var response = await RSMNG.TAUMEDIKA.GLOBAL.callClientAction(Xrm, "AGENTCOMMISSION_CALCULATION", jsonInput);
                                        processedAgentCommission++;
                                        processPercentage = (processedAgentCommission / selectedAgentsCommission.length) * 100;
                                        Xrm.Utility.showProgressIndicator(`Calcolo della provvigione in corso con Agenti ${processedAgentCommission} di ${selectedAgentsCommission.length} (${selectedItem["fullname"].substring(0, 30) + "..."}) (${processPercentage.toFixed(1)}%)`);
                                        if (response.result != 0) {
                                            resposeMsg.message = response.message;
                                            agentsCommissionWithErrors.push(resposeMsg);
                                        }
                                    } catch (error) {
                                        resposeMsg.message = error.message;
                                        agentsCommissionWithErrors.push(resposeMsg);
                                    }
                                }
                                Xrm.Utility.closeProgressIndicator();
                                let commissionCalculationDetail = "";
                                if (agentsCommissionWithErrors.length > 0) {
                                    commissionCalculationDetail = "Lista degli agenti dove il calcolo della provvigione ha avuto errori:"
                                    for (var i = 0; i < agentsCommissionWithErrors.length; i++) {
                                        commissionCalculationDetail += `\r\n${agentsCommissionWithErrors[i].id}`;
                                        formContext.ui.setFormNotification(`Il calcolo della provvigione non e' stata terminata con successo. L'agente': [${agentsCommissionWithErrors[i].id}] non e' stato processato. In dettaglio: [${agentsCommissionWithErrors[i].message}]`, "WARNING", i);
                                    }
                                    //Aggiorno lo stato della Provvigione
                                    statusData = {
                                        "statecode": _self.formModel.fields.statecodeValues.Attivo,
                                        "statuscode": _self.formModel.fields.statuscodeValues.Calcolataerrori_StateAttivo,
                                        "res_commissioncalculationdetail": commissionCalculationDetail
                                    }
                                } else {
                                    formContext.ui.setFormNotification(`Il calcolo della provvigione stata terminata con successo.`, "INFO", i);
                                    //Aggiorno lo stato della Provvigione
                                    statusData = {
                                        "statecode": _self.formModel.fields.statecodeValues.Attivo,
                                        "statuscode": _self.formModel.fields.statuscodeValues.Calcolata_StateAttivo,
                                        "res_commissioncalculationdetail": commissionCalculationDetail
                                    }
                                }
                                let resultChangeStatus = await Xrm.WebApi.online.updateRecord(_self.formModel.entity.logicalName, formContext.data.entity.getId(), statusData);
                                formContext.data.refresh(false).then(function () { formContext.ui.refreshRibbon(true); }, function () { formContext.ui.refreshRibbon(true); });
                            }
                        } catch (e) {
                            formContext.ui.setFormNotification(`Il calcolo della provvigione non e' stata terminata con successo. In dettaglio: [${e.message}]`, "ERROR", "001");
                        }
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

