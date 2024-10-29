using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSMNG.TAUMEDIKA;
using RSMNG.TAUMEDIKA.DataModel;

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
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);


            #region Controllo Codice fiscale
            PluginRegion = "Controllo Codice fiscale";
            if (target.ContainsAttributeNotNull(DataModel.account.res_taxcode))
            {

                bool isExist = RSMNG.TAUMEDIKA.Shared.Account.Utility.CheckFiscalCode(crmServiceProvider.Service, (string)target.Attributes[DataModel.account.res_taxcode], target.Id);
                if (isExist)
                {
                    throw new ApplicationException("il codice fiscale inserito è associato ad un'altro account.");
                }
            }
            #endregion

            #region Imposto in automatico il campo Nazione testo
            PluginRegion = "Imposto in automatico il campo Nazione testo";

            if (target.Contains(account.res_countryid))
            {
                EntityReference erCountry = target.GetAttributeValue<EntityReference>(account.res_countryid);

                string countryName = erCountry != null ? RSMNG.TAUMEDIKA.Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : null;


                target[account.address1_country] = countryName;
            }


            

            

            #endregion
        }
    }
}
