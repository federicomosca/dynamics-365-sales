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

        public static Dictionary<string, string> accountToAddressMandatoryFieldsMapping = new Dictionary<string, string>
        {
            {account.address1_line1, res_address.res_addressField},
            {account.address1_city, res_address.res_city},
            {account.address1_postalcode, res_address.res_postalcode}
        };

        public static Dictionary<string, string> accountToAddressOptionalFieldsMapping = new Dictionary<string, string>
        {
            {account.address1_stateorprovince, res_address.res_province},
            {account.res_location, res_address.res_location},
            {account.res_countryid, res_address.res_countryid}
        };

        public static Dictionary<string, string> contactToAddressMandatoryFieldsMapping = new Dictionary<string, string>
        {
            {account.address1_name, res_address.res_addressField},
            {account.address1_city, res_address.res_city},
            {account.address1_postalcode, res_address.res_postalcode}
        };

        public static Dictionary<string, string> contactToAddressOptionalFieldsMapping = new Dictionary<string, string>
        {
            {account.address1_stateorprovince, res_address.res_province},
            {account.res_location, res_address.res_location},
            {account.res_countryid, res_address.res_countryid}
        };

        public static EntityCollection GetAddresses(CrmServiceProvider crmServiceProvider, Guid customerIdString)
        {
            //crmServiceProvider.TracingService.Trace("Sono nella funzione GetLinkedAddresses"); /** <------------< TRACE >------------ */

            var fetchLinkedAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""{res_address.logicalName}"">
                                <filter type=""and"">
                                  <condition attribute=""{res_address.statecode}"" operator=""eq"" value=""0"" />
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
        public static EntityCollection GetAddresses(CrmServiceProvider crmServiceProvider, Guid customerIdString, Guid updatedAddressId)
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

        public static void CreateCustomerAddress(CrmServiceProvider crmServiceProvider, Entity parent, bool isAlreadyDefaultAddress, Entity parentPreImage = null)
        {
            Dictionary<string, string> mandatoryFieldsMapping = null;
            Dictionary<string, string> optionalFieldsMapping = null;

            //mappatura dei campi obbligatori o facoltativi di ACCOUNT
            if (parent.LogicalName == "account")
            {
                mandatoryFieldsMapping = accountToAddressMandatoryFieldsMapping;
                optionalFieldsMapping = accountToAddressOptionalFieldsMapping;
            }

            //mappatura dei campi obbligatori o facoltativi di CONTACT
            if (parent.LogicalName == "contact")
            {
                mandatoryFieldsMapping = contactToAddressMandatoryFieldsMapping;
                optionalFieldsMapping = contactToAddressOptionalFieldsMapping;
            }

            Entity defaultAddress = new Entity(res_address.logicalName);

            //valorizzo i campi obbligatori, se sono stati cancellati con un work-around li prendo dalla parentPreImage
            foreach (var field in mandatoryFieldsMapping)
            {
                string customerField = field.Key;
                string addressField = field.Value;

                parent.TryGetAttributeValue<object>(customerField, out var customerValue);

                defaultAddress[addressField] = customerValue ?? parentPreImage.GetAttributeValue<object>(customerField);
            }

            //valorizzo i campi facoltativi, se sono stati cancellati, svuoto i campi
            foreach (var field in optionalFieldsMapping)
            {
                string customerField = field.Key;
                string addressField = field.Value;

                parent.TryGetAttributeValue<object>(customerField, out var customerValue);

                defaultAddress[addressField] = customerValue ?? parentPreImage.GetAttributeValue<object>(customerField);
            }

            //link col customer
            defaultAddress[res_address.res_customerid] = new EntityReference(parent.LogicalName, parent.Id);

            //flag
            defaultAddress[res_address.res_iscustomeraddress] = true;

            //se non c'è già un indirizzo con Default = SI, imposto il nuovo indirizzo a Default = SI
            defaultAddress[res_address.res_isdefault] = !isAlreadyDefaultAddress;

            crmServiceProvider.Service.Create(defaultAddress);
        }

        public static void UpdateCustomerAddress(CrmServiceProvider crmServiceProvider, Entity parent, Entity parentPreImage, Guid customerAddressId)
        {
            Dictionary<string, string> mandatoryFieldsMapping = null;
            Dictionary<string, string> optionalFieldsMapping = null;

            //mappatura dei campi obbligatori o facoltativi di ACCOUNT
            if (parent.LogicalName == "account")
            {
                mandatoryFieldsMapping = accountToAddressMandatoryFieldsMapping;
                optionalFieldsMapping = accountToAddressOptionalFieldsMapping;
            }

            //mappatura dei campi obbligatori o facoltativi di CONTACT
            if (parent.LogicalName == "contact")
            {
                mandatoryFieldsMapping = contactToAddressMandatoryFieldsMapping;
                optionalFieldsMapping = contactToAddressOptionalFieldsMapping;
            }

            Entity customerAddress = new Entity(res_address.logicalName, customerAddressId);

            //valorizzo i campi obbligatori, se sono stati cancellati con un work-around li prendo dalla parentPreImage
            foreach (var field in mandatoryFieldsMapping)
            {
                string customerField = field.Key;
                string addressField = field.Value;

                parent.TryGetAttributeValue<object>(customerField, out var customerValue);

                customerAddress[addressField] = customerValue ?? parentPreImage.GetAttributeValue<object>(customerField);
            }

            //valorizzo i campi facoltativi, se sono stati cancellati, svuoto i campi
            foreach (var field in optionalFieldsMapping)
            {
                string customerField = field.Key;
                string addressField = field.Value;

                parent.TryGetAttributeValue<object>(customerField, out var customerValue);

                customerAddress[addressField] = customerValue ?? parentPreImage.GetAttributeValue<object>(customerField);
            }

            crmServiceProvider.Service.Update(customerAddress);
        }
    }
}
