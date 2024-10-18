using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Document
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.res_document.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Effettuo il calcolo dei totali della provvigione
            PluginRegion = "Effettuo il calcolo dei totali della provvigione";
            if (target.ContainsAttributeNotNull(res_document.res_agentcommissionid))
            {
                Shared.AgentCommission.Utility.UpdateTotalCommission(crmServiceProvider.Service, target.GetAttributeValue<EntityReference>(res_document.res_agentcommissionid).Id);
            }
            #endregion

        }
    }
}
