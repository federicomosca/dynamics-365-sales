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
            bool isTrace = true;
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Guid targetId = target.Id;

            #region Valorizzo il campo Totale imponibile
            PluginRegion = "Valorizzo il campo Totale imponibile";

            //totale imponibile = importo - sconto totale
            decimal taxableamount = 0m;
            decimal baseamount;

            /**
             * recupero il prezzo unitario dalla voce di listino associata
             * recupero la quantità dal target e moltiplico i due valori
             * al risultato si sottrae lo sconto totale per ottenere il totale imponibile
             */
            var fetchQuoteDetail = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch top=""1"">
                                        <entity name=""quote"">
                                        <link-entity name=""pricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""listino"">
                                            <link-entity name=""productpricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""vocedilistino"">
                                            <attribute name=""amount"" alias=""importo"" />
                                            <link-entity name=""product"" from=""productid"" to=""productid"" alias=""prodotto"">
                                                <link-entity name=""quotedetail"" from=""productid"" to=""productid"" alias=""rigaofferta"">
                                                <filter>
                                                    <condition attribute=""quotedetailid"" operator=""eq"" value=""{targetId}"" uitype=""quotedetail"" />
                                                </filter>
                                                </link-entity>
                                            </link-entity>
                                            </link-entity>
                                        </link-entity>
                                        </entity>
                                    </fetch>";

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
                    target[quotedetail.res_taxableamount] = taxableamount;
                    crmServiceProvider.Service.Update(target);
                    if (isTrace) crmServiceProvider.TracingService.Trace($"Update del campo Totale imponibile effettuato.");
                }
            }
            #endregion

            #region Valorizzo i campi Codice IVA, Aliquota IVA e Totale IVA
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA e Totale IVA";

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

            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchCodiceIVA));

            if (collection.Entities.Count > 0)
            {
                Entity enQuoteDetail = collection.Entities[0];

                Guid codiceiva = enQuoteDetail.GetAttributeValue<AliasedValue>("codiceiva")?.Value is Guid codiceIvaLookup ? codiceIvaLookup : Guid.Empty;
                decimal aliquota = enQuoteDetail.GetAttributeValue<AliasedValue>("aliquota")?.Value is decimal res_rate ? res_rate : 0m;

                //calcolo il totale iva e lo salvo nel target
                decimal totaleiva = taxableamount * (aliquota / 100);
                target[quotedetail.tax] = totaleiva;
                if (isTrace) crmServiceProvider.TracingService.Trace($"Totale iva: {totaleiva}");

                if (codiceiva != Guid.Empty)
                {
                    EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceiva);

                    target[quotedetail.res_vatnumberid] = erCodiceIVA;
                    target[quotedetail.res_vatrate] = aliquota;
                    crmServiceProvider.Service.Update(target);
                    if (isTrace) crmServiceProvider.TracingService.Trace($"Update dei campi Codice IVA e Aliquota IVA effettuato.");
                }
                else throw new ApplicationException("Codice IVA non trovato");
            }
            #endregion
        }
    }
}

