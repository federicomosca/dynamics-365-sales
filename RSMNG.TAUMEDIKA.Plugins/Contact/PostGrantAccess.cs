using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Contact
{
    public class PostGrantAccess : RSMNG.BaseClass
    {
        public PostGrantAccess(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "GrantAccess";
            PluginPrimaryEntityName = DataModel.contact.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference target)
            {
                crmServiceProvider.TracingService.Trace("I GOT TARGET AND IT IS ER");
                #region Gestisci permesso

                //qui definisco il record padre che sta condividendo i permessi
                Guid contactId = target.Id;
                crmServiceProvider.TracingService.Trace($"Contact ID: {contactId}");

                if (context.InputParameters.Contains("PrincipalAccess") && context.InputParameters["PrincipalAccess"] is PrincipalAccess principalAccess)
                {
                    //qui definisco l'utente (o team) a cui sto trasferendo i permessi
                    Guid principalId = principalAccess.Principal.Id;
                    string principalLogicalName = principalAccess.Principal.LogicalName;
                    var principal = new EntityReference(principalLogicalName, principalId);

                    crmServiceProvider.TracingService.Trace($"Principal ID: {principalId}");
                    crmServiceProvider.TracingService.Trace($"Principal Logical Name: {principalLogicalName}");

                    //qui recupero i permessi selezionati nel record padre
                    AccessRights rights = principalAccess.AccessMask;
                    crmServiceProvider.TracingService.Trace($"Rights: {rights}");

                    var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""res_address"">
                                <filter>
                                  <condition attribute=""res_customerid"" operator=""eq"" value=""{contactId}"" />
                                  <condition attribute=""statecode"" operator=""eq"" value=""0"" />
                                </filter>
                              </entity>
                            </fetch>";
                    EntityCollection addresses = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses));

                    if (addresses.Entities.Count > 0)
                    {
                        crmServiceProvider.TracingService.Trace($"I've found addresses");

                        foreach (var address in addresses.Entities)
                        {
                            crmServiceProvider.TracingService.Trace($"Address ID: {address.Id}");
                            crmServiceProvider.Service.GrantAccess(principal, target, rights);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

