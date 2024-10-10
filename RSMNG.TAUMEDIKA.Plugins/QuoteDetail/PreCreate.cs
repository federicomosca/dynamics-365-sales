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
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            bool isTrace = true;

            void Trace(string key, object value)
            {
                if (isTrace) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Guid targetId = target.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Codice Articolo
            PluginRegion = "Valorizzo il campo Codice Articolo";

            target.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);

            if (erProduct == null) throw new ApplicationException("Product entity reference not found");

            Entity prodotto = crmServiceProvider.Service.Retrieve(DataModel.product.logicalName, erProduct.Id, new ColumnSet(DataModel.product.productnumber));
            prodotto.TryGetAttributeValue<string>(DataModel.product.productnumber, out string productNumber);

            target[quotedetail.res_itemcode] = productNumber != null ? productNumber : string.Empty;
            #endregion

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Totale imponibile e Totale IVA di Offerta
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Totale imponibile e Totale IVA di Offerta";

            target.TryGetAttributeValue<EntityReference>(quotedetail.uomid, out EntityReference erUom);
            target.TryGetAttributeValue<EntityReference>(quotedetail.quoteid, out EntityReference erQuote);

            if (erUom == null) throw new ApplicationException("Unit of measurement entity reference not found");
            if (erQuote == null) throw new ApplicationException("Quote entity reference not found");

            Trace("fetch", "Fetch del prodotto associato alla riga offerta per recuperare lookup dell'iva e l'aliquota. \n" +
                "Recupero l'importo del listino prezzi associato per determinare il prezzo unitario del prodotto.");

            /**
             * [RIGA OFFERTA]
             * recupero l'importo associato al prodotto per l'unità di misura selezionata
             * nella voce listino del listino prezzi associato all'offerta,
             * offerta con id recuperato dal campo lookup della riga offerta
             * 
             * [OFFERTA]
             * recupero i campi "prezzo" dell'offerta per aggiornare i valori
             */
            var fetchImportoVoceListino = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""quote"">
                                        <attribute name=""totaldiscountamount"" alias=""OffertaScontoTotaleApplicato"" />
                                        <attribute name=""totallineitemamount"" alias=""OffertaTotaleProdotti"" />
                                        <attribute name=""freightamount"" alias=""OffertaImportoSpesaAccessoria"" />
                                        <attribute name=""totaltax"" alias=""OffertaTotaleIva"" />
                                        <filter>
                                          <condition attribute=""quoteid"" operator=""eq"" value=""{erQuote.Id}"" />
                                        </filter>
                                        <link-entity name=""pricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""listino"">
                                          <link-entity name=""productpricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""voce"">
                                            <attribute name=""amount"" alias=""VoceDiListinoImporto"" />
                                            <filter>
                                              <condition attribute=""productid"" operator=""eq"" value=""{erProduct.Id}"" />
                                              <condition attribute=""uomid"" operator=""eq"" value=""{erUom.Id}"" />
                                            </filter>
                                            <link-entity name=""product"" from=""productid"" to=""productid"" alias=""prodotto"">
                                              <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIva"">
                                                <attribute name=""res_rate"" alias=""RigaOffertaCodiceIvaAliquota"" />
                                                <attribute name=""res_vatnumberid"" alias=""RigaOffertaCodiceIvaGuid"" />
                                              </link-entity>
                                            </link-entity>
                                          </link-entity>
                                        </link-entity>
                                        <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIvaSpesaAccessoria"">
                                          <attribute name=""res_rate"" alias=""OffertaAliquotaIVA"" />
                                        </link-entity>
                                      </entity>
                                    </fetch>";

            Trace("fetch", fetchImportoVoceListino);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchImportoVoceListino));

            if (collection.Entities.Count > 0)
            {
                prodotto = collection.Entities[0];

                //---------------------------- Riga Offerta ----------------------------//

                //dalla fetch
                Guid rigaOffertaCodiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("RigaOffertaCodiceIvaGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                decimal rigaOffertaCodiceIvaAliquota = prodotto.GetAttributeValue<AliasedValue>("RigaOffertaCodiceIvaAliquota")?.Value is decimal rate ? rate : 0m; Trace("riga_offerta_aliquota_iva", rigaOffertaCodiceIvaAliquota);
                decimal voceDiListinoImporto = prodotto.GetAttributeValue<AliasedValue>("VoceDiListinoImporto")?.Value is Money amount ? amount.Value : 0m; Trace("riga_offerta_prezzo_unitario", voceDiListinoImporto);

                //dal target
                decimal rigaOffertaQuantità = target.GetAttributeValue<decimal>(quotedetail.quantity); Trace("riga_offerta_quantità", rigaOffertaQuantità);
                decimal rigaOffertaScontoTotale = target.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m; Trace("riga_offerta_sconto_totale", rigaOffertaScontoTotale);

                if (rigaOffertaCodiceIvaGuid == Guid.Empty) throw new ApplicationException("Vat Number not found");

                EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, rigaOffertaCodiceIvaGuid);

                //imposto l'entity reference del codice iva nel campo lookup di riga offerta
                target[quotedetail.res_vatnumberid] = erCodiceIVA;

                //imposto l'aliquota nel campo nascosto di riga offerta
                target[quotedetail.res_vatrate] = rigaOffertaCodiceIvaAliquota;

                //calcolo l'importo [riga offerta]
                decimal rigaOffertaImporto = voceDiListinoImporto * rigaOffertaQuantità; Trace("riga_offerta_importo", rigaOffertaImporto);

                //calcolo il totale imponibile [riga offerta]
                decimal rigaOffertaTotaleImponibile = rigaOffertaImporto - rigaOffertaScontoTotale; Trace("riga_offerta_totale_imponibile", rigaOffertaTotaleImponibile);

                //calcolo il totale iva [riga offerta]
                decimal rigaOffertaTotaleIVA = rigaOffertaTotaleImponibile * (rigaOffertaCodiceIvaAliquota / 100); Trace("riga_offerta_totale_iva", rigaOffertaTotaleIVA);

                //aggiorno i campi di riga offerta
                target[quotedetail.res_taxableamount] = new Money(rigaOffertaTotaleImponibile);
                target[quotedetail.tax] = new Money(rigaOffertaTotaleIVA);

                //---------------------------- Offerta ----------------------------//

                //creo l'oggetto Offerta da aggiornare
                Entity offerta = new Entity(quote.logicalName, erQuote.Id);

                //recupero l'importo spesa accessoria dell'offerta dalla fetch
                decimal offertaTotaleProdotti = prodotto.GetAttributeValue<AliasedValue>("OffertaTotaleProdotti")?.Value is Money totallineitemamount ? totallineitemamount.Value : 0m; Trace("Offerta_Totale_Prodotti", offertaTotaleProdotti);
                decimal offertaScontoTotaleApplicato = prodotto.GetAttributeValue<AliasedValue>("OffertaScontoTotaleApplicato")?.Value is Money totaldiscountamount ? totaldiscountamount.Value : 0m; Trace("Offerta_Sconto_Totale_Applicato", offertaScontoTotaleApplicato);
                decimal offertaImportoSpesaAccessoria = prodotto.GetAttributeValue<AliasedValue>("OffertaImportoSpesaAccessoria")?.Value is Money freightamount ? freightamount.Value : 0m; Trace("Offerta_Importo_Spesa_Accessoria", offertaImportoSpesaAccessoria);

                //calcolo il totale imponibile dell'offerta (totale prodotti - sconto totale applicato + importo spesa accessoria)
                decimal offertaTotaleImponibile = offertaTotaleProdotti - offertaScontoTotaleApplicato + offertaImportoSpesaAccessoria; Trace("Offerta_Totale_Imponibile", offertaTotaleImponibile);

                //totale iva di tutte le righe
                decimal sommaRigheOffertaTotaleIva = prodotto.GetAttributeValue<AliasedValue>("OffertaTotaleIva")?.Value is Money totaltax ? totaltax.Value : 0m; Trace("Somma_Righe_Offerta_Totale_Iva", sommaRigheOffertaTotaleIva);

                //offerta aliquota iva
                decimal offertaAliquotaIva = prodotto.GetAttributeValue<AliasedValue>("OffertaAliquotaIVA")?.Value is decimal quoteRate ? quoteRate : 0m; Trace("Offerta_Aliquota_Iva", offertaAliquotaIva);

                //iva su importo spesa accessoria
                decimal offertaIvaImportoSpesaAccessoria = offertaImportoSpesaAccessoria * (offertaAliquotaIva / 100); Trace("Offerta_Iva_Importo_Spesa_Accessoria", offertaIvaImportoSpesaAccessoria);

                //sommatoria del totale iva di tutte le righe + iva calcolata su importo spesa accessoria
                decimal offertaTotaleIva = sommaRigheOffertaTotaleIva + offertaIvaImportoSpesaAccessoria; Trace("Offerta_Totale_Iva", offertaTotaleIva);

                //aggiorno i campi dell'offerta
                offerta[quote.totalamountlessfreight] = new Money(offertaTotaleImponibile);
                offerta[quote.totaltax] = new Money(offertaTotaleIva);

                crmServiceProvider.Service.Update(offerta);
            }
            #endregion
        }
    }
}

