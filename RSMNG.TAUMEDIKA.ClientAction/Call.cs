using Microsoft.SqlServer.Server;
using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RSMNG.Plugins;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.Xrm.Sdk.Query;
using System.Security.Cryptography.Xml;

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
            PluginActiveTrace = false;
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

                switch (actionName)
                {
                    case "UPDATE_QUOTE_STATUS":
                        #region Aggiornamento status dell'offerta
                        PluginRegion = "Aggiornamento status dell'offerta";
                        jsonDataOutput = Shared.Quote.Utility.UpdateQuoteStatusCode(serviceAdmin, tracingService, jsonDataInput);
                        #endregion

                        break;
                    case "UPDATE_SALESORDER_STATUS":
                        #region Aggiornamento status dell'ordine
                        PluginRegion = "Aggiornamento status dell'ordine";
                        jsonDataOutput = Shared.SalesOrder.Utility.UpdateSalesOrderCode(serviceAdmin, tracingService, jsonDataInput);
                        #endregion
                        break;
                    case "COPYPRICELEVEL":
                        #region Copio il listino prezzi
                        PluginRegion = "Copio il listino prezzi";
                        jsonDataOutput = Shared.PriceLevel.Utility.CopyPriceLevel(serviceAdmin, tracingService, jsonDataInput);
                        #endregion
                        break;
                    case "GET_DISTRIBUTIONFILE":
                        #region Prelevo il file di Distribuzione
                        PluginRegion = "Prelevo il file di Distribuzione";
                        jsonDataOutput = Shared.DataIntegration.Utility.GetDistributionFile(serviceAdmin, tracingService, jsonDataInput);
                        #endregion
                        break;
                    case "DELETE_ALL_PAYMENTSCHEDULE":
                        #region Cancello tutti pagamenti
                        PluginRegion = "Cancello tutti pagamenti";
                        jsonDataOutput = Shared.PaymentSchedule.Utility.DeleteAllPaymentSchedule(serviceAdmin);
                        #endregion
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
    }
}
