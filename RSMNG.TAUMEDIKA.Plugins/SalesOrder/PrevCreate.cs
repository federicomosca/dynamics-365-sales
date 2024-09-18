using Microsoft.Xrm.Sdk;
using RSMNG.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrder
{
    public class PrevCreate : RSMNG.BaseClass
    {
        public PrevCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PREVALIDATION;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.salesorder.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Genero Nr. Ordine custom
            PluginRegion = "Genero Nr. Ordine custom";
            string NrOrdine = String.Empty;
            if (crmServiceProvider.PluginContext.ParentContext != null && crmServiceProvider.PluginContext.ParentContext.MessageName != "Revise")
            {
                //Prelevo l'AutoNumber per l'esecuzione
                NrOrdine = Autonumber.GetAutoNumber(crmServiceProvider.ServiceFactory, DataModel.salesorder.logicalName,"", $@"{DateTime.Today.ToString("yyyy")}");
                if (!string.IsNullOrEmpty(NrOrdine))
                {
                    target.Attributes.Remove(DataModel.salesorder.ordernumber);
                    target.Attributes.Add(DataModel.salesorder.ordernumber, NrOrdine);
                }
            }
            #endregion
        }
    }
}
