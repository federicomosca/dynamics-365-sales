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
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            var ts = PluginActiveTrace ? crmServiceProvider.TracingService : null;

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Controllo univocità "Default per Agenti", "Importo ERP", "Default sito web"
            PluginRegion = "Controllo univocità \"Default per Agenti\", \"Importo ERP\", \"Default sito web\"";

            target.TryGetAttributeValue<bool>(pricelevel.res_isdefaultforagents, out bool isDefaultPerAgenti);
            target.TryGetAttributeValue<bool>(pricelevel.res_iserpimport, out bool isERPImport);
            target.TryGetAttributeValue<bool>(pricelevel.res_isdefaultforwebsite, out bool isDefaultPerWebsite);

            ts.Trace($"Default per agenti: {isDefaultPerAgenti}");
            ts.Trace($"Import ERP: {isERPImport}");
            ts.Trace($"Default web site: {isDefaultPerWebsite}");

            string field = null;
            if (isDefaultPerAgenti) { field = "AGENTI"; }
            if (isERPImport) { field = "ERP"; }
            if (isDefaultPerWebsite) { field = "WEBSITE"; }

            ts.Trace($"Field: {field}");
            Utility.checkIsDefault(crmServiceProvider.Service, postImage.Id, field);
            #endregion
        }
    }
}
