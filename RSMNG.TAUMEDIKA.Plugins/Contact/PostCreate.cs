﻿using Microsoft.Xrm.Sdk;
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
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Creo indirizzo scheda cliente
            PluginRegion = "Creo indirizzo scheda cliente";

            bool isAlreadyDefaultAddress = false;

            //controllo se è stato compilato l'indirizzo
            if (target.Contains(contact.address1_name))
            {
                if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace("Il contatto ha valorizzato il campo Indirizzo"); }

                target.TryGetAttributeValue<string>(contact.address1_postalcode, out string CAP);
                target.TryGetAttributeValue<string>(contact.address1_city, out string città);

                //cap e città sono obbligatori indirizzo è valorizzato
                if (!string.IsNullOrEmpty(CAP) || !string.IsNullOrEmpty(città))
                {
                    if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace("Sia città sia CAP sono valorizzati"); }

                    //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                    EntityCollection linkedAddressesCollection = Utility.GetAddresses(crmServiceProvider, target.Id);

                    //se non trovo nemmeno un indirizzo
                    if (linkedAddressesCollection.Entities.Count == 0)
                    {
                        //creo il nuovo indirizzo di default (se uno dei valori facoltativi è null, viene impostata una stringa vuota di default)
                        Utility.CreateCustomerAddress(crmServiceProvider, target, isAlreadyDefaultAddress);
                    }
                }
                else throw new ApplicationException("Se il campo Indirizzo è valorizzato, i campi CAP e Città sono obbligatori.");
            }
            #endregion
        }
    }
}
