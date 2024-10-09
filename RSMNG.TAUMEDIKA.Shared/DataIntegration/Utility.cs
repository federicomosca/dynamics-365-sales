using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web.SessionState;

namespace RSMNG.TAUMEDIKA.Shared.DataIntegration
{
    public class Utility
    {
        public static string GetDistributionFile(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            return Helper.DownloadFile(trace,service, DataModel.res_dataintegration.res_distributionfile, DataModel.res_dataintegration.logicalName,jsonDataInput);
        }
    }
}
