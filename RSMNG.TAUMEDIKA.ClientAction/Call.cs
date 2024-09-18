using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
            string result = "OK";

            

            try
            {
                //--- Deserializza --------------------------------------------------------------------------
                PriceLevel pl = RSMNG.Plugins.Controller.Deserialize<PriceLevel>(Uri.UnescapeDataString(jsonDataInput), typeof(PriceLevel));
                //-------------------------------------------------------------------------------------------

                
                Entity enPriceLevel = new Entity();

                OptionSetValueCollection optSet = new OptionSetValueCollection(pl.selectedScope.Select(scope => new OptionSetValue(scope)).ToList());


                enPriceLevel.Attributes.Add(pricelevel.name, pl.name);
                enPriceLevel.Attributes.Add(pricelevel.begindate, pl.begindate);
                enPriceLevel.Attributes.Add(pricelevel.enddate, pl.enddate);
                enPriceLevel.Attributes.Add(pricelevel.description, pl.description);
                enPriceLevel.Attributes.Add(pricelevel.transactioncurrencyid, new EntityReference("transactioncurrencyid", new Guid(pl.transactioncurrencyid)));
                enPriceLevel.Attributes.Add(pricelevel.res_isdefaultforagents, pl.isDefaultForAgents);
                enPriceLevel.Attributes.Add(pricelevel.res_isdefaultforwebsite, pl.isDefautWebsite);
                enPriceLevel.Attributes.Add(pricelevel.res_scopetypecodes, pl.selectedScope != null && pl.selectedScope.Any() ? optSet : null);

                service.Create(enPriceLevel);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


            return result;
        }
    }

    [System.Runtime.Serialization.DataContract]
    public class PriceLevel
    {
        [System.Runtime.Serialization.DataMember]
        public string name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int[] selectedScope { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime? begindate { get; set; }
        [System.Runtime.Serialization.DataMember]
        public DateTime? enddate { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string transactioncurrencyid { get; set; }
        [System.Runtime.Serialization.DataMember]
        public bool isDefautWebsite { get; set; }
        [System.Runtime.Serialization.DataMember]
        public bool isDefaultForAgents { get; set; }
        [System.Runtime.Serialization.DataMember]
        public object description { get; set; }
    }



}
