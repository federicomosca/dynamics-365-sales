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
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                #region CheckDefaultDuplicates
                PluginRegion = "CheckDefaultDuplicates";

                target.GetPostImage(preImage).TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);

                target.GetPostImage(preImage).TryGetAttributeValue<bool>(DataModel.res_address.res_isdefault, out bool isDefault);

                if (isDefault)
                {
                    if (erCustomer != null)
                    {
                        var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                        <entity name=""res_address"">
                                        <attribute name=""res_isdefault"" />
                                        <filter>
                                            <condition attribute=""res_addressid"" operator=""ne"" value=""{target.Id}"" />
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
                }
                #endregion
            }
        }
    }
}

