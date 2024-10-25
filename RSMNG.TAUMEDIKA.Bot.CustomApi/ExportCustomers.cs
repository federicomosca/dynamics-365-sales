using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.Plugins.HTTP;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ExportCustomers;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi
{
    public class ExportCustomers : RSMNG.BaseClass
    {
        public ExportCustomers(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.TRANSACTION;
            PluginMessage = "res_ExportCustomers";
            PluginPrimaryEntityName = "none";
            PluginRegion = "ExportCustomers";
        }

        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            crmServiceProvider.TracingService.Trace("Inizio");
            Response response = null;
            string outResult = "OK";
            string outMessage = "Nessun errore riscontrato";
            string outErrorCode = "00";
            Entity outFile = new Entity();
            bool debug = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.debug) ? (bool)crmServiceProvider.PluginContext.InputParameters[ParametersIn.debug] : false;
            Entity enFiltersRequest = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.filters) ? (Entity)crmServiceProvider.PluginContext.InputParameters[ParametersIn.filters] : null;

            if (!debug)
            {
                try
                {
                    #region Controllo i parametri obbligatori
                    PluginRegion = "Controllo i parametri obbligatori";
                    if (enFiltersRequest.NotContainsAttributeOrNull(Filters.statecodes))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'Stati' obbligatorio";
                    }
                    else if (enFiltersRequest.NotContainsAttributeOrNull(Filters.lastxdays))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'Ultimi x giorni' obbligatorio";
                    }
                    #endregion

                    if (outResult == "OK")
                    {
                        #region Estraggo il Clienti che rispecchiano il filtro impostato
                        PluginRegion = "Estraggo il Clienti che rispecchiano il filtro impostato";
                        var fetchData = new
                        {
                            modifiedon = (int)enFiltersRequest.Attributes[Filters.lastxdays],
                            statecode = $"<value>{enFiltersRequest.GetAttributeValue<EntityCollection>(Filters.statecodes).Entities.Select(sc => sc.GetAttributeValue<int>(StateCodes.statecode).ToString()).Aggregate((acc, next) => $"{acc}</value><value>{next}")}</value>"
                        };
                        var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{account.logicalName}"">
                            <attribute name=""{account.accountnumber}"" />
                            <attribute name=""{account.address1_city}"" />
                            <attribute name=""{account.address1_country}"" />
                            <attribute name=""{account.address1_line1}"" />
                            <attribute name=""{account.address1_postalcode}"" />
                            <attribute name=""{account.address1_stateorprovince}"" />
                            <attribute name=""{account.emailaddress1}"" />
                            <attribute name=""{account.emailaddress3}"" />
                            <attribute name=""{account.fax}"" />
                            <attribute name=""{account.name}"" />
                            <attribute name=""{account.ownerid}"" />
                            <attribute name=""{account.primarycontactid}"" />
                            <attribute name=""{account.res_paymenttermid}"" />
                            <attribute name=""{account.res_sdi}"" />
                            <attribute name=""{account.res_taxcode}"" />
                            <attribute name=""{account.res_vatnumber}"" />
                            <attribute name=""{account.telephone1}"" />
                            <attribute name=""{account.res_mobilenumber}"" />
                            <attribute name=""{account.res_bankdetailsid}"" />
                            <filter>
                              <condition attribute=""{account.statecode}"" operator=""in"">
                                {fetchData.statecode}
                              </condition>
                              <condition attribute=""{account.modifiedon}"" operator=""last-x-days"" value=""{fetchData.modifiedon/*5*/}"" />
                            </filter>
                          </entity>
                        </fetch>";
                        List<Entity> lCustomers = crmServiceProvider.Service.RetrieveAll(fetchXml);
                        crmServiceProvider.TracingService.Trace($"Query:{fetchXml}");
                        #endregion

                        #region Popolo la classe Customer
                        PluginRegion = "Popolo la classe Customer";
                        List<Model.ExportCustomers.Customer> customers = new List<Model.ExportCustomers.Customer>();
                        if (lCustomers?.Count > 0)
                        {
                            customers = lCustomers
                                  .Select(entity => new Model.ExportCustomers.Customer
                                  {
                                      Cod = entity.ContainsAttributeNotNull(account.accountnumber) ? entity.GetAttributeValue<string>(account.accountnumber) : "",
                                      CodiceFiscale = entity.ContainsAttributeNotNull(account.res_taxcode) ? entity.GetAttributeValue<string>(account.res_taxcode) : "",
                                      PartitaIva = entity.ContainsAttributeNotNull(account.res_vatnumber) ? entity.GetAttributeValue<string>(account.res_vatnumber) : "",
                                      Denominazione = entity.ContainsAttributeNotNull(account.name) ? entity.GetAttributeValue<string>(account.name) : "",
                                      Indirizzo = entity.ContainsAttributeNotNull(account.address1_line1) ? entity.GetAttributeValue<string>(account.address1_line1) : "",
                                      Cap = entity.ContainsAttributeNotNull(account.address1_postalcode) ? entity.GetAttributeValue<string>(account.address1_postalcode) : "",
                                      Citta = entity.ContainsAttributeNotNull(account.address1_city) ? entity.GetAttributeValue<string>(account.address1_city) : "",
                                      Prov = entity.ContainsAttributeNotNull(account.address1_stateorprovince) ? entity.GetAttributeValue<string>(account.address1_stateorprovince) : "",
                                      Regione = "",
                                      Nazione = entity.ContainsAttributeNotNull(account.address1_country) ? entity.GetAttributeValue<string>(account.address1_country) : "",
                                      CodDestinatarioFattElettr = entity.ContainsAttributeNotNull(account.res_sdi) ? entity.GetAttributeValue<string>(account.res_sdi) : entity.ContainsAttributeNotNull(account.emailaddress3) ? entity.GetAttributeValue<string>(account.emailaddress3) : "",
                                      Referente = entity.ContainsAttributeNotNull(account.primarycontactid) ? Shared.Contact.Utility.GetName(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.primarycontactid).Id) : "",
                                      Tel = entity.ContainsAttributeNotNull(account.telephone1) ? entity.GetAttributeValue<string>(account.telephone1) : "",
                                      Cell = entity.ContainsAttributeNotNull(account.res_mobilenumber) ? entity.GetAttributeValue<string>(account.res_mobilenumber) : "",
                                      Fax = entity.ContainsAttributeNotNull(account.fax) ? entity.GetAttributeValue<string>(account.fax) : "",
                                      Email = entity.ContainsAttributeNotNull(account.emailaddress1) ? entity.GetAttributeValue<string>(account.emailaddress1) : "",
                                      Pec = entity.ContainsAttributeNotNull(account.emailaddress3) ? entity.GetAttributeValue<string>(account.emailaddress3) : "",
                                      Agente = entity.ContainsAttributeNotNull(account.ownerid) && entity.GetAttributeValue<EntityReference>(account.ownerid).LogicalName == systemuser.logicalName ? Shared.SystemUser.Utility.GetAgentNumber(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.ownerid).Id) : "",
                                      Pagamento = entity.ContainsAttributeNotNull(account.res_paymenttermid) ? Shared.PaymentTerm.Utility.GetName(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.res_paymenttermid).Id) : "",
                                      NsBanca = entity.ContainsAttributeNotNull(account.res_bankdetailsid) ? Shared.BankDetails.Utility.GetName(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.res_bankdetailsid).Id) : "",
                                      Extra6 = entity.Id.ToString(),
                                      Note = entity.ContainsAttributeNotNull(account.description) ? entity.GetAttributeValue<string>(account.description) : ""
                                  })
                                  .ToList();
                        }
                        #endregion

                        #region Serializzo i Customes
                        PluginRegion = "Serializzo i Customes";
                        string customersJSON = RSMNG.Plugins.Controller.Serialize<List<Model.ExportCustomers.Customer>>(customers, typeof(List<Model.ExportCustomers.Customer>));
                        crmServiceProvider.TracingService.Trace($"Clienti trovati:{customersJSON}");
                        #endregion

                        #region Creo il file csv in memoria
                        PluginRegion = "Creo il file csv in memoria";
                        // Creazione del contenuto CSV
                        StringBuilder csvBuilder = new StringBuilder();

                        // Aggiungi l'intestazione
                        var properties = typeof(Customer).GetProperties()
                                   .Where(prop => Attribute.IsDefined(prop, typeof(DataMemberAttribute)));

                        string headers = string.Join(";", properties.Select(prop =>
                        {
                            var dataMemberAttribute = prop.GetCustomAttribute<DataMemberAttribute>();
                            return dataMemberAttribute?.Name ?? prop.Name;
                        }));
                        crmServiceProvider.TracingService.Trace($"headers:{headers}");

                        csvBuilder.AppendLine(headers);

                        // Aggiungi i dati di ogni cliente
                        foreach (Model.ExportCustomers.Customer customer in customers)
                        {
                            csvBuilder.AppendLine($"\"{customer.Cod}\";\"{customer.CodiceFiscale}\";\"{customer.PartitaIva}\";\"{customer.Denominazione}\";" +
                                                  $"\"{customer.Indirizzo}\";\"{customer.Cap}\";\"{customer.Citta}\";\"{customer.Prov}\";" +
                                                  $"\"{customer.Regione}\";\"{customer.Nazione}\";\"{customer.CodDestinatarioFattElettr}\";\"{customer.Referente}\";" +
                                                  $"\"{customer.Tel}\";\"{customer.Cell}\";\"{customer.Fax}\";\"{customer.Email}\";" +
                                                  $"\"{customer.Pec}\";\"{customer.Agente}\";\"{customer.Pagamento}\";" +
                                                  $"\"{customer.NsBanca}\";\"{customer.Extra6}\";\"{customer.Note}\"");
                        }
                        crmServiceProvider.TracingService.Trace($"contenutofile:{csvBuilder.ToString()}");


                        ////// Converte la stringa CSV in un array di byte
                        //byte[] csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

                        ////// Converte la stringa CSV in un array di byte
                        //byte[] jsonBytes = Encoding.UTF8.GetBytes(customersJSON.ToString());

                        //// Crea un Data URI
                        //string customersFileDataUri = "data:text/csv;base64," + Convert.ToBase64String(csvBytes);


                        //// Converte la stringa CSV in un array di byte
                        byte[] csvBytes = null;

                        //// Converte la stringa CSV in un array di byte
                        byte[] jsonBytes = null;

                        // Crea un Data URI
                        string customersFileDataUri = null;

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Usa StreamWriter per scrivere la stringa nel MemoryStream con codifica UTF-8
                            using (StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8))
                            {
                                writer.Write(csvBuilder.ToString());
                                writer.Flush(); // Assicurati che tutti i dati siano scritti nel MemoryStream
                                memoryStream.Position = 0; // Resetta la posizione per leggere dall'inizio
                            }
                            csvBytes = memoryStream.ToArray();

                            jsonBytes = Encoding.UTF8.GetBytes(customersJSON.ToString());

                            // Converti il contenuto del MemoryStream in una stringa Base64
                            customersFileDataUri = "data:text/csv;base64," + Convert.ToBase64String(csvBytes);

                        }
                        #endregion

                        #region definisco la data corretta
                        TimeZoneInfo europeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeTimeZone);
                        #endregion

                        #region Creo il log DataIntegration
                        PluginRegion = "Creo il log DataIntegration";
                        Entity enDataIntegration = new Entity(res_dataintegration.logicalName);
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationtype, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationtypeValues.Export));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationaction, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationactionValues.Clienti));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_name, $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()} - {GlobalOptionSetConstants.res_opt_integrationactionValues.Clienti.ToString()} - {localTime.ToString("dd/MM/yyyy HH:mm:ss")}");
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, "Esportazione effettuata con successo");
                        Guid enDataIntegrationId = crmServiceProvider.Service.Create(enDataIntegration);
                        #endregion

                        #region Salvo il file nel log DataIntegration CSV
                        PluginRegion = "Salvo il file nel log DataIntegration CSV";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Input uploadFile_Input_Csv = new RSMNG.TAUMEDIKA.Model.UploadFile_Input()
                        {
                            MimeType = "text/csv",
                            FileName = $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()}_{GlobalOptionSetConstants.res_opt_integrationactionValues.Clienti.ToString()}_{localTime.ToString("dd_MM_yyyy_HH_mm_ss")}.csv",
                            Id = enDataIntegrationId.ToString(),
                            FileSize = csvBytes.Length,
                            Content = Convert.ToBase64String(csvBytes)
                        };

                        string resultUploadCsv = Helper.UploadFile(crmServiceProvider.TracingService, crmServiceProvider.Service, res_dataintegration.res_integrationfile, res_dataintegration.logicalName, RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.UploadFile_Input>(uploadFile_Input_Csv, typeof(RSMNG.TAUMEDIKA.Model.UploadFile_Input)));
                        #endregion

                        #region Salvo il file nel log DataIntegration Json
                        PluginRegion = "Salvo il file nel log DataIntegration JSON";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Input uploadFile_Input_Json = new RSMNG.TAUMEDIKA.Model.UploadFile_Input()
                        {
                            MimeType = "text/json",
                            FileName = $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()}_{GlobalOptionSetConstants.res_opt_integrationactionValues.Clienti.ToString()}_{localTime.ToString("dd_MM_yyyy_HH_mm_ss")}.json",
                            Id = enDataIntegrationId.ToString(),
                            FileSize = jsonBytes.Length,
                            Content = Convert.ToBase64String(jsonBytes)
                        };

                        string resultUploadJson = Helper.UploadFile(crmServiceProvider.TracingService, crmServiceProvider.Service, res_dataintegration.res_distributionfile, res_dataintegration.logicalName, RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.UploadFile_Input>(uploadFile_Input_Json, typeof(RSMNG.TAUMEDIKA.Model.UploadFile_Input)));
                        #endregion

                        #region Controllo l'esito del salvataggio del file
                        PluginRegion = "Controllo l'esito del salvataggio del file";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Output uploadFile_Output = RSMNG.Plugins.Controller.Deserialize<RSMNG.TAUMEDIKA.Model.UploadFile_Output>(resultUploadCsv);

                        if (uploadFile_Output?.result != 0)
                        {
                            outResult = "KO";
                            outErrorCode = "02";
                            outMessage = $"{uploadFile_Output.message}";
                        }
                        #endregion

                        if (outResult == "OK")
                        {
                            #region Cambio di stato al log DataIntegration
                            PluginRegion = "Cambio di stato al log DataIntegration";
                            Helper.SetStateCode(crmServiceProvider.Service, res_dataintegration.logicalName, enDataIntegrationId, (int)res_dataintegration.statecodeValues.Inattivo, (int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo);
                            #endregion

                            #region Popolo il parametro di output file
                            PluginRegion = "Popolo il parametro di output file";
                            outFile.Attributes.Add("mimetype", uploadFile_Input_Csv.MimeType);
                            outFile.Attributes.Add("name", uploadFile_Input_Csv.FileName);
                            outFile.Attributes.Add("size", uploadFile_Input_Csv.FileSize);
                            outFile.Attributes.Add("content", uploadFile_Input_Csv.Content);
                            outFile.Attributes.Add("datauri", customersFileDataUri);
                            #endregion
                        }
                    }
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
                    crmServiceProvider.PluginContext.OutputParameters[ParametersOut.file] = outFile;
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
                crmServiceProvider.PluginContext.OutputParameters[ParametersOut.file] = outFile;
            }
            crmServiceProvider.TracingService.Trace("Fine");
        }
    }
}
