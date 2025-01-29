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

                decimal totaleImponibile = target.ContainsAttributeNotNull(quotedetail.res_taxableamount) ? target.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : target.NotContainsAttributeOrNull(quotedetail.res_taxableamount) ? 0 : preImage.ContainsAttributeNotNull(quotedetail.res_taxableamount) ? preImage.GetAttributeValue<Money>(quotedetail.res_taxableamount).Value : 0;

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

                    totaleImponibile += enTotQuoteDetail.ContainsAliasNotNull("TotaleImponibile") ? enTotQuoteDetail.GetAliasedValue<Money>("TotaleImponibile").Value : 0;
                   
                    Entity enQuote = new Entity(quote.logicalName, erQuote.Id);

                    enQuote[quote.totallineitemamount] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                }
            }
            #endregion
        }
    }
}

