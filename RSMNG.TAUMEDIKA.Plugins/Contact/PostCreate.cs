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
            #region Trace Activation Method
            bool isFirstExecute = true;
            void Trace(string key, object value)
            {
                bool isTraceActive = false;
                if (isFirstExecute)
                {
                    crmServiceProvider.TracingService.Trace($"TRACE IS ACTIVE: {isTraceActive}");

                    isFirstExecute = false;
                }
                if (isTraceActive) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            #endregion

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
                //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                EntityCollection defaultAddressCollection = Utility.GetDefaultAddress(crmServiceProvider, target.Id);

                //se non trovo nemmeno un indirizzo
                if (defaultAddressCollection.Entities.Count < 0)
                {
                    //recupero gli eventuali altri valori compilati nei campi Provincia, Località, Nazione
                    target.TryGetAttributeValue<string>(contact.address1_stateorprovince, out string provincia);
                    target.TryGetAttributeValue<string>(contact.res_location, out string località);
                    target.TryGetAttributeValue<string>(contact.res_countryid, out string nazione);

                    //creo il nuovo indirizzo di default (se uno dei valori facoltativi è null, viene impostata una stringa vuota di default)
                    Utility.CreateNewDefaultAddress(target, crmServiceProvider.Service, indirizzo, città, CAP, provincia, località, nazione);
                }
            }
            #endregion
        }
    }
}
