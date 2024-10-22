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
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Creo indirizzo di default
            PluginRegion = "Creo indirizzo di default";

            List<string> campiIndirizzo = new List<string>{
                        contact.address1_name,
                        contact.address1_city,
                        contact.address1_postalcode,
                    };

            bool isIndirizzo = false;
            foreach (string campoValorizzato in campiIndirizzo)
            {
                if (target.Contains(campoValorizzato))
                {
                    isIndirizzo = true;
                    break;
                }
            }

            //se almeno uno dei valori è stato modificato...
            if (isIndirizzo)
            {
                //recupero il primo indirizzo del Cliente che abbia Indirizzo Scheda Cliente e Default a SI
                EntityCollection linkedAddressesCollection = Utility.GetLinkedAddresses(crmServiceProvider, target.Id);

                //se non trovo nemmeno un indirizzo
                if (linkedAddressesCollection.Entities.Count == 0)
                {
                    //creo il nuovo indirizzo di default (se uno dei valori facoltativi è null, viene impostata una stringa vuota di default)
                    Utility.CreateNewDefaultAddress(crmServiceProvider, target);
                }
            }
            else throw new ApplicationException("I campi Indirizzo, Città e CAP sono obbligatori");
            #endregion
        }
    }
}
