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
                    #region Creazione/aggiornamento indirizzo di default
                    PluginRegion = "Creazione/aggiornamento indirizzo di default";

                    bool isAlreadyDefaultAddress = false;

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
                        EntityCollection linkedAddressesCollection = Utility.GetLinkedAddresses(crmServiceProvider, target.Id);

                        //se non trovo nemmeno un indirizzo
                        if (linkedAddressesCollection.Entities.Count == 0)
                        {
                            //creo il nuovo indirizzo con indirizzo scheda cliente si e default si
                            Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress, preImage);
                        }
                        else
                        {
                            //ho trovato almeno un indirizzo, può essere indirizzo scheda cliente

                            //

                            Entity customerAddress = linkedAddressesCollection.Entities[0];
                            Utility.UpdateCustomerAddress(crmServiceProvider, target, preImage, customerAddress.Id);
                        }
                    }
                    #endregion
                }
            }
        }
    }
}
