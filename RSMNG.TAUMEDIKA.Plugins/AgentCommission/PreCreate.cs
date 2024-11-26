using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.AgentCommission
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.res_agentcommission.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";
            crmServiceProvider.VerifyMandatoryField(Shared.AgentCommission.Utility.mandatoryFields);
            #endregion

            #region Genera nome
            PluginRegion = "Genera nome";
            string sName = Shared.SystemUser.Utility.GetName(crmServiceProvider.Service, target.GetAttributeValue<EntityReference>(res_agentcommission.ownerid).Id);
            sName += " - " + Shared.Commission.Utility.GetName(crmServiceProvider.Service, target.GetAttributeValue<EntityReference>(res_agentcommission.res_commissionid).Id);
            target.AddWithRemove(res_agentcommission.res_name, sName);
            #endregion

            #region Aggiorna totali
            PluginRegion = "Aggiorna totali";
            if (target.Contains(res_agentcommission.res_calculatedcommission) || target.Contains(res_agentcommission.res_adjustment))
            {
                decimal res_calculatedcommission = target.ContainsAttributeNotNull(res_agentcommission.res_calculatedcommission) ? target.GetAttributeValue<Money>(res_agentcommission.res_calculatedcommission).Value : 0;
                decimal res_adjustment = target.ContainsAttributeNotNull(res_agentcommission.res_adjustment) ? target.GetAttributeValue<Money>(res_agentcommission.res_adjustment).Value : 0;
                target.AddWithRemove(res_agentcommission.res_commissiontotalamount, new Money(res_calculatedcommission + res_adjustment));
            }
            #endregion

        }
    }
}
