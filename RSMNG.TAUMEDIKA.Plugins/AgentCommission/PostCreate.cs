using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.AgentCommission
{
    public class PostCreate : RSMNG.BaseClass
    {
        public PostCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.res_agentcommission.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Condivido la commissione agente all'agente
            if (target.ContainsAttributeNotNull(res_agentcommission.res_agentid))
            {
                crmServiceProvider.ServiceAdmin.GrantAccess(target.GetAttributeValue<EntityReference>(res_agentcommission.res_agentid), target.ToEntityReference(), Microsoft.Crm.Sdk.Messages.AccessRights.ReadAccess);
            }
            #endregion
        }
    }
}
