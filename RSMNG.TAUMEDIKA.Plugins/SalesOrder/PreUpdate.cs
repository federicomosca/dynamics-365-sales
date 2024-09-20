using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Plugins.Shared;
using RSMNG.TAUMEDIKA.Plugins.Shared.SalesOrder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrder
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.salesorder.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            if (target.Contains(salesorder.totallineitemamount) || target.Contains(salesorder.totaldiscountamount))
            {
                decimal taxableAmountSum;
                decimal totalDiscountAmount;

                taxableAmountSum = Utility.CalculateSums(crmServiceProvider.Service, crmServiceProvider.TracingService, target);

                Money totDiscount = target.Contains(salesorder.totaldiscountamount) ? target.GetAttributeValue<Money>(salesorder.totaldiscountamount) : preImage.GetAttributeValue<Money>(salesorder.totaldiscountamount);
                totalDiscountAmount = totDiscount != null ? totDiscount.Value : 0;


                target[salesorder.totallineitemamount] = taxableAmountSum != 0 ? new Money(taxableAmountSum) : null;
                target[salesorder.totalamountlessfreight] = taxableAmountSum - totalDiscountAmount;
            }
        }
    }
}

