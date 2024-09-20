using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Plugins.Shared;
using RSMNG.TAUMEDIKA.Plugins.Shared.SalesOrder;
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

            if (target.Contains(salesorder.totallineitemamount) ||
                target.Contains(salesorder.totaldiscountamount) ||
                target.Contains(salesorder.totaltax) ||
                target.Contains(salesorder.freightamount) ||
                target.Contains(salesorder.res_vatnumberid) ||
                target.Contains(salesorder.freightamount)
                )
            {
                decimal taxableAmountSum = 0;
                decimal taxRowsSum = 0;
                decimal freightAmount = 0;
                decimal totalDiscountAmount = 0;
                decimal rateVatNumber = 0;
                decimal freightAmountRate = 0;

                //----Sconto Totale
                Money totDiscount = target.Contains(salesorder.totaldiscountamount) ? target.GetAttributeValue<Money>(salesorder.totaldiscountamount) : preImage.GetAttributeValue<Money>(salesorder.totaldiscountamount);
                totalDiscountAmount = totDiscount != null ? totDiscount.Value : 0;

                //----Importo Spesa Accessoria
                Money freightAmountMoney = target.Contains(salesorder.freightamount) ? target.GetAttributeValue<Money>(salesorder.freightamount) : preImage.GetAttributeValue<Money>(salesorder.freightamount);
                freightAmount = freightAmountMoney != null ? freightAmountMoney.Value : 0;

                //----Recupera Aliquota Codice IVA Spesa Accessoria
                EntityReference erVatNumber = target.GetAttributeValue<EntityReference>(salesorder.res_vatnumberid);

                if(freightAmount != 0 && erVatNumber != null)
                {
                    Entity enVatNumber = crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, erVatNumber.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { res_vatnumber.res_rate }));

                    rateVatNumber = enVatNumber.ContainsAttributeNotNull(res_vatnumber.res_rate) ? enVatNumber.GetAttributeValue<decimal>(res_vatnumber.res_rate) : 0;
                    
                    freightAmountRate = freightAmount * rateVatNumber;
                }


                //----Recupera somme aggregati su Righe Ordine
                var fetchData = new
                {
                    salesorderid = target.Id,
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch aggregate=""true"">
                              <entity name=""salesorderdetail"">
                                <attribute name=""res_taxableamount"" alias=""taxableAmount"" aggregate=""sum"" />
                                <attribute name=""tax"" alias=""Tax"" aggregate=""sum"" />
                                <filter>
                                  <condition attribute=""salesorderid"" operator=""eq"" value=""{fetchData.salesorderid}"" />
                                </filter>
                              </entity>
                            </fetch>";

                EntityCollection ecSum = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                if (ecSum != null && ecSum.Entities.Count > 0)
                {
                    totalDiscountAmount = ecSum[0].ContainsAliasNotNull("taxableAmount") ? ecSum[0].GetAliasedValue<Money>("taxableAmount").Value : 0;
                    taxRowsSum = ecSum[0].ContainsAliasNotNull("Tax") ? ecSum[0].GetAliasedValue<Money>("Tax").Value : 0;
                }

                decimal totalAmountlessFreight = taxableAmountSum - totalDiscountAmount;
                decimal totalTax = taxRowsSum + freightAmountRate;

                target[salesorder.totallineitemamount] = taxableAmountSum != 0 ? new Money(taxableAmountSum) : null;
                target[salesorder.totalamountlessfreight] = totalAmountlessFreight != 0 ? new Money(totalAmountlessFreight) : null;
                target[salesorder.totaltax] = (totalTax) != 0 ? new Money(totalTax) : null;

                target[salesorder.totalamount] = (totalAmountlessFreight + totalTax) != 0 ? new Money(totalAmountlessFreight + totalTax) : null;
            }
        }
    }
}

