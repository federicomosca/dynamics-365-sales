using Microsoft.Xrm.Sdk;
using RSMNG.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Quote
{
    public class PrevCreate : RSMNG.BaseClass
    {
        public PrevCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PREVALIDATION;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.quote.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Genero Nr. Offerta custom
            PluginRegion = "Genero Nr. Offerta custom";
            string NrOfferta = String.Empty;
            if (crmServiceProvider.PluginContext.ParentContext != null && crmServiceProvider.PluginContext.ParentContext.MessageName != "Revise")
            {
                //Prelevo l'AutoNumber per l'esecuzione
                NrOfferta = Autonumber.GetAutoNumber(crmServiceProvider.ServiceFactory, DataModel.quote.logicalName,"", $@"{DateTime.Today.ToString("yyyy")}");
                if (!string.IsNullOrEmpty(NrOfferta))
                {
                    target.Attributes.Remove(DataModel.quote.quotenumber);
                    target.Attributes.Add(DataModel.quote.quotenumber, NrOfferta);
                }
            }
            #endregion
        }
    }
}
