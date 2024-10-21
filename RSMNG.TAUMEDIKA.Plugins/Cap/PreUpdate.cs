using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Cap
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.res_cap.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Genero in automatico il nome del CAP
            PluginRegion = "Genero in automatico il nome del CAP";

            string code = postImage.ContainsAttributeNotNull(res_cap.res_code) ? postImage.GetAttributeValue<string>(res_cap.res_code) : string.Empty;
            string city = postImage.ContainsAttributeNotNull(res_cap.res_city) ? postImage.GetAttributeValue<string>(res_cap.res_city) : string.Empty;

            target.AddWithRemove(res_cap.res_name, $"{code} - {city}");
            #endregion
        }
    }
}
