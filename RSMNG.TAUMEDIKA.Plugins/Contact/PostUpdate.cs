using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Contact
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.contact.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                if (preImage != null && preImage.Contains("contactid"))
                {
                    Entity postImage = target.GetPostImage(preImage);

                    string contactId = preImage.Id.ToString();

                    #region CreateDefaultAddress
                    PluginRegion = "CreateDefaultAddress";

                    /**
                     * controllo che i campi Indirizzo, Città e CAP siano valorizzati
                     * se almeno uno è valorizzato chiamo il metodo per controllare la presenza di altri address
                     * se non ve ne sono, viene creato un nuovo indirizzo con i valori dei suddetti campi 
                     * e viene settato come indirizzo di default
                     */
                    postImage.TryGetAttributeValue<string>(DataModel.contact.address1_name, out string address);
                    postImage.TryGetAttributeValue<string>(DataModel.contact.address1_city, out string city);
                    postImage.TryGetAttributeValue<string>(DataModel.contact.address1_postalcode, out string postalcode);

                    if (!string.IsNullOrEmpty(contactId) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(postalcode))
                    {
                        Utility.CheckAddress(crmServiceProvider, target.LogicalName, contactId, address, city, postalcode);
                    }
                    #endregion
                }
            }
        }
    }
}
