using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Address
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Gestione Indirizzo Default
            PluginRegion = "Gestione Indirizzo Default";

            postImage.TryGetAttributeValue<EntityReference>(res_address.res_customerid, out EntityReference erCustomer);
            target.TryGetAttributeValue<bool>(res_address.res_isdefault, out bool isDefault);

            if (isDefault)
            {
                Guid customerId = erCustomer != null ? erCustomer.Id : Guid.Empty;

                if (customerId != Guid.Empty)
                {
                    //controllo se ci sono indirizzi di default
                    EntityCollection defaultAddressesCollection = Utility.GetDefaultAddresses(crmServiceProvider, customerId, target.Id);

                    //se ci sono, imposto Default e Indirizzo scheda cliente a NO e faccio update
                    if (defaultAddressesCollection.Entities.Count > 0)
                    {
                        crmServiceProvider.TracingService.Trace("Check", "Esiste già un indirizzo Default = SI e Indirizzo scheda cliente = SI"); /** <------------< TRACE >------------ */

                        foreach (Entity duplicate in defaultAddressesCollection.Entities)
                        {
                            crmServiceProvider.TracingService.Trace("Indirizzo duplicato", duplicate); /** <------------< TRACE >------------ */
                            duplicate[res_address.res_isdefault] = false;
                            duplicate[res_address.res_iscustomeraddress] = false;
                            crmServiceProvider.Service.Update(duplicate);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

