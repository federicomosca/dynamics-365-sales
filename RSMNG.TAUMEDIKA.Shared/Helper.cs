using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA
{
    public class Helper
    {
    }
    public class Model
    {
        public class BasicOutput
        {
            public int result { get; set; }
            public string message { get; set; }
        }
    }
}
