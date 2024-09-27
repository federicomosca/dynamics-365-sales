using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Address
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            if (crmServiceProvider.PluginContext.PreEntityImages.Contains("PreImage"))
            {
                Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

                Entity postImage = target.GetPostImage(preImage);

                #region Controllo campi obbligatori
                PluginRegion = "Controllo campi obbligatori";
                crmServiceProvider.VerifyMandatoryField(Utility.mandatoryFields);
                #endregion

                #region Genera nome
                PluginRegion = "Genera nome";

                string addressName = string.Empty;
                string customerName = string.Empty;
                string addressCity = string.Empty;
                string addressStreet = string.Empty;

                postImage.TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);

                if (erCustomer != null)
                {
                    if (erCustomer.LogicalName == DataModel.contact.logicalName)
                    {
                        Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(DataModel.contact.fullname));
                        customerName = customer.GetAttributeValue<string>(DataModel.contact.fullname) ?? string.Empty;
                    }
                    if (erCustomer.LogicalName == DataModel.account.logicalName)
                    {
                        Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(DataModel.account.name));
                        customerName = customer.GetAttributeValue<string>(DataModel.account.name) ?? string.Empty;
                    }
                }

                addressStreet = postImage.GetAttributeValue<string>(DataModel.res_address.res_addressField) ?? string.Empty;
                addressCity = postImage.GetAttributeValue<string>(DataModel.res_address.res_city) ?? string.Empty;

                addressName = $"{customerName} {addressCity} {addressStreet}";

                target[DataModel.res_address.res_name] = addressName;
                #endregion

            }
        }
    }
}

