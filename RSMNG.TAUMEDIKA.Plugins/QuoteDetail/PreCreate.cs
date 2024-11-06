using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.QuoteDetail
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            string name = target.ContainsAttributeNotNull(quotedetail.quotedetailname) ? target.GetAttributeValue<string>(quotedetail.quotedetailname) : string.Empty;
            string priceperunit = target.ContainsAttributeNotNull(quotedetail.priceperunit) ? target.GetAttributeValue<Money>(quotedetail.quotedetailname).Value.ToString() : string.Empty;
            string baseamount = target.ContainsAttributeNotNull(quotedetail.baseamount) ? target.GetAttributeValue<Money>(quotedetail.baseamount).Value.ToString() : string.Empty;

            if(PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Nome: {name}"); }
            if(PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Prezzo Unitario: {priceperunit}"); }
            if(PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Importo: {baseamount}"); }

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

        }
    }
}

