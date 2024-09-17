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
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            string contactId = target.Id.ToString();

            #region CreateDefaultAddress
            PluginRegion = "CreateDefaultAddress";

            /**
             * controllo che i campi Indirizzo, Città e CAP siano valorizzati
             * se almeno uno è valorizzato chiamo il metodo per controllare la presenza di altri address
             * se non ve ne sono, viene creato un nuovo indirizzo con i valori dei suddetti campi 
             * e viene settato come indirizzo di default
             */

            target.TryGetAttributeValue<string>(DataModel.contact.address1_name, out string address);
            target.TryGetAttributeValue<string>(DataModel.contact.address1_city, out string city);
            target.TryGetAttributeValue<string>(DataModel.contact.address1_postalcode, out string postalcode);

            if (contactId != null & (!string.IsNullOrEmpty(address) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(postalcode)))
            {
                Utility.CheckAddress(crmServiceProvider, target.LogicalName, contactId, address, city, postalcode);
            }
            #endregion
        }
    }
}
