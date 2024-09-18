using Microsoft.Xrm.Sdk;
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
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.contact.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
                Entity postImage = target.GetPostImage(preImage);

                #region Gestisci permesso

                IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;
                if (context != null)
                {
                    Guid systemUserId = context.UserId;
                    if (systemUserId != Guid.Empty)
                    {
                        try
                        {
                            Utility.CascadeSharingPermissions(DataModel.contact.logicalName, preImage.Id, systemUserId, crmServiceProvider.Service);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidPluginExecutionException($"Error in CascadeSharingPermissions: {ex.Message}");
                        }
                    }
                    else
                    {
                        throw new Exception("System User Id not found");
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("PluginContext is not of type IPluginExecutionContext.");
                }

                #endregion
            }
        }
    }
}

