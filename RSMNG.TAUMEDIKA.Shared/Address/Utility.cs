using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Shared.Address
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                DataModel.res_address.res_customerid,
                DataModel.res_address.res_addressField,
                DataModel.res_address.res_postalcode,
                DataModel.res_address.res_city
            };

        public static void CheckDefaultDuplicates(CrmServiceProvider crmServiceProvider, string pluginMessage, Entity target, Entity preImage = null)
        {
            string updateCondition = string.Empty;
            if (pluginMessage == "Update")
            {
                if (preImage != null)
                {
                    Entity postImage = target.GetPostImage(preImage);
                    target = postImage;
                    updateCondition = $@"<condition attribute=""res_addressid"" operator=""ne"" value=""{target.Id}"" />";
                }
            }

            target.TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);
            target.TryGetAttributeValue<bool>(DataModel.res_address.res_isdefault, out bool isDefault);

            if (isDefault)
            {
                if (erCustomer != null)
                {
                    var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                        <entity name=""res_address"">
                                        <attribute name=""res_isdefault"" />
                                        <filter>
                                            <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                            {updateCondition}
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

        }
    }
}
