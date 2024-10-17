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
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo univocità "Default per Agenti", "Importo ERP", "Default sito web"
            PluginRegion = "Controllo univocità \"Default per Agenti\", \"Importo ERP\", \"Default sito web\"";

            target.TryGetAttributeValue<bool>(pricelevel.res_isdefaultforagents, out bool isDefaultPerAgenti);
            target.TryGetAttributeValue<bool>(pricelevel.res_iserpimport, out bool isERPImport);
            target.TryGetAttributeValue<bool>(pricelevel.res_isdefaultforwebsite, out bool isDefaultPerWebsite);

            string field = null;
            if (isDefaultPerAgenti) { field = "AGENTI"; }
            if (isERPImport) { field = "ERP"; }
            if (isDefaultPerWebsite) { field = "WEBSITE"; }

            Utility.checkIsDefault(crmServiceProvider.Service, field);
            #endregion
        }
    }
}
