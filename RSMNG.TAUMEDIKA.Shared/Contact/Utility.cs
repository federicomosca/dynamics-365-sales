using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.Contact
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
            Entity enEntity = service.Retrieve(DataModel.contact.logicalName, entityId, new ColumnSet(new string[] { DataModel.contact.contactid, DataModel.contact.fullname}));
            if (enEntity.Attributes.Contains(DataModel.contact.fullname) && enEntity.Attributes[DataModel.contact.fullname] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.contact.fullname);
            }

            return ret;
        }
    }
}
