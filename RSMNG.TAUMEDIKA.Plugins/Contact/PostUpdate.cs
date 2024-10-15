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
                 * se Indirizzo o Città o Cap sono valorizzati
                 * creo un nuovo indirizzo di default con i nuovi valori
                 * 
                 */
                target.TryGetAttributeValue<string>(DataModel.contact.address1_name, out string address);
                target.TryGetAttributeValue<string>(DataModel.contact.address1_city, out string city);
                target.TryGetAttributeValue<string>(DataModel.contact.address1_postalcode, out string postalcode);


                if (!string.IsNullOrEmpty(address) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(postalcode))
                {
                    //controllo se c'è già un indirizzo di default
                    EntityCollection addresses = Utility.GetDefaultAddress(crmServiceProvider, target.Id);

                    if (addresses.Entities.Count > 0)
                    {
                        foreach (var duplicate in addresses.Entities)
                        {
                            duplicate[DataModel.res_address.res_isdefault] = false;
                            crmServiceProvider.Service.Update(duplicate);
                        }
                    }

                    /**
                     * creo il record di Address e lo valorizzo con i values passati al metodo come argomenti
                     */
                    Utility.CreateNewDefaultAddress(target, crmServiceProvider.Service,
                        address ?? preImage.GetAttributeValue<string>(DataModel.contact.address1_name),
                        city ?? preImage.GetAttributeValue<string>(DataModel.contact.address1_city),
                        postalcode ?? preImage.GetAttributeValue<string>(DataModel.contact.address1_postalcode)
                        );
                }
                #endregion
            }
        }
    }
}

