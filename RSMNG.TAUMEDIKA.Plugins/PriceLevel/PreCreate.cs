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
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = pricelevel.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            ITracingService ts = crmServiceProvider.TracingService;

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo univocità "Default per Agenti", "Importo ERP", "Default sito web"
            PluginRegion = "Controllo univocità \"Default per Agenti\", \"Importo ERP\", \"Default sito web\"";

            target.TryGetAttributeValue<OptionSetValue>(pricelevel.statecode, out OptionSetValue statecode);
            int stato = (int)statecode.Value;

            if (stato == (int)pricelevel.statecodeValues.Attivo)
            {
                if (target.Contains(pricelevel.res_isdefaultforagents) ||
                    target.Contains(pricelevel.res_iserpimport) ||
                    target.Contains(pricelevel.res_isdefaultforagents))
                {
                    target.TryGetAttributeValue<bool>(pricelevel.res_isdefaultforagents, out bool isDefaultPerAgenti);
                    target.TryGetAttributeValue<bool>(pricelevel.res_iserpimport, out bool isERPImport);
                    target.TryGetAttributeValue<bool>(pricelevel.res_isdefaultforwebsite, out bool isDefaultPerWebsite);

                    if (PluginActiveTrace) ts.Trace($"Default per agenti: {isDefaultPerAgenti}");   /* <--------------------------< Trace >-- */
                    if (PluginActiveTrace) ts.Trace($"Import ERP: {isERPImport}");                  /* <--------------------------< Trace >-- */
                    if (PluginActiveTrace) ts.Trace($"Default web site: {isDefaultPerWebsite}");    /* <--------------------------< Trace >-- */

                    string field = null;
                    if (isDefaultPerAgenti) { field = "Default per agenti"; }
                    if (isERPImport) { field = "Import ERP"; }
                    if (isDefaultPerWebsite) { field = "Default per sito web"; }

                    if (PluginActiveTrace) ts.Trace($"Field: {field}");                             /* <--------------------------< Trace >-- */
                    Utility.checkIsDefault(crmServiceProvider.Service, crmServiceProvider, target.Id, field);
                }
            }
            #endregion
        }
    }
}
