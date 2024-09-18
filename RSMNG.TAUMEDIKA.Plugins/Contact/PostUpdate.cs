using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Contact
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
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

                #region Crea indirizzo di default
                PluginRegion = "Crea indirizzo di default";

                /**
                 * controllo che i campi Indirizzo, Città e CAP siano valorizzati
                 * se almeno uno è valorizzato chiamo il metodo per controllare la presenza di altri address
                 * se non ve ne sono, viene creato un nuovo indirizzo con i valori dei suddetti campi 
                 * e viene settato come indirizzo di default
                 */
                target.TryGetAttributeValue<string>(DataModel.contact.address1_name, out string address);
                target.TryGetAttributeValue<string>(DataModel.contact.address1_city, out string city);
                target.TryGetAttributeValue<string>(DataModel.contact.address1_postalcode, out string postalcode);


                if (!string.IsNullOrEmpty(address) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(postalcode))
                {
                    /**
                     * creo il record di Address e lo valorizzo con i values passati al metodo come argomenti
                     */
                    Guid addressId = Utility.CreateNewDefaultAddress(target,crmServiceProvider.Service,
                        address ?? preImage.GetAttributeValue<string>(DataModel.contact.address1_name),
                        city ?? preImage.GetAttributeValue<string>(DataModel.contact.address1_city),
                        postalcode ?? preImage.GetAttributeValue<string>(DataModel.contact.address1_postalcode)
                        );

                    //controllo se c'è già un indirizzo di default
                    EntityCollection addresses = Utility.CheckDefaultAddress(crmServiceProvider, target.Id, addressId);

                    if (addresses.Entities.Count > 0)
                    {
                        foreach (var duplicate in addresses.Entities)
                        {
                            duplicate[DataModel.res_address.res_isdefault] = false;
                            crmServiceProvider.Service.Update(duplicate);
                        }
                    }
                }
                #endregion

                #region Gestisci permesso

                IPluginExecutionContext context = crmServiceProvider.PluginContext as IPluginExecutionContext;
                if (context != null)
                {
                    Guid systemUserId = context.UserId;
                    if (systemUserId != Guid.Empty)
                    {
                        try
                        {
                            Helper.CascadeSharingPermissions(DataModel.contact.logicalName, preImage.Id, systemUserId, crmServiceProvider.Service);
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

