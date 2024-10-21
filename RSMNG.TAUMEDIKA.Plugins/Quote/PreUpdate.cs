using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Country;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Quote
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.quote.logicalName;
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            crmServiceProvider.PluginContext.PreEntityImages.TryGetValue("PreImage", out Entity preImage);
            if (preImage == null) { return; }

            Entity postImage = target.GetPostImage(preImage);

            Guid quoteId = postImage.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.Quote.Utility.mandatoryFields);
            target.TryGetAttributeValue<EntityReference>(quote.res_additionalexpenseid, out EntityReference additionalExpense);
            if (additionalExpense != null)
            {
                target.TryGetAttributeValue<EntityReference>(quote.res_vatnumberid, out EntityReference vatNumber);
                if (vatNumber == null) { throw new ApplicationException($"Il campo Codice IVA Spesa Accessoria è obbligatorio"); }
            }
            #endregion

            #region Calcolo automatizzato Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale [DISABLED]
            PluginRegion = "Calcolo automatizzato Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale";
            //if (target.Contains(quote.totallineitemamount) ||
            //    target.Contains(quote.totaldiscountamount) ||
            //    target.Contains(quote.totaltax) ||
            //    target.Contains(quote.freightamount) ||
            //    target.Contains(quote.res_vatnumberid) ||
            //    target.Contains(quote.freightamount)
            //    )
            //{
            //    decimal taxableAmountSum = 0;     // Somma 'Totale Imponibile' righe ordine
            //    decimal taxRowsSum = 0;
            //    decimal freightAmount = 0;
            //    decimal totalDiscountAmount = 0; // Sconto totale
            //    decimal rateVatNumber = 0;
            //    decimal freightAmountRate = 0;
            //    decimal totalAmountlessFreight = 0; // Totale imponibile
            //    bool isTrace = false;


            //    //----Importo Spesa Accessoria
            //    Money freightAmountMoney = target.Contains(quote.freightamount) ? target.GetAttributeValue<Money>(quote.freightamount) : preImage.GetAttributeValue<Money>(quote.freightamount);
            //    freightAmount = freightAmountMoney != null ? freightAmountMoney.Value : 0;

            //    //----Recupera Aliquota Codice IVA Spesa Accessoria
            //    EntityReference erVatNumber = target.Contains(quote.res_vatnumberid) ? target.GetAttributeValue<EntityReference>(quote.res_vatnumberid) : preImage.GetAttributeValue<EntityReference>(quote.res_vatnumberid);

            //    if (freightAmount != 0 && erVatNumber != null)
            //    {
            //        Entity enVatNumber = crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, erVatNumber.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { res_vatnumber.res_rate }));

            //        rateVatNumber = enVatNumber.ContainsAttributeNotNull(res_vatnumber.res_rate) ? enVatNumber.GetAttributeValue<decimal>(res_vatnumber.res_rate) : 0;

            //        freightAmountRate = freightAmount * (rateVatNumber / 100);
            //    }


            //    //----Recupera somme aggregati su Righe Ordine
            //    var fetchData = new
            //    {
            //        quoteid = target.Id,
            //    };
            //    var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            //                <fetch aggregate=""true"">
            //                  <entity name=""{quotedetail.logicalName}"">
            //                    <attribute name=""{quotedetail.res_taxableamount}"" alias=""taxableAmount"" aggregate=""sum"" />
            //                    <attribute name=""{quotedetail.manualdiscountamount}"" alias=""ManualDiscountAmount"" aggregate=""sum"" />
            //                    <attribute name=""{quotedetail.tax}"" alias=""Tax"" aggregate=""sum"" />
            //                    <filter>
            //                      <condition attribute=""{quotedetail.quoteid}"" operator=""eq"" value=""{fetchData.quoteid}"" />
            //                    </filter>
            //                  </entity>
            //                </fetch>";
            //    if (isTrace) { crmServiceProvider.TracingService.crmServiceProvider.TracingService.Trace(fetchXml); }

            //    EntityCollection ecSum = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

            //    if (ecSum != null)
            //    {
            //        taxableAmountSum = ecSum[0].ContainsAliasNotNull("taxableAmount") ? ecSum[0].GetAliasedValue<Money>("taxableAmount").Value : 0;
            //        taxRowsSum = ecSum[0].ContainsAliasNotNull("Tax") ? ecSum[0].GetAliasedValue<Money>("Tax").Value : 0;
            //        totalDiscountAmount = ecSum[0].ContainsAliasNotNull("ManualDiscountAmount") ? ecSum[0].GetAliasedValue<Money>("ManualDiscountAmount").Value : 0;

            //        if (isTrace)
            //        {
            //            crmServiceProvider.TracingService.Trace("totalDiscountAmount: " + taxableAmountSum.ToString() + "\n" +
            //                                                                "taxRowsSum: " + taxRowsSum);
            //        }
            //    }

            //    totalAmountlessFreight = taxableAmountSum - totalDiscountAmount + freightAmount; // Totale imponibile
            //    decimal totalTax = taxRowsSum + freightAmountRate;
            //    decimal totalAmount = totalAmountlessFreight + totalTax;

            //    target[quote.totallineitemamount] = taxableAmountSum != 0 ? new Money(taxableAmountSum) : null; // Totale Prodotti = Somma totale imponibile righe
            //    target[quote.totalamountlessfreight] = totalAmountlessFreight != 0 ? new Money(totalAmountlessFreight) : null;
            //    target[quote.totaldiscountamount] = totalDiscountAmount != 0 ? new Money(totalDiscountAmount) : null;   // Somma Sconto Totale righe
            //    target[quote.totaltax] = (totalTax) != 0 ? new Money(totalTax) : null;

            //    target[quote.totalamount] = (totalAmount) != 0 ? new Money(totalAmount) : null;

            //    if (isTrace)
            //    {
            //        crmServiceProvider.TracingService.Trace(
            //            "totallineitemamount: " + taxableAmountSum.ToString() + "\n" +
            //            "totalamountlessfreight: " + totalAmountlessFreight.ToString() + "\n" +
            //            "totaltax: " + totalTax.ToString() + "\n" +
            //            "totalamount: " + totalAmount.ToString()

            //            );
            //    }
            //}
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

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";
            postImage.TryGetAttributeValue<EntityReference>(DataModel.quote.res_countryid, out EntityReference erCountry);
            string countryName = erCountry != null ? Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

            target[DataModel.quote.shipto_country] = countryName;
            #endregion

            #region Ricalcolo di Totale imponibile, Importo totale, Totale IVA
            PluginRegion = "Ricalcolo di Totale imponibile, Importo totale, Totale IVA";

            if (target.Contains(quote.totalamountlessfreight) &&
                target.Contains(quote.totaltax) &&
                target.Contains(quote.totalamount) &&
                target.Contains(quote.totaldiscountamount) &&
                target.Contains(quote.totallineitemamount)
                )
            {
                decimal totaleIva,
                    totaleProdotti;

                totaleIva = postImage.GetAttributeValue<Money>(quote.totaltax)?.Value ?? 0;
                totaleProdotti = postImage.GetAttributeValue<Money>(quote.totallineitemamount)?.Value ?? 0;

                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_prodotti {totaleProdotti}");
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_iva {totaleIva}");

                decimal totaleImponibile, importoTotale;


                var fetchQuote = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""{quote.logicalName}"">
                                    <attribute name=""{quote.freightamount}"" alias=""ImportoSpesaAccessoria"" />
                                    <filter>
                                      <condition attribute=""{quote.quoteid}"" operator=""eq"" value=""{quoteId}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIva"">
                                      <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace(fetchQuote);

                EntityCollection quoteCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchQuote));

                if (quoteCollection.Entities.Count > 0)
                {
                    Entity enQuote = quoteCollection.Entities[0];

                    decimal importoSpesaAccessoria = enQuote.GetAttributeValue<AliasedValue>("ImportoSpesaAccessoria")?.Value is Money freightamount ? freightamount.Value : 0;
                    decimal aliquota = enQuote.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal res_rate ? res_rate : 0;
                    decimal aliquotaImportoSpesaAccessoria = importoSpesaAccessoria * (aliquota / 100);

                    totaleIva += aliquotaImportoSpesaAccessoria;
                    totaleImponibile = totaleProdotti + importoSpesaAccessoria;
                    importoTotale = totaleImponibile + totaleIva;

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"importo_totale {importoTotale}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_iva {totaleIva}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_imponibile {totaleImponibile}");

                    target[quote.totaltax] = totaleIva != 0 ? new Money(totaleIva) : null;
                    target[quote.totalamountlessfreight] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                    target[quote.totalamount] = importoTotale != 0 ? new Money(importoTotale) : null;
                }
            }
            #endregion
        }
    }
}

