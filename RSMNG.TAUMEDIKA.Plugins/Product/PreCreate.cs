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
            
            crmServiceProvider.TracingService.Trace("Check", "Trace attivo.");
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Valorizzo il campo Categoria principale
            PluginRegion = "Valorizzo il campo Categoria principale";

            target.TryGetAttributeValue<OptionSetValue>(product.res_origincode, out OptionSetValue originCode);

            crmServiceProvider.TracingService.Trace("originCode", originCode);

            int dynamics = (int)product.res_origincodeValues.Dynamics;
            crmServiceProvider.TracingService.Trace("dynamics", dynamics);
            int origine = (int)originCode.Value;
            crmServiceProvider.TracingService.Trace("origine ", origine);

            if (origine == dynamics)
            {
                crmServiceProvider.TracingService.Trace("origine == dynamics", origine == dynamics);
                target.TryGetAttributeValue<EntityReference>(product.parentproductid, out EntityReference erFamigliaAssociata);

                //in creazione, se entità principale non è null, vuol dire che il prodotto è una sottocategoria
                if (erFamigliaAssociata != null)
                {
                    crmServiceProvider.TracingService.Trace("erParentProduct", erFamigliaAssociata);

                    Entity categoriaPrincipale = crmServiceProvider.Service.Retrieve($"{product.logicalName}", erFamigliaAssociata.Id, new ColumnSet(product.parentproductid));
                    categoriaPrincipale.TryGetAttributeValue<EntityReference>(product.parentproductid, out EntityReference erFamigliaAssociataPadre);

                    target[product.res_parentcategoryid] = erFamigliaAssociataPadre ?? null;
                }
            }
            #endregion
        }
    }
}

