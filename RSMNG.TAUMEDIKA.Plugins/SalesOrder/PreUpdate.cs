using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrder
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = salesorder.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            Guid salesorderId = postImage.Id;

            #region Popolo in automatico il Destinatario
            string destination = string.Empty;
            if (postImage.ContainsAttributeNotNull(salesorder.res_shippingreference))
            {
                destination = postImage.GetAttributeValue<string>(salesorder.res_shippingreference);
            }
            if (string.IsNullOrEmpty(destination) && postImage.ContainsAttributeNotNull(salesorder.customerid))
            {
                destination = Shared.Account.Utility.GetName(crmServiceProvider.Service, postImage.GetAttributeValue<EntityReference>(salesorder.customerid).Id);
            }
            target.AddWithRemove(salesorder.res_recipient, destination);
            #endregion

            #region Valorizzazione automatica del campo Motivo Stato Precedente
            PluginRegion = "Valorizzazione automatica del campo Motivo Stato Precedente";

            //recupero il motivo stato dalla preimage e lo salvo nel campo motivo stato precedente
            preImage.TryGetAttributeValue<OptionSetValue>(quote.statuscode, out var previousStatusCode);
            if (previousStatusCode != null)
            {
                target["res_oldstatuscode"] = previousStatusCode;
            }
            #endregion

            #region Ricalcolo di Totale imponibile, Importo totale, Totale IVA
            PluginRegion = "Ricalcolo di Totale imponibile, Importo totale, Totale IVA";

            if (target.Contains(salesorder.totalamountlessfreight) &&
                target.Contains(salesorder.totaltax) &&
                target.Contains(salesorder.totalamount) &&
                target.Contains(salesorder.totaldiscountamount) &&
                target.Contains(salesorder.totallineitemamount)
                )
            {
                decimal totaleIva,
                    totaleProdotti;

                totaleIva = postImage.GetAttributeValue<Money>(salesorder.totaltax)?.Value ?? 0;
                totaleProdotti = postImage.GetAttributeValue<Money>(salesorder.totallineitemamount)?.Value ?? 0;

                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totaleIva {totaleIva}");
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totaleProdotti {totaleProdotti}");

                decimal totaleImponibile, importoTotale;


                var fetchSalesOrder = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""{salesorder.logicalName}"">
                                    <attribute name=""{salesorder.freightamount}"" />
                                    <filter>
                                      <condition attribute=""{salesorder.salesorderid}"" operator=""eq"" value=""{salesorderId}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""{res_vatnumber.logicalName}"">
                                      <attribute name=""{res_vatnumber.res_rate}"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace(fetchSalesOrder);

                EntityCollection orderCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchSalesOrder));

                if (orderCollection.Entities.Count > 0)
                {
                    Entity enSalesOrder = orderCollection.Entities[0];

                    decimal importoSpesaAccessoria = enSalesOrder.ContainsAttributeNotNull(salesorder.freightamount) ?  enSalesOrder.GetAttributeValue<Money>(salesorder.freightamount).Value : 0;
                   
                    decimal aliquota = enSalesOrder.ContainsAliasNotNull($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") ? enSalesOrder.GetAliasedValue<decimal>($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") : 1;
                    decimal aliquotaImportoSpesaAccessoria = importoSpesaAccessoria * ((aliquota == 0 ? 1: aliquota) / 100);

                    totaleIva += aliquotaImportoSpesaAccessoria != 0 ? aliquotaImportoSpesaAccessoria : 0;
                    totaleImponibile = totaleProdotti + importoSpesaAccessoria;
                    importoTotale = totaleImponibile + totaleIva;

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"importoTotale {importoTotale}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totaleIva {totaleIva}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totaleImponibile {totaleImponibile}");

                    target[salesorder.totaltax] = totaleIva != 0 ? new Money(totaleIva) : null;
                    target[salesorder.totalamountlessfreight] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                    target[salesorder.totalamount] = importoTotale != 0 ? new Money(importoTotale) : null;
                }
            }
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";

            if (target.Contains(salesorder.res_countryid))
            {
                postImage.TryGetAttributeValue<EntityReference>(salesorder.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

                target[salesorder.shipto_country] = countryName;

            }
            #endregion

            #region Data
            
            EntityReference erQuote = target.Contains(salesorder.quoteid) ? target.GetAttributeValue<EntityReference>(salesorder.quoteid) : preImage.GetAttributeValue<EntityReference>(salesorder.quoteid);
            DateTime? date = target.Contains(salesorder.res_date) ? target.GetAttributeValue<DateTime?>(salesorder.res_date) : preImage.GetAttributeValue<DateTime?>(salesorder.res_date);

            if(date == null && erQuote != null)
            {

                var fetchData = new
                {
                    quoteid = erQuote.Id,
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""quoteclose"">
                                        <attribute name=""actualend"" />
                                        <filter>
                                          <condition attribute=""quoteid"" operator=""eq"" value=""{fetchData.quoteid}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";

                EntityCollection ecQuoteClose = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));
                
                if (ecQuoteClose.Entities.Count > 0 && ecQuoteClose.Entities[0].ContainsAttributeNotNull("actualend"))
                {                    
                    target[salesorder.res_date] = ecQuoteClose.Entities[0].GetAttributeValue<DateTime>("actualend");
                }

                    
            }
            #endregion
        }
    }
}

