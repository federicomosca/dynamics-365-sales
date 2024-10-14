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
            #region Trace Activation Method
            bool isFirstExecute = true;
            void Trace(string key, object value)
            {
                bool isTraceActive = true;
                if (isFirstExecute)
                {
                    crmServiceProvider.TracingService.Trace($"TRACE IS ACTIVE: {isTraceActive}");

                    isFirstExecute = false;
                }
                if (isTraceActive) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            #endregion

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);
            Guid targetId = target.Id;

            EntityReference erQuote = postImage.GetAttributeValue<EntityReference>(quotedetail.quoteid);

            if (target.Contains(quotedetail.tax) || target.Contains(quotedetail.manualdiscountamount) || target.Contains(quotedetail.res_taxableamount))
            {

                decimal aliquota = 0;
                decimal importoSpesaAccessoria = 0;

                decimal scontoTotale = 0;
                decimal totaleImponibile = 0;
                decimal totaleIva = 0;

                var fetchData = new
                {
                    quoteid = erQuote.Id
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch aggregate=""true"">
                                      <entity name=""quotedetail"">
                                        <attribute name=""manualdiscountamount"" alias=""ScontoTotale"" aggregate=""sum"" />
                                        <attribute name=""res_taxableamount"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""tax"" alias=""TotaleIva"" aggregate=""sum"" />
                                        <filter>
                                          <condition attribute=""quoteid"" operator=""eq"" value=""{fetchData.quoteid}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";

                EntityCollection aggregatiRigheOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                if (aggregatiRigheOfferta.Entities.Count  > 0)
                {

                    scontoTotale = aggregatiRigheOfferta.Entities[0].ContainsAliasNotNull("ScontoTotale") ? aggregatiRigheOfferta.Entities[0].GetAliasedValue<Money>("ScontoTotale").Value : 0;
                    totaleImponibile = aggregatiRigheOfferta.Entities[0].ContainsAliasNotNull("TotaleImponibile") ? aggregatiRigheOfferta.Entities[0].GetAliasedValue<Money>("TotaleImponibile").Value : 0;
                    totaleIva = aggregatiRigheOfferta.Entities[0].ContainsAliasNotNull("TotaleIva") ? aggregatiRigheOfferta.Entities[0].GetAliasedValue<Money>("TotaleIva").Value : 0;

                    var fetchData2 = new
                    {
                        quoteid = erQuote.Id
                    };
                    // Recupero Importo Spesa Accessoria  e Aliquota
                    var fetchXml2 = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""quote"">
                                    <attribute name=""freightamount"" />
                                    <filter>
                                      <condition attribute=""quoteid"" operator=""eq"" value=""{fetchData2.quoteid}"" />
                                    </filter>
                                    <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""IVA"">
                                      <attribute name=""res_rate"" alias=""Aliquota"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";

                    EntityCollection ecOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml2));

                    if (ecOfferta.Entities.Count > 0)
                    {
                        importoSpesaAccessoria = ecOfferta.Entities[0].ContainsAttributeNotNull(quote.freightamount) ? ecOfferta.Entities[0].GetAttributeValue<Money>(quote.freightamount).Value : 0;
                        aliquota = ecOfferta.Entities[0].ContainsAliasNotNull("Aliquota") ? ecOfferta.Entities[0].GetAliasedValue<decimal>("Aliquota") : 0;
                    }

                    ////----------------------------------< CAMPI OFFERTA DA AGGIORNARE >----------------------------------//

                    decimal offertaTotaleProdotti,      // S [quotedetail] totale imponibile
                        offertaScontoTotale,            // S [quotedetail] sconto totale
                        offertaTotaleIva;               // S [quotedetail] totale iva + iva calcolata su importo spesa accessoria

                    Trace("scontoTotale", scontoTotale);
                    Trace("totaleImponibile", totaleImponibile);
                    Trace("importoSpesaAccessoria", importoSpesaAccessoria);
                    Trace("aliquota", aliquota);
                    Trace("TotaleIva", totaleIva);
                    //--------------------------------------< CALCOLO DEI CAMPI >---------------------------------------//

                    offertaTotaleProdotti = totaleImponibile;                                                       Trace("offerta_Totale_Prodotti", offertaTotaleProdotti);
                    offertaScontoTotale = scontoTotale;                                                             Trace("offerta_Sconto_Totale", offertaScontoTotale);
                    offertaTotaleIva = totaleIva + (importoSpesaAccessoria * (aliquota / 100));                     Trace("offerta_Totale_Iva", offertaTotaleIva);

                    Entity enQuote = new Entity(quote.logicalName, erQuote.Id);

                    enQuote[quote.totallineitemamount] = offertaTotaleProdotti != 0 ? new Money(offertaTotaleProdotti) : null;
                    enQuote[quote.totaldiscountamount] = offertaScontoTotale != 0 ? new Money(offertaScontoTotale) : null;
                    enQuote[quote.totaltax] = offertaTotaleIva != 0 ? new Money(offertaTotaleIva) : null;

                    crmServiceProvider.Service.Update(enQuote);

                }  
                
            }


            //#region Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale dell'Offerta correlata
            //PluginRegion = "Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale dell'Offerta correlata";

            /**
             * fetch di tutti i campi interessati nel calcolo di tutte le quotedetail,
             * associate alla quote che si deve aggiornare
             * 
             * in particolare recupero le seguenti entità correlate all'offerta:
             * quote detail > sconto totale, totale imponibile, totale iva
             * spesa accessoria > importo
             * codice IVA spesa accessoria > aliquota
             */
            //var fetchAggregatiRigheOfferta = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            //                    <fetch aggregate=""true"">
            //                        <entity name=""{quote.logicalName}"">
            //                        <attribute name=""{quote.quoteid}"" alias=""quoteid"" groupby=""true"" />
            //                        <link-entity name=""{quotedetail.logicalName}"" from=""quoteid"" to=""quoteid"" link-type=""inner"" alias=""quotedetail"">
            //                            <attribute name=""{quotedetail.manualdiscountamount}"" alias=""ScontoTotale"" aggregate=""sum"" />
            //                            <attribute name=""{quotedetail.res_taxableamount}"" alias=""TotaleImponibile"" aggregate=""sum"" />
            //                            <attribute name=""{quotedetail.tax}"" alias=""TotaleIva"" aggregate=""sum"" />
            //                        </link-entity>
            //                        <link-entity name=""{res_additionalexpense.logicalName}"" from=""res_additionalexpenseid"" to=""res_additionalexpenseid"" alias=""additionalexpense"">
            //                            <attribute name=""{res_additionalexpense.res_amount}"" alias=""Importo"" groupby=""true"" />
            //                        </link-entity>
            //                        <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""vatnumber"">
            //                            <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" groupby=""true"" />
            //                        </link-entity>
            //                        </entity>
            //                    </fetch>";

            //Trace("fetchAggregatiRigheOfferta", fetchAggregatiRigheOfferta);
            //EntityCollection aggregatiRigheOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAggregatiRigheOfferta));


            

            //if (aggregatiRigheOfferta.Entities.Count <= 0) throw new ApplicationException("Quote entity not found.");
            //Entity aggregato = aggregatiRigheOfferta.Entities[0];

           

            //------------------------------------< LOGICA RELATIVA ALL'IVA >------------------------------------//

            //Guid quoteid = aggregato.GetAttributeValue<AliasedValue>("quoteid")?.Value as Guid? ?? Guid.Empty;
            //if (quoteid == Guid.Empty) throw new ApplicationException("Quote ID fetched not found");
            //decimal offertaImportoSpesaAccessoria = aggregato.GetAttributeValue<AliasedValue>("Importo")?.Value is Money res_amount ? res_amount.Value : 0m; Trace("offerta_importo_Spesa_Accessoria", offertaImportoSpesaAccessoria);
            //decimal offertaAliquotaSpesaAccessoria = aggregato.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal res_rate ? res_rate : 0m; Trace("offerta_Aliquota_Spesa_Accessoria", offertaAliquotaSpesaAccessoria);

            

           
            //#endregion
        }
    }
}

