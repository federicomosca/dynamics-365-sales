using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ImportProducts;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Data.SqlTypes;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
using System.Collections;
using RSMNG.TAUMEDIKA.Shared.Product;
using Microsoft.Xrm.Sdk.Metadata;
using System.Globalization;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi
{
    public class ImportProducts : RSMNG.BaseClass
    {
        public ImportProducts(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.TRANSACTION;
            PluginMessage = "res_ImportProducts";
            PluginPrimaryEntityName = "none";
            PluginRegion = "ImportProducts";
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
                        string res_ImportProducts_Configuration = RSMNG.Plugins.Data.GetEnviromentVariable(crmServiceProvider.Service, "res_ImportProducts_Configuration");
                        #endregion

                        #region Deserializzo la configurazione delle colonne
                        PluginRegion = "Deserializzo la configurazione delle colonne";
                        Configuration configuration = RSMNG.Plugins.Controller.Deserialize<Configuration>(res_ImportProducts_Configuration);
                        #endregion

                        #region Depuro il file csv
                        PluginRegion = "Depuro il file csv";
                        // Decodifica Base64 in byte[]
                        byte[] csvBytes = Convert.FromBase64String(file.GetAttributeValue<string>(FileIn.content));

                        // Converte i byte[] in stringa (contenuto del CSV)
                        string csvContent = Encoding.UTF8.GetString(csvBytes);

                        // Crea un List<string[]> per memorizzare le righe
                        List<List<string>> rows = new List<List<string>>();

                        ////Sostituisco CRLF con un carattere
                        //csvContent = csvContent.Replace("\r\n", "CRLF_PLACEHOLDER").Replace("\n", " ").Replace("CRLF_PLACEHOLDER", "\r\n");

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

                        //Rimuovo l'intestazione
                        rows = rows.Skip(1).ToList();

                        //Rimuovo le righe in eccesso
                        if (rows.Count > 2)
                        {
                            // Calcola il numero di righe rimanenti
                            int righeRimanenti = rows.Count - configuration.number_lines_remove;

                            // Usa Take per mantenere solo le righe rimanenti
                            rows = rows.Take(righeRimanenti).ToList();

                            //rows = rows.Skip(1).Take(rows.Count - 2).ToList();
                        }

                        crmServiceProvider.TracingService.Trace($"NumeroRigheDopo:{rows.Count}");

                        #endregion

                        #region Carico/creo riferimenti delle tabelle di default
                        PluginRegion = "Carico/creo riferimenti delle tabelle di default";

                        //Listino Prezzi
                        PluginRegion = "Listino Prezzi";
                        var fetchDataPL = new
                        {
                            res_iserpimport = "1",
                            statecode = "0"
                        };
                        var fetchXmlPL = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{pricelevel.logicalName}"">
                            <attribute name=""{pricelevel.name}"" />
                            <attribute name=""{pricelevel.res_iserpimport}"" />
                            <attribute name=""{pricelevel.transactioncurrencyid}"" />
                            <filter>
                              <condition attribute=""{pricelevel.res_iserpimport}"" operator=""eq"" value=""{fetchDataPL.res_iserpimport/*1*/}"" />
                              <condition attribute=""{pricelevel.statecode}"" operator=""eq"" value=""{fetchDataPL.statecode/*0*/}"" />
                            </filter>
                          </entity>
                        </fetch>";
                        List<Entity> lPriceLevel = crmServiceProvider.Service.RetrieveAll(fetchXmlPL);
                        if (lPriceLevel == null)
                        {
                            lPriceLevel = new List<Entity>();
                        }
                        if (lPriceLevel.Count == 0)
                        {
                            Entity ePriceLevel = new Entity(pricelevel.logicalName);
                            ePriceLevel.Attributes.Add(pricelevel.name, "Listino Danea");
                            ePriceLevel.Attributes.Add(pricelevel.res_iserpimport, true);
                            ePriceLevel.Attributes.Add(pricelevel.transactioncurrencyid, RSMNG.Plugins.TransactionCurrency.GetDefaultCurrency(crmServiceProvider.Service));
                            ePriceLevel.Attributes.Add(pricelevel.res_scopetypecodes, new OptionSetValueCollection() { new OptionSetValue((int)pricelevel.res_scopetypecodesValues.Agenti) });
                            crmServiceProvider.Service.Create(ePriceLevel);
                            lPriceLevel.Add(ePriceLevel);
                        }

                        //Unita Di vendite
                        PluginRegion = "Unita Di vendite";
                        var fetchDataUS = new
                        {
                            res_isdefault = "1"
                        };
                        var fetchXmlUS = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{uomschedule.logicalName}"">
                            <attribute name=""{uomschedule.name}"" />
                            <attribute name=""{uomschedule.uomscheduleid}"" />
                            <filter>
                              <condition attribute=""{uomschedule.res_isdefault}"" operator=""eq"" value=""{fetchDataUS.res_isdefault}"" />
                            </filter>
                          </entity>
                        </fetch>";
                        List<Entity> lUomSchedule = crmServiceProvider.Service.RetrieveAll(fetchXmlUS);
                        if (lUomSchedule == null)
                        {
                            lUomSchedule = new List<Entity>();
                        }
                        if (lUomSchedule.Count == 0)
                        {
                            Entity eUomSchedule = new Entity(uomschedule.logicalName);
                            eUomSchedule.Attributes.Add(uomschedule.name, "Unità primaria");
                            eUomSchedule.Attributes.Add(uomschedule.description, null);
                            eUomSchedule.Attributes.Add(uomschedule.res_isdefault, true);
                            eUomSchedule.Attributes.Add(uomschedule.baseuomname, "Unità primaria");
                            crmServiceProvider.Service.Create(eUomSchedule);
                            lUomSchedule.Add(eUomSchedule);
                        }

                        //Unità di misura
                        PluginRegion = "Unità di misura";
                        var fetchXmlU = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{uom.logicalName}"">
                            <attribute name=""{uom.baseuom}"" />
                            <attribute name=""{uom.name}"" />
                            <attribute name=""{uom.res_isdefault}"" />
                            <attribute name=""{uom.uomid}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lUom = crmServiceProvider.Service.RetrieveAll(fetchXmlU);
                        if (lUom == null)
                        {
                            lUom = new List<Entity>();
                        }

                        //Codici IVA
                        PluginRegion = "Codici IVA";
                        var fetchXmlIVA = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_vatnumber.logicalName}"">
                            <attribute name=""{res_vatnumber.res_code}"" />
                            <attribute name=""{res_vatnumber.res_name}"" />
                            <attribute name=""{res_vatnumber.res_vatnumberid}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lVatNumber = crmServiceProvider.Service.RetrieveAll(fetchXmlIVA);
                        if (lVatNumber == null)
                        {
                            lVatNumber = new List<Entity>();
                        }
                        #endregion 

                        #region Popolo la struttura che conterrà i dati da integrare il base al csv depurato
                        PluginRegion = "Popolo la struttura che conterrà i dati da integrare il base al csv depurato";
                        List<Shared.Product.ImportProductDanea> productsDanea = new List<Shared.Product.ImportProductDanea>();

                        foreach (List<string> row in rows)
                        {
                            //Controllare la categoria per determinare il raggruppamento
                            PluginRegion = "Controllo la categoria per determinare il raggruppamento";
                            string category = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.EntitaPrincipale)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.EntitaPrincipale)).position] : null;
                            string delimiter = "  »  ";
                            List<string> lCategories = string.IsNullOrEmpty(category) ? new List<string>() : category.Split(new string[] { delimiter }, StringSplitOptions.None)?.ToList();

                            //Definisco l'unità di misura Predefinita
                            PluginRegion = "Definisco l'unità di misura";
                            string sUom = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.UnitaPredefinita)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.UnitaPredefinita)).position] : null;
                            Entity eUom = null;
                            if (sUom == "")
                            {
                                eUom = lUom.FirstOrDefault(u => u.GetAttributeValue<bool>(uom.res_isdefault) == true);
                            }
                            else
                            {
                                eUom = lUom.FirstOrDefault(u => u.GetAttributeValue<string>(uom.name).ToLower() == sUom.ToLower());
                            }
                            Entity eBaseUom = lUom.FirstOrDefault(u => u.NotContainsAttributeOrNull(uom.baseuom));

                            if (eUom == null)
                            {
                                //Creo l'unita di misura
                                eUom = new Entity(uom.logicalName);
                                eUom.Attributes.Add(uom.uomscheduleid, lUomSchedule[0].ToEntityReference());
                                eUom.Attributes.Add(uom.name, sUom);
                                eUom.Attributes.Add(uom.quantity, new decimal(1));
                                eUom.Attributes.Add(uom.baseuom, eBaseUom.ToEntityReference());
                                crmServiceProvider.Service.Create(eUom);
                                lUom.Add(eUom);
                            }


                            //Definisco l'unità di misura Peso
                            PluginRegion = "Definisco l'unità di misura del peso";
                            string sUomPeso = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.UnitaDimisuraPeso)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.UnitaDimisuraPeso)).position] : null;
                            Entity eUomPeso = null;
                            if (sUomPeso == "")
                            {
                                eUomPeso = lUom.FirstOrDefault(u => u.GetAttributeValue<bool>(uom.res_isdefault) == true);
                            }
                            else
                            {
                                eUomPeso = lUom.FirstOrDefault(u => u.GetAttributeValue<string>(uom.name).ToLower() == sUomPeso.ToLower());
                            }
                            Entity eBaseUomPeso = lUom.FirstOrDefault(u => u.NotContainsAttributeOrNull(uom.baseuom));

                            if (eUomPeso == null)
                            {
                                //Creo l'unita di misura
                                eUomPeso = new Entity(uom.logicalName);
                                eUomPeso.Attributes.Add(uom.uomscheduleid, lUomSchedule[0].ToEntityReference());
                                eUomPeso.Attributes.Add(uom.name, sUomPeso);
                                eUomPeso.Attributes.Add(uom.quantity, new decimal(1));
                                eUomPeso.Attributes.Add(uom.baseuom, eBaseUomPeso.ToEntityReference());
                                crmServiceProvider.Service.Create(eUomPeso);
                                lUom.Add(eUomPeso);
                            }

                            //Definisco il Codice Iva
                            PluginRegion = "Definisco il Codice Iva";
                            string sVatNumber = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.CodiceIVA)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.CodiceIVA)).position] : null;
                            Entity eVatNumber = lVatNumber.FirstOrDefault(u => u.GetAttributeValue<string>(res_vatnumber.res_code) == sVatNumber);

                            //Definisco il Prezzo di listino
                            // Stringa con il valore da convertire
                            PluginRegion = "Definisco il Prezzo di listino";
                            crmServiceProvider.TracingService.Trace(row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PrezzoDiListino)).position]);
                            string priceList = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PrezzoDiListino)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PrezzoDiListino)).position] : "0";
                            priceList = string.IsNullOrEmpty(priceList) ? "0" : priceList;

                            // Rimuovi il simbolo di valuta e gli spazi
                            string cleanedPriceList = priceList.Replace("€", "").Trim();

                            // Converte la stringa in decimal utilizzando la cultura italiana (dove la virgola è il separatore decimale)
                            decimal priceListOk = Decimal.Parse(cleanedPriceList, NumberStyles.Number, new CultureInfo("it-IT"));

                            // Converte la stringa in decimal utilizzando la cultura italiana (dove la virgola è il separatore decimale)
                            PluginRegion = "Definisco il Peso Lordo";
                            string pesoLordo = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PesoLordo)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PesoLordo)).position] : null;
                            decimal? pesoLordoOk = null;
                            if (!string.IsNullOrEmpty(pesoLordo))
                            {
                                pesoLordoOk = Decimal.Parse(pesoLordo, NumberStyles.Number, new CultureInfo("it-IT"));
                            }

                            // Converte la stringa in decimal utilizzando la cultura italiana (dove la virgola è il separatore decimale)
                            PluginRegion = "Definisco il Peso Netto";
                            string pesoNetto = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PesoNetto)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.PesoNetto)).position] : null;
                            decimal? pesoNettoOk = null;
                            if (!string.IsNullOrEmpty(pesoNetto))
                            {
                                pesoNettoOk = Decimal.Parse(pesoNetto, NumberStyles.Number, new CultureInfo("it-IT"));
                            }

                            // Converte la stringa in decimal utilizzando la cultura italiana (dove la virgola è il separatore decimale)
                            PluginRegion = "Definisco il Volume Imballo";
                            string volumeImballo = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.VolumeImballo)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.VolumeImballo)).position] : null;
                            decimal? volumeImballoOk = null;
                            if (!string.IsNullOrEmpty(volumeImballo))
                            {
                                volumeImballoOk = Decimal.Parse(volumeImballo, NumberStyles.Number, new CultureInfo("it-IT"));
                            }

                            PluginRegion = "Definisco la Tipologia";
                            string productTypeCode = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Tipologia)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Tipologia)).position] : null;

                            //Imposto i dati di default e i dati che fanno match
                            PluginRegion = "Imposto i dati di default e i dati che fanno match";
                            Shared.Product.ImportProductDanea productDanea = new Shared.Product.ImportProductDanea()
                            {
                                Origine = new Shared.Product.Option() { Text = "ERP", Value = 100000001, ExternalValue = null },
                                Nome = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Nome)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Nome)).position] : null,
                                Codice = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Codice)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Codice)).position] : null,
                                Descrizione = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Descrizione)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Descrizione)).position] : null,
                                DecimaliSupportati = 2,
                                StrutturaProdotto = new Shared.Product.Option() { Text = "Prodotto", Value = 1, ExternalValue = null },
                                Produttore = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Produttore)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Produttore)).position] : null,
                                Fornitore = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Fornitore)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.Fornitore)).position] : null,
                                Stato = new Shared.Product.Option() { Text = "Attivo", Value = 0, ExternalValue = null },
                                MotivoStato = new Shared.Product.Option() { Text = "Attivo", Value = 1, ExternalValue = null },
                                CodiceABarre = configuration.fields.FirstOrDefault(f => f.name_product == nameof(Shared.Product.ImportProductDanea.CodiceABarre)) != null ? row[configuration.fields.First(f => f.name_product == nameof(Shared.Product.ImportProductDanea.CodiceABarre)).position] : null,
                                UnitaDiVendita = lUomSchedule.Count > 0 ? new Shared.Product.LookUp() { Entity = lUomSchedule[0].LogicalName, Id = lUomSchedule[0].Id, Text = lUomSchedule[0].GetAttributeValue<string>(uomschedule.name) } : null,
                                UnitaPredefinita = eUom != null ? new Shared.Product.LookUp() { Entity = eUom.LogicalName, Id = eUom.Id, Text = eUom.GetAttributeValue<string>(uom.name) } : null,
                                Categoria = lCategories.Count == 2 ? new ProductCategoryDanea() { Codice = lCategories[0], Nome = lCategories[0] } : null,
                                EntitaPrincipale = lCategories.Count == 0 ? null : lCategories.Count <= 2 ? new ProductCategoryDanea() { Codice = lCategories.Count == 1 ? lCategories[0] : string.Join(" - ", lCategories), Nome = lCategories[lCategories.Count == 1 ? 0 : 1] } : null,
                                CodiceIVA = eVatNumber != null ? new Shared.Product.LookUp() { Entity = eVatNumber.LogicalName, Id = eVatNumber.Id, Text = eVatNumber.GetAttributeValue<string>(res_vatnumber.res_name) } : null,
                                PesoLordo = pesoLordoOk,
                                PesoNetto = pesoNettoOk,
                                VolumeImballo = volumeImballoOk,
                                UnitaDimisuraPeso = eUomPeso != null ? new Shared.Product.LookUp() { Entity = eUomPeso.LogicalName, Id = eUomPeso.Id, Text = eUomPeso.GetAttributeValue<string>(uom.name) } : null,
                                PrezzoDiListino = priceListOk,
                                Tipologia = new Shared.Product.Option() { Text = null, Value = null, ExternalValue = productTypeCode }
                            };
                            productsDanea.Add(productDanea);
                            crmServiceProvider.TracingService.Trace(productsDanea.ToString());
                        }
                        #endregion

                        #region Serializzo la struttura creata
                        PluginRegion = "Serializzo la struttura creata";
                        string productsDaneaOutput = RSMNG.Plugins.Controller.Serialize<List<Shared.Product.ImportProductDanea>>(productsDanea, typeof(List<Shared.Product.ImportProductDanea>));
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(productsDaneaOutput);
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
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationaction, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationactionValues.Articoli));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_name, $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Import.ToString()} - {GlobalOptionSetConstants.res_opt_integrationactionValues.Articoli.ToString()} - {localTime.ToString("dd/MM/yyyy HH:mm:ss")}");
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
                            FileName = $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Import.ToString()}_{GlobalOptionSetConstants.res_opt_integrationactionValues.Articoli.ToString()}_{localTime.ToString("dd_MM_yyyy_HH_mm_ss")}.json",
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
                            outMessage = "Importazione degli Prodotti avviata (in fase di validazione)";
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
