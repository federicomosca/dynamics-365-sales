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

namespace RSMNG.TAUMEDIKA.Shared.QuoteDetail
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                DataModel.quotedetail.quoteid,
                DataModel.quotedetail.productid,
                DataModel.quotedetail.quantity,
                DataModel.quotedetail.uomid,
            };
    }
}
