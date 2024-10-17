using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Product
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = product.logicalName;
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
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Valorizzo il campo Categoria principale
            PluginRegion = "Valorizzo il campo Categoria principale";

            postImage.TryGetAttributeValue<OptionSetValue>(product.res_origincode, out OptionSetValue originCode);
            Trace("originCode", originCode);


            int dynamics = (int)product.res_origincodeValues.Dynamics;
            int origine = (int)originCode.Value;

            Trace("dynamics", dynamics);
            Trace("origine ", origine);

            if (origine == dynamics)
            {
                Trace("origine == dynamics", origine == dynamics);
                postImage.TryGetAttributeValue<EntityReference>(product.parentproductid, out EntityReference erFamigliaAssociata);

                //in creazione, se entità principale non è null, vuol dire che il prodotto è una sottocategoria
                if (erFamigliaAssociata != null)
                {
                    Trace("erParentProduct", erFamigliaAssociata);

                    Entity categoriaPrincipale = crmServiceProvider.Service.Retrieve($"{product.logicalName}", erFamigliaAssociata.Id, new ColumnSet(product.parentproductid));
                    categoriaPrincipale.TryGetAttributeValue<EntityReference>(product.parentproductid, out EntityReference erFamigliaAssociataPadre);

                    target[product.res_parentcategoryid] = erFamigliaAssociataPadre ?? null;
                }
                else
                {
                    target[product.res_parentcategoryid] = null;
                }
            }
            #endregion
        }
    }
}

