using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.SystemUser
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
            Entity enEntity = service.Retrieve(DataModel.systemuser.logicalName, entityId, new ColumnSet(new string[] { DataModel.systemuser.systemuserid, DataModel.systemuser.fullname }));
            if (enEntity.Attributes.Contains(DataModel.systemuser.fullname) && enEntity.Attributes[DataModel.systemuser.fullname] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.systemuser.fullname);
            }

            return ret;
        }
        public static string GetAgentNumber(IOrganizationService service, Guid entityId)
        {
            string ret = string.Empty;
            if (entityId.Equals(Guid.Empty))
            {
                return ret;
            }
            Entity enEntity = service.Retrieve(DataModel.systemuser.logicalName, entityId, new ColumnSet(new string[] { DataModel.systemuser.systemuserid, DataModel.systemuser.res_isagente, DataModel.systemuser.res_agentnumber }));
            if (enEntity.ContainsAttributeNotNull(DataModel.systemuser.res_isagente) 
                && enEntity.GetAttributeValue<bool>(DataModel.systemuser.res_isagente)== true
                && enEntity.ContainsAttributeNotNull(DataModel.systemuser.res_agentnumber))
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.systemuser.res_agentnumber);
            }

            return ret;
        }
    }
}
