using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Crm.Sdk.Messages;
using System.Runtime.Serialization;
using RSMNG.Plugins;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Quote.Model;
using static RSMNG.TAUMEDIKA.Model;

namespace RSMNG.TAUMEDIKA.Shared.Quote
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                DataModel.quote.name,
                DataModel.quote.revisionnumber,
                DataModel.quote.pricelevelid,
                DataModel.quote.quotenumber,
                DataModel.quote.customerid,
                DataModel.quote.ownerid,
                DataModel.quote.transactioncurrencyid,
            };
        public static string UpdateQuoteStatusCode(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            string result = string.Empty;
            BasicOutput basicOutput = new BasicOutput() { result = 0, message = "Ok update effettuato con successo." };

            try
            {
                QuoteStatusRequest quoteRequest = Controller.Deserialize<QuoteStatusRequest>(Uri.UnescapeDataString(jsonDataInput), typeof(QuoteStatusRequest));

                string entityId = quoteRequest.EntityId ?? string.Empty;
                int? statecode = quoteRequest.StateCode ?? null;
                int? statuscode = quoteRequest.StatusCode ?? null;

                if (statecode == null || statuscode == null || entityId == string.Empty) { throw new Exception("Button or EntityId not found."); }

                Helper.UpdateEntityStatusCode(service, trace, quote.logicalName, entityId, (int)statecode, (int)statuscode);
            }
            catch (Exception ex)
            {
                basicOutput.result = -1;
                basicOutput.message = ex.Message;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                result = RSMNG.Plugins.Controller.Serialize<BasicOutput>(basicOutput, typeof(BasicOutput));
            }

            return result;

        }
    }
    namespace Model
    {
        [DataContract]
        public class QuoteStatusRequest : RSMNG.TAUMEDIKA.Model.BasicOutput
        {
            [DataMember] public string EntityId { get; set; }
            [DataMember] public int? StateCode { get; set; }
            [DataMember] public int? StatusCode { get; set; }
        }
    }
}
