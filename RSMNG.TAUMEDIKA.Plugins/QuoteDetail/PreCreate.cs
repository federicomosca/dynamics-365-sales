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

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Totale imponibile e Totale IVA di Offerta
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Totale imponibile e Totale IVA di Offerta";

            if (productId == null) { throw new ApplicationException("Product not found."); }

            Trace("fetch", "Fetch del prodotto associato alla riga offerta per recuperare lookup dell'iva e l'aliquota. \n" +
                "Recupero l'importo del listino prezzi associato per determinare il prezzo unitario del prodotto.");

            /**
             * recupero l'importo associato al prodotto per l'unità di misura selezionata
             * nella voce listino del listino prezzi associato all'offerta,
             * offerta con id recuperato dal campo lookup della riga offerta
             */
            var fetchProduct = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""quote"">
                                        <attribute name=""freightamount"" alias=""importospesaaccessoria"" />
                                        <attribute name=""totallineitemamount"" alias=""totaleprodotti"" />
                                        <attribute name=""totaldiscountamount"" alias=""scontototaleapplicato"" />
                                        <attribute name=""totaltax"" alias=""totaleiva"" />
                                        <filter>
                                          <condition attribute=""quoteid"" operator=""eq"" value=""{quoteId}"" />
                                        </filter>
                                        <link-entity name=""pricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""listino"">
                                          <link-entity name=""productpricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""voce"">
                                            <attribute name=""amount"" alias=""importo"" />
                                            <filter>
                                              <condition attribute=""productid"" operator=""eq"" value=""{productId}"" />
                                              <condition attribute=""uomid"" operator=""eq"" value=""{uomId}"" />
                                            </filter>
                                            <link-entity name=""product"" from=""productid"" to=""productid"" alias=""prodotto"">
                                              <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""codiceiva"">
                                                <attribute name=""res_rate"" alias=""aliquota"" />
                                                <attribute name=""res_vatnumberid"" alias=""iva"" />
                                              </link-entity>
                                            </link-entity>
                                          </link-entity>
                                        </link-entity>
                                        <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""codiceIvaSpesaAccessoria"">
                                          <attribute name=""res_rate"" alias=""aliquotaOfferta"" />
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
                    //---------------------------- Riga Offerta ----------------------------//

                    //dalla fetch
                    Guid codiceiva = product.GetAttributeValue<AliasedValue>("iva")?.Value is Guid codiceIvaLookup ? codiceIvaLookup : Guid.Empty;
                    decimal aliquota = product.GetAttributeValue<AliasedValue>("aliquota")?.Value is decimal res_rate ? res_rate : 0m; Trace("aliquota", aliquota);
                    decimal prezzounitario = product.GetAttributeValue<AliasedValue>("importo")?.Value is Money importo ? importo.Value : 0m; Trace("prezzo unitario", prezzounitario);

                    //dal target
                    decimal quantità = target.GetAttributeValue<decimal>(quotedetail.quantity); Trace("quantità", quantità);
                    decimal scontototale = target.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m; Trace("totale sconto", scontototale);

                    if (codiceiva == Guid.Empty) throw new ApplicationException("Vat Number not found");

                    EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceiva);

                    //imposto l'entity reference del codice iva nel campo lookup di riga offerta
                    target[quotedetail.res_vatnumberid] = erCodiceIVA;

                    //imposto l'aliquota nel campo nascosto di riga offerta
                    target[quotedetail.res_vatrate] = aliquota;

                    //calcolo l'importo [riga offerta]
                    decimal baseamount = prezzounitario * quantità; Trace("importo", baseamount);

                    //calcolo il totale imponibile [riga offerta]
                    decimal taxableamount = baseamount - scontototale; Trace("totale imponibile", taxableamount);

                    //calcolo il totale iva [riga offerta]
                    decimal tax = taxableamount * (aliquota / 100); Trace("totale iva", tax);

                    //aggiorno i campi di riga offerta
                    target[quotedetail.res_taxableamount] = new Money(taxableamount);
                    target[quotedetail.tax] = new Money(tax);

                    //---------------------------- Offerta ----------------------------//

                    //creo l'oggetto Offerta da aggiornare
                    Entity offerta = new Entity(quote.logicalName, quoteId);

                    //recupero l'importo spesa accessoria dell'offerta dalla fetch
                    decimal totaleprodotti = product.GetAttributeValue<AliasedValue>("totaleprodotti")?.Value is Money totallineitemamount ? totallineitemamount.Value : 0m; Trace("totale_prodotti", totaleprodotti);
                    decimal scontototaleapplicato = product.GetAttributeValue<AliasedValue>("scontototaleapplicato")?.Value is Money totaldiscountamount ? totaldiscountamount.Value : 0m; Trace("sconto_totale_applicato", scontototaleapplicato);
                    decimal importospesaaccessoria = product.GetAttributeValue<AliasedValue>("importospesaaccessoria")?.Value is Money freightamount ? freightamount.Value : 0m; Trace("importo_spesa_accessoria", importospesaaccessoria);

                    //calcolo il totale imponibile dell'offerta (totale prodotti - sconto totale applicato + importo spesa accessoria)
                    decimal totalamountlessfreight = totaleprodotti - scontototaleapplicato + importospesaaccessoria; Trace("totale_imponibile_offerta",totalamountlessfreight);

                    //totale iva di tutte le righe
                    decimal totalTaxDetails = product.GetAttributeValue<AliasedValue>("totaleiva")?.Value is Money totaltaxdetails ? totaltaxdetails.Value : 0m; Trace("totale_iva_righe", totalTaxDetails);

                    //iva su importo spesa accessoria
                    decimal aliquotaOfferta = product.GetAttributeValue<AliasedValue>("aliquotaOfferta")?.Value is decimal quoteRate ? quoteRate : 0m; Trace("aliquota_offerta", aliquotaOfferta);
                    decimal taxAdditionalExpense = importospesaaccessoria * (aliquotaOfferta / 100); Trace("iva_importo_spesa_accessoria", taxAdditionalExpense);

                    //sommatoria del totale iva di tutte le righe + iva calcolata su importo spesa accessoria
                    decimal totaltax = totalTaxDetails + taxAdditionalExpense; Trace("totale iva offerta", totaltax);

                    //aggiorno i campi dell'offerta
                    offerta[quote.totalamountlessfreight] = new Money(totalamountlessfreight);
                    offerta[quote.totaltax] = new Money(totaltax);

                    crmServiceProvider.Service.Update(offerta);
                }
            }
            #endregion
        }
    }
}

