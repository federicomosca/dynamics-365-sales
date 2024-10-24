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

                #region Creazione/aggiornamento indirizzo di default
                PluginRegion = "Creazione/aggiornamento indirizzo di default";

                bool isAlreadyDefaultAddress = false;

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
                    //recupero gli indirizzi correlati
                    EntityCollection linkedAddressesCollection = Utility.GetLinkedAddresses(crmServiceProvider, target.Id);

                    //se non trovo nemmeno un indirizzo
                    if (linkedAddressesCollection.Entities.Count == 0)
                    {
                        //creo il nuovo indirizzo con indirizzo scheda cliente si e default si
                        Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress, preImage);
                    }
                    else
                    {
                        //ho trovato almeno un indirizzo, non può essere indirizzo scheda cliente
                        //dunque è per forza default

                        Entity linkedAddress = linkedAddressesCollection.Entities[0];

                        //dunque devo aggiornare l'indirizzo scheda cliente


                        //linkedAddress.TryGetAttributeValue<bool>(res_address.res_iscustomeraddress, out bool isCustomerAddress);
                        //linkedAddress.TryGetAttributeValue<bool>(res_address.res_isdefault, out bool isDefault);

                        ////se c'è già un indirizzo scheda cliente
                        //if (isCustomerAddress)
                        //{
                        //    //aggiorno coi nuovi dati
                        //    Utility.UpdateCustomerAddress(crmServiceProvider, target, preImage, linkedAddress);
                        //}
                        //else //se non c'è già un indirizzo scheda cliente
                        //{
                        //    isAlreadyDefaultAddress = true; //è per forza default, altrimenti la fetch non l'avrebbe trovato
                        //    Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress, preImage);
                        //}
                    }
                }
                #endregion
            }
        }
    }
}

