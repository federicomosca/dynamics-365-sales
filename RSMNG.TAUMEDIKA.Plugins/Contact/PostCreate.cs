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
            void Trace(string key, object value)
            {
                //TRACE TOGGLE
                bool isTraceActive = false;
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

            #region Creo indirizzo di default
            PluginRegion = "Creo indirizzo di default";

            //recupero Indirizzo, Città e CAP
            target.TryGetAttributeValue<string>(contact.address1_name, out string indirizzo);
            target.TryGetAttributeValue<string>(contact.address1_city, out string città);
            target.TryGetAttributeValue<string>(contact.address1_postalcode, out string CAP);

            //se sono stati valorizzati tutti e 3...
            if (!string.IsNullOrEmpty(indirizzo) && !string.IsNullOrEmpty(città) && !string.IsNullOrEmpty(CAP))
            {
                Trace("Check", "Indirizzo, Città e CAP sono stati valorizzati"); /** <------------< TRACE >------------ */


                //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                Entity defaultAddress = Utility.GetDefaultAddress(crmServiceProvider, target.Id);

                //se non trovo nemmeno un indirizzo
                if (defaultAddress == null)
                {
                    Trace("Check", "Non ho trovato un indirizzo Default = SI e Indirizzo scheda cliente = SI"); /** <------------< TRACE >------------ */

                    //recupero gli eventuali altri valori compilati nei campi Provincia, Località, Nazione
                    target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out string provincia);
                    target.TryGetAttributeValue<string>(contact.res_location, out string località);
                    target.TryGetAttributeValue<EntityReference>(contact.res_countryid, out EntityReference nazione);

                    //creo il nuovo indirizzo di default (se uno dei valori facoltativi è null, viene impostata una stringa vuota di default)
                    Utility.CreateNewDefaultAddress(target, crmServiceProvider, indirizzo, città, CAP, provincia, località, nazione);

                    Trace("Check", "Ho creato un nuovo indirizzo di default"); /** <------------< TRACE >------------ */
                }
            }
            else throw new ApplicationException("I campi Indirizzo, Città e CAP sono obbligatori");
            #endregion
        }
    }
}
