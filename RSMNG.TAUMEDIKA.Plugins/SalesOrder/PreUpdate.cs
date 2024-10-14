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
            PluginPrimaryEntityName = DataModel.salesorder.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Calcolo automatizzato Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale
            PluginRegion = "Calcolo automatizzato Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale";

            if (target.Contains(salesorder.totallineitemamount) ||
                target.Contains(salesorder.totaldiscountamount) ||
                target.Contains(salesorder.totaltax) ||
                target.Contains(salesorder.freightamount) ||
                target.Contains(salesorder.res_vatnumberid) ||
                target.Contains(salesorder.freightamount)
                )
            {
                decimal totImponibileRighe = 0;
                decimal totIvaRighe = 0;
                decimal importoSpesaAccessoria = 0;
                decimal totScontoRighe = 0;
                decimal aliquotaSpesaAccessoria = 0;
                decimal freightAmountRate = 0;
                decimal totaleImponibile = 0;
                decimal totaleIva = 0;
                decimal importoTotale = 0;
                decimal scontoTotaleApplicato = 0;

                bool isTrace = false;


                //----Importo Spesa Accessoria
                Money freightamount = target.Contains(salesorder.freightamount) ? target.GetAttributeValue<Money>(salesorder.freightamount) : preImage.GetAttributeValue<Money>(salesorder.freightamount);
                importoSpesaAccessoria = freightamount != null ? freightamount.Value : 0;

                //----Recupera Aliquota Codice IVA Spesa Accessoria
                EntityReference erIvaSpesaAccessoria = target.Contains(salesorder.res_vatnumberid) ? target.GetAttributeValue<EntityReference>(salesorder.res_vatnumberid) : preImage.GetAttributeValue<EntityReference>(salesorder.res_vatnumberid);

                if (importoSpesaAccessoria != 0 && erIvaSpesaAccessoria != null)
                {
                    Entity enVatNumber = crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, erIvaSpesaAccessoria.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { res_vatnumber.res_rate }));

                    aliquotaSpesaAccessoria = enVatNumber.ContainsAttributeNotNull(res_vatnumber.res_rate) ? enVatNumber.GetAttributeValue<decimal>(res_vatnumber.res_rate) : 0;
                    freightAmountRate = importoSpesaAccessoria * (aliquotaSpesaAccessoria / 100);
                }

                // Se target contiene questi tre valori, vuol dire che sono stati calcolati nel Post Update delle righe
                if (!target.Contains(salesorder.totalamountlessfreight) && !target.Contains(salesorder.totaltax) && !target.Contains(salesorder.totaldiscountamount))
                {
                    var fetchData = new
                    {
                        salesorderid = target.Id,
                    };
                    var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch aggregate=""true"">
                              <entity name=""salesorderdetail"">
                                <attribute name=""res_taxableamount"" alias=""taxableAmount"" aggregate=""sum"" />
                                <attribute name=""manualdiscountamount"" alias=""ManualDiscountAmount"" aggregate=""sum"" />
                                <attribute name=""tax"" alias=""Tax"" aggregate=""sum"" />
                                <filter>
                                  <condition attribute=""salesorderid"" operator=""eq"" value=""{fetchData.salesorderid}"" />
                                </filter>
                              </entity>
                            </fetch>";
                    if (isTrace) { crmServiceProvider.TracingService.Trace(fetchXml); }

                    EntityCollection ecSum = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                    if (ecSum != null)
                    {
                        totImponibileRighe = ecSum[0].ContainsAliasNotNull("taxableAmount") ? ecSum[0].GetAliasedValue<Money>("taxableAmount").Value : 0;
                        totIvaRighe = ecSum[0].ContainsAliasNotNull("Tax") ? ecSum[0].GetAliasedValue<Money>("Tax").Value : 0;
                        totScontoRighe = ecSum[0].ContainsAliasNotNull("ManualDiscountAmount") ? ecSum[0].GetAliasedValue<Money>("ManualDiscountAmount").Value : 0;

                        if (isTrace)
                        {
                            crmServiceProvider.TracingService.Trace("totalDiscountAmount: " + totImponibileRighe.ToString() + "\n" + "taxRowsSum: " + totIvaRighe);
                        }
                    }

                    scontoTotaleApplicato = totScontoRighe;
                    totaleIva = totIvaRighe + freightAmountRate;

                }
                else if (target.Contains(salesorder.totalamountlessfreight) && target.Contains(salesorder.totaltax) && target.Contains(salesorder.totaldiscountamount))
                {
                    // prendo valori presenti nel target passati dal post update delle righe
                    scontoTotaleApplicato = target.ContainsAttributeNotNull(salesorder.totaldiscountamount) ? target.GetAttributeValue<decimal>(salesorder.totaldiscountamount) : 0;
                    totaleIva = target.ContainsAttributeNotNull(salesorder.totaltax) ? target.GetAttributeValue<Money>(salesorder.totaltax).Value : 0;
                    totImponibileRighe = target.ContainsAttributeNotNull(salesorder.totallineitemamount) ? target.GetAttributeValue<Money>(salesorder.totallineitemamount).Value : 0;



                }

                totaleImponibile = totImponibileRighe - scontoTotaleApplicato + importoSpesaAccessoria;
                importoTotale = totaleImponibile + totaleIva;

                target[salesorder.totallineitemamount] = totImponibileRighe != 0 ? new Money(totImponibileRighe) : null; // Totale Righe = Somma totale imponibile righe
                target[salesorder.totalamountlessfreight] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                target[salesorder.totaldiscountamount] = totScontoRighe != 0 ? new Money(totScontoRighe) : null;
                target[salesorder.totaltax] = (totaleIva) != 0 ? new Money(totaleIva) : null;

                target[salesorder.totalamount] = (importoTotale) != 0 ? new Money(importoTotale) : null;

                if (isTrace)
                {
                    crmServiceProvider.TracingService.Trace(
                        "totallineitemamount: " + totImponibileRighe.ToString() + "\n" +
                        "totalamountlessfreight: " + totaleImponibile.ToString() + "\n" +
                        "totaltax: " + totaleIva.ToString() + "\n" +
                        "totalamount: " + importoTotale.ToString()

                        );
                }
            }
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";

            if (target.Contains(quote.res_countryid))
            {
                postImage.TryGetAttributeValue<EntityReference>(quote.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

                target[salesorder.shipto_country] = countryName;

            }
            #endregion
        }
    }
}

