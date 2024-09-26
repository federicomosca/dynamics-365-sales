using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.QuoteDetail
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            var trace = crmServiceProvider.TracingService;

            if (target.Contains(quotedetail.res_taxableamount))
            {

                EntityReference enSalesOrder = target.Contains(quotedetail.quoteid) ? target.GetAttributeValue<EntityReference>(quotedetail.quoteid) : preImage.GetAttributeValue<EntityReference>(quotedetail.quoteid);
               
                if(enSalesOrder != null)
                {
                    // viene sovrascritto dalla logica nativa
                    //Utility.SetSalesOrder(crmServiceProvider.Service, trace, target, enSalesOrder.Id);
                }
                
            }


        }
    }
}

