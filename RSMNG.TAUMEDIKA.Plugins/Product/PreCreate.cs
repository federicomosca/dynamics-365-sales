using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Product
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.product.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            void Trace(string key, object value)
            {
                //TRACE TOGGLE
                bool isTraceActive = false;
                {
                    if (isTraceActive)
                    {
                        key = string.Concat(key.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
                        value = value.ToString();
                        crmServiceProvider.TracingService.Trace($"{key}: {value}");
                    }
                }
            }
            Trace("Check", "Trace attivo.");
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Valorizzo il campo Categoria principale
            PluginRegion = "Valorizzo il campo Categoria principale";

            target.TryGetAttributeValue<OptionSetValue>(product.res_origincode, out OptionSetValue originCode);

            Trace("originCode", originCode);

            int dynamics = (int)product.res_origincodeValues.Dynamics;
            Trace("dynamics", dynamics);
            int origine = (int)originCode.Value;
            Trace("origine ", origine);

            if (origine == dynamics)
            {
                Trace("origine == dynamics", origine == dynamics);
                target.TryGetAttributeValue<EntityReference>(product.parentproductid, out EntityReference erParentProduct);

                //in creazione, se entità principale non è null, vuol dire che il prodotto è una sottocategoria
                if (erParentProduct != null)
                {
                    Trace("erParentProduct", erParentProduct);
                    target[product.res_parentcategoryid] = erParentProduct;
                }
            }
            #endregion
        }
    }
}

