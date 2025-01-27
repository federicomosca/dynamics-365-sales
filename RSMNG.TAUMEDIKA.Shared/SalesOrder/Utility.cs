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
using RSMNG.Plugins;
using static RSMNG.TAUMEDIKA.Model;

namespace RSMNG.TAUMEDIKA.Shared.SalesOrder

{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
            salesorder.pricelevelid,
            salesorder.ordernumber,
            salesorder.customerid,
            salesorder.ownerid,
            salesorder.transactioncurrencyid,
        };
        public static string UpdateSalesOrderCode(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            string result = string.Empty;
            BasicOutput basicOutput = new BasicOutput() { result = 0, message = "Ok update effettuato con successo." };

            trace.Trace("update Sales Order Code");
            try
            {
                Model.SalesOrderStatusRequest salesORderRequest = Controller.Deserialize<Model.SalesOrderStatusRequest>(Uri.UnescapeDataString(jsonDataInput), typeof(Model.SalesOrderStatusRequest));

                string entityId = salesORderRequest.EntityId ?? string.Empty;
                int? statecode = salesORderRequest.StateCode ?? null;
                int? statuscode = salesORderRequest.StatusCode ?? null;

                trace.Trace(entityId);
                trace.Trace("statecode: " + statecode.ToString());
                trace.Trace("statuscode: " + statuscode.ToString());


                if (statecode == null || statuscode == null || entityId == string.Empty) { throw new Exception("Button or EntityId not found."); }
                trace.Trace("dentro if");
                Helper.UpdateEntityStatusCode(service, trace, salesorder.logicalName, entityId, (int)statecode, (int)statuscode);
            }
            catch (Exception ex)
            {
                basicOutput.result = -1;
                basicOutput.message = ex.Message;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                result = Controller.Serialize<BasicOutput>(basicOutput, typeof(BasicOutput));
            }

            return result;
        }
        public static void UpdateTotalsSalesOrder(IOrganizationService service, ITracingService trace, Entity target, Guid salesOrderId)
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

            trace.Trace("sum: " + (taxableAmount + taxableAmountSum).ToString());
            enSalesOrder[salesorder.totallineitemamount] = taxableAmount + taxableAmountSum != 0 ? new Money(taxableAmount + taxableAmountSum) : null;

            service.Update(enSalesOrder);
        }

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
