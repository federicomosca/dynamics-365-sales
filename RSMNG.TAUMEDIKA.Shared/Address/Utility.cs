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

        //recupero eventuali indirizzi attivi del cliente con Default = SI e Indirizzo scheda cliente = SI
        public static Entity GetDefaultAddress(CrmServiceProvider crmServiceProvider, Guid customerIdString)
        {
            void Trace(string key, object value)
            {
                //TRACE TOGGLE
                bool isTraceActive = true;
                {
                    if (isTraceActive)
                    {
                        key = string.Concat(key.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
                        value = value.ToString();
                        crmServiceProvider.TracingService.Trace($"{key}: {value}");
                    }
                }
            }

            Trace("Check", "Sono nella funzione GetDefaultAddress"); /** <------------< TRACE >------------ */

            var fetchDefaultAddress = $@"<?xml version=""1.0"" encoding=""utf-16""?>
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
            Trace("fetchDefaultAddress", fetchDefaultAddress);

            EntityCollection defaultAddressCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchDefaultAddress));

            if (defaultAddressCollection.Entities.Count > 0)
            {
                return defaultAddressCollection.Entities[0];
            }
            return null;
        }
        public static void CreateNewDefaultAddress(Entity target, CrmServiceProvider crmServiceProvider,
            string indirizzo,
            string città,
            string CAP,
            string provincia = "",
            string località = "",
            EntityReference nazione = null)
        {
            void Trace(string key, object value)
            {
                //TRACE TOGGLE
                bool isTraceActive = true;
                {
                    if (isTraceActive)
                    {
                        key = string.Concat(key.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
                        value = value.ToString();
                        crmServiceProvider.TracingService.Trace($"{key}: {value}");
                    }
                }
            }

            Trace("Check", "Sono nella funzione CreateNewDefaultAddress"); /** <------------< TRACE >------------ */


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

            crmServiceProvider.Service.Create(enAddress);
        }
    }
}
