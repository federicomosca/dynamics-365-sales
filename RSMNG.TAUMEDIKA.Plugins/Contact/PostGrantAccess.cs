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
            IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is EntityReference target)
            {
                #region Gestisci permesso

                //qui definisco il record padre che sta condividendo i permessi
                Guid contactId = target.Id;

                if (context.InputParameters.Contains("PrincipalAccess") && context.InputParameters["PrincipalAccess"] is PrincipalAccess principalAccess)
                {
                    //qui definisco l'utente (o team) a cui sto trasferendo i permessi
                    Guid principalId = principalAccess.Principal.Id;
                    var principal = new EntityReference("systemuser", principalId);

                    //qui recupero i permessi selezionati nel record padre
                    AccessRights grantedAccessRights = principalAccess.AccessMask;

                    EntityCollection addresses = Utility.CascadeSharingPermissions(contactId, crmServiceProvider.Service);

                    if (addresses.Entities.Count > 0)
                    {
                        foreach (var address in addresses.Entities)
                        {
                            crmServiceProvider.Service.GrantAccess(principal, target, grantedAccessRights);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

