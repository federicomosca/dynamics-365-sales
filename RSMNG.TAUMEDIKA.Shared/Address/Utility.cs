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
        public static EntityCollection GetLinkedAddresses(CrmServiceProvider crmServiceProvider, Guid customerIdString, Guid? updatedAddressId = null)
        {
            crmServiceProvider.TracingService.Trace("Sono nella funzione GetDefaultAddresses"); /** <------------< TRACE >------------ */

            var fetchDefaultAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""{res_address.logicalName}"">
                                <filter type=""and"">
                                  <condition attribute=""{res_address.statecode}"" operator=""eq"" value=""0"" />
                                  <condition attribute=""{res_address.res_customerid}"" operator=""eq"" value=""{customerIdString}"" />
                                  <condition attribute=""{res_address.res_addressid}"" operator=""ne"" value=""{updatedAddressId}"" />
                                </filter>
                                <filter type=""or"">
                                  <condition attribute=""{res_address.res_isdefault}"" operator=""eq"" value=""1"" />
                                  <condition attribute=""{res_address.res_iscustomeraddress}"" operator=""eq"" value=""1"" />
                                </filter>
                              </entity>
                            </fetch>";
            crmServiceProvider.TracingService.Trace(fetchDefaultAddresses);
            return crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchDefaultAddresses));
        }

        public static void CreateNewDefaultAddress(Entity target, CrmServiceProvider crmServiceProvider,
            string indirizzo,
            string città,
            string CAP,
            string provincia = "",
            string località = "",
            EntityReference nazione = null)
        {
            crmServiceProvider.TracingService.Trace("Check", "Sono nella funzione CreateNewDefaultAddress"); /** <------------< TRACE >------------ */


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

        public static void UpdateDefaultAddress(CrmServiceProvider crmServiceProvider, Entity target, Entity preImage, Entity defaultAddress, bool isAlreadyDefaultAddress)
        {
            if (target.LogicalName == "account")
            {
                //recupero Indirizzo, Città e CAP
                target.TryGetAttributeValue<string>(account.address1_line1, out string indirizzo);
                target.TryGetAttributeValue<string>(account.address1_city, out string città);
                target.TryGetAttributeValue<string>(account.address1_postalcode, out string CAP);

                //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                target.TryGetAttributeValue<string>(account.address1_stateorprovince, out string provincia);
                target.TryGetAttributeValue<string>(account.res_location, out string località);
                target.TryGetAttributeValue<EntityReference>(account.res_countryid, out EntityReference nazione);

                defaultAddress[res_address.res_addressField] = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(account.address1_line1);
                defaultAddress[res_address.res_city] = !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(account.address1_city);
                defaultAddress[res_address.res_postalcode] = !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(account.address1_postalcode);
                defaultAddress[res_address.res_province] = !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(account.address1_stateorprovince);
                defaultAddress[res_address.res_location] = !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(account.res_location);
                defaultAddress[res_address.res_countryid] = nazione ?? preImage.GetAttributeValue<EntityReference>(account.res_countryid);
                if (isAlreadyDefaultAddress) { defaultAddress[res_address.res_isdefault] = false; }
            }

            if (target.LogicalName == "contact")
            {
                //recupero Indirizzo, Città e CAP
                target.TryGetAttributeValue<string>(contact.address1_name, out string indirizzo);
                target.TryGetAttributeValue<string>(contact.address1_city, out string città);
                target.TryGetAttributeValue<string>(contact.address1_postalcode, out string CAP);

                //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out string provincia);
                target.TryGetAttributeValue<string>(contact.res_location, out string località);
                target.TryGetAttributeValue<EntityReference>(contact.res_countryid, out EntityReference nazione);

                defaultAddress[res_address.res_addressField] = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(contact.address1_name);
                defaultAddress[res_address.res_city] = !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(contact.address1_city);
                defaultAddress[res_address.res_postalcode] = !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(contact.address1_postalcode);
                defaultAddress[res_address.res_province] = !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(contact.address1_stateorprovince);
                defaultAddress[res_address.res_location] = !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(contact.res_location);
                defaultAddress[res_address.res_countryid] = nazione ?? preImage.GetAttributeValue<EntityReference>(contact.res_countryid);
                if (isAlreadyDefaultAddress) { defaultAddress[res_address.res_isdefault] = false; }
            }

            crmServiceProvider.Service.Update(defaultAddress);
        }
    }
}
