using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Contact
{
    public class PostCreate : RSMNG.BaseClass
    {
        public PostCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.contact.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            #region Trace Activation Method
            bool isFirstExecute = true;
            void Trace(string key, object value)
            {
                bool isTraceActive = false;
                if (isFirstExecute)
                {
                    crmServiceProvider.TracingService.Trace($"TRACE IS ACTIVE: {isTraceActive}");

                    object objectExample = new object();
                    Trace("Esempio", objectExample);
                    isFirstExecute = false;
                }
                if (isTraceActive) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            #endregion

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Crea indirizzo di default
            PluginRegion = "Crea indirizzo di default";

            /**
             * controllo che i campi Indirizzo, Città e CAP siano valorizzati
             * se almeno uno è valorizzato viene creato un nuovo indirizzo con i valori dei suddetti campi 
             * e viene settato come indirizzo di default
             */
            target.TryGetAttributeValue<string>(DataModel.contact.address1_name, out string address);
            target.TryGetAttributeValue<string>(DataModel.contact.address1_city, out string city);
            target.TryGetAttributeValue<string>(DataModel.contact.address1_postalcode, out string postalcode);

            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(postalcode))
            {
                /**
                 * creo il record di Address e lo valorizzo con i values passati al metodo come argomenti
                 */
                Utility.CreateNewDefaultAddress(target, crmServiceProvider.Service, address, city, postalcode);
            }
            #endregion
        }
    }
}
