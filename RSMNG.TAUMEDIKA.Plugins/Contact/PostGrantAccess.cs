using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
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
            crmServiceProvider.TracingService.Trace("I'm in the GrantAccess plugin");

            #region Gestisci permesso

            // Ottieni il contesto del plugin
            IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;

            // Verifica se è presente un parametro "Target" nella richiesta
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference target)
            {
                //qui definisco il record padre che sta condividendo i permessi
                Guid contactId = target.Id;
                var recordToShare = new EntityReference(DataModel.contact.logicalName, contactId);

                if (context.InputParameters.Contains("PrincipalAccess") && context.InputParameters["PrincipalAccess"] is PrincipalAccess principalAccess)
                {
                    //qui definisco l'utente (o team) a cui sto trasferendo i permessi
                    Guid recipientId = principalAccess.Principal.Id;

                    //qui recupero i permessi selezionati nel record padre
                    AccessRights grantedAccessRights = principalAccess.AccessMask;

                    EntityCollection addresses = Utility.CascadeSharingPermissions(contactId, crmServiceProvider.Service);

                    if (addresses.Entities.Count > 0)
                    {
                        foreach (var child in addresses.Entities)
                        {
                            var grantChildAccessRequest = new GrantAccessRequest
                            {
                                Target = new EntityReference(DataModel.res_address.logicalName, child.Id), // Record figlio
                                PrincipalAccess = new PrincipalAccess
                                {
                                    Principal = new EntityReference("systemuser", recipientId), // Utente destinatario
                                    AccessMask = grantedAccessRights // Permessi trasferiti
                                }
                            };
                            crmServiceProvider.Service.Execute(grantChildAccessRequest);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

