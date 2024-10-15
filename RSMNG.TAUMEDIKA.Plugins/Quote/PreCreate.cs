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

                target[DataModel.contact.address1_country] = countryName;
            }
            #endregion

            #region Controllo anagrafica del potenziale cliente [DISABLED]
            //PluginRegion = "Controllo anagrafica del potenziale cliente";

            //target.TryGetAttributeValue<EntityReference>(DataModel.quote.customerid, out EntityReference erCustomer);

            //if (erCustomer != null)
            //{
            //    //recupero l'entità cliente e verifico anagrafica
            //    if (erCustomer.LogicalName == DataModel.contact.logicalName)
            //    {
            //        return;
            //    }
            //    if (erCustomer.LogicalName == DataModel.account.logicalName)
            //    {
            //        ColumnSet accountColumnSet = new ColumnSet(
            //            DataModel.account.res_accountnaturecode,
            //            DataModel.account.res_taxcode, //codice fiscale
            //            DataModel.account.res_vatnumber, //partita iva
            //            DataModel.account.res_sdi,
            //            DataModel.account.emailaddress3, //pec
            //            DataModel.account.address1_line1,
            //            DataModel.account.address1_city,
            //            DataModel.account.address1_postalcode
            //            );
            //        Entity account = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, accountColumnSet);
            //        if (account != null)
            //        {
            //            //determino la natura giuridica del cliente e verifico che sia valorizzato il codice fiscale o la partita iva
            //            account.TryGetAttributeValue<OptionSetValue>(DataModel.account.res_accountnaturecode, out OptionSetValue accountNature);
            //            if (accountNature != null)
            //            {
            //                if (accountNature.Value == (int)DataModel.account.res_accountnaturecodeValues.Personafisica)
            //                {
            //                    account.TryGetAttributeValue<string>(DataModel.account.res_taxcode, out string taxCode);
            //                    if (taxCode == null) { throw new Exception("Il codice fiscale del potenziale cliente non è valorizzato"); }
            //                }

            //                if (accountNature.Value == (int)DataModel.account.res_accountnaturecodeValues.Personagiuridica)
            //                {
            //                    account.TryGetAttributeValue<string>(DataModel.account.res_vatnumber, out string vatNumber);
            //                    if (vatNumber == null) { throw new Exception("La partita IVA del potenziale cliente non è valorizzata"); }
            //                }
            //            }

            //            //verifico che o SDI o PEC siano valorizzati
            //            account.TryGetAttributeValue<string>(DataModel.account.res_sdi, out string SDI);
            //            account.TryGetAttributeValue<string>(DataModel.account.emailaddress3, out string PEC);

            //            if (SDI == null && PEC == null) { throw new Exception("SDI e PEC del potenziale cliente non sono valorizzati"); }

            //            //verifico che i dati legati all'indirizzo (sede legale) del cliente siano valorizzati
            //            account.TryGetAttributeValue<string>(DataModel.account.address1_line1, out string address);
            //            account.TryGetAttributeValue<string>(DataModel.account.address1_city, out string city);
            //            account.TryGetAttributeValue<string>(DataModel.account.address1_postalcode, out string CAP);

            //            if (address == null || city == null || CAP == null) { throw new Exception("Un dato relativo all'indirizzo del cliente non è valorizzato"); }
            //        }
            //    }
            //}
            #endregion
        }
    }
}

