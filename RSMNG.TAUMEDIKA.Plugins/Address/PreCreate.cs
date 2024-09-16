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

            string addressName = string.Empty;
            string customerName = string.Empty;
            string addressCity = string.Empty;
            string addressStreet = string.Empty;

            target.TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);

            if (erCustomer != null)
            {
                if (erCustomer.LogicalName == "contact")
                {
                    Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(DataModel.contact.fullname));
                    customerName = customer.GetAttributeValue<string>(DataModel.contact.fullname) ?? string.Empty;
                }
                if (erCustomer.LogicalName == "account")
                {
                    Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(DataModel.account.name));
                    customerName = customer.GetAttributeValue<string>(DataModel.account.name) ?? string.Empty;
                }
            }

            addressStreet = target.GetAttributeValue<string>(DataModel.res_address.res_addressField) ?? string.Empty;
            addressCity = target.GetAttributeValue<string>(DataModel.res_address.res_city) ?? string.Empty;

            addressName = $"{customerName} {addressCity} {addressStreet}";

            target[DataModel.res_address.res_name] = addressName;
            #endregion

            #region CheckDefaultDuplicates
            PluginRegion = "CheckDefaultDuplicates";

            target.TryGetAttributeValue<bool>(DataModel.res_address.res_isdefault, out bool isDefault);

            if (isDefault)
            {
                var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                        <entity name=""res_address"">
                                        <attribute name=""res_isdefault"" />
                                        <filter>
                                            <condition attribute=""res_customerid"" operator=""eq"" value=""{erCustomer.Id}"" />
                                            <condition attribute=""res_isdefault"" operator=""eq"" value=""1"" />
                                        </filter>
                                        </entity>
                                    </fetch>";

                EntityCollection addresses = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses));
                if (addresses.Entities.Count > 0)
                {
                    foreach (Entity address in addresses.Entities)
                    {
                        address[DataModel.res_address.res_isdefault] = false;
                        crmServiceProvider.Service.Update(address);
                    }
                }
            }
            #endregion
        }
    }
}

