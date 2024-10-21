using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.VatNumber
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.res_vatnumber.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Genero in automatico il nome del Codice IVA
            PluginRegion = "Genero in automatico il nome del Codice IVA";

            string code = target.ContainsAttributeNotNull(res_vatnumber.res_code) ? target.GetAttributeValue<string>(res_vatnumber.res_code) : string.Empty;
            string description = target.ContainsAttributeNotNull(res_vatnumber.res_description) ? target.GetAttributeValue<string>(res_vatnumber.res_description) : string.Empty;

            target.AddWithRemove(res_cap.res_name, $"{code} - {description}");
            #endregion
        }
    }
}
