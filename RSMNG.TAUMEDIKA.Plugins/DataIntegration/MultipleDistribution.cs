using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.DataIntegration
{
    public class MultipleDistribution : RSMNG.BaseClass
    {
        public MultipleDistribution(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "res_MultipleDistribution";
            PluginPrimaryEntityName = res_dataintegration.logicalName;
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            #region Dichiarazioni variabili
            PluginRegion = "Dichiarazioni variabili";
            EntityReference erDataIntegration = (EntityReference)crmServiceProvider.PluginContext.InputParameters["Target"];
            //Input
            int numberRows = (int)crmServiceProvider.PluginContext.InputParameters["NumberRows"];

            //Output
            int statusCode = (int)res_dataintegrationdetail.statuscodeValues.Distribuito_StateInattivo;
            string detailMessage = string.Empty;

            //Altro
            EntityCollection ecDataIntegrationDetail = null;
            Entity eDataIntegration = crmServiceProvider.Service.Retrieve(res_dataintegration.logicalName, erDataIntegration.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { res_dataintegration.res_integrationaction }));
            #endregion

            #region Distribuisco i DataIntegrationDetail
            PluginRegion = "Distribuisco i DataIntegrationDetail";
            try
            {
                #region Controllo il tipo di distribuzione da fare in base all'azione
                PluginRegion = "Controllo il tipo di distribuzione da fare in base all'azione";
                switch (eDataIntegration.GetAttributeValue<OptionSetValue>(res_dataintegration.res_integrationaction).Value)
                {
                    case (int)res_dataintegration.res_integrationactionValues.Articoli:
                        
                        break;
                }
                #endregion
            }
            catch (Exception e)
            {
                statusCode = (int)res_dataintegrationdetail.statuscodeValues.NotDistribuito_StateInattivo;
                detailMessage += $@"\r\n- Errore: {e.Message}";
            }
            finally
            {
                crmServiceProvider.PluginContext.OutputParameters["StatusCode"] = statusCode;
                crmServiceProvider.PluginContext.OutputParameters["DetailMessage"] = detailMessage;
            }
            #endregion
        }
    }
}
