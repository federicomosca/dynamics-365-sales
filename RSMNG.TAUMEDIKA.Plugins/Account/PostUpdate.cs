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
                    #region Creazione/aggiornamento Indirizzo scheda cliente
                    PluginRegion = "Creazione/aggiornamento Indirizzo scheda cliente";

                    List<string> campiIndirizzo = new List<string>{
                        account.address1_line1,
                        account.address1_city,
                        account.address1_postalcode,
                        account.address1_stateorprovince,
                        account.res_location,
                        account.res_countryid
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
                            Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress, preImage);
                        }

                        //indirizzi = 1 > Indirizzo scheda cliente == false ? lo creo : lo aggiorno
                        else if (indirizzi.Entities.Count == 1)
                        {
                            indirizzo = indirizzi.Entities[0];
                            bool isIndirizzoSchedaCliente = indirizzo.GetAttributeValue<bool>(res_address.res_iscustomeraddress);

                            if (!isIndirizzoSchedaCliente)
                            {
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
                            indirizzo = indirizzi.Entities.SingleOrDefault(address => address.GetAttributeValue<bool>(res_address.res_iscustomeraddress) == true);
                            Utility.UpdateCustomerAddress(crmServiceProvider, target, preImage, indirizzo.Id);
                        }
                    }
                    #endregion
                }
            }
        }
    }
}
