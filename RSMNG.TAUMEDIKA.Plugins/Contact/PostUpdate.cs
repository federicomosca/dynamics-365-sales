using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"{target.LogicalName}: {PluginStage}, {PluginMessage}"); }

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                #region Creazione/aggiornamento Indirizzo scheda cliente
                PluginRegion = "Creazione/aggiornamento Indirizzo scheda cliente";

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
                    EntityCollection indirizzi = Utility.GetAddresses(crmServiceProvider, target.Id);
                    bool isAlreadyDefaultAddress = false;
                    Entity indirizzo = null;

                    //indirizzi = 0 > creo indirizzo con Indirizzo scheda cliente = true e Default = true
                    if (indirizzi.Entities.Count == 0)
                    {
                        if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Results == 0"); }

                        Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress, preImage);
                    }

                    //indirizzi = 1 > Indirizzo scheda cliente == false ? lo creo : lo aggiorno
                    else if (indirizzi.Entities.Count == 1)
                    {
                        if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Results == 1"); }

                        indirizzo = indirizzi.Entities[0];

                        if (PluginActiveTrace) { foreach (var x in indirizzo.Attributes) { crmServiceProvider.TracingService.Trace($"{x.Key}: {x.Value}"); } }

                        bool isIndirizzoSchedaCliente = indirizzo.GetAttributeValue<bool>(res_address.res_iscustomeraddress);

                        if (!isIndirizzoSchedaCliente)
                        {
                            isAlreadyDefaultAddress = true;
                            Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress, preImage);
                        }
                        else
                        {
                            Utility.UpdateCustomerAddress(crmServiceProvider, target, preImage, indirizzo.Id);
                        }
                    }

                    //indirizzi = 2 > linq .Where(Indirizzo scheda cliente == true) e lo aggiorno
                    else if (indirizzi.Entities.Count == 2)
                    {
                        if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Results == 2"); }

                        indirizzo = indirizzi.Entities.SingleOrDefault(address => address.GetAttributeValue<bool>(res_address.res_iscustomeraddress) == true);

                        if (PluginActiveTrace) { foreach (var x in indirizzo.Attributes) { crmServiceProvider.TracingService.Trace($"{x.Key}: {x.Value}"); } }

                        Utility.UpdateCustomerAddress(crmServiceProvider, target, preImage, indirizzo.Id);
                    }
                }
                #endregion
            }
        }
    }
}

