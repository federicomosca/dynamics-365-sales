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
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference targetEntity)
            {
                Guid contactId = targetEntity.Id; // Questo è l'accountId
                Guid userId = context.UserId;

                var grantAccessRequest = new GrantAccessRequest
            {
                Target = new EntityReference(DataModel.contact.logicalName, contactId), // Il record da condividere
                PrincipalAccess = new PrincipalAccess
                {
                    Principal = new EntityReference("systemuser", userId), // Utente o team
                    AccessMask = AccessRights.ReadAccess // Tipo di permessi da concedere
                }
            };

            // Esegui la richiesta
            crmServiceProvider.Service.Execute(grantAccessRequest);
            }

            //IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;
            //if (context != null)
            //{
            //    if (systemUserId != Guid.Empty)
            //    {
            //        try
            //        {
            //            Utility.CascadeSharingPermissions(DataModel.contact.logicalName, target.Id, systemUserId, crmServiceProvider.Service);
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new InvalidPluginExecutionException($"Error in CascadeSharingPermissions: {ex.Message}");
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception("System User Id not found");
            //    }
            //}
            //else
            //{
            //    throw new InvalidPluginExecutionException("PluginContext is not of type IPluginExecutionContext.");
            //}

            #endregion
        }
    }
}

