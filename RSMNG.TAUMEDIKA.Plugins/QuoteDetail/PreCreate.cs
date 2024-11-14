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



            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            EntityReference enIva = target.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid);
            string vatnumberId = enIva != null ? enIva.Id.ToString() : "null";
            crmServiceProvider.TracingService.Trace("vatnubmer id: " + vatnumberId);
            /*
            if(target.Contains(quotedetail.res_isfromcanvas) && target.GetAttributeValue<bool>(quotedetail.res_isfromcanvas) == true)
            {
                if(target.ContainsAttributeNotNull(quotedetail.ispriceoverridden) && target.GetAttributeValue<bool>(quotedetail.ispriceoverridden) == true)
                {
                    string name = target.ContainsAttributeNotNull(quotedetail.quotedetailname) ? target.GetAttributeValue<string>(quotedetail.quotedetailname) : string.Empty;
                    decimal prezzoUnitario = target.ContainsAttributeNotNull(quotedetail.priceperunit) ? target.GetAttributeValue<Money>(quotedetail.priceperunit).Value : 0;
                    decimal quantita = target.ContainsAttributeNotNull(quotedetail.quantity) ? target.GetAttributeValue<decimal>(quotedetail.quantity) : 0;
                    decimal Importo = target.ContainsAttributeNotNull(quotedetail.baseamount) ? target.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;



                    totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                    totaleIva = omaggio ? 0 : (totaleImponibile * aliquota) / 100;
                    importoTotale = totaleImponibile + totaleIva;

                    target[quotedetail.res_vatnumberid] = codiceIva;
                    target[quotedetail.res_vatrate] = aliquota;
                    target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
                    target[quotedetail.tax] = new Money(totaleIva);
                    target[quotedetail.extendedamount] = new Money(importoTotale);
                }
                
            }
            */
            
        }
    }
}

