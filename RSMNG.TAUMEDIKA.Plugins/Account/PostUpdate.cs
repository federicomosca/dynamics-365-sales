using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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

                    #region Creazione/aggiornamento indirizzo di default
                    PluginRegion = "Creazione/aggiornamento indirizzo di default";

                    //recupero Indirizzo, Città e CAP
                    target.TryGetAttributeValue<string>(account.address1_name, out string indirizzo);
                    target.TryGetAttributeValue<string>(account.address1_city, out string città);
                    target.TryGetAttributeValue<string>(account.address1_postalcode, out string CAP);

                    //se uno dei tre è stato modificato...
                    if (!string.IsNullOrEmpty(indirizzo) || !string.IsNullOrEmpty(città) || !string.IsNullOrEmpty(CAP))
                    {
                        //recupero gli eventuali altri valori compilati nei campi Provincia, Località, Nazione
                        target.TryGetAttributeValue<string>(account.address1_stateorprovince, out string provincia);
                        target.TryGetAttributeValue<string>(account.res_location, out string località);
                        target.TryGetAttributeValue<EntityReference>(account.res_countryid, out EntityReference nazione);

                        //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                        EntityCollection defaultAddressCollection = Utility.GetDefaultAddress(crmServiceProvider, target.Id);

                        //se non trovo nemmeno un indirizzo
                        if (defaultAddressCollection.Entities.Count < 0)
                        {
                            //creo il nuovo indirizzo di default
                            Utility.CreateNewDefaultAddress(target, crmServiceProvider.Service,
                                !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(account.address1_name),
                                !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(account.address1_city),
                                !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(account.address1_postalcode),
                                !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(account.address1_stateorprovince),
                                !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(account.res_location),
                                nazione ?? preImage.GetAttributeValue<EntityReference>(account.res_countryid)
                                );
                        }
                        else
                        {
                            //se l'indirizzo di default già esiste lo aggiorno

                            Entity defaultAddress = defaultAddressCollection.Entities[0];

                            defaultAddress[res_address.res_addressField] = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(account.address1_name);
                            defaultAddress[res_address.res_city] = !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(account.address1_city);
                            defaultAddress[res_address.res_postalcode] = !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(account.address1_postalcode);
                            defaultAddress[res_address.res_province] = !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(account.address1_stateorprovince);
                            defaultAddress[res_address.res_location] = !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(account.res_location);
                            defaultAddress[res_address.res_countryid] = nazione ?? preImage.GetAttributeValue<EntityReference>(account.res_countryid);

                            crmServiceProvider.Service.Update(defaultAddress);
                        }
                    }
                    #endregion
                }
            }
        }
    }
}
