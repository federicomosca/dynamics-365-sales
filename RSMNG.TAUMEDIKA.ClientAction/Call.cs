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
                        jsonDataOutput = Controller.Serialize<Model.BasicOutput>(basicOutput, typeof(Model.BasicOutput));
                        #endregion

                        break;
                    case "APPROVE_QUOTE":

                        #region Aggiornamento stato da "Bozza"/"In Approvazione" ad "Approvato"
                        PluginRegion = "Aggiornamento stato da \"Bozza\"/\"In Approvazione\" ad \"Approvato\"";
                        basicOutput.result = 0; basicOutput.message = "Operazione effettuata.";
                        try
                        {
                        }
                        catch (Exception ex)
                        {
                            basicOutput.result = -1;
                            basicOutput.message = ex.Message;
                        }
                        jsonDataOutput = updateQuoteStatusCode(serviceAdmin, tracingService, jsonDataInput);
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

        public static string updateQuoteStatusCode(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            //deserializzo il json
            Shared.Quote.Model.QuoteStatusRequest quote = Controller.Deserialize<Shared.Quote.Model.QuoteStatusRequest>(jsonDataInput);

            var quoteStatus = quote.QuoteStatus;

            //tramite il quoteId faccio la retrieve dell'offerta che voglio aggiornare
            Guid quoteId = new Guid(quote.QuoteId ?? null);

            Entity enQuote = service.Retrieve(DataModel.quote.logicalName, quoteId, new Microsoft.Xrm.Sdk.Query.ColumnSet(DataModel.quote.statuscode));

            //recupero lo statuscode, lo modifico e faccio update
            OptionSetValue statuscode = enQuote?.GetAttributeValue<OptionSetValue>(DataModel.quote.statuscode) ?? null;

            enQuote[DataModel.quote.statuscode] = DataModel.quote.statuscodeValues.Approvata_StateAttiva;

            service.Update(enQuote);

            return "Quote Approved.";
        }
        public static string CopyPriceLevel(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            string result = string.Empty;

            Model.BasicOutput basicOutput = new Model.BasicOutput() { result = 0, message = "Ok copia effettuata con successo." };

            try
            {
                ThrowTestException(false);

                //--- Deserializza --------------------------------------------------------------------------
                PriceLevel pl = RSMNG.Plugins.Controller.Deserialize<PriceLevel>(Uri.UnescapeDataString(jsonDataInput), typeof(PriceLevel));
                //-------------------------------------------------------------------------------------------


                Entity enPriceLevel = new Entity(pricelevel.logicalName);

                OptionSetValueCollection optSet = new OptionSetValueCollection(pl.selectedScope.Select(scope => new OptionSetValue(scope)).ToList());

                DateTime? beginnerDate = null;
                DateTime? endDate = null;
                if (pl.begindate != null) { beginnerDate = DateTime.ParseExact(pl.begindate, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal); }
                if (pl.enddate != null) { endDate = DateTime.ParseExact(pl.enddate, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal); }

                enPriceLevel.Attributes.Add(pricelevel.name, pl.name);
                enPriceLevel.Attributes.Add(pricelevel.begindate, beginnerDate);
                enPriceLevel.Attributes.Add(pricelevel.enddate, endDate);
                enPriceLevel.Attributes.Add(pricelevel.description, pl.description);
                enPriceLevel.Attributes.Add(pricelevel.transactioncurrencyid, new EntityReference("transactioncurrency", new Guid(pl.transactioncurrencyid)));
                enPriceLevel.Attributes.Add(pricelevel.res_isdefaultforagents, false); // Esiste solo un record con isdefaultforagents a true, quindi questo valore a priori non può essere copiato.
                enPriceLevel.Attributes.Add(pricelevel.res_isdefaultforwebsite, pl.isDefautWebsite);
                enPriceLevel.Attributes.Add(pricelevel.res_scopetypecodes, pl.selectedScope != null && pl.selectedScope.Any() ? optSet : null);

                service.Create(enPriceLevel);
            }
            catch (Exception ex)
            {
                basicOutput.result = -1;
                basicOutput.message = ex.Message;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                result = RSMNG.Plugins.Controller.Serialize<Model.BasicOutput>(basicOutput, typeof(Model.BasicOutput));
            }

            return result;
        }
        static void ThrowTestException(bool isThrow)
        {
            if (isThrow)
            {
                // Simulate an exception
                throw new InvalidOperationException("This is a test exception to force a catch block.");
            }

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
        public string begindate { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string enddate { get; set; }
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
