using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.Account
{
    public class Utility
    {
        public static string GetName(IOrganizationService service, Guid entityId)
        {
            string ret = string.Empty;
            if (entityId.Equals(Guid.Empty))
            {
                return ret;
            }
            Entity enEntity = service.Retrieve(DataModel.account.logicalName, entityId, new ColumnSet(new string[] { DataModel.account.accountid, DataModel.account.name }));
            if (enEntity.Attributes.Contains(DataModel.account.name) && enEntity.Attributes[DataModel.account.name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.account.name);
            }

            return ret;
        }
        public static bool CheckFiscalCode(IOrganizationService service, string fiscalCode, Guid? accountId = null)
        {
            var fetchData = new
            {
                res_taxcode = fiscalCode,
                accountid = accountId != null ? $@"<condition attribute=""{DataModel.account.accountid}"" operator=""ne"" value=""{accountId.Value.ToString()}"" />" : "",
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch returntotalrecordcount=""true"">
              <entity name=""{DataModel.account.logicalName}"">
                <attribute name=""{DataModel.account.accountid}"" />
                <filter>
                  <condition attribute=""{DataModel.account.res_taxcode}"" operator=""eq"" value=""{fetchData.res_taxcode}"" />
                  {fetchData.accountid}
                </filter>
              </entity>
            </fetch>";
            return service.RetrieveMultiple(new FetchExpression(fetchXml)).TotalRecordCount == -1;
        }
    }
}
