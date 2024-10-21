using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.VatNumber
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.res_vatnumber.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Genero in automatico il nome del Codice IVA
            PluginRegion = "Genero in automatico il nome del Codice IVA";

            string code = postImage.ContainsAttributeNotNull(res_vatnumber.res_code) ? postImage.GetAttributeValue<string>(res_vatnumber.res_code) : string.Empty;
            string description = postImage.ContainsAttributeNotNull(res_vatnumber.res_description) ? postImage.GetAttributeValue<string>(res_vatnumber.res_description) : string.Empty;

            target.AddWithRemove(res_cap.res_name, $"{code} - {description}");
            #endregion
        }
    }
}
