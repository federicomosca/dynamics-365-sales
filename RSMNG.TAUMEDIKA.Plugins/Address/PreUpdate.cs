using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
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
            PluginPrimaryEntityName = res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Controllo campo Indirizzo scheda cliente
            PluginRegion = "Controllo campo Indirizzo scheda cliente";

            target.TryGetAttributeValue<bool>(res_address.res_iscustomeraddress, out bool isCustomerAddressModified);
            if (isCustomerAddressModified) throw new ApplicationException("Il campo Indirizzo scheda cliente non è modificabile dall'utente");
            #endregion

            #region Controllo campo Nome
            PluginRegion = "Controllo campo Nome";

            target.TryGetAttributeValue<string>(res_address.res_name, out string nome);
            if (nome != null) throw new ApplicationException("Il campo nome non è modificabile dall'utente");
            #endregion

            #region Controllo Indirizzo scheda cliente
            PluginRegion = "Controllo Indirizzo scheda cliente";

            preImage.TryGetAttributeValue<bool>(res_address.res_iscustomeraddress, out bool isCustomerAddress);

            //se è un indirizzo scheda cliente
            if (isCustomerAddress)
            {
                //campi non modificabili dall'utente se Indirizzo scheda cliente = SI
                List<string> campiSchedaCliente = new List<string>
            {
                res_address.res_customerid,
                res_address.res_addressField,
                res_address.res_postalcode,
                res_address.res_city,
                res_address.res_province,
                res_address.res_location,
                res_address.res_countryid,
            };

                foreach (string campoModificato in campiSchedaCliente)
                {
                    if (target.Contains(campoModificato) && target.GetAttributeValue<object>(campoModificato) != null)
                        throw new ApplicationException("I record con il campo Indirizzo scheda cliente = SI non sono modificabili a eccezione del campo Default");
                }
            }
            #endregion

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

            postImage.TryGetAttributeValue<EntityReference>(res_address.res_customerid, out EntityReference erCustomer);

            if (erCustomer != null)
            {
                if (erCustomer.LogicalName == contact.logicalName)
                {
                    Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(contact.fullname));
                    customerName = customer.GetAttributeValue<string>(contact.fullname) ?? string.Empty;
                }
                if (erCustomer.LogicalName == account.logicalName)
                {
                    Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(account.name));
                    customerName = customer.GetAttributeValue<string>(account.name) ?? string.Empty;
                }
            }

            addressStreet = postImage.GetAttributeValue<string>(res_address.res_addressField) ?? string.Empty;
            addressCity = postImage.GetAttributeValue<string>(res_address.res_city) ?? string.Empty;

            addressName = $"{customerName} - {addressCity} - {addressStreet}";

            target[res_address.res_name] = addressName;
            #endregion
        }
    }
}

