using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Runtime.Serialization;
using RSMNG.TAUMEDIKA.DataModel;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;


namespace RSMNG.TAUMEDIKA.Shared.PriceLevel
{
    public class Utility
    {
        public static string CopyPriceLevel(IOrganizationService service, ITracingService trace, String jsonDataInput)
        {
            string result = string.Empty;

            RSMNG.TAUMEDIKA.Model.BasicOutput basicOutput = new RSMNG.TAUMEDIKA.Model.BasicOutput() { result = 0, message = "Ok copia effettuata con successo." };

            try
            {
                Helper.ThrowTestException(false);

                //--- Deserializza --------------------------------------------------------------------------
                Model.PriceLevel pl = RSMNG.Plugins.Controller.Deserialize<Model.PriceLevel>(Uri.UnescapeDataString(jsonDataInput), typeof(Model.PriceLevel));
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
                result = RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.BasicOutput>(basicOutput, typeof(RSMNG.TAUMEDIKA.Model.BasicOutput));
            }

            return result;
        }
        public static void checkIsDefault(IOrganizationService service, CrmServiceProvider crmService, Guid priceLevelId, string field)
        {
            string condition = null;
            switch (field)
            {
                case "Default per agenti":
                    condition = $@"<condition attribute=""{pricelevel.statecode}"" operator=""eq"" value=""0"" />
                                   <condition attribute=""{pricelevel.res_isdefaultforagents}"" operator=""eq"" value=""1"" />";
                    break;

                case "Import ERP":
                    condition = $@"<condition attribute=""{pricelevel.statecode}"" operator=""eq"" value=""0"" />
                                   <condition attribute=""{pricelevel.res_iserpimport}"" operator=""eq"" value=""1"" />";
                    break;

                case "Default per sito web":
                    condition = $@"<condition attribute=""{pricelevel.res_isdefaultforwebsite}"" operator=""eq"" value=""1"" />";
                    break;
            }

            var fetchPriceLevel = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                    <fetch top=""1"">
                      <entity name=""pricelevel"">
                        <filter>
                          <condition attribute=""{pricelevel.pricelevelid}"" operator=""ne"" value=""{priceLevelId}"" />
                          {condition}
                        </filter>
                      </entity>
                    </fetch>";

            crmService.TracingService.Trace(fetchPriceLevel);
            EntityCollection ec = service.RetrieveMultiple(new FetchExpression(fetchPriceLevel));

            if (ec.Entities.Count > 0)
            {
                throw new ApplicationException($"Non può esistere più di un record {field}");
            }
        }
        public static EntityReference GetPriceLevelERP(IOrganizationService service)
        {
            EntityReference erPriceLevel = null;
            var fetchData = new
            {
                res_iserpimport = "1"
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch>
              <entity name=""pricelevel"">
                <attribute name=""pricelevelid"" />
                <attribute name=""name"" />
                <filter>
                  <condition attribute=""res_iserpimport"" operator=""eq"" value=""{fetchData.res_iserpimport/*1*/}"" />
                </filter>
              </entity>
            </fetch>";
            EntityCollection ecPriceLevel = service.RetrieveMultiple(new FetchExpression(fetchXml));
            if (ecPriceLevel?.Entities?.Count() > 0)
            {
                erPriceLevel = ecPriceLevel.Entities[0].ToEntityReference();
            }

            return erPriceLevel;
        }
    }
    namespace Model
    {

        [DataContract]
        public class PriceLevel
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int[] selectedScope { get; set; }
            [DataMember]
            public string begindate { get; set; }
            [DataMember]
            public string enddate { get; set; }
            [DataMember]
            public string transactioncurrencyid { get; set; }
            [DataMember]
            public bool isDefautWebsite { get; set; }
            [DataMember]
            public bool isDefaultForAgents { get; set; }
            [DataMember]
            public object description { get; set; }
        }

    }
}
