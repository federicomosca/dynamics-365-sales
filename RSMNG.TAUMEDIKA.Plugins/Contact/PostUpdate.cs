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
            PluginActiveTrace = true;
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
                    //recupero Indirizzo, Città e CAP
                    target.TryGetAttributeValue<string>(contact.address1_name, out string indirizzo);
                    target.TryGetAttributeValue<string>(contact.address1_city, out string città);
                    target.TryGetAttributeValue<string>(contact.address1_postalcode, out string CAP);

                    //recupero gli eventuali valori facoltativi dei campi Provincia, Località, Nazione
                    target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out string provincia);
                    target.TryGetAttributeValue<string>(contact.res_location, out string località);
                    target.TryGetAttributeValue<EntityReference>(contact.res_countryid, out EntityReference nazione);

                    //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                    EntityCollection linkedAddressesCollection = Utility.GetLinkedAddresses(crmServiceProvider, target.Id);

                    //se non trovo nemmeno un indirizzo
                    if (linkedAddressesCollection.Entities.Count == 0)
                    {
                        //creo il nuovo indirizzo con indirizzo scheda cliente si e default si
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
                        //ho trovato almeno un indirizzo
                        Entity defaultAddress = linkedAddressesCollection.Entities[0];

                        //se è indirizzo scheda cliente = true
                        defaultAddress.TryGetAttributeValue<bool>(res_address.res_iscustomeraddress, out bool isCustomerAddress);
                        defaultAddress.TryGetAttributeValue<bool>(res_address.res_isdefault, out bool isDefault);

                        if (isCustomerAddress)
                        {
                            //se è indirizzo scheda cliente, aggiorno coi nuovi dati e imposto default a false se c'è già un indirizzo di default
                            Utility.UpdateDefaultAddress(crmServiceProvider, target, preImage, defaultAddress, isDefault);
                        }
                        else
                        {
                            //se non è indirizzo scheda cliente, è per forza default (altrimenti la fetch non l'avrebbe trovato)
                            Utility.CreateNewDefaultAddress(target, crmServiceProvider,
                            !string.IsNullOrEmpty(indirizzo) ? indirizzo : preImage.GetAttributeValue<string>(contact.address1_name),
                            !string.IsNullOrEmpty(città) ? città : preImage.GetAttributeValue<string>(contact.address1_city),
                            !string.IsNullOrEmpty(CAP) ? CAP : preImage.GetAttributeValue<string>(contact.address1_postalcode),
                            !string.IsNullOrEmpty(provincia) ? provincia : preImage.GetAttributeValue<string>(contact.address1_stateorprovince),
                            !string.IsNullOrEmpty(località) ? località : preImage.GetAttributeValue<string>(contact.res_location),
                            nazione ?? preImage.GetAttributeValue<EntityReference>(contact.res_countryid)
                            );
                        }
                    }
                }
                #endregion
            }
        }
    }
}

