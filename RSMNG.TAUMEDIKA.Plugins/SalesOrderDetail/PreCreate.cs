using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Quote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            void Trace(string key, object value)
            {
                bool isTrace = true;
                if (isTrace) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            var service = crmServiceProvider.Service;

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            List<String> mandatoryFieldName = new List<String>();
            mandatoryFieldName.Add(salesorderdetail.productid);
            mandatoryFieldName.Add(salesorderdetail.salesorderid);
            mandatoryFieldName.Add(salesorderdetail.uomid);
            mandatoryFieldName.Add(salesorderdetail.productid);
            VerifyMandatoryField(crmServiceProvider, mandatoryFieldName);
            #endregion


            #region Imposta Codice Articolo
            PluginRegion = "Imposta codice articolo";

            string codiceArticolo = null;

            target.TryGetAttributeValue<EntityReference>(salesorderdetail.productid, out EntityReference erProduct);

            if (erProduct == null) throw new ApplicationException("Product entity reference not found");

            Entity prodotto = service.Retrieve(product.logicalName, erProduct.Id, new ColumnSet(new string[] { product.productnumber }));
            codiceArticolo = prodotto.GetAttributeValue<string>(salesorderdetail.productnumber);

            target[salesorderdetail.res_itemcode] = codiceArticolo;

            #endregion

            #region Valorizzo i campi (Riga Ordine)[Codice IVA, Aliquota IVA, Totale IVA] e (Ordine)[Totale imponibile, Totale IVA]
            PluginRegion = "Valorizzo i campi (Riga Ordine)[Codice IVA, Aliquota IVA, Totale IVA] e (Ordine)[Totale imponibile, Totale IVA]";

            target.TryGetAttributeValue<EntityReference>(salesorderdetail.uomid, out EntityReference erUom);
            target.TryGetAttributeValue<EntityReference>(salesorderdetail.salesorderid, out EntityReference erSalesOrder);

            if (erUom == null) throw new ApplicationException("Unit of measurement entity reference not found");
            if (erSalesOrder == null) throw new ApplicationException("Salesorder entity reference not found");

            Trace("fetch", "Fetch del prodotto associato alla riga ordine per recuperare lookup dell'iva e l'aliquota.");

            /**
             * [RIGA ORDINE]
             * recupero l'importo associato al prodotto per l'unità di misura selezionata
             * nella voce listino del listino prezzi associato all'ordine,
             * ordine con id recuperato dal campo lookup della riga ordine
             * 
             * [ORDINE]
             * recupero i campi "prezzo" dell'ordine per aggiornare i valori
             */
            var fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""salesorder"">
                                    <attribute name=""{salesorder.totalamountlessfreight}"" alias=""OrdineImportoSpesaAccessoria"" />
                                    <attribute name=""{salesorder.totaldiscountamount}"" alias=""OrdineScontoTotaleApplicato"" />
                                    <attribute name=""{salesorder.totallineitemamount}"" alias=""OrdineTotaleProdotti"" />
                                    <attribute name=""{salesorder.totaltax}"" alias=""OrdineTotaleIva"" />
                                    <link-entity name=""{quote.logicalName}"" from=""quoteid"" to=""quoteid"" alias=""Offerta"">
                                      <filter>
                                        <condition attribute=""{quote.quoteid}"" operator=""eq"" value=""77ce5e3f-5e85-ef11-ac20-00224884a5f7"" uiname=""Dubbi esistenziali"" uitype=""quote"" />
                                      </filter>
                                      <link-entity name=""{pricelevel.logicalName}"" from=""pricelevelid"" to=""pricelevelid"" alias=""Listino"">
                                        <link-entity name=""{productpricelevel.logicalName}"" from=""pricelevelid"" to=""pricelevelid"" alias=""VoceListino"">
                                          <filter>
                                            <condition attribute=""{productpricelevel.productid}"" operator=""eq"" value=""99197fd0-f71f-eb11-a813-000d3a33f3b4"" uiname=""Café Grande"" uitype=""product"" />
                                            <condition attribute=""{productpricelevel.uomid}"" operator=""eq"" value=""e21af1d0-2782-ef11-ac20-00224884a5f7"" uiname=""nr"" uitype=""uom"" />
                                          </filter>
                                          <link-entity name=""{product.logicalName}"" from=""productid"" to=""productid"" alias=""Prodotto"">
                                            <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIVA"">
                                              <attribute name=""{res_vatnumber.res_rate}"" alias=""RigaOrdineCodiceIvaAliquota"" />
                                              <attribute name=""{res_vatnumber.res_vatnumberid}"" alias=""RigaOrdineCodiceIvaGuid"" />
                                            </link-entity>
                                          </link-entity>
                                        </link-entity>
                                      </link-entity>
                                    </link-entity>
                                  </entity>
                                </fetch>";

            Trace("fetch", fetchProdotto);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

            if (collection.Entities.Count > 0)
            {
                prodotto = collection.Entities[0];

                //---------------------------- Riga Ordine ----------------------------//

                //dalla fetch
                Guid rigaOrdineCodiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("RigaOrdineCodiceIvaGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                decimal rigaOrdineCodiceIvaAliquota = prodotto.GetAttributeValue<AliasedValue>("RigaOrdineCodiceIvaAliquota")?.Value is decimal rate ? rate : 0m; Trace("riga_ordine_aliquota_iva", rigaOrdineCodiceIvaAliquota);

                //dal target
                decimal rigaOrdinePrezzoUnitario = target.GetAttributeValue<Money>(salesorderdetail.baseamount)?.Value ?? 0m; Trace("riga_ordine_prezzo_unitario", rigaOrdinePrezzoUnitario);
                decimal rigaOrdineQuantità = target.GetAttributeValue<decimal>(salesorderdetail.quantity); Trace("riga_ordine_quantità", rigaOrdineQuantità);
                decimal rigaOrdineScontoTotale = target.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount)?.Value ?? 0m; Trace("riga_ordine_sconto_totale", rigaOrdineScontoTotale);

                if (rigaOrdineCodiceIvaGuid == Guid.Empty) throw new ApplicationException("Vat Number not found");

                EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, rigaOrdineCodiceIvaGuid);

                //imposto l'entity reference del codice iva nel campo lookup di riga ordine
                target[salesorderdetail.res_vatnumberid] = erCodiceIVA;

                //imposto l'aliquota nel campo nascosto di riga ordine
                target[salesorderdetail.res_vatrate] = rigaOrdineCodiceIvaAliquota;

                //calcolo l'importo [riga ordine]
                decimal rigaOrdineImporto = rigaOrdinePrezzoUnitario * rigaOrdineQuantità; Trace("riga_ordine_importo", rigaOrdineImporto);

                //calcolo il totale imponibile [riga ordine]
                decimal rigaOrdineTotaleImponibile = rigaOrdineImporto - rigaOrdineScontoTotale; Trace("riga_ordine_totale_imponibile", rigaOrdineTotaleImponibile);

                //calcolo il totale iva [riga ordine]
                decimal rigaOrdineTotaleIVA = rigaOrdineTotaleImponibile * (rigaOrdineCodiceIvaAliquota / 100); Trace("riga_ordine_totale_iva", rigaOrdineTotaleIVA);

                //aggiorno i campi di riga ordine
                target[salesorderdetail.res_taxableamount] = new Money(rigaOrdineTotaleImponibile);
                target[salesorderdetail.tax] = new Money(rigaOrdineTotaleIVA);

                //---------------------------- Ordine ----------------------------//

                //creo l'oggetto Ordine da aggiornare
                Entity ordine = new Entity(salesorder.logicalName, erSalesOrder.Id);

                //recupero l'importo spesa accessoria dell'ordine dalla fetch
                decimal ordineTotaleProdotti = prodotto.GetAttributeValue<AliasedValue>("OrdineTotaleProdotti")?.Value is Money totallineitemamount ? totallineitemamount.Value : 0m; Trace("Ordine_Totale_Prodotti", ordineTotaleProdotti);
                decimal ordineScontoTotaleApplicato = prodotto.GetAttributeValue<AliasedValue>("OrdineScontoTotaleApplicato")?.Value is Money totaldiscountamount ? totaldiscountamount.Value : 0m; Trace("Ordine_Sconto_Totale_Applicato", ordineScontoTotaleApplicato);
                decimal ordineImportoSpesaAccessoria = prodotto.GetAttributeValue<AliasedValue>("OrdineImportoSpesaAccessoria")?.Value is Money freightamount ? freightamount.Value : 0m; Trace("Ordine_Importo_Spesa_Accessoria", ordineImportoSpesaAccessoria);

                //calcolo il totale imponibile dell'ordine (totale prodotti - sconto totale applicato + importo spesa accessoria)
                decimal ordineTotaleImponibile = ordineTotaleProdotti - ordineScontoTotaleApplicato + ordineImportoSpesaAccessoria; Trace("Ordine_Totale_Imponibile", ordineTotaleImponibile);

                //totale iva di tutte le righe
                decimal sommaRigheOrdineTotaleIva = prodotto.GetAttributeValue<AliasedValue>("OrdineTotaleIva")?.Value is Money totaltax ? totaltax.Value : 0m; Trace("Somma_Righe_Ordine_Totale_Iva", sommaRigheOrdineTotaleIva);

                //ordine aliquota iva

                var fetchAliquota = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                        <fetch>
                                          <entity name=""{salesorder.logicalName}"">
                                            <filter>
                                              <condition attribute=""{salesorder.salesorderid}"" operator=""eq"" value=""{erSalesOrder.Id}"" />
                                            </filter>
                                            <link-entity name=""{quote.logicalName}"" from=""quoteid"" to=""quoteid"" alias=""Offerta"">
                                              <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""OffertaCodiceIVA"">
                                                <attribute name=""{res_vatnumber.res_rate}"" alias=""OrdineAliquotaIVA"" />
                                              </link-entity>
                                            </link-entity>
                                          </entity>
                                        </fetch>";

                EntityCollection aliquotaCollection = service.RetrieveMultiple(new FetchExpression(fetchAliquota));
                if (aliquotaCollection == null) throw new ApplicationException("Cannot find Vat Number linked to Quote");

                Entity offertaCodiceIVA = aliquotaCollection.Entities[0];
                decimal ordineAliquotaIVA = offertaCodiceIVA.GetAttributeValue<AliasedValue>("OrdineAliquotaIVA")?.Value is decimal salesOrderRate ? salesOrderRate : 0m; Trace("Ordine_Aliquota_Iva", ordineAliquotaIVA);
                //decimal ordineAliquotaIVA = prodotto.GetAttributeValue<AliasedValue>("OrdineAliquotaIva")?.Value is decimal salesorderRate ? salesorderRate : 0m; Trace("Ordine_Aliquota_Iva", ordineAliquotaIVA);

                //iva su importo spesa accessoria
                decimal ordineIvaImportoSpesaAccessoria = ordineImportoSpesaAccessoria * (ordineAliquotaIVA / 100); Trace("Ordine_Iva_Importo_Spesa_Accessoria", ordineIvaImportoSpesaAccessoria);

                //sommatoria del totale iva di tutte le righe + iva calcolata su importo spesa accessoria
                decimal ordineTotaleIva = sommaRigheOrdineTotaleIva + ordineIvaImportoSpesaAccessoria; Trace("Ordine_Totale_Iva", ordineTotaleIva);

                //aggiorno i campi dell'ordine
                ordine[salesorder.totalamountlessfreight] = new Money(ordineTotaleImponibile);
                ordine[salesorder.totaltax] = new Money(ordineTotaleIva);

                crmServiceProvider.Service.Update(ordine);
            }
            #endregion

        }
    }
}

