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

            #region Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale dell'Offerta correlata
            PluginRegion = "Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale dell'Offerta correlata";

            /**
             * fetch di tutti i campi interessati nel calcolo di tutte le quotedetail,
             * associate alla quote che si deve aggiornare
             * 
             * in particolare recupero le seguenti entità correlate all'offerta:
             * quote detail > sconto totale, totale imponibile, totale iva
             * spesa accessoria > importo
             * codice IVA spesa accessoria > aliquota
             */
            var fetchAggregatiRigheOfferta = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch aggregate=""true"">
                                    <entity name=""{quote.logicalName}"">
                                    <attribute name=""{quote.quoteid}"" alias=""quoteid"" groupby=""true"" />
                                    <link-entity name=""{quotedetail.logicalName}"" from=""quoteid"" to=""quoteid"" link-type=""inner"" alias=""quotedetail"">
                                        <attribute name=""{quotedetail.manualdiscountamount}"" alias=""ScontoTotale"" aggregate=""sum"" />
                                        <attribute name=""{quotedetail.res_taxableamount}"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""{quotedetail.tax}"" alias=""TotaleIva"" aggregate=""sum"" />
                                    </link-entity>
                                    <link-entity name=""{res_additionalexpense.logicalName}"" from=""res_additionalexpenseid"" to=""res_additionalexpenseid"" alias=""additionalexpense"">
                                        <attribute name=""{res_additionalexpense.res_amount}"" alias=""Importo"" groupby=""true"" />
                                    </link-entity>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""vatnumber"">
                                        <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" groupby=""true"" />
                                    </link-entity>
                                    </entity>
                                </fetch>";
            Trace("fetchAggregatiRigheOfferta", fetchAggregatiRigheOfferta);
            EntityCollection aggregatiRigheOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAggregatiRigheOfferta));

            if (aggregatiRigheOfferta.Entities.Count <= 0) throw new ApplicationException("Quote entity not found.");
            Entity aggregatoRigheOfferta = aggregatiRigheOfferta.Entities[0];

            //----------------------------------< CAMPI OFFERTA DA AGGIORNARE >----------------------------------//

            decimal offertaTotaleProdotti,      // S [quotedetail] totale imponibile
                offertaScontoTotale,            // S [quotedetail] sconto totale
                offertaTotaleImponibile,        // totaleprodotti - sconto totale
                offertaTotaleIva,               // S [quotedetail] totale iva + iva calcolata su importo spesa accessoria
                offertaImportoTotale;           // totale imponibile + totale iva

            //------------------------------------< LOGICA RELATIVA ALL'IVA >------------------------------------//

            Guid quoteid = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("quoteid")?.Value as Guid? ?? Guid.Empty;
            if (quoteid == Guid.Empty) throw new ApplicationException("Quote ID fetched not found");
            decimal righeSpesaAccessoria = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("Importo")?.Value is Money res_amount ? res_amount.Value : 0m; Trace("righe_Spesa_Accessoria", righeSpesaAccessoria);
            decimal righeAliquotaSpesaAccessoria = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("Aliquota")?.Value is Money res_rate ? res_rate.Value : 0m; Trace("righe_Aliquota_Spesa_Accessoria", righeAliquotaSpesaAccessoria);

            //-------------------------------------< AGGREGATI DALLA FETCH >-------------------------------------//

            decimal righeScontoTotale = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("ScontoTotale")?.Value is Money manualdiscountamount ? manualdiscountamount.Value : 0m; Trace("righe_Sconto_Totale", righeScontoTotale);
            decimal righeTotaleImponibile = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("TotaleImponibile")?.Value is Money res_taxableamount ? res_taxableamount.Value : 0m; Trace("righe_Totale_Imponibile", righeTotaleImponibile);
            decimal righeTotaleIva = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("TotaleIva")?.Value is Money tax ? tax.Value : 0m; Trace("righe_Totale_Iva", righeTotaleIva);

            //--------------------------------------< CALCOLO DEI CAMPI >---------------------------------------//

            offertaTotaleProdotti = righeTotaleImponibile; Trace("offerta_Totale_Prodotti", offertaTotaleProdotti);
            offertaScontoTotale = righeScontoTotale; Trace("offerta_Sconto_Totale", offertaScontoTotale);
            offertaTotaleImponibile = offertaTotaleProdotti - offertaScontoTotale + righeSpesaAccessoria; Trace("offerta_Totale_Imponibile", offertaTotaleImponibile);
            offertaTotaleIva = righeTotaleIva + (righeSpesaAccessoria * (righeAliquotaSpesaAccessoria / 100)); Trace("offerta_Totale_Iva", offertaTotaleIva);
            offertaImportoTotale = offertaTotaleImponibile + offertaTotaleIva; Trace("offerta_Importo_Totale", offertaImportoTotale);

            Entity enQuote = new Entity(quote.logicalName, quoteid);

            enQuote[quote.totallineitemamount] = new Money(offertaTotaleProdotti);
            enQuote[quote.totaldiscountamount] = new Money(offertaScontoTotale);
            enQuote[quote.totaltax] = new Money(offertaTotaleIva);
            enQuote[quote.totalamountlessfreight] = new Money(offertaTotaleImponibile);
            enQuote[quote.totalamount] = new Money(offertaImportoTotale);

            crmServiceProvider.Service.Update(enQuote);
            #endregion
        }
    }
}

