using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.PriceLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.PriceLevel
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = pricelevel.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo univocità DEFAULT PER AGENTI
            //PluginRegion = "controllo univocità DEFAULT PER AGENTI";

            //if(target.Contains(pricelevel.res_isdefaultforagents) || target.Contains(pricelevel.statecode))
            //{
            //    bool isDefaultForAgents = target.GetAttributeValue<bool>(pricelevel.res_isdefaultforagents);

            //    if (isDefaultForAgents) { Utility.CheckDefaultForAgents(crmServiceProvider.Service); }
            //}
            
            #endregion


        }
    }
}
