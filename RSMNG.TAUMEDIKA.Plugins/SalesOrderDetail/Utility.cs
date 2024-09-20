using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Crm.Sdk.Messages;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA.Shared.SalesOrderDetail

{
    public class Utility
    {
        public static void SetSalesOrder(IOrganizationService service, Entity target, Guid salesOrderId)
        {
            Decimal taxableAmountSum = 0;
            Decimal taxableAmount = 0;

            var fetchData = new
            {
                salesorderid = salesOrderId,
                salesorderdetailid = target.Id
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch aggregate=""true"">
                              <entity name=""salesorderdetail"">
                                <attribute name=""res_taxableamount"" alias=""taxableAmount"" aggregate=""sum"" />
                                <filter>
                                  <condition attribute=""salesorderid"" operator=""eq"" value=""{fetchData.salesorderid}"" />
                                  <condition attribute='salesorderdetailid' operator='ne' value=""{fetchData.salesorderdetailid}"" />
                                </filter>
                              </entity>
                            </fetch>";

            EntityCollection ecSum = service.RetrieveMultiple(new FetchExpression(fetchXml));
            Entity enSalesOrder = new Entity(salesorder.logicalName, salesOrderId);

            taxableAmount = target.ContainsAttributeNotNull(salesorderdetail.res_taxableamount) ? target.GetAttributeValue<Money>(salesorderdetail.res_taxableamount).Value : 0;
            taxableAmountSum = ecSum[0].ContainsAliasNotNull("taxableAmount") ? ecSum[0].GetAliasedValue<Money>("taxableAmount").Value : 0;

            enSalesOrder[salesorder.totallineitemamount] = taxableAmount + taxableAmountSum != 0 ? new Money(taxableAmount + taxableAmountSum) : null;

        }
    }
}
