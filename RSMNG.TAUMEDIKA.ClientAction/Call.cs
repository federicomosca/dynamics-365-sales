using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.ClientAction
{
    public class Call : RSMNG.BaseClass
    {
        public Call(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "res_ClientAction";
            PluginPrimaryEntityName = "none";
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            try
            {
                IOrganizationService serviceAdmin = crmServiceProvider.ServiceAdmin;
                ITracingService tracingService = crmServiceProvider.TracingService;
                String jsonDataOutput = "";
                String actionName = (String)crmServiceProvider.PluginContext.InputParameters["actionName"];
                String jsonDataInput = (String)crmServiceProvider.PluginContext.InputParameters["jsonDataInput"];
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"ActionName:{actionName}.");
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"JsonDataInput:{jsonDataInput}.");
                Model.BasicOutput basicOutput = new Model.BasicOutput();
                switch (actionName)
                {
                    case "CASE":

                        #region Azione1
                        PluginRegion = "Azione1";
                        basicOutput.result = 0; basicOutput.message = "Operazione effettuata.";
                        try
                        {
                        }
                        catch (Exception ex)
                        {
                            basicOutput.result = -1;
                            basicOutput.message = ex.Message;
                        }
                        jsonDataOutput = Plugins.Controller.Serialize<Model.BasicOutput>(basicOutput, typeof(Model.BasicOutput));
                        #endregion
                        
                        break;
                    case "COPYPRICELEVEL":
                        jsonDataOutput = CopyPriceLevel(serviceAdmin, tracingService, jsonDataInput);
                        break;
                }
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"JsonDataOutput:{jsonDataOutput}.");
                crmServiceProvider.PluginContext.OutputParameters["jsonDataOutput"] = jsonDataOutput;
            }
            catch (Exception ex)
            {
                throw new Exception("ERRORE CALL ACTION: " + ex);
            }
        }

        public static string CopyPriceLevel(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            string result = String.Empty;

            trace.Trace("COPYPRICELEVEL");





            return result;
        }
    }
}
