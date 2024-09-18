using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Account
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.account.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                if (preImage != null && preImage.Contains("accountid"))
                {
                    Entity postImage = target.GetPostImage(preImage);

                    string accountId = preImage.Id.ToString();

                    #region Crea indirizzo di default
                    PluginRegion = "Crea indirizzo di default";

                    /**
                     * controllo che i campi Indirizzo, Città e CAP siano valorizzati
                     * se almeno uno è valorizzato chiamo il metodo per controllare la presenza di altri address
                     * se non ve ne sono, viene creato un nuovo indirizzo con i valori dei suddetti campi 
                     * e viene settato come indirizzo di default
                     */
                    postImage.TryGetAttributeValue<string>(DataModel.account.address1_line1, out string address);
                    postImage.TryGetAttributeValue<string>(DataModel.account.address1_city, out string city);
                    postImage.TryGetAttributeValue<string>(DataModel.account.address1_postalcode, out string postalcode);

                    if (!string.IsNullOrEmpty(address) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(postalcode))
                    {
                        Guid addressId = Utility.CreateNewDefaultAddress(target, crmServiceProvider.Service,
                        address ?? preImage.GetAttributeValue<string>(DataModel.account.address1_name),
                        city ?? preImage.GetAttributeValue<string>(DataModel.account.address1_city),
                        postalcode ?? preImage.GetAttributeValue<string>(DataModel.account.address1_postalcode)
                        );

                        //controllo se c'è già un indirizzo di default
                        EntityCollection addresses = Utility.GetDefaultAddress(crmServiceProvider, target.Id, addressId);

                        if (addresses.Entities.Count > 0)
                        {
                            foreach (var duplicate in addresses.Entities)
                            {
                                duplicate[DataModel.res_address.res_isdefault] = false;
                                crmServiceProvider.Service.Update(duplicate);
                            }
                        }
                        #endregion
                    }
                }
            }
        }
    }
}
