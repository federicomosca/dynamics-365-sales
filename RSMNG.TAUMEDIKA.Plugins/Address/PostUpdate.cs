using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Address
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
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

                #region Update Eventuali Duplicati Default
                PluginRegion = "Update Eventuali Duplicati Default";

                Entity postImage = target.GetPostImage(preImage);
                target = postImage;

                target.TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);
                target.TryGetAttributeValue<bool>(DataModel.res_address.res_isdefault, out bool isDefault);

                if (isDefault)
                {
                    if (erCustomer != null)
                    {
                        var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                        <entity name=""{DataModel.res_address.logicalName}"">
                                        <attribute name=""{DataModel.res_address.res_isdefault}"" />
                                        <filter>
                                            <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                            <condition attribute=""{DataModel.res_address.res_addressid}"" operator=""ne"" value=""{target.Id}"" />
                                            <condition attribute=""{DataModel.res_address.res_customerid}"" operator=""eq"" value=""{erCustomer.Id}"" />
                                            <condition attribute=""{DataModel.res_address.res_isdefault}"" operator=""eq"" value=""1"" />
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

