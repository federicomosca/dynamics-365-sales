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

            void Trace(string key, object value)
            {
                if (isTrace) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }

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
            Entity product;

            target.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);
            target.TryGetAttributeValue<EntityReference>(quotedetail.uomid, out EntityReference erUom);
            target.TryGetAttributeValue<EntityReference>(quotedetail.quoteid, out EntityReference erQuote);

            Guid productId = erProduct.Id;
            Guid uomId = erUom.Id;
            Guid quoteId = erQuote.Id;

            if (productId != null)
            {
                product = crmServiceProvider.Service.Retrieve(DataModel.product.logicalName, productId, new ColumnSet(DataModel.product.productnumber));
                if (product != null) { product.TryGetAttributeValue<string>(DataModel.product.productnumber, out productNumber); }
            }
            if (productNumber != string.Empty)
            {
                target[quotedetail.res_itemcode] = productNumber;
            }
            #endregion

            #region Valorizzo i campi Codice IVA, Aliquota IVA e Totale IVA
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA e Totale IVA";
            decimal baseamount;
            decimal taxableamount;
            decimal aliquota;

            if (productId == null) { throw new ApplicationException("Product not found."); }

            Trace("fetch", "Fetch del prodotto associato alla riga offerta per recuperare lookup dell'iva e l'aliquota. \n" +
                "Recupero l'importo del listino prezzi associato per determinare il prezzo unitario del prodotto.");

            /**
             * recupero il prezzo unitario dalla voce di listino associata
             * recupero la quantità dal target e moltiplico i due valori
             * al risultato si sottrae lo sconto totale per ottenere il totale imponibile
             */
            var fetchProduct = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""quote"">
                                        <filter>
                                          <condition attribute=""quoteid"" operator=""eq"" value=""{quoteId}"" />
                                        </filter>
                                        <link-entity name=""pricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""listino"">
                                          <link-entity name=""productpricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""voce"">
                                            <attribute name=""amount"" />
                                            <filter>
                                              <condition attribute=""productid"" operator=""eq"" value=""{productId}"" />
                                              <condition attribute=""uomid"" operator=""eq"" value=""{uomId}"" />
                                            </filter>
                                            <link-entity name=""product"" from=""productid"" to=""productid"" alias=""prodotto"">
                                              <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"">
                                                <attribute name=""res_rate"" alias=""aliquota"" />
                                                <attribute name=""res_vatnumberid"" alias=""iva"" />
                                              </link-entity>
                                            </link-entity>
                                          </link-entity>
                                        </link-entity>
                                      </entity>
                                    </fetch>";

            Trace("fetch", fetchProduct);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProduct));

            if (collection.Entities.Count > 0)
            {
                product = collection.Entities[0];

                if (product != null)
                {
                    Guid codiceiva = product.GetAttributeValue<AliasedValue>("iva")?.Value is Guid codiceIvaLookup ? codiceIvaLookup : Guid.Empty;
                    aliquota = product.GetAttributeValue<AliasedValue>("aliquota")?.Value is decimal res_rate ? res_rate : 0m; Trace("aliquota", aliquota);

                    if (codiceiva != Guid.Empty)
                    {
                        EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceiva);

                        target[quotedetail.res_vatnumberid] = erCodiceIVA;
                        target[quotedetail.res_vatrate] = aliquota;
                    }
                    else throw new ApplicationException("Codice IVA non trovato");
                    #endregion

                    #region Valorizzo i campi Totale imponibile e Totale IVA
                    PluginRegion = "Valorizzo i campi Totale imponibile e Totale IVA";

                    decimal prezzounitario = product.GetAttributeValue<AliasedValue>("importo")?.Value is Money importo ? importo.Value : 0m;

                    decimal quantità = target.GetAttributeValue<decimal>(quotedetail.quantity); Trace("quantità", quantità);
                    decimal totalesconto = target.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m; Trace("totale sconto", totalesconto);

                    //calcolo l'importo
                    baseamount = prezzounitario * quantità; Trace("importo", baseamount);

                    //calcolo il totale imponibile
                    taxableamount = baseamount - totalesconto; Trace("taxable amount", taxableamount);

                    //calcolo il totale iva
                    decimal totaleiva = taxableamount * (aliquota / 100); Trace("totale iva", totaleiva);

                    target[quotedetail.res_taxableamount] = new Money(taxableamount);
                    target[quotedetail.tax] = new Money(totaleiva);
                }
            }
            #endregion
        }
    }
}

