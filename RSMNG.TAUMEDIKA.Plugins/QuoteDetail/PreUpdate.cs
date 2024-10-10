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
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            bool isTrace = false;
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            Guid targetId = target.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Codice Articolo
            PluginRegion = "Aggiorno il campo Codice Articolo";

            //product -> product number
            string productNumber = string.Empty;

            target.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);
            if (erProduct != null)
            {
                Entity product = crmServiceProvider.Service.Retrieve(DataModel.product.logicalName, erProduct.Id, new ColumnSet(DataModel.product.productnumber));
                if (product != null) { product.TryGetAttributeValue<string>(DataModel.product.productnumber, out productNumber); }
            }
            if (productNumber != string.Empty)
            {
                target[quotedetail.res_itemcode] = productNumber;
            }
            #endregion

            #region Valorizzo il campo Totale imponibile
            PluginRegion = "Valorizzo il campo Totale imponibile";

            decimal importo = target.GetAttributeValue<Money>(quotedetail.baseamount)?.Value ?? 0m;
            decimal manualdiscountamount = target.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m;

            if (isTrace) crmServiceProvider.TracingService.Trace($"importo :{importo}");
            if (isTrace) crmServiceProvider.TracingService.Trace($"manualdiscountamount :{manualdiscountamount}");

            //calcolo il totale imponibile
            decimal totaleImponibile = importo - manualdiscountamount;
            if (isTrace) crmServiceProvider.TracingService.Trace($"totaleImponibile :{totaleImponibile}");

            //valorizzo il campo totale imponibile
            target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
            #endregion
        }
    }
}

