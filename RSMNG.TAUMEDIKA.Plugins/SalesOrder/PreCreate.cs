using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
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
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, RSMNG.TAUMEDIKA.Shared.SalesOrder.Utility.mandatoryFields);
            #endregion

            #region Popolo in automatico il Destinatario
            string destination = string.Empty;
            if (target.ContainsAttributeNotNull(salesorder.res_shippingreference))
            {
                destination = target.GetAttributeValue<string>(salesorder.res_shippingreference);
            }
            if (string.IsNullOrEmpty(destination) && target.ContainsAttributeNotNull(salesorder.customerid))
            {
                destination = Shared.Account.Utility.GetName(crmServiceProvider.Service, target.GetAttributeValue<EntityReference>(salesorder.customerid).Id);
            }
            target.AddWithRemove(salesorder.res_recipient, destination);
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";
            if (target.Contains(DataModel.salesorder.res_countryid))
            {
                target.TryGetAttributeValue<EntityReference>(DataModel.salesorder.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? RSMNG.TAUMEDIKA.Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

                target[DataModel.contact.address1_country] = countryName;
            }
            #endregion

            #region Imposto il motivo stato su Approvato
            PluginRegion = "Imposto il motivo stato su Approvato";

            if(PluginActiveTrace) crmServiceProvider.TracingService.Trace($"Parent Context: {crmServiceProvider.PluginContext.ParentContext.PrimaryEntityName}");
            #endregion
        }
    }
}

