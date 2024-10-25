using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Address
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            target.TryGetAttributeValue<EntityReference>(DataModel.res_address.res_customerid, out EntityReference erCustomer);

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

            addressStreet = target.GetAttributeValue<string>(DataModel.res_address.res_addressField) ?? string.Empty;
            addressCity = target.GetAttributeValue<string>(DataModel.res_address.res_city) ?? string.Empty;

            addressName = $"{customerName} - {addressCity} - {addressStreet}";

            target[DataModel.res_address.res_name] = addressName;
            #endregion

            #region Gestione Indirizzo Default
            PluginRegion = "Gestione Indirizzo Default";

            target.TryGetAttributeValue<bool>(res_address.res_isdefault, out bool isDefault);

            if (isDefault)
            {
                Guid customerId = erCustomer != null ? erCustomer.Id : Guid.Empty;

                if (customerId != Guid.Empty)
                {
                    EntityCollection defaultAddressesCollection = Utility.GetAddresses(crmServiceProvider, customerId);

                    //controllo se ci sono già indirizzi di default
                    if (defaultAddressesCollection.Entities.Count > 0)
                    {
                        //se ci sono, imposto Default e Indirizzo scheda cliente a NO e faccio update
                        foreach (Entity duplicate in defaultAddressesCollection.Entities)
                        {
                            duplicate[res_address.res_isdefault] = false;
                            duplicate[res_address.res_iscustomeraddress] = false;
                            crmServiceProvider.Service.Update(duplicate);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

