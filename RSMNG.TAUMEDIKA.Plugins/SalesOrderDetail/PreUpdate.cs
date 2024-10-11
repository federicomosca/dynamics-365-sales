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

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            bool isTrace = false;
            var service = crmServiceProvider.Service;
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            #region Re-Imposta Codice Articolo
            PluginRegion = "Re-Imposta codice articolo";

            if (target.Contains(salesorderdetail.productid))
            {
                string codiceArticolo = null;

                EntityReference erProduct = target.GetAttributeValue<EntityReference>(salesorderdetail.productid);
                if (erProduct != null)
                {
                    Entity enProduct = service.Retrieve(product.logicalName, erProduct.Id, new ColumnSet(new string[] { product.productnumber }));
                    codiceArticolo = enProduct.GetAttributeValue<string>(salesorderdetail.productnumber);
                }

                target[salesorderdetail.res_itemcode] = codiceArticolo;
            }
            #endregion

            #region Valorizzo il campo Totale imponibile
            PluginRegion = "Valorizzo il campo Totale imponibile";

            decimal importo = target.GetAttributeValue<Money>(salesorderdetail.baseamount)?.Value ?? 0m;
            decimal manualdiscountamount = target.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount)?.Value ?? 0m;

            if (isTrace) crmServiceProvider.TracingService.Trace($"importo :{importo}");
            if (isTrace) crmServiceProvider.TracingService.Trace($"manualdiscountamount :{manualdiscountamount}");

            //calcolo il totale imponibile
            decimal totaleImponibile = importo - manualdiscountamount;
            if (isTrace) crmServiceProvider.TracingService.Trace($"totaleImponibile :{totaleImponibile}");

            //valorizzo il campo totale imponibile
            target[salesorderdetail.res_taxableamount] = new Money(totaleImponibile);
            #endregion


        }
    }
}

