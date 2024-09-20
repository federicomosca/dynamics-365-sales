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

namespace RSMNG.TAUMEDIKA.Plugins.Shared.SalesOrder

{
    public class Utility
    {
        public static void CalculateSums(IOrganizationService service, ITracingService trace, Entity target)
        {
            // Imposta Totale Righe sovrascrivendo la logica nativa

            Decimal taxableAmountSum = 0;

            var fetchData = new
            {
                salesorderid = target.Id,
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch aggregate=""true"">
                              <entity name=""salesorderdetail"">
                                <attribute name=""res_taxableamount"" alias=""taxableAmount"" aggregate=""sum"" />
                                <filter>
                                  <condition attribute=""salesorderid"" operator=""eq"" value=""{fetchData.salesorderid}"" />
                                </filter>
                              </entity>
                            </fetch>";

            EntityCollection ecSum = service.RetrieveMultiple(new FetchExpression(fetchXml));

            taxableAmountSum = ecSum[0].ContainsAliasNotNull("taxableAmount") ? ecSum[0].GetAliasedValue<Money>("taxableAmount").Value : 0;

            
            target[salesorder.totallineitemamount] = taxableAmountSum != 0 ? new Money(taxableAmountSum) : null;

            
        }
    }
}
