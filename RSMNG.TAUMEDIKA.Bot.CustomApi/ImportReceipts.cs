using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ImportReceipts;
using RSMNG.TAUMEDIKA.DataModel;
using System.IO;
using System.Globalization;


namespace RSMNG.TAUMEDIKA.Bot.CustomApi
{
    public class ImportReceipts : RSMNG.BaseClass
    {
        public ImportReceipts(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.TRANSACTION;
            PluginMessage = "res_ImportReceipts";
            PluginPrimaryEntityName = "none";
            PluginRegion = "ImportReceipts";
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
                    #region Controllo i parametri del file
                    PluginRegion = "Controllo i parametri del file";
                    if (file.NotContainsAttributeOrStringNullOrEmpty(FileIn.name))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'name' del file obbligatorio";
                    }
                    else if (file.NotContainsAttributeOrStringNullOrEmpty(FileIn.mimetype))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'mimetype' del file obbligatorio";
                    }
                    else if (file.NotContainsAttributeOrStringNullOrEmpty(FileIn.content))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'content' del file obbligatorio";
                    }
                    else if (file.NotContainsAttributeOrStringNullOrEmpty(FileIn.size))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'size' del file obbligatorio";
                    }
                    #endregion

                    if (outResult == "OK")
                    {
                        #region Prelevo la variabile d'ambiente che mi indica le colonne da considerare
                        PluginRegion = "Prelevo la variabile d'ambiente che mi indica le colonne da considerare";
                        string res_ImportReceipts_Configuration = RSMNG.Plugins.Data.GetEnviromentVariable(crmServiceProvider.Service, "res_ImportReceipts_Configuration");
                        #endregion

                        #region Deserializzo la configurazione delle colonne
                        PluginRegion = "Deserializzo la configurazione delle colonne";
                        Configuration configuration = RSMNG.Plugins.Controller.Deserialize<Configuration>(res_ImportReceipts_Configuration);
                        #endregion

                        #region Depuro il file csv
                        PluginRegion = "Depuro il file csv";
                        // Decodifica Base64 in byte[]
                        byte[] csvBytes = Convert.FromBase64String(file.GetAttributeValue<string>(FileIn.content));

                        crmServiceProvider.TracingService.Trace($"Lunghezza CSV:{csvBytes.Length}");

                        // Converte i byte[] in stringa (contenuto del CSV)
                        string csvContent = Encoding.UTF8.GetString(csvBytes);

                        // Crea un List<string[]> per memorizzare le righe
                        List<List<string>> rows = new List<List<string>>();

                        // Leggi il contenuto CSV riga per riga utilizzando StringReader
                        using (StringReader sr = new StringReader(csvContent))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                // Dividi la riga in colonne (basato su punto e virgola)
                                string[] cells = line.Split(';');

                                // Aggiungi la riga (come array) alla lista
                                rows.Add(cells.ToList());
                            }
                        }

                        crmServiceProvider.TracingService.Trace($"NumeroRighePrima:{rows.Count}");

                        //Aggiorno la configurazione con la posizione corretta della cella
                        foreach (Field field in configuration.fields)
                        {
                            field.position = rows[configuration.header_line].IndexOf(field.name);
                        }

                        //Rimuovo le righe in eccesso
                        if (rows.Count > 2)
                        {
                            rows = rows.Skip(1).Take(rows.Count - 2).ToList();
                        }

                        crmServiceProvider.TracingService.Trace($"NumeroRigheDopo:{rows.Count}");

                        #endregion

                        #region Carico/creo riferimenti delle tabelle di default
                        PluginRegion = "Carico/creo riferimenti delle tabelle di default";

                        //Cliente
                        PluginRegion = "Carico i Clienti";
                        var fetchDataA = new
                        {
                            statecode = "0"
                        };
                        var fetchXmlA = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{account.logicalName}"">
                            <attribute name=""{account.name}"" />
                            <attribute name=""{account.accountid}"" />
                            <filter>
                              <condition attribute=""{account.statecode}"" operator=""eq"" value=""{fetchDataA.statecode}"" />
                            </filter>
                          </entity>
                        </fetch>";

                        List<Entity> lAccount = crmServiceProvider.Service.RetrieveAll(fetchXmlA);
                        if (lAccount == null)
                        {
                            lAccount = new List<Entity>();
                        }

                        //Agente
                        PluginRegion = "Carico gli Agenti";
                        var fetchDataSU = new
                        {
                            isdisabled = "0"
                        };
                        var fetchXmlSU = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{systemuser.logicalName}"">
                            <attribute name=""{systemuser.fullname}"" />
                            <attribute name=""{systemuser.systemuserid}"" />
                            <attribute name=""{systemuser.res_agentnumber}"" />
                            <filter>
                              <condition attribute=""{systemuser.isdisabled}"" operator=""eq"" value=""{fetchDataSU.isdisabled}"" />
                            </filter>
                          </entity>
                        </fetch>";

                        List<Entity> lSystemUser = crmServiceProvider.Service.RetrieveAll(fetchXmlSU);
                        if (lSystemUser == null)
                        {
                            lSystemUser = new List<Entity>();
                        }

                        //Condizione di pagamento
                        PluginRegion = "Condizione di pagamento";
                        var fetchDataPT = new
                        {
                            statecode = "0"
                        };
                        var fetchXmlPT = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_paymentterm.logicalName}"">
                            <attribute name=""{res_paymentterm.res_name}"" />
                            <attribute name=""{res_paymentterm.res_paymenttermid}"" />
                            <filter>
                              <condition attribute=""{res_paymentterm.statecode}"" operator=""eq"" value=""{fetchDataPT.statecode}"" />
                            </filter>
                          </entity>
                        </fetch>";

                        List<Entity> lPaymentTerm = crmServiceProvider.Service.RetrieveAll(fetchXmlPT);
                        if (lPaymentTerm == null)
                        {
                            lPaymentTerm = new List<Entity>();
                        }

                        //Coordinate Bancarie
                        PluginRegion = "Coordinate Bancarie";
                        var fetchDataBD = new
                        {
                            statecode = "0"
                        };
                        var fetchXmlBD = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_bankdetails.logicalName}"">
                            <attribute name=""{res_bankdetails.res_bankdetailsid}"" />
                            <attribute name=""{res_bankdetails.res_iban}"" />
                            <attribute name=""{res_bankdetails.res_name}"" />
                            <attribute name=""{res_bankdetails.res_site}"" />
                            <filter>
                              <condition attribute=""{res_bankdetails.statecode}"" operator=""eq"" value=""{fetchDataBD.statecode/*0*/}"" />
                            </filter>
                          </entity>
                        </fetch>";

                        List<Entity> lBankDetails = crmServiceProvider.Service.RetrieveAll(fetchXmlBD);
                        if (lBankDetails == null)
                        {
                            lBankDetails = new List<Entity>();
                        }
                        #endregion

                        #region Popolo la struttura che conterrà i dati da integrare il base al csv depurato
                        PluginRegion = "Popolo la struttura che conterrà i dati da integrare il base al csv depurato";
                        List<Shared.Document.ImportReceiptDanea> receiptsDanea = new List<Shared.Document.ImportReceiptDanea>();

                        foreach (List<string> row in rows)
                        {

                            //Definisco il Cliente
                            PluginRegion = "Definisco il cliente";
                            string sCodCliente = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.CodCliente)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.CodCliente)).position] : null;
                            Entity eCliente = lAccount.FirstOrDefault(u => u.GetAttributeValue<string>(account.accountnumber) == sCodCliente);

                            //Definisco l'Agente
                            PluginRegion = "Definisco l'agente";
                            string sCodAgente = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Agente)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Agente)).position] : null;
                            Entity eAgente = null;
                            if (!string.IsNullOrEmpty(sCodAgente))
                            {
                                eAgente = lSystemUser.FirstOrDefault(u => u.GetAttributeValue<string>(systemuser.res_agentnumber) == sCodAgente);
                                if (eAgente == null)
                                {
                                    //Assegno come agente l'utente applicazione che esegue l'importazione
                                    eAgente = crmServiceProvider.Service.Retrieve(systemuser.logicalName, crmServiceProvider.PluginContext.UserId, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { systemuser.systemuserid, systemuser.fullname }));
                                }
                            }

                            //Definisco le condizioni di pagamento
                            PluginRegion = "Definisco le condizioni di pagamento";
                            string sPaymentTerm = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Pagamento)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Pagamento)).position] : null;
                            Entity ePaymentTerm = null;
                            if (!string.IsNullOrEmpty(sPaymentTerm))
                            {
                                ePaymentTerm = lPaymentTerm.FirstOrDefault(u => u.GetAttributeValue<string>(res_paymentterm.res_name) == sPaymentTerm);
                                if (ePaymentTerm == null)
                                {
                                    ePaymentTerm = new Entity(res_paymentterm.logicalName);
                                    ePaymentTerm.Attributes.Add(res_paymentterm.res_name, sPaymentTerm);
                                    Guid ePaymentTermId = crmServiceProvider.Service.Create(ePaymentTerm);
                                    ePaymentTerm.Id = ePaymentTermId;
                                }
                            }

                            //Definisco le coordinate bancarie
                            PluginRegion = "Definisco le coordinate bancarie";
                            string sBankDetails = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.CoordBancarie)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.CoordBancarie)).position] : null;
                            Entity eBankDetails = null;
                            if (!string.IsNullOrEmpty(sBankDetails))
                            {
                                eBankDetails = lPaymentTerm.FirstOrDefault(u => u.GetAttributeValue<string>(res_bankdetails.res_name) == sBankDetails);
                                if (eBankDetails == null)
                                {
                                    eBankDetails = new Entity(res_bankdetails.logicalName);
                                    eBankDetails.Attributes.Add(res_bankdetails.res_name, sBankDetails);
                                    Guid eBankDetailsId = crmServiceProvider.Service.Create(eBankDetails);
                                    eBankDetails.Id = eBankDetailsId;
                                }
                            }

                            //Definisco il prezzo da saldare
                            PluginRegion = "Definisco il prezzo da saldare";
                            string daSaldare = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.DaSaldare)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.DaSaldare)).position] : "0";
                            decimal? isPendingPayment = Helper.ValidateMoney(daSaldare);

                            //Definisco il totale netto iva
                            PluginRegion = "Definisco il totale netto iva";
                            string totNettoIva = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.TotNettoIva)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.TotNettoIva)).position] : "0";
                            decimal? netTotalExcludingVat = Helper.ValidateMoney(totNettoIva);

                            //Definisco l'iva
                            PluginRegion = "Definisco l'iva";
                            string iva = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Iva)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Iva)).position] : "0";
                            decimal? vat = Helper.ValidateMoney(iva);

                            //Definisco il totale documento
                            PluginRegion = "Definisco il totale documento";
                            string totaleDocumento = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.TotDoc)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.TotDoc)).position] : "0";
                            decimal? documentoTotal = Helper.ValidateMoney(totaleDocumento);

                            //Definisco la data
                            string data = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Data)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Data)).position] : "";
                            DateTime? date = Helper.ValidateDateTime(data);

                            //Definisco la data ultimo pag.
                            string dataUltimoPag = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.DataUltimoPag)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.DataUltimoPag)).position] : "";
                            DateTime? lastPaymentDate = Helper.ValidateDateTime(dataUltimoPag);

                            //Imposto i dati di default e i dati che fanno match
                            PluginRegion = "Imposto i dati di default e i dati che fanno match";
                            Shared.Document.ImportReceiptDanea receiptDanea = new Shared.Document.ImportReceiptDanea()
                            {
                                CodCliente = sCodCliente,
                                Cliente = eCliente != null ? new Shared.Document.LookUp() { Entity = eCliente.LogicalName, Id = eCliente.Id, Text = eCliente.GetAttributeValue<string>(account.name) } : null,
                                Data = date != null ? date?.ToString("yyyy-MM-dd") : null,
                                Anno = date != null ? date?.Year.ToString() : null,
                                NDoc = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.NDoc)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.NDoc)).position] : null,
                                DaSaldare = isPendingPayment,
                                TotNettoIva = netTotalExcludingVat,
                                CodAgente = sCodAgente,
                                Agente = eAgente != null ? new Shared.Document.LookUp() { Entity = eAgente.LogicalName, Id = eAgente.Id, Text = eAgente.GetAttributeValue<string>(systemuser.fullname) } : null,
                                DataUltimoPag = lastPaymentDate != null ? lastPaymentDate?.ToString("yyyy-MM-dd") : null,
                                Iva = vat,
                                TotDoc = documentoTotal,
                                Pagamento = ePaymentTerm != null ? new Shared.Document.LookUp() { Entity = ePaymentTerm.LogicalName, Id = ePaymentTerm.Id, Text = ePaymentTerm.GetAttributeValue<string>(res_paymentterm.res_name) } : null,
                                CoordBancarie = eBankDetails != null ? new Shared.Document.LookUp() { Entity = eBankDetails.LogicalName, Id = eBankDetails.Id, Text = eBankDetails.GetAttributeValue<string>(res_bankdetails.res_name) } : null,
                                Commento = configuration.fields.FirstOrDefault(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Commento)) != null ? row[configuration.fields.First(f => f.name_receipt == nameof(Shared.Document.ImportReceiptDanea.Commento)).position] : null
                            };
                            receiptsDanea.Add(receiptDanea);
                            crmServiceProvider.TracingService.Trace(receiptsDanea.ToString());
                        }
                        #endregion

                        #region Serializzo la struttura creata
                        PluginRegion = "Serializzo la struttura creata";
                        string receiptsDaneaOutput = RSMNG.Plugins.Controller.Serialize<List<Shared.Document.ImportReceiptDanea>>(receiptsDanea, typeof(List<Shared.Document.ImportReceiptDanea>));
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(receiptsDaneaOutput);
                        string base64Json = Convert.ToBase64String(jsonBytes);
                        #endregion

                        #region definisco la data corretta
                        PluginRegion = "Definisco la data corretta";
                        TimeZoneInfo europeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeTimeZone);
                        #endregion

                        #region Creo il log DataIntegration
                        PluginRegion = "Creo il log DataIntegration";
                        Entity enDataIntegration = new Entity(res_dataintegration.logicalName);
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationtype, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationtypeValues.Import));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationaction, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationactionValues.DocumentiRicevute));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_name, $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Import.ToString()} - {GlobalOptionSetConstants.res_opt_integrationactionValues.DocumentiRicevute.ToString()} - {localTime.ToString("dd/MM/yyyy HH:mm:ss")}");
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, "Importazione in fase di validazione");
                        Guid enDataIntegrationId = crmServiceProvider.Service.Create(enDataIntegration);
                        #endregion

                        #region Salvo il file nel log DataIntegration Csv
                        PluginRegion = "Salvo il file nel log DataIntegration";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Input uploadFile_Input_Csv = new RSMNG.TAUMEDIKA.Model.UploadFile_Input()
                        {
                            MimeType = file.GetAttributeValue<string>(FileIn.mimetype),
                            FileName = file.GetAttributeValue<string>(FileIn.name),
                            Id = enDataIntegrationId.ToString(),
                            FileSize = file.GetAttributeValue<int>(FileIn.size),
                            Content = file.GetAttributeValue<string>(FileIn.content)
                        };

                        string resultUploadCsv = Helper.UploadFile(crmServiceProvider.TracingService, crmServiceProvider.Service, res_dataintegration.res_integrationfile, res_dataintegration.logicalName, RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.UploadFile_Input>(uploadFile_Input_Csv, typeof(RSMNG.TAUMEDIKA.Model.UploadFile_Input)));
                        #endregion

                        #region Salvo il file nel log DataIntegration Json
                        PluginRegion = "Salvo il file nel log DataIntegration";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Input uploadFile_Input_Json = new RSMNG.TAUMEDIKA.Model.UploadFile_Input()
                        {
                            MimeType = "text/json",
                            FileName = $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Import.ToString()}_{GlobalOptionSetConstants.res_opt_integrationactionValues.DocumentiRicevute.ToString()}_{localTime.ToString("dd_MM_yyyy_HH_mm_ss")}.json",
                            Id = enDataIntegrationId.ToString(),
                            FileSize = jsonBytes.Length,
                            Content = base64Json
                        };

                        string resultUploadJson = Helper.UploadFile(crmServiceProvider.TracingService, crmServiceProvider.Service, res_dataintegration.res_distributionfile, res_dataintegration.logicalName, RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.UploadFile_Input>(uploadFile_Input_Json, typeof(RSMNG.TAUMEDIKA.Model.UploadFile_Input)));
                        #endregion

                        #region Controllo l'esito del salvataggio del file di Distribuzione
                        PluginRegion = "Controllo l'esito del salvataggio del file";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Output uploadFile_Output = RSMNG.Plugins.Controller.Deserialize<RSMNG.TAUMEDIKA.Model.UploadFile_Output>(resultUploadJson);

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
                            Helper.SetStateCode(crmServiceProvider.Service, res_dataintegration.logicalName, enDataIntegrationId, (int)res_dataintegration.statecodeValues.Attivo, (int)res_dataintegration.statuscodeValues.InValidazione_StateAttivo);
                            #endregion

                            #region Popolo il parametro di output file
                            PluginRegion = "Popolo il parametro di output";
                            outResult = "OK";
                            outMessage = "Importazione delle Ricevute avviata (in fase di validazione)";
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
