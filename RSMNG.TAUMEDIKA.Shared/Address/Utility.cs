using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Crm.Sdk.Messages;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA.Shared.Address
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                res_address.res_customerid,
                res_address.res_addressField,
                res_address.res_postalcode,
                res_address.res_city
            };

        /**
        * fetcho gli address legati al customer (contact o account)
        * se non esiste nessun address, creo un nuovo record address e lo valorizzo con i values passati come argomenti al metodo
        * metto Default a true
        */
        public static EntityCollection GetDefaultAddress(CrmServiceProvider crmServiceProvider, Guid customerIdString)
        {
            var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""{res_address.logicalName}"">
                                <filter type=""and"">
                                  <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                  <condition attribute=""{res_address.res_customerid}"" operator=""eq"" value=""{customerIdString}"" />
                                  <condition attribute=""{res_address.res_isdefault}"" operator=""eq"" value=""1"" />
                                </filter>
                              </entity>
                            </fetch>";

            return crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses));
        }
        public static void CreateNewDefaultAddress(Entity target, IOrganizationService service, string address = "", string city = "", string postalcode = "")
        {
            Entity enAddress = new Entity(res_address.logicalName);
            enAddress[res_address.res_addressField] = address;
            enAddress[res_address.res_city] = city;
            enAddress[res_address.res_postalcode] = postalcode;

            enAddress[res_address.res_customerid] = new EntityReference(target.LogicalName, target.Id);

            enAddress[res_address.res_isdefault] = true;
            enAddress[res_address.res_iscustomeraddress] = true;

            service.Create(enAddress);
        }
    }
}
