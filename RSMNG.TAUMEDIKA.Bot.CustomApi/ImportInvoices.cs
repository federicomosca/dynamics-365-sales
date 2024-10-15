using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ImportInvoices;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi
{
    public class ImportInvoices : RSMNG.BaseClass
    {
        public ImportInvoices(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.TRANSACTION;
            PluginMessage = "res_ImportInvoices";
            PluginPrimaryEntityName = "none";
            PluginRegion = "ImportInvoices";
        }

        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            crmServiceProvider.TracingService.Trace("Inizio");
            Response response = null;
            string outResult = "OK";
            string outMessage = "Nessun errore riscontrato";
            string outErrorCode = "00";
            bool debug = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.debug) ? (bool)crmServiceProvider.PluginContext.InputParameters[ParametersIn.debug] : false;
            Entity file = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.file) ? (Entity)crmServiceProvider.PluginContext.InputParameters[ParametersIn.file] : null;

            if (!debug)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    response = new Response(outErrorCode, outMessage);
                    crmServiceProvider.PluginContext.OutputParameters[ParametersOut.result] = outResult;
                    crmServiceProvider.PluginContext.OutputParameters[ParametersOut.error] = response.ErrorResponseEntity;
                }
            }
            else
            {
                outResult = "OK";
                outErrorCode = "00";
                outMessage = "'DEBUG MODE' API eseguita correttamente";
                response = new Response(outErrorCode, outMessage);
                crmServiceProvider.PluginContext.OutputParameters[ParametersOut.result] = outResult;
                crmServiceProvider.PluginContext.OutputParameters[ParametersOut.error] = response.ErrorResponseEntity;
            }
            crmServiceProvider.TracingService.Trace("Fine");
        }
    }
}
