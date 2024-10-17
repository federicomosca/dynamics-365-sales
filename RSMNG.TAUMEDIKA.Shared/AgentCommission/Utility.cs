using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA.Shared.AgentCommission
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                res_agentcommission.ownerid,
                res_agentcommission.res_commissionid
            };
        public static string GetName(IOrganizationService service, Guid entityId)
        {
            string ret = string.Empty;
            if (entityId.Equals(Guid.Empty))
            {
                return ret;
            }
            Entity enEntity = service.Retrieve(DataModel.res_agentcommission.logicalName, entityId, new ColumnSet(new string[] { DataModel.res_agentcommission.res_agentcommissionid, DataModel.res_agentcommission.res_name }));
            if (enEntity.Attributes.Contains(DataModel.res_agentcommission.res_name) && enEntity.Attributes[DataModel.res_agentcommission.res_name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.res_agentcommission.res_name);
            }

            return ret;
        }
    }
}
