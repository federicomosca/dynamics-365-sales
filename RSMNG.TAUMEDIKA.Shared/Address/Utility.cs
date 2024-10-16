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
using System.Web.UI.WebControls;

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
                                  <condition attribute=""{res_address.res_iscustomeraddress}"" operator=""eq"" value=""1"" />
                                </filter>
                              </entity>
                            </fetch>";

            return crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses));
        }
        public static void CreateNewDefaultAddress(Entity target, IOrganizationService service,
            string indirizzo,
            string città,
            string CAP,
            string provincia = "",
            string località = "",
            string nazione = "")
        {
            Entity enAddress = new Entity(res_address.logicalName);
            enAddress[res_address.res_addressField] = indirizzo;
            enAddress[res_address.res_city] = città;
            enAddress[res_address.res_postalcode] = CAP;
            enAddress[res_address.res_province] = provincia;
            enAddress[res_address.res_location] = località;
            enAddress[res_address.res_countryid] = nazione;

            enAddress[res_address.res_customerid] = new EntityReference(target.LogicalName, target.Id);

            enAddress[res_address.res_iscustomeraddress] = true;
            enAddress[res_address.res_isdefault] = true;

            service.Create(enAddress);
        }
    }
}
