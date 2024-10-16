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

                #region Creazione/aggiornamento indirizzo di default
                PluginRegion = "Creazione/aggiornamento indirizzo di default";

                List<string> campiIndirizzo = new List<string>{
                        contact.address1_name,
                        contact.address1_city,
                        contact.address1_postalcode,
                        contact.address1_stateorprovince,
                        contact.res_location,
                        contact.res_countryid
                    };

                bool isAddressUpdated = false;
                foreach (string campoModificato in campiIndirizzo)
                {
                    if (target.Contains(campoModificato))
                    {
                        isAddressUpdated = true;
                        break;
                    }
                }

                //se almeno uno dei valori è stato modificato...
                if (isAddressUpdated)
                {
                    //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                    Entity defaultAddress = Utility.GetDefaultAddress(crmServiceProvider, target.Id);

                    //se non trovo nemmeno un indirizzo
                    if (defaultAddress == null)
                    {
                        //recupero Indirizzo, Città e CAP
                        target.TryGetAttributeValue<string>(contact.address1_name, out string indirizzo);
                        target.TryGetAttributeValue<string>(contact.address1_city, out string città);
                        target.TryGetAttributeValue<string>(contact.address1_postalcode, out string CAP);

                        //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                        target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out string provincia);
                        target.TryGetAttributeValue<string>(contact.res_location, out string località);
                        target.TryGetAttributeValue<EntityReference>(contact.res_countryid, out EntityReference nazione);

                        //creo il nuovo indirizzo di default
                        Utility.CreateNewDefaultAddress(target, crmServiceProvider,
                            !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(contact.address1_name),
                            !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(contact.address1_city),
                            !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(contact.address1_postalcode),
                            !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(contact.address1_stateorprovince),
                            !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(contact.res_location),
                            nazione ?? preImage.GetAttributeValue<EntityReference>(contact.res_countryid)
                            );
                    }
                    else
                    {
                        //se l'indirizzo di default già esiste lo aggiorno

                        //recupero Indirizzo, Città e CAP
                        target.TryGetAttributeValue<string>(contact.address1_name, out string indirizzo);
                        target.TryGetAttributeValue<string>(contact.address1_city, out string città);
                        target.TryGetAttributeValue<string>(contact.address1_postalcode, out string CAP);

                        //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                        target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out string provincia);
                        target.TryGetAttributeValue<string>(contact.res_location, out string località);
                        target.TryGetAttributeValue<EntityReference>(contact.res_countryid, out EntityReference nazione);

                        defaultAddress[res_address.res_addressField] = !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(contact.address1_name);
                        defaultAddress[res_address.res_city] = !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(contact.address1_city);
                        defaultAddress[res_address.res_postalcode] = !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(contact.address1_postalcode);
                        defaultAddress[res_address.res_province] = !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(contact.address1_stateorprovince);
                        defaultAddress[res_address.res_location] = !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(contact.res_location);
                        defaultAddress[res_address.res_countryid] = nazione ?? preImage.GetAttributeValue<EntityReference>(contact.res_countryid);

                        crmServiceProvider.Service.Update(defaultAddress);
                    }
                }
                #endregion
            }
        }
    }
}

