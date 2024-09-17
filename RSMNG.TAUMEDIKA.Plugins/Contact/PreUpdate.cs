using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Contact
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.contact.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                Entity postImage = target.GetPostImage(preImage);

                #region Valorizzo il campo Nazione (testo)
                PluginRegion = "Valorizzo il campo Nazione (testo)";
                postImage.TryGetAttributeValue<EntityReference>(DataModel.contact.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? erCountry.Name : string.Empty;

                target[DataModel.contact.address1_country] = countryName;
                #endregion
            }
        }
    }
}

