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
    public class PostCreate : RSMNG.BaseClass
    {
        public PostCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Create";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Valorizzo i campi Codice IVA e Aliquota IVA
            PluginRegion = "Valorizzo i campi Codice IVA e Aliquota IVA";
            Guid targetId = target.Id;

            var fetchCodiceIVA = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                    <entity name=""quotedetail"">
                                    <filter>
                                        <condition attribute=""quotedetailid"" operator=""eq"" value=""{targetId}"" />
                                    </filter>
                                    <link-entity name=""product"" from=""productid"" to=""productid"" alias=""product"">
                                        <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""codiceivalookup"">
                                        <attribute name=""res_rate"" alias=""aliquota"" />
                                        <attribute name=""res_vatnumberid"" alias=""codiceiva"" />
                                        </link-entity>
                                    </link-entity>
                                    </entity>
                                </fetch>";

            crmServiceProvider.TracingService.Trace(fetchCodiceIVA);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchCodiceIVA));

            if (collection.Entities.Count > 0)
            {
                Entity quotedetail = collection.Entities[0];

                Guid codiceiva = quotedetail.Contains("codiceiva") ? (Guid)quotedetail.GetAttributeValue<AliasedValue>("codiceiva").Value : Guid.Empty;
                Decimal? aliquota = quotedetail.Contains("aliquota") ? (Decimal?)quotedetail.GetAttributeValue<AliasedValue>("aliquota").Value : null;
                if (codiceiva != null && aliquota.HasValue)
                {
                    EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceiva);

                    target[DataModel.quotedetail.res_vatnumberid] = erCodiceIVA;
                    target[DataModel.quotedetail.res_vatrate] = aliquota;
                    crmServiceProvider.Service.Update(target);
                }
                else throw new ApplicationException("Codice IVA non trovato");
            }
            #endregion

            #region Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale
            PluginRegion = "Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale";

            Decimal totallineitemamount,    //totale righe        somma del totale imponibile di tutte le quotedetail
                totaldiscountamount,        //sconto totale       somma dello sconto totale di tutte le quotedetail
                totalamountlessfreight,     //totale imponibile   totale righe - sconto totale
                totaltax,                   //totale iva          somma del totale iva di tutte le quotedetail + iva calcolata su importo spesa accessoria
                totalamount = 0;            //importo totale      totale imponibile + totale iva

            /**
             * fetch di tutti i campi interessati nel calcolo di tutte le quotedetail
             * meno questa appena creata (i cui dati sono raccolti dal target in prima istanza)
             * associate alla quote che si deve aggiornare
             */

            //dati della quotedetail appena creata + totale iva della quote di riferimento
            Decimal taxableamount,         //totale imponibile
                manualdiscountamount,       //sconto totale
                tax,                        //totale iva
                quotetotaltax = 0;          //totale iva (quote)

            taxableamount = target.Contains(quotedetail.res_taxableamount) ? (Decimal)target.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : 0;
            manualdiscountamount = target.Contains(quotedetail.manualdiscountamount) ? (Decimal)target.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
            tax = target.Contains(quotedetail.tax) ? (Decimal)target.GetAttributeValue<Money>(quotedetail.tax).Value : 0;

            var fetchQuoteDetails = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                        <fetch>
                                          <entity name=""quote"">
                                            <attribute name=""totaltax"" alias=""totaltax"" />
                                            <link-entity name=""quotedetail"" from=""quoteid"" to=""quoteid"" link-type=""inner"" alias=""quotedetail"">
                                              <attribute name=""manualdiscountamount"" alias=""manualdiscountamount"" />
                                              <attribute name=""res_taxableamount"" alias=""taxableamount"" />
                                              <attribute name=""tax"" alias=""tax"" />
                                              <filter>
                                                <condition attribute=""quotedetailid"" operator=""ne"" value=""{targetId}"" />
                                              </filter>
                                            </link-entity>
                                          </entity>
                                        </fetch>";
            crmServiceProvider.TracingService.Trace(fetchQuoteDetails);

            EntityCollection quoteDetailsCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchQuoteDetails));

            if (quoteDetailsCollection.Entities.Count < 0) { throw new ApplicationException("Impossibile recuperare i dettagli dell'offerta."); }

            Entity quote = quoteDetailsCollection.Entities[0];

            //totale iva dell'offerta

            //FORSE DEVO PRENDERE GLI ALIASED VALUE, CASTARLI A DECIMAL E PASSARLI AI CAMPI DA INSERIRE NELL'UPDATE, CAMPI CHE DOVRANNO ESSERE MONEY E NON DECIMAL

            quotetotaltax = quote != null && quote.Contains("totaltax") ? (Decimal)quote.GetAttributeValue<AliasedValue>("totaltax").Value : 0;


            //recupero totale imponibile, sconto totale e totale iva di tutte le righe di dettaglio
            Decimal collectionTaxableAmount = 0;
            Decimal collectionManualDiscountAmount = 0;
            Decimal collectionTax = 0;

            foreach (Entity entity in quoteDetailsCollection.Entities)
            {
                Decimal detailManualDiscountAmount = entity != null && entity.Contains("manualdiscountamount") ? (Decimal)entity.GetAttributeValue<AliasedValue>("manualdiscountamount").Value : 0;
                Decimal detailTaxableAmount = entity != null && entity.Contains("taxableamount") ? (Decimal)entity.GetAttributeValue<AliasedValue>("taxableamount").Value : 0;
                Decimal detailTax = entity != null && entity.Contains("tax") ? (Decimal)entity.GetAttributeValue<AliasedValue>("tax").Value : 0;

                collectionManualDiscountAmount += detailManualDiscountAmount;
                collectionTaxableAmount += detailTaxableAmount;
                collectionTax += detailTax;
            }

            totallineitemamount = collectionTaxableAmount + taxableamount;
            totaldiscountamount = collectionManualDiscountAmount + manualdiscountamount;
            totalamountlessfreight = totallineitemamount - totaldiscountamount;
            totaltax = collectionTax + tax;
            totalamount = totalamountlessfreight + totaltax;

            quote[DataModel.quote.totallineitemamount] = totallineitemamount;
            quote[DataModel.quote.totaldiscountamount] = totaldiscountamount;
            quote[DataModel.quote.totalamountlessfreight] = totalamountlessfreight;
            quote[DataModel.quote.totaltax] = totaltax;
            quote[DataModel.quote.totalamount] = totalamount;

            crmServiceProvider.Service.Update(quote);
            #endregion
        }
    }
}

