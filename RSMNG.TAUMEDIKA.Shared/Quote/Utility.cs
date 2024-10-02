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

namespace RSMNG.TAUMEDIKA.Shared.Quote
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                DataModel.quote.name,
                DataModel.quote.res_vatnumberid,
                DataModel.quote.revisionnumber,
                DataModel.quote.pricelevelid,
                DataModel.quote.quotenumber,
                DataModel.quote.customerid,
                DataModel.quote.ownerid,
                DataModel.quote.transactioncurrencyid,
            };
    }
    namespace Model
    {
        [DataContract]
        public class QuoteStatusRequest : RSMNG.TAUMEDIKA.Model.BasicOutput
        {
            [DataMember] public string EntityId { get; set; }
            [DataMember] public string Button { get; set; }
        }
    }
}
