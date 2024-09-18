using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Entity postImage = target.GetPostImage(preImage);

                    string accountId = preImage.Id.ToString();

                    #region Crea indirizzo di default
                    PluginRegion = "Crea indirizzo di default";

                    /**
                     * controllo che i campi Indirizzo, Città e CAP siano valorizzati
                     * se almeno uno è valorizzato chiamo il metodo per controllare la presenza di altri address
                     * se non ve ne sono, viene creato un nuovo indirizzo con i valori dei suddetti campi 
                     * e viene settato come indirizzo di default
                     */
                    postImage.TryGetAttributeValue<string>(DataModel.account.address1_line1, out string address);
                    postImage.TryGetAttributeValue<string>(DataModel.account.address1_city, out string city);
                    postImage.TryGetAttributeValue<string>(DataModel.account.address1_postalcode, out string postalcode);

                    if (!string.IsNullOrEmpty(address) || !string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(postalcode))
                    {
                        EntityCollection addresses = Utility.CheckDefaultAddress(crmServiceProvider, postImage.LogicalName, postImage.Id.ToString());

                        /**
                         * creo il record di Address e lo valorizzo con i values passati al metodo come argomenti
                         */
                        Entity enAddress = new Entity(DataModel.res_address.logicalName);
                        enAddress[DataModel.res_address.res_addressField] = address;
                        enAddress[DataModel.res_address.res_city] = city;
                        enAddress[DataModel.res_address.res_postalcode] = postalcode;

                        Guid customerId = new Guid(postImage.Id.ToString());
                        enAddress[DataModel.res_address.res_customerid] = new EntityReference(postImage.LogicalName, customerId);

                        enAddress[DataModel.res_address.res_isdefault] = true;
                        enAddress[DataModel.res_address.res_iscustomeraddress] = true;

                        Guid addressId = crmServiceProvider.Service.Create(enAddress);

                        if (addresses.TotalRecordCount != -1)
                        {
                            foreach (var duplicate in addresses.Entities)
                            {
                                duplicate[DataModel.res_address.res_isdefault] = false;
                            }
                        }
                        #endregion
                    }
                }
            }
        }
    }
}
