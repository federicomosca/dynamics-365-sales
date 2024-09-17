using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Account
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.account.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo Codice fiscale
            PluginRegion = "Controllo Codice fiscale";
            if (target.ContainsAttributeNotNull(DataModel.account.res_taxcode))
            {
                bool isExist = Shared.Account.Utility.CheckFiscalCode(crmServiceProvider.Service, (string)target.Attributes[DataModel.account.res_taxcode],target.Id);
                if (isExist)
                {
                    throw new ApplicationException("il codice fiscale inserito è associato ad un'altro account.");
                }
            }
            #endregion

            #region Imposto in automatico il campo Nazione testo
            PluginRegion = "Imposto in automatico il campo Nazione testo";
            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                Entity postImage = target.GetPostImage(preImage);

                postImage.TryGetAttributeValue<EntityReference>(DataModel.account.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : null;

                target[DataModel.account.address1_country] = countryName;
            }
            #endregion
        }
    }
}
