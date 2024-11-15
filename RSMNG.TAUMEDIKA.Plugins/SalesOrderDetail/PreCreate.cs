using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Quote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            List<String> mandatoryFieldName = new List<String>();
            mandatoryFieldName.Add(salesorderdetail.productid);
            mandatoryFieldName.Add(salesorderdetail.salesorderid);
            mandatoryFieldName.Add(salesorderdetail.uomid);
            mandatoryFieldName.Add(salesorderdetail.productid);
            VerifyMandatoryField(crmServiceProvider, mandatoryFieldName);
            #endregion
        }
    }
}

