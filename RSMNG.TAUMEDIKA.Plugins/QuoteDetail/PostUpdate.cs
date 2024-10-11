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
            #region Trace
            void Trace(string key, object value)
            {
                //flag per attivare/disattivare il trace
                bool isTrace = false;
                if (isTrace) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            string oggettoEsempio = "L'object passato come secondo argomento viene convertito a stringa";
            Trace("Esempio", oggettoEsempio);
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

            EntityCollection aggregatiRigheOfferta = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAggregatiRigheOfferta));

            if (aggregatiRigheOfferta.Entities.Count > 0) throw new ApplicationException("Quote entity not found.");

            Entity aggregatoRigheOfferta = aggregatiRigheOfferta.Entities[0];
            if (aggregatoRigheOfferta == null) throw new ApplicationException("Quote ID needed for fetch is missing");

            /**
             * campi dell'offerta che verranno aggiornati
             */
            decimal offertaTotaleProdotti = 0m,     //somma del totale imponibile di tutte le quotedetail
                offertaScontoTotale = 0m,           //somma dello sconto totale di tutte le quotedetail
                offertaTotaleImponibile,            //totale righe - sconto totale
                offertaTotaleIva,                   //somma del totale iva di tutte le quotedetail + iva calcolata su importo spesa accessoria
                offertaImportoTotale;               //totale imponibile + totale iva

            Guid quoteid = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("quoteid")?.Value as Guid? ?? Guid.Empty;
            if (quoteid != Guid.Empty) throw new ApplicationException("Quote ID fetched not found");

            //creo l'offerta da aggiornare
            Entity enQuote = new Entity(quote.logicalName, quoteid);

            //spesa accessoria e aliquota per il calcolo del totale iva
            decimal spesaAccessoria = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("Importo")?.Value is Money res_amount ? res_amount.Value : 0m;
            decimal codiceIvaSpesaAccessoria = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("Aliquota")?.Value is Money res_rate ? res_rate.Value : 0m;

            /**
             * inizio a valorizzare totale iva con l'aliquota applicata alla spesa accessoria
             * a questo risultato andrà poi aggiunta la somma del totale iva di tutte le righe offerta
             */
            offertaTotaleIva = spesaAccessoria * (codiceIvaSpesaAccessoria / 100);

            //recupero totale imponibile, sconto totale e totale iva di tutte le righe di dettaglio
            decimal scontoTotale = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("ScontoTotale")?.Value is Money manualdiscountamount ? manualdiscountamount.Value : 0m;
            decimal totaleImponibile = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("TotaleImponibile")?.Value is Money res_taxableamount ? res_taxableamount.Value : 0m;
            decimal totaleIva = aggregatoRigheOfferta.GetAttributeValue<AliasedValue>("TotaleIva")?.Value is Money tax ? tax.Value : 0m;

            //calcolo il totale imponibile e l'importo totale
            offertaTotaleImponibile = offertaTotaleProdotti - offertaScontoTotale + spesaAccessoria;
            offertaImportoTotale = offertaTotaleImponibile + offertaTotaleIva;

            enQuote[quote.totallineitemamount] = new Money(totaleImponibile);
            enQuote[quote.totaldiscountamount] = new Money(scontoTotale);
            enQuote[quote.totaltax] = new Money(totaleIva);
            enQuote[quote.totalamountlessfreight] = new Money(offertaTotaleImponibile);
            enQuote[quote.totalamount] = new Money(offertaImportoTotale);

            crmServiceProvider.Service.Update(enQuote);
            #endregion
        }
    }
}

