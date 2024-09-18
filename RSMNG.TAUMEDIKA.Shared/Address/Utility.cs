using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
        public static EntityCollection CheckDefaultAddress(CrmServiceProvider crmServiceProvider, Guid customerIdString, Guid newDefaultAddressId)
        {
            var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""{DataModel.res_address.logicalName}"">
                                <attribute name=""{DataModel.res_address.res_isdefault}"" />
                                <filter type=""and"">
                                  <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                  <condition attribute=""{DataModel.res_address.res_addressid}"" operator=""ne"" value=""{newDefaultAddressId.ToString()}"" />
                                  <condition attribute=""{DataModel.res_address.res_customerid}"" operator=""eq"" value=""{customerIdString.ToString()}"" />
                                  <condition attribute=""{DataModel.res_address.res_isdefault}"" operator=""eq"" value=""1"" />
                                </filter>
                              </entity>
                            </fetch>";

            return crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses));
        }
        public static Guid CreateNewDefaultAddress(Entity target, IOrganizationService service, string address = "", string city = "", string postalcode = "")
        {
            Entity enAddress = new Entity(DataModel.res_address.logicalName);
            enAddress[DataModel.res_address.res_addressField] = address;
            enAddress[DataModel.res_address.res_city] = city;
            enAddress[DataModel.res_address.res_postalcode] = postalcode;

            enAddress[DataModel.res_address.res_customerid] = new EntityReference(target.LogicalName, target.Id);

            enAddress[DataModel.res_address.res_isdefault] = true;
            enAddress[DataModel.res_address.res_iscustomeraddress] = true;

            Guid addressId = service.Create(enAddress);
            return addressId;
        }
    }
}
