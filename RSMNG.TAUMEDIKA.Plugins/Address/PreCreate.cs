using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Address
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region GenerateName
            PluginRegion = "GenerateName";

            target.TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);
            ColumnSet customerColumns = new ColumnSet(
                DataModel.account.name,
                DataModel.contact.fullname
                );

            Entity customer = crmServiceProvider.Service.Retrieve(DataModel.res_address.res_customerid, erCustomer.Id, customerColumns);

            if (customer != null)
            {
                customer.TryGetAttributeValue<string>(DataModel.account.name, out string accountName);
                customer.TryGetAttributeValue<string>(DataModel.contact.fullname, out string contactName);

                crmServiceProvider.TracingService.Trace($"Account Name: {accountName}, Contact Name: {contactName}");
            }

            #endregion
        }
    }
}

