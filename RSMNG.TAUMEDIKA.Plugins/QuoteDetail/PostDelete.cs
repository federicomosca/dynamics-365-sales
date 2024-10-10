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
    public class PostDelete : RSMNG.BaseClass
    {
        public PostDelete(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Delete";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Aggiorno i campi Totale prodotti, Sconto totale applicato, Totale imponibile, Totale IVA e Importo totale sull'Offerta correlata
            PluginRegion = "Aggiorno i campi Totale prodotti, Sconto totale applicato, Totale imponibile, Totale IVA e Importo totale sull'Offerta correlata";


            #endregion
        }
    }
}

