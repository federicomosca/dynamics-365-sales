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
            #region Trace Activation Method
            bool isFirstExecute = true;
            void Trace(string key, object value)
            {
                bool isTraceActive = false;
                if (isFirstExecute)
                {
                    crmServiceProvider.TracingService.Trace($"TRACE IS ACTIVE: {isTraceActive}");

                    isFirstExecute = false;
                }
                if (isTraceActive) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            #endregion

            IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference target)
            {
                #region Gestisci permesso

                //qui definisco il record padre che sta condividendo i permessi
                Guid contactId = target.Id;
                Trace("Contact ID", contactId);

                if (context.InputParameters.Contains("PrincipalAccess") && context.InputParameters["PrincipalAccess"] is PrincipalAccess principalAccess)
                {
                    //qui definisco l'utente (o team) a cui sto trasferendo i permessi
                    Guid principalId = principalAccess.Principal.Id;
                    string principalLogicalName = principalAccess.Principal.LogicalName;
                    var principal = new EntityReference(principalLogicalName, principalId);

                    Trace("Principal ID", principalId);
                    Trace("Principal_logical_name", principalLogicalName);

                    //qui recupero i permessi selezionati nel record padre
                    AccessRights rights = principalAccess.AccessMask;
                    Trace("Rights", rights);

                    var fetchAddresses = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""{res_address.logicalName}"">
                                <filter>
                                  <condition attribute=""{res_address.statecode}"" operator=""eq"" value=""{res_address.statecodeValues.Attivo}"" />
                                  <condition attribute=""{res_address.res_customerid}"" operator=""eq"" value=""{contactId}"" />
                                </filter>
                              </entity>
                            </fetch>";
                    EntityCollection addresses = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAddresses));

                    if (addresses.Entities.Count > 0)
                    {
                        Trace("Check", "Fetched results.");
                        foreach (var address in addresses.Entities)
                        {
                            Trace("address_id", address.Id);
                            crmServiceProvider.Service.GrantAccess(principal, target, rights);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

