using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.SalesOrderDetail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            var trace = crmServiceProvider.TracingService;

            if (target.Contains(salesorderdetail.res_taxableamount))
            {

                EntityReference enSalesOrder = target.Contains(salesorderdetail.salesorderid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid) : preImage.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid);
               
                if(enSalesOrder != null)
                {
                    // viene sovrascritto dalla logica nativa
                    //Utility.SetSalesOrder(crmServiceProvider.Service, trace, target, enSalesOrder.Id);
                }
                
            }


        }
    }
}

