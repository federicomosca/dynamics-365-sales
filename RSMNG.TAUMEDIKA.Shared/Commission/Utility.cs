using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.Commission
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
            Entity enEntity = service.Retrieve(DataModel.res_commission.logicalName, entityId, new ColumnSet(new string[] { DataModel.res_commission.res_commissionid, DataModel.res_commission.res_name }));
            if (enEntity.Attributes.Contains(DataModel.res_commission.res_name) && enEntity.Attributes[DataModel.res_commission.res_name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.res_commission.res_name);
            }

            return ret;
        }
    }
}
