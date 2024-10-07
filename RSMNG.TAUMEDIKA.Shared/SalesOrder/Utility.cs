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
using System.Runtime.Serialization;

namespace RSMNG.TAUMEDIKA.Plugins.Shared.SalesOrder

{
    public class Utility
    {
        //public static EntityCollection GetAggregateSum(IOrganizationService service, ITracingService trace, Entity target)
        //{

        //    var fetchData = new
        //    {
        //        salesorderid = target.Id,
        //    };
        //    var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
        //                    <fetch aggregate=""true"">
        //                      <entity name=""salesorderdetail"">
        //                        <attribute name=""res_taxableamount"" alias=""taxableAmount"" aggregate=""sum"" />
        //                        <attribute name=""tax"" alias=""Tax"" aggregate=""sum"" />
        //                        <filter>
        //                          <condition attribute=""salesorderid"" operator=""eq"" value=""{fetchData.salesorderid}"" />
        //                        </filter>
        //                      </entity>
        //                    </fetch>";

        //    EntityCollection ecSum = service.RetrieveMultiple(new FetchExpression(fetchXml));

        //    return ecSum;

        //    //return taxableAmountSum = ecSum[0].ContainsAliasNotNull("taxableAmount") ? ecSum[0].GetAliasedValue<Money>("taxableAmount").Value : 0;
            
        //}
    }
    namespace Model
    {
        [DataContract]
        public class SalesOrderStatusRequest : RSMNG.TAUMEDIKA.Model.BasicOutput
        {
            [DataMember] public string EntityId { get; set; }
            [DataMember] public int? StateCode { get; set; }
            [DataMember] public int? StatusCode { get; set; }
        }
    }

}
