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
            void Trace(string key, object value)
            {
                //TRACE TOGGLE
                bool isTraceActive = true;
                {
                    if (isTraceActive)
                    {
                        key = string.Concat(key.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
                        value = value.ToString();
                        crmServiceProvider.TracingService.Trace($"{key}: {value}");
                    }
                }
            }

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
                    //controllo se c'è già un indirizzo di default
                    Entity defaultAddress = Utility.GetDefaultAddress(crmServiceProvider, customerId);

                    //se c'è
                    if (defaultAddress == null)
                    {
                        Trace("Check", "Esiste già un indirizzo Default = SI e Indirizzo scheda cliente = SI"); /** <------------< TRACE >------------ */

                        defaultAddress[res_address.res_isdefault] = false;
                        crmServiceProvider.Service.Update(defaultAddress);
                    }
                }
            }
            #endregion
        }
    }
}

