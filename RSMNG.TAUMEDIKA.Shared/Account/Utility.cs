using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
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
    }
}
