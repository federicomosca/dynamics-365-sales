using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Aggiorno i campi Totale Imponibile, Sconto Totale e Totale Iva nell'entità parent
            PluginRegion = "Aggiorno i campi Totale Imponibile, Sconto Totale e Totale Iva nell'entità parent";

            if (target.Contains(salesorderdetail.tax) || target.Contains(salesorderdetail.manualdiscountamount) || target.Contains(salesorderdetail.res_taxableamount))
            {
                EntityReference erSalesOrder = postImage.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid);

                if (erSalesOrder == null)
                {
                    crmServiceProvider.TracingService.Trace("Quote non trovata, interrompendo il plugin.");
                    return;
                }

                // Verifica se i valori devono essere effettivamente aggiornati confrontandoli con quelli precedenti
                decimal scontoTotalePre = preImage.Contains(salesorderdetail.manualdiscountamount) ? preImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount).Value : 0;
                decimal totaleImponibilePre = preImage.Contains(salesorderdetail.res_taxableamount) ? preImage.GetAttributeValue<Money>(salesorderdetail.res_taxableamount).Value : 0;
                decimal totaleIvaPre = preImage.Contains(salesorderdetail.tax) ? preImage.GetAttributeValue<Money>(salesorderdetail.tax).Value : 0;

                decimal scontoTotale = target.Contains(salesorderdetail.manualdiscountamount) ? target.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount).Value : 0;
                decimal totaleImponibile = target.Contains(salesorderdetail.res_taxableamount) ? target.GetAttributeValue<Money>(salesorderdetail.res_taxableamount).Value : 0;
                decimal totaleIva = target.Contains(salesorderdetail.tax) ? target.GetAttributeValue<Money>(salesorderdetail.tax).Value : 0;

                if (scontoTotale == scontoTotalePre && totaleImponibile == totaleImponibilePre && totaleIva == totaleIvaPre)
                {
                    crmServiceProvider.TracingService.Trace("I valori non sono cambiati, evitato aggiornamento.");
                    return; // Interrompe l'aggiornamento se i valori non sono cambiati
                }

                decimal aliquotaOrdine = 0;
                decimal importoSpesaAccessoriaOrdine = 0;

                decimal scontoTotaleRigheOrdine;
                decimal totaleImponibileRigheOrdine;
                decimal totaleIvaRigheOrdine;

                var fetchData = new
                {
                    salesorderid = erSalesOrder.Id
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch aggregate=""true"">
                                      <entity name=""{salesorderdetail.logicalName}"">
                                        <attribute name=""{salesorderdetail.manualdiscountamount}"" alias=""ScontoTotale"" aggregate=""sum"" />
                                        <attribute name=""{salesorderdetail.res_taxableamount}"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""{salesorderdetail.tax}"" alias=""TotaleIva"" aggregate=""sum"" />
                                        <filter>
                                          <condition attribute=""{salesorderdetail.salesorderid}"" operator=""eq"" value=""{fetchData.salesorderid}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";

                EntityCollection aggregatiRigheOrdine = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                if (aggregatiRigheOrdine.Entities.Count > 0)
                {

                    scontoTotaleRigheOrdine = aggregatiRigheOrdine.Entities[0].ContainsAliasNotNull("ScontoTotale") ? aggregatiRigheOrdine.Entities[0].GetAliasedValue<Money>("ScontoTotale").Value : 0;
                    totaleImponibileRigheOrdine = aggregatiRigheOrdine.Entities[0].ContainsAliasNotNull("TotaleImponibile") ? aggregatiRigheOrdine.Entities[0].GetAliasedValue<Money>("TotaleImponibile").Value : 0;
                    totaleIvaRigheOrdine = aggregatiRigheOrdine.Entities[0].ContainsAliasNotNull("TotaleIva") ? aggregatiRigheOrdine.Entities[0].GetAliasedValue<Money>("TotaleIva").Value : 0;

                    var fetchData2 = new
                    {
                        salesorderid = erSalesOrder.Id
                    };

                    // Recupero Importo Spesa Accessoria  e Aliquota
                    var fetchXml2 = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""{salesorder.logicalName}"">
                                    <attribute name=""{salesorder.freightamount}"" />
                                    <filter>
                                      <condition attribute=""{salesorder.salesorderid}"" operator=""eq"" value=""{fetchData2.salesorderid}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""IVA"">
                                      <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";

                    EntityCollection ecOrdine = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml2));

                    if (ecOrdine.Entities.Count > 0)
                    {
                        importoSpesaAccessoriaOrdine = ecOrdine.Entities[0].ContainsAttributeNotNull(salesorder.freightamount) ? ecOrdine.Entities[0].GetAttributeValue<Money>(salesorder.freightamount).Value : 0;
                        aliquotaOrdine = ecOrdine.Entities[0].ContainsAliasNotNull("Aliquota") ? ecOrdine.Entities[0].GetAliasedValue<decimal>("Aliquota") : 0;
                    }

                    ////----------------------------------< CAMPI ORDINE DA AGGIORNARE >----------------------------------//

                    decimal ordineTotaleProdotti,      // S [salesorderdetail] totale imponibile
                        ordineScontoTotale,            // S [salesorderdetail] sconto totale
                        ordineTotaleIva;               // S [salesorderdetail] totale iva + iva calcolata su importo spesa accessoria

                    //--------------------------------------< CALCOLO DEI CAMPI >---------------------------------------//

                    ordineTotaleProdotti = totaleImponibileRigheOrdine;
                    ordineScontoTotale = scontoTotaleRigheOrdine;
                    ordineTotaleIva = totaleIvaRigheOrdine + (importoSpesaAccessoriaOrdine * (aliquotaOrdine / 100));

                    Entity enSalesOrder = new Entity(salesorder.logicalName, erSalesOrder.Id);

                    enSalesOrder[salesorder.totallineitemamount] = ordineTotaleProdotti != 0 ? new Money(ordineTotaleProdotti) : null;
                    enSalesOrder[salesorder.totaldiscountamount] = ordineScontoTotale != 0 ? new Money(ordineScontoTotale) : null;
                    enSalesOrder[salesorder.totaltax] = ordineTotaleIva != 0 ? new Money(ordineTotaleIva) : null;

                }
            }
            #endregion
        }
    }
}

