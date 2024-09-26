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
            Entity postImage = target.GetPostImage(preImage);

            #region Aggiorno i campi Totale IVA e Sconto totale sull'Offerta correlata
            PluginRegion = "Aggiorno i campi Totale IVA e Sconto totale sull'Offerta correlata";

            target.TryGetAttributeValue<Money>(quotedetail.tax, out Money tax); //totale IVA
            target.TryGetAttributeValue<Money>(quotedetail.manualdiscountamount, out Money manualDiscountAmount); //sconto totale
            postImage.TryGetAttributeValue<EntityReference>(quotedetail.quoteid, out EntityReference erQuote);

            if (tax != null || manualDiscountAmount != null)
            {
                /**
                 * fetch aggregate per recuperare la somma di tutte le righe offerta correlate all'offerta
                 * in particolare i campi Totale sconto e Totale IVA, per operare poi un aggiornamento
                 * con i relativi dati della presente riga offerta (esclusa dalla fetch)
                 */
                string fetchQuoteDetailsSum = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                            <fetch aggregate=""true"">
                                              <entity name=""quotedetail"">
                                                <attribute name=""manualdiscountamount"" alias=""totaleSconto"" aggregate=""sum"" />
                                                <attribute name=""tax"" alias=""totaleIVA"" aggregate=""sum"" />
                                                <filter>
                                                  <condition attribute=""quotedetailid"" operator=""ne"" value=""{postImage.Id}"" />
                                                  <condition attribute=""quoteid"" operator=""eq"" value=""{erQuote.Id}"" />
                                                </filter>
                                              </entity>
                                            </fetch>";

                EntityCollection quoteDetails = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchQuoteDetailsSum));
                if (quoteDetails.Entities.Count > 0)
                {
                    Entity sum = quoteDetails.Entities[0];

                    AliasedValue aliasTotaleSconto = (AliasedValue)sum.GetAttributeValue<AliasedValue>("totaleSconto");
                    AliasedValue aliasTotaleIVA = (AliasedValue)sum.GetAttributeValue<AliasedValue>("totaleIVA");

                    Money totaleSconto = (Money)aliasTotaleSconto.Value;
                    Money totaleIVA = (Money)aliasTotaleIVA.Value;

                    Entity quote = crmServiceProvider.Service.Retrieve(DataModel.quote.logicalName, erQuote.Id, new ColumnSet(
                        DataModel.quote.totaldiscountamount,
                        DataModel.quote.totaltax));

                    quote.TryGetAttributeValue<Money>(DataModel.quote.totaldiscountamount, out Money totalDiscountAmount);
                    quote.TryGetAttributeValue<Money>(DataModel.quote.totaltax, out Money totalTax);

                    if (totalDiscountAmount != null || totalTax != null)
                    {
                        quote[DataModel.quote.totaldiscountamount] = (decimal)totalDiscountAmount.Value + (decimal)totaleSconto.Value;
                        quote[DataModel.quote.totaltax] = (decimal)totalTax.Value + (decimal)totaleIVA.Value;

                        crmServiceProvider.Service.Update(quote);
                    }
                }
            }
            #endregion
        }
    }
}

