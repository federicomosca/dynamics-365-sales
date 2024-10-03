using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.Product
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
            Entity enEntity = service.Retrieve(DataModel.product.logicalName, entityId, new ColumnSet(new string[] { DataModel.product.productid, DataModel.product.name }));
            if (enEntity.Attributes.Contains(DataModel.product.name) && enEntity.Attributes[DataModel.product.name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.product.name);
            }

            return ret;
        }
    }
}
