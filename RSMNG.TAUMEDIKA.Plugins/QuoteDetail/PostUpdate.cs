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

            string condition = string.Empty;

            string quoteDetailRemoved = $@"<condition attribute=""quotedetailid"" operator=""ne"" value=""{postImage.Id}"" />";

            target.TryGetAttributeValue<EntityReference>(quotedetail.quoteid, out EntityReference targetQuote);
            preImage.TryGetAttributeValue<EntityReference>(quotedetail.quoteid, out EntityReference preImageQuote);

            if (targetQuote != null) { condition = quoteDetailRemoved; }

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
                                                  {condition}
                                                  <condition attribute=""quoteid"" operator=""eq"" value=""{preImageQuote.Id}"" />
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

                Entity quote = crmServiceProvider.Service.Retrieve(DataModel.quote.logicalName, preImageQuote.Id, new ColumnSet(
                    DataModel.quote.totaldiscountamount,
                    DataModel.quote.totaltax));

                quote[DataModel.quote.totaldiscountamount] = totaleSconto;
                quote[DataModel.quote.totaltax] = totaleIVA;

                crmServiceProvider.Service.Update(quote);
            }
            #endregion
        }
    }
}

