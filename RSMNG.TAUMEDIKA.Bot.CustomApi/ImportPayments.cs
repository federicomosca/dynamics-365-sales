using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ImportPayments;
using RSMNG.TAUMEDIKA.DataModel;
using System.IO;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi
{
    public class ImportPayments : RSMNG.BaseClass
    {
        public ImportPayments(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.TRANSACTION;
            PluginMessage = "res_ImportPayments";
            PluginPrimaryEntityName = "none";
            PluginRegion = "ImportPayments";
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
                        string res_ImportInvoices_Configuration = RSMNG.Plugins.Data.GetEnviromentVariable(crmServiceProvider.Service, "res_ImportPayments_Configuration");
                        #endregion

                        #region Deserializzo la configurazione delle colonne
                        PluginRegion = "Deserializzo la configurazione delle colonne";
                        Configuration configuration = RSMNG.Plugins.Controller.Deserialize<Configuration>(res_ImportInvoices_Configuration);
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
