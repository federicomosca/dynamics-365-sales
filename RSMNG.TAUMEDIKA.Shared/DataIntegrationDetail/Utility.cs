using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.DataIntegrationDetail
{
    public class Utility
    {
        public static EntityCollection GetDataIntegrationDetails(IOrganizationService service, Guid dataIntegrationId, int numberRows, int statusCode)
        {
            var fetchData = new
            {
                statuscode = statusCode.ToString(),
                res_dataintegrationid = dataIntegrationId.ToString(),

            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch top=""{numberRows}"">
              <entity name=""{res_dataintegrationdetail.logicalName}"">
                <attribute name=""{res_dataintegrationdetail.res_rownum}"" />
                <attribute name=""{res_dataintegrationdetail.res_integrationrow}"" />
                <filter>
                  <condition attribute=""{res_dataintegrationdetail.statuscode}"" operator=""eq"" value=""{fetchData.statuscode}"" />
                  <condition attribute=""{res_dataintegrationdetail.res_dataintegrationid}"" operator=""eq"" value=""{fetchData.res_dataintegrationid}"" />
                </filter>
              </entity>
            </fetch>";
            return service.RetrieveMultiple(new FetchExpression(fetchXml));
        }
    }
}
