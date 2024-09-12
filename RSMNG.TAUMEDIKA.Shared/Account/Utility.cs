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
            Entity enEntity = service.Retrieve(account.logicalName, entityId, new ColumnSet(new string[] { account.accountid, account.name }));
            if (enEntity.Attributes.Contains(account.name) && enEntity.Attributes[account.name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(account.name);
            }

            return ret;
        }
    }
}
