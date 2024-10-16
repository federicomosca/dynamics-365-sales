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
                bool isTraceActive = true;
                {
                    if (isTraceActive)
                    {
                        key = string.Concat(key.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
                        value = value.ToString();
                        crmServiceProvider.TracingService.Trace($"{key}: {value}");
                    }
                }
            }

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Valorizzo il campo Categoria principale
            PluginRegion = "Valorizzo il campo Categoria principale";

            target.TryGetAttributeValue<OptionSetValue>(product.res_origincode, out OptionSetValue originCode);
            OptionSetValue dynamics = new OptionSetValue((int)product.res_origincodeValues.Dynamics);

            if (originCode == dynamics)
            {
                Trace("originCode", originCode);
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

