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
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            bool isTrace = true;
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Guid targetId = target.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Codice Articolo
            PluginRegion = "Valorizzo il campo Codice Articolo";

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

            //totale imponibile = importo - sconto totale
            decimal taxableamount = 0m;
            decimal baseamount;

            target.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference productId);

            /**
             * recupero il prezzo unitario dalla voce di listino associata
             * recupero la quantità dal target e moltiplico i due valori
             * al risultato si sottrae lo sconto totale per ottenere il totale imponibile
             */
            if (productId != null)
            {
                var fetchQuoteDetail = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""product"">
                                        <filter>
                                          <condition attribute=""productid"" operator=""eq"" value=""{productId.Id}"" />
                                        </filter>
                                        <link-entity name=""pricelevel"" from=""pricelevelid"" to=""pricelevelid"" link-type=""inner"" alias=""Listino"">
                                          <link-entity name=""productpricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""VoceListino"">
                                            <attribute name=""amount"" alias=""importo"" />
                                            <filter>
                                              <condition attribute=""productid"" operator=""eq"" value=""{productId.Id}"" />
                                            </filter>
                                          </link-entity>
                                        </link-entity>
                                      </entity>
                                    </fetch>";
                if (isTrace) crmServiceProvider.TracingService.Trace(fetchQuoteDetail);
                EntityCollection quoteDetailCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchQuoteDetail));

                if (quoteDetailCollection.Entities.Count > 0)
                {
                    Entity enQuoteDetail = quoteDetailCollection.Entities[0];

                    if (enQuoteDetail != null)
                    {
                        decimal prezzounitario = enQuoteDetail.GetAttributeValue<AliasedValue>("importo")?.Value is Money importo ? importo.Value : 0m;

                        decimal quantità = target.GetAttributeValue<decimal>(quotedetail.quantity);
                        decimal manualdiscountamount = target.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m;

                        //calcolo l'importo
                        baseamount = prezzounitario * quantità;
                        if (isTrace) crmServiceProvider.TracingService.Trace($"Importo: {baseamount}");

                        //calcolo il totale imponibile
                        taxableamount = baseamount - manualdiscountamount;
                        if (isTrace) crmServiceProvider.TracingService.Trace($"Totale imponibile: {taxableamount}");

                        //valorizzo il campo totale imponibile
                        target[quotedetail.res_taxableamount] = new Money(taxableamount);

                        if (isTrace) crmServiceProvider.TracingService.Trace($"Update del campo Totale imponibile effettuato.");
                    }
                }
            }
            #endregion
        }
    }
}

