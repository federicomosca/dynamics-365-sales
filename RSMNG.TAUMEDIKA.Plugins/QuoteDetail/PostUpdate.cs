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
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            #region Aggiorno i campi Totale Imponibile, Sconto Totale e Totale Iva nell'entità parent
            PluginRegion = "Aggiorno i campi Totale Imponibile, Sconto Totale e Totale Iva nell'entità parent";

            if (target.Contains(quotedetail.tax) || target.Contains(quotedetail.manualdiscountamount) || target.Contains(quotedetail.res_taxableamount))
            {

                EntityReference erQuote = preImage.GetAttributeValue<EntityReference>(quotedetail.quoteid);

                //decimal scontoTotale = target.ContainsAttributeNotNull(quotedetail.manualdiscountamount) ? target.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : target.NotContainsAttributeOrNull(quotedetail.manualdiscountamount) ? 0 : preImage.ContainsAttributeNotNull(quotedetail.manualdiscountamount) ? preImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                decimal totaleImponibile = target.ContainsAttributeNotNull(quotedetail.res_taxableamount) ? target.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : target.NotContainsAttributeOrNull(quotedetail.res_taxableamount) ? 0 : preImage.ContainsAttributeNotNull(quotedetail.res_taxableamount) ? preImage.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : 0;
                //decimal totaleIva = target.ContainsAttributeNotNull(quotedetail.tax) ? target.GetAttributeValue<Money>(quotedetail.tax).Value : target.NotContainsAttributeOrNull(quotedetail.tax) ? 0 : preImage.ContainsAttributeNotNull(quotedetail.tax) ? preImage.GetAttributeValue<Money>(quotedetail.tax).Value : 0;

                decimal aliquota = 0;
                decimal importoSpesaAccessoria = 0;

                var fetchData = new
                {
                    quoteid = erQuote.Id
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch aggregate=""true"">
                                      <entity name=""{quotedetail.logicalName}"">
                                        <attribute name=""{quotedetail.manualdiscountamount}"" alias=""ScontoTotale"" aggregate=""sum"" />
                                        <attribute name=""{quotedetail.res_taxableamount}"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""{quotedetail.tax}"" alias=""TotaleIva"" aggregate=""sum"" />
                                        <filter>
                                          <condition attribute=""{quotedetail.quoteid}"" operator=""eq"" value=""{fetchData.quoteid}"" />
                                          <condition attribute=""{quotedetail.quotedetailid}"" operator=""ne"" value=""{target.Id}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";

                crmServiceProvider.TracingService.Trace(fetchXml);

                EntityCollection aggregatiRigheOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                if (aggregatiRigheOfferta?.Entities?.Count > 0)
                {
                    Entity enTotQuoteDetail = aggregatiRigheOfferta.Entities[0];

                    //scontoTotale += enTotQuoteDetail.ContainsAliasNotNull("ScontoTotale") ? enTotQuoteDetail.GetAliasedValue<Money>("ScontoTotale").Value : 0;
                    totaleImponibile += enTotQuoteDetail.ContainsAliasNotNull("TotaleImponibile") ? enTotQuoteDetail.GetAliasedValue<Money>("TotaleImponibile").Value : 0;
                    //totaleIva += enTotQuoteDetail.ContainsAliasNotNull("TotaleIva") ? enTotQuoteDetail.GetAliasedValue<Money>("TotaleIva").Value : 0;

                    //var fetchData2 = new
                    //{
                    //    quoteid = erQuote.Id
                    //};
                    //// Recupero Importo Spesa Accessoria  e Aliquota
                    //var fetchXml2 = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                    //            <fetch>
                    //              <entity name=""{quote.logicalName}"">
                    //                <attribute name=""{quote.freightamount}"" />
                    //                <filter>
                    //                  <condition attribute=""{quote.quoteid}"" operator=""eq"" value=""{fetchData2.quoteid}"" />
                    //                </filter>
                    //                <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""IVA"">
                    //                  <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                    //                </link-entity>
                    //              </entity>
                    //            </fetch>";

                    //crmServiceProvider.TracingService.Trace(fetchXml2);
                    //EntityCollection ecOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml2));

                    //if (ecOfferta.Entities.Count > 0)
                    //{

                    //    importoSpesaAccessoria = ecOfferta.Entities[0].ContainsAttributeNotNull(quote.freightamount) ? ecOfferta.Entities[0].GetAttributeValue<Money>(quote.freightamount).Value : 0;
                    //    aliquota = ecOfferta.Entities[0].ContainsAliasNotNull("Aliquota") ? ecOfferta.Entities[0].GetAliasedValue<decimal>("Aliquota") : 0;
                    //}

                    //////----------------------------------< CAMPI OFFERTA DA AGGIORNARE >----------------------------------//

                    //decimal offertaTotaleProdotti,      // S [quotedetail] totale imponibile
                    //    offertaScontoTotale,            // S [quotedetail] sconto totale
                    //    offertaTotaleIva;               // S [quotedetail] totale iva + iva calcolata su importo spesa accessoria

                    //if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"scontoTotale {scontoTotale}");
                    //if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totaleImponibile {totaleImponibile}");
                    //if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"importoSpesaAccessoria {importoSpesaAccessoria}");
                    //if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"aliquota {aliquota}");
                    //if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"TotaleIva {totaleIva}");
                    ////--------------------------------------< CALCOLO DEI CAMPI >---------------------------------------//

                    //offertaTotaleProdotti = totaleImponibile;
                    ////offertaScontoTotale = scontoTotale;
                    ////offertaTotaleIva = totaleIva + (importoSpesaAccessoria * (aliquota / 100));

                    //if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"offerta_Totale_Prodotti {offertaTotaleProdotti}");
                    ////if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"offerta_Sconto_Totale {offertaScontoTotale}");
                    ////if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"offerta_Totale_Iva {offertaTotaleIva}");

                    Entity enQuote = new Entity(quote.logicalName, erQuote.Id);

                    enQuote[quote.totallineitemamount] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                    //enQuote[quote.totaldiscountamount] = offertaScontoTotale != 0 ? new Money(offertaScontoTotale) : null;
                    //enQuote[quote.totaltax] = offertaTotaleIva != 0 ? new Money(offertaTotaleIva) : null;

                    //crmServiceProvider.Service.Update(enQuote);

                }
            }
            #endregion
        }
    }
}

