using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Account
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.account.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo Codice fiscale
            PluginRegion = "Controllo Codice fiscale";
            if (target.ContainsAttributeNotNull(DataModel.account.res_taxcode)) {
                bool isExist = Shared.Account.Utility.CheckFiscalCode(crmServiceProvider.Service, (string)target.Attributes[DataModel.account.res_taxcode]);
                if (isExist)
                {
                    throw new ApplicationException("il codice fiscale inserito è associato ad un'altro account.");
                }
            }
            #endregion
        }
    }
}
