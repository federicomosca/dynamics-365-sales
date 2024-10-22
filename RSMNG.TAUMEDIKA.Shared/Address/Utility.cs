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

        //recupero eventuali indirizzi attivi del cliente con Default = SI o Indirizzo scheda cliente = SI
        public static EntityCollection GetLinkedAddresses(CrmServiceProvider crmServiceProvider, Guid customerIdString, Guid? updatedAddressId = null)
        {
            //crmServiceProvider.TracingService.Trace("Sono nella funzione GetLinkedAddresses"); /** <------------< TRACE >------------ */

            var fetchLinkedAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""{res_address.logicalName}"">
                                <filter type=""and"">
                                  <condition attribute=""{res_address.statecode}"" operator=""eq"" value=""0"" />
                                  <condition attribute=""{res_address.res_addressid}"" operator=""ne"" value=""{updatedAddressId}"" />
                                  <condition attribute=""{res_address.res_customerid}"" operator=""eq"" value=""{customerIdString}"" />
                                </filter>
                                <filter type=""or"">
                                  <condition attribute=""{res_address.res_isdefault}"" operator=""eq"" value=""1"" />
                                  <condition attribute=""{res_address.res_iscustomeraddress}"" operator=""eq"" value=""1"" />
                                </filter>
                              </entity>
                            </fetch>";
            //crmServiceProvider.TracingService.Trace(fetchDefaultAddresses);
            return crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchLinkedAddresses));
        }

        public static void CreateNewDefaultAddress(CrmServiceProvider crmServiceProvider, Entity target, Entity preImage = null)
        {
            //crmServiceProvider.TracingService.Trace("Sono nella funzione CreateNewDefaultAddress"); /** <------------< TRACE >------------ */
            string indirizzo = string.Empty,
                città = string.Empty,
                CAP = string.Empty,
                provincia = string.Empty,
                località = string.Empty;

            EntityReference nazione = null;

            if (target.LogicalName == "account")
            {

                //recupero Indirizzo, Città e CAP
                target.TryGetAttributeValue<string>(account.address1_line1, out indirizzo);
                target.TryGetAttributeValue<string>(account.address1_city, out città);
                target.TryGetAttributeValue<string>(account.address1_postalcode, out CAP);

                //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                target.TryGetAttributeValue<string>(account.address1_stateorprovince, out provincia);
                target.TryGetAttributeValue<string>(account.res_location, out località);
                target.TryGetAttributeValue<EntityReference>(account.res_countryid, out nazione);

                indirizzo = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage?.GetAttributeValue<string>(account.address1_line1);
                città = !string.IsNullOrEmpty(città) ? città : preImage?.GetAttributeValue<string>(account.address1_city);
                CAP = !string.IsNullOrEmpty(CAP) ? CAP : preImage?.GetAttributeValue<string>(account.address1_postalcode);
                provincia = !string.IsNullOrEmpty(provincia) ? provincia : preImage?.GetAttributeValue<string>(account.address1_stateorprovince);
                località = !string.IsNullOrEmpty(località) ? località : preImage?.GetAttributeValue<string>(account.res_location);
                nazione = nazione ?? preImage?.GetAttributeValue<EntityReference>(account.res_countryid);
            }
            
            if (target.LogicalName == "contact")
            {

                //recupero Indirizzo, Città e CAP
                target.TryGetAttributeValue<string>(contact.address1_name, out indirizzo);
                target.TryGetAttributeValue<string>(contact.address1_city, out città);
                target.TryGetAttributeValue<string>(contact.address1_postalcode, out CAP);

                //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out provincia);
                target.TryGetAttributeValue<string>(contact.res_location, out località);
                target.TryGetAttributeValue<EntityReference>(contact.res_countryid, out nazione);

                indirizzo = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage?.GetAttributeValue<string>(contact.address1_name);
                città = !string.IsNullOrEmpty(città) ? città : preImage?.GetAttributeValue<string>(contact.address1_city);
                CAP = !string.IsNullOrEmpty(CAP) ? CAP : preImage?.GetAttributeValue<string>(contact.address1_postalcode);
                provincia = !string.IsNullOrEmpty(provincia) ? provincia : preImage?.GetAttributeValue<string>(contact.address1_stateorprovince);
                località = !string.IsNullOrEmpty(località) ? località : preImage?.GetAttributeValue<string>(contact.res_location);
                nazione = nazione ?? preImage?.GetAttributeValue<EntityReference>(contact.res_countryid);
            }

            Entity defaultAddress = new Entity(res_address.logicalName);
            defaultAddress[res_address.res_addressField] = indirizzo;
            defaultAddress[res_address.res_city] = città;
            defaultAddress[res_address.res_postalcode] = CAP;
            defaultAddress[res_address.res_province] = provincia;
            defaultAddress[res_address.res_location] = località;
            defaultAddress[res_address.res_countryid] = nazione;

            defaultAddress[res_address.res_customerid] = new EntityReference(target.LogicalName, target.Id);

            defaultAddress[res_address.res_iscustomeraddress] = true;
            defaultAddress[res_address.res_isdefault] = true;

            crmServiceProvider.Service.Create(defaultAddress);
        }

        public static void UpdateCustomerAddress(CrmServiceProvider crmServiceProvider, Entity target, Entity preImage, Entity customerAddress, bool isDefault)
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

                customerAddress[res_address.res_addressField] = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(account.address1_line1);
                customerAddress[res_address.res_city] = !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(account.address1_city);
                customerAddress[res_address.res_postalcode] = !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(account.address1_postalcode);
                customerAddress[res_address.res_province] = !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(account.address1_stateorprovince);
                customerAddress[res_address.res_location] = !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(account.res_location);
                customerAddress[res_address.res_countryid] = nazione ?? preImage.GetAttributeValue<EntityReference>(account.res_countryid);
                customerAddress[res_address.res_isdefault] = isDefault;
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

                customerAddress[res_address.res_addressField] = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(contact.address1_name);
                customerAddress[res_address.res_city] = !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(contact.address1_city);
                customerAddress[res_address.res_postalcode] = !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(contact.address1_postalcode);
                customerAddress[res_address.res_province] = !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(contact.address1_stateorprovince);
                customerAddress[res_address.res_location] = !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(contact.res_location);
                customerAddress[res_address.res_countryid] = nazione ?? preImage.GetAttributeValue<EntityReference>(contact.res_countryid);
                customerAddress[res_address.res_isdefault] = isDefault;
            }

            crmServiceProvider.Service.Update(customerAddress);
        }
    }
}
