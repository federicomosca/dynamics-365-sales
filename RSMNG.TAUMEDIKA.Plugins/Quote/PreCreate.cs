using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Quote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Quote
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.quote.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, Utility.mandatoryFields);
            target.TryGetAttributeValue<EntityReference>(quote.res_additionalexpenseid, out EntityReference additionalExpense);
            if (additionalExpense != null)
            {
                target.TryGetAttributeValue<EntityReference>(quote.res_vatnumberid, out EntityReference vatNumber);
                if (vatNumber == null) { throw new ApplicationException($"Il campo Codice IVA Spesa Accessoria è obbligatorio"); }
            }
            #endregion

            #region Popolo in automatico il Destinatario
            string destination = string.Empty;
            if (target.ContainsAttributeNotNull(quote.res_shippingreference))
            {
                destination = target.GetAttributeValue<string>(quote.res_shippingreference);
            }
            if (string.IsNullOrEmpty(destination) && target.ContainsAttributeNotNull(quote.customerid))
            {
                destination = Shared.Account.Utility.GetName(crmServiceProvider.Service, target.GetAttributeValue<EntityReference>(quote.customerid).Id);
            }
            target.AddWithRemove(quote.res_recipient, destination);
            #endregion

            #region Valorizzazione automatica del campo Importo spesa accessoria [DISABLED]
            //PluginRegion = "Valorizzazione automatica del campo Importo spesa accessoria"; 
            //Money amount = null;
            //target.TryGetAttributeValue<EntityReference>(DataModel.quote.res_additionalexpenseid, out EntityReference erAdditionalExpense);
            //if (erAdditionalExpense != null)
            //{
            //    Entity enAdditionalExpense = crmServiceProvider.Service.Retrieve(
            //        DataModel.res_additionalexpense.logicalName,
            //        erAdditionalExpense.Id,
            //        new ColumnSet(DataModel.res_additionalexpense.res_amount));

            //    amount = enAdditionalExpense != null && enAdditionalExpense.Contains(DataModel.res_additionalexpense.res_amount) ?
            //        enAdditionalExpense.GetAttributeValue<Money>(DataModel.res_additionalexpense.res_amount) : null;

            //    target[DataModel.quote.freightamount] = amount;
            //}
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";
            if (target.Contains(quote.res_countryid))
            {
                target.TryGetAttributeValue<EntityReference>(DataModel.quote.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? RSMNG.TAUMEDIKA.Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

                target[contact.address1_country] = countryName;
            }
            #endregion

            
           
        }
    }
}

