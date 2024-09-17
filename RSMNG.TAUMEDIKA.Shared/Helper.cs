using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace RSMNG.TAUMEDIKA
{
    public class Helper
    {
        /**
        * fetcho gli address legati al customer
        * se non esiste nessun address, creo un nuovo record address e lo valorizzo con i values passati come argomenti al metodo
        * metto Default a true
        */
        public static void CheckAddress(RSMNG.CrmServiceProvider crmServiceProvider, string logicalName, string customerIdString, string address = "", string city = "", string postalcode = "")
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

            if (!results)
            {
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
        }
    }
    public class Model
    {
        public class BasicOutput
        {
            public int result { get; set; }
            public string message { get; set; }
        }
    }
}
