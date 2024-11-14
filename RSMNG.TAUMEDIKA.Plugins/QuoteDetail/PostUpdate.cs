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
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);



            ///TEST
            StringBuilder traceMessage = new StringBuilder();
            traceMessage.AppendLine("Attributes received in Target:");

            // Loop through each attribute in the entity
            foreach (var attribute in target.Attributes)
            {
                string attributeName = attribute.Key;
                object attributeValue = attribute.Value;

                // Append each attribute's name and value to the trace message
                traceMessage.AppendLine($"{attributeName}: {attributeValue}");
            }

            // Trace the final message
            crmServiceProvider.TracingService.Trace(traceMessage.ToString());


            //////////








            #region Aggiorno i campi Totale Imponibile, Sconto Totale e Totale Iva nell'entità parent
            PluginRegion = "Aggiorno i campi Totale Imponibile, Sconto Totale e Totale Iva nell'entità parent";

            if (target.Contains(quotedetail.tax) || target.Contains(quotedetail.manualdiscountamount) || target.Contains(quotedetail.res_taxableamount))
            {
                
                EntityReference erQuote = postImage.GetAttributeValue<EntityReference>(quotedetail.quoteid);

                if (erQuote == null)
                {
                    crmServiceProvider.TracingService.Trace("Quote non trovata, interrompendo il plugin.");
                    return;
                }

                // Verifica se i valori devono essere effettivamente aggiornati confrontandoli con quelli precedenti
                decimal scontoTotalePre = preImage.Contains(quotedetail.manualdiscountamount) ? preImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                decimal totaleImponibilePre = preImage.Contains(quotedetail.res_taxableamount) ? preImage.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : 0;
                decimal totaleIvaPre = preImage.Contains(quotedetail.tax) ? preImage.GetAttributeValue<Money>(quotedetail.tax).Value : 0;

                decimal scontoTotale = target.Contains(quotedetail.manualdiscountamount) ? target.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                decimal totaleImponibile = target.Contains(quotedetail.res_taxableamount) ? target.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : 0;
                decimal totaleIva = target.Contains(quotedetail.tax) ? target.GetAttributeValue<Money>(quotedetail.tax).Value : 0;

                if (scontoTotale == scontoTotalePre && totaleImponibile == totaleImponibilePre && totaleIva == totaleIvaPre)
                {
                    //crmServiceProvider.TracingService.Trace("I valori non sono cambiati, evitato aggiornamento.");
                    return; // Interrompe l'aggiornamento se i valori non sono cambiati
                }
                
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
                                        </filter>
                                      </entity>
                                    </fetch>";

                crmServiceProvider.TracingService.Trace(fetchXml);

                EntityCollection aggregatiRigheOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                if (aggregatiRigheOfferta.Entities.Count > 0)
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
                                  <entity name=""{quote.logicalName}"">
                                    <attribute name=""{quote.freightamount}"" />
                                    <filter>
                                      <condition attribute=""{quote.quoteid}"" operator=""eq"" value=""{fetchData2.quoteid}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""IVA"">
                                      <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";

                    crmServiceProvider.TracingService.Trace(fetchXml2);
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

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"scontoTotale {scontoTotale}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totaleImponibile {totaleImponibile}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"importoSpesaAccessoria {importoSpesaAccessoria}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"aliquota {aliquota}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"TotaleIva {totaleIva}");
                    //--------------------------------------< CALCOLO DEI CAMPI >---------------------------------------//

                    offertaTotaleProdotti = totaleImponibile;
                    offertaScontoTotale = scontoTotale;
                    offertaTotaleIva = totaleIva + (importoSpesaAccessoria * (aliquota / 100));

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"offerta_Totale_Prodotti {offertaTotaleProdotti}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"offerta_Sconto_Totale {offertaScontoTotale}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"offerta_Totale_Iva {offertaTotaleIva}");

                    Entity enQuote = new Entity(quote.logicalName, erQuote.Id);
                    
                    enQuote[quote.totallineitemamount] = offertaTotaleProdotti != 0 ? new Money(offertaTotaleProdotti) : null;
                    enQuote[quote.totaldiscountamount] = offertaScontoTotale != 0 ? new Money(offertaScontoTotale) : null;
                    enQuote[quote.totaltax] = offertaTotaleIva != 0 ? new Money(offertaTotaleIva) : null;

                    crmServiceProvider.Service.Update(enQuote);
                    
                }
            }
            #endregion
        }
    }
}

