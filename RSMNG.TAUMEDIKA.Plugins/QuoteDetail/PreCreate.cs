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
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Guid targetId = target.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Codice Articolo
            PluginRegion = "Valorizzo il campo Codice Articolo";

            //product -> product number
            string productNumber = string.Empty;

            target.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);
            if (erProduct != null)
            {
                Entity product = crmServiceProvider.Service.Retrieve(DataModel.product.logicalName, erProduct.Id, new ColumnSet(DataModel.product.productnumber));
                if (product != null) { product.TryGetAttributeValue<string>(DataModel.product.productnumber, out productNumber); }
            }
            if (productNumber != string.Empty)
            {
                target[quotedetail.res_itemcode] = productNumber;
            }
            #endregion
        }
    }
}

