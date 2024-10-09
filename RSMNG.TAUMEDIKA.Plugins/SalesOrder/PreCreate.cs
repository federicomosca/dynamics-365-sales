using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrder
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.salesorder.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, RSMNG.TAUMEDIKA.Shared.SalesOrder.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";
            target.TryGetAttributeValue<EntityReference>(DataModel.salesorder.res_countryid, out EntityReference erCountry);
            string countryName = erCountry != null ? RSMNG.TAUMEDIKA.Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

            target[DataModel.contact.address1_country] = countryName;
            #endregion

        }
    }
}

