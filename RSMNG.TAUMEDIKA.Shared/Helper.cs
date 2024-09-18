using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA
{
    public class Helper
    {
        public static void CascadeSharingPermissions(string customerLogicalName, Guid customerId, Guid userId, IOrganizationService service)
        {
            /**
             * questa funzione prende in ingresso logical name e id dell'entità (contact o account) su cui si è effettuata l'operazione di share,
             * l'id dell'utente (o team) corrente e il service,
             * recupera i permessi della stessa, recupera gli address correlati 
             * e gli assegna gli stessi permessi (update)
             */
            AccessRights customerRights = GetEntityPermissions(customerLogicalName, customerId, userId, service);

            var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""res_address"">
                                <filter>
                                  <condition attribute=""res_customerid"" operator=""eq"" value=""{customerId}"" />
                                  <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                </filter>
                              </entity>
                            </fetch>";
            EntityCollection addresses = service.RetrieveMultiple(new FetchExpression(fetchAddresses));
            foreach(Entity address in addresses.Entities)
            {
                SharePermissionsWithAddress(address.Id, userId, customerRights, service);
            }
        }

        private static AccessRights GetEntityPermissions(string logicalName, Guid entityId, Guid principalId, IOrganizationService service)
        {
            var accessRequest = new RetrievePrincipalAccessRequest
            {
                Target = new EntityReference(logicalName, entityId),
                Principal = new EntityReference("systemuser", principalId)
            };

            var accessResponse = (RetrievePrincipalAccessResponse)service.Execute(accessRequest);

            return accessResponse.AccessRights;
        }

        private static void SharePermissionsWithAddress(Guid addressId, Guid userId, AccessRights permissions, IOrganizationService service)
        {
            var grantRequest = new GrantAccessRequest
            {
                Target = new EntityReference(DataModel.res_address.logicalName, addressId), // Nome logico dell'entità Address
                PrincipalAccess = new PrincipalAccess
                {
                    Principal = new EntityReference("systemuser", userId), // Utente a cui assegnare i permessi
                    AccessMask = permissions // Copia i permessi del customer sull'indirizzo
                }
            };

            service.Execute(grantRequest); // Esegui l'operazione di condivisione
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
