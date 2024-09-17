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

        /**
        * fetcho gli address legati al customer (contact o account)
        * se non esiste nessun address, creo un nuovo record address e lo valorizzo con i values passati come argomenti al metodo
        * metto Default a true
        */
        public static void CheckAddress(CrmServiceProvider crmServiceProvider, string logicalName, string customerIdString, string address = "", string city = "", string postalcode = "", string pluginMessage = "")
        {
            if (!string.IsNullOrEmpty(pluginMessage))
            {

                var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch returntotalrecordcount=""true"">
                              <entity name=""res_address"">
                                <attribute name=""res_isdefault"" />
                                <filter type=""and"">
                                  <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                  <condition attribute=""res_customerid"" operator=""eq"" value=""{customerIdString}"" />
                                  <condition attribute=""res_isdefault"" operator=""eq"" value=""1"" />
                                  <condition attribute=""res_iscustomeraddress"" operator=""eq"" value=""1"" />
                                </filter>
                              </entity>
                            </fetch>";

                bool results = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses)).TotalRecordCount == -1;

                if (results) { return; }
            }
            /**
             * creo il record di Address e lo valorizzo con i values passati al metodo come argomenti
             */
            Entity enAddress = new Entity("res_address");
            enAddress[DataModel.res_address.res_addressField] = address;
            enAddress[DataModel.res_address.res_city] = city;
            enAddress[DataModel.res_address.res_postalcode] = postalcode;

            Guid customerId = new Guid(customerIdString);
            enAddress[DataModel.res_address.res_customerid] = new EntityReference(logicalName, customerId);

            enAddress[DataModel.res_address.res_isdefault] = true;
            enAddress[DataModel.res_address.res_iscustomeraddress] = true;

            Guid addressId = crmServiceProvider.Service.Create(enAddress);
        }

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
