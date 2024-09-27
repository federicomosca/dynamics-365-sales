using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.Plugins.HTTP;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ExportCustomers;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
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
                            <filter>
                              <condition attribute=""{account.statecode}"" operator=""in"">
                                <value>{fetchData.statecode/*0*/}</value>
                              </condition>
                              <condition attribute=""{account.modifiedon}"" operator=""last-x-days"" value=""{fetchData.modifiedon/*5*/}"" />
                            </filter>
                          </entity>
                        </fetch>";
                        EntityCollection ecCustomers = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));
                        #endregion

                        #region Popolo la classe Customer
                        PluginRegion = "Popolo la classe Customer";
                        List<Model.ExportCustomers.Customer> customers = new List<Model.ExportCustomers.Customer>();
                        if (ecCustomers?.Entities?.Count > 0)
                        {
                            customers = ecCustomers.Entities
                                  .Select(entity => new Model.ExportCustomers.Customer
                                  {
                                      Cod = entity.ContainsAttributeNotNull(account.accountnumber) ? entity.GetAttributeValue<int?>(account.accountnumber) : null,
                                      CodiceFiscale = entity.ContainsAttributeNotNull(account.res_taxcode) ? entity.GetAttributeValue<string>(account.res_taxcode) : null,
                                      PartitaIva = entity.ContainsAttributeNotNull(account.res_vatnumber) ? entity.GetAttributeValue<string>(account.res_vatnumber) : null,
                                      Denominazione = entity.ContainsAttributeNotNull(account.name) ? entity.GetAttributeValue<string>(account.name) : null,
                                      Indirizzo = entity.ContainsAttributeNotNull(account.address1_line1) ? entity.GetAttributeValue<string>(account.address1_line1) : null,
                                      Cap = entity.ContainsAttributeNotNull(account.address1_postalcode) ? entity.GetAttributeValue<string>(account.address1_postalcode) : null,
                                      Citta = entity.ContainsAttributeNotNull(account.address1_city) ? entity.GetAttributeValue<string>(account.address1_city) : null,
                                      Prov = entity.ContainsAttributeNotNull(account.address1_stateorprovince) ? entity.GetAttributeValue<string>(account.address1_stateorprovince) : null,
                                      Regione = null,
                                      Nazione = entity.ContainsAttributeNotNull(account.address1_country) ? entity.GetAttributeValue<string>(account.address1_country) : null,
                                      CodDestinatarioFattElettr = entity.ContainsAttributeNotNull(account.res_sdi) ? entity.GetAttributeValue<string>(account.res_sdi) : null,
                                      Referente = entity.ContainsAttributeNotNull(account.primarycontactid) ? Shared.Contact.Utility.GetName(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.primarycontactid).Id) : null,
                                      Tel = entity.ContainsAttributeNotNull(account.telephone1) ? entity.GetAttributeValue<string>(account.telephone1) : null,
                                      Cell = entity.ContainsAttributeNotNull(account.res_mobilenumber) ? entity.GetAttributeValue<string>(account.res_mobilenumber) : null,
                                      Fax = entity.ContainsAttributeNotNull(account.fax) ? entity.GetAttributeValue<string>(account.fax) : null,
                                      Email = entity.ContainsAttributeNotNull(account.emailaddress1) ? entity.GetAttributeValue<string>(account.emailaddress1) : null,
                                      Pec = entity.ContainsAttributeNotNull(account.emailaddress3) ? entity.GetAttributeValue<string>(account.emailaddress3) : null,
                                      Agente = entity.ContainsAttributeNotNull(account.ownerid) && entity.GetAttributeValue<EntityReference>(account.ownerid).LogicalName == systemuser.logicalName ? Shared.SystemUser.Utility.GetAgentNumber(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.ownerid).Id) : null,
                                      Pagamento = entity.ContainsAttributeNotNull(account.res_paymenttermid) ? Shared.PaymentTerm.Utility.GetName(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.res_paymenttermid).Id) : null,
                                      NsBanca = entity.ContainsAttributeNotNull(account.res_bankdetailsid) ? Shared.BankDetails.Utility.GetName(crmServiceProvider.Service, entity.GetAttributeValue<EntityReference>(account.res_bankdetailsid).Id) : null,
                                      Extra6 = entity.Id.ToString(),
                                      Note = entity.ContainsAttributeNotNull(account.description) ? entity.GetAttributeValue<string>(account.description) : null
                                  })
                                  .ToList();
                        }
                        #endregion

                        #region Serializzo i Customes
                        PluginRegion = "Serializzo i Customes";
                        string customersJSON = RSMNG.Plugins.Controller.Serialize<List<Model.ExportCustomers.Customer>>(customers, typeof(List<Model.ExportCustomers.Customer>));
                        #endregion

                        #region Creo il file csv in memoria
                        PluginRegion = "Creo il file csv in memoria";
                        // Creazione del contenuto CSV
                        StringBuilder csvBuilder = new StringBuilder();

                        // Aggiungi l'intestazione
                        string headers = typeof(Model.ExportCustomers.Customer).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Select(p => $"\"{p.GetCustomAttribute<DataMemberAttribute>()?.Name ?? p.Name}\"").ToString();

                        csvBuilder.AppendLine(string.Join(";", headers));

                        // Aggiungi i dati di ogni cliente
                        foreach (Model.ExportCustomers.Customer customer in customers)
                        {
                            csvBuilder.AppendLine($"\"{customer.Cod}\";\"{customer.CodiceFiscale}\";\"{customer.PartitaIva}\";\"{customer.Denominazione}\";" +
                                                  $"\"{customer.Indirizzo}\";\"{customer.Cap}\";\"{customer.Citta}\";\"{customer.Prov}\";" +
                                                  $"\"{customer.Regione}\";\"{customer.Nazione}\";\"{customer.CodDestinatarioFattElettr}\";\"{customer.Referente}\";" +
                                                  $"\"{customer.Referente}\";\"{customer.Tel}\";\"{customer.Cell}\";\"{customer.Fax}\";" +
                                                  $"\"{customer.Email}\";\" {customer.Pec} \";\" {customer.Agente} \";\" {customer.Pagamento} \";" +
                                                  $"\"{customer.NsBanca}\";\"{customer.Extra6}\";{customer.Note}\"");
                        }

                        // Converte la stringa CSV in un array di byte
                        byte[] csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

                        // Crea un Data URI
                        string customersFileDataUri = "data:text/csv;base64," + Convert.ToBase64String(csvBytes);
                        #endregion

                        #region Creo il log DataIntegration
                        PluginRegion = "Creo il log DataIntegration";
                        Entity enDataIntegration = new Entity(res_dataintegration.logicalName);
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationtype, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationtypeValues.Export));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationaction, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationactionValues.Clienti));
                        enDataIntegration.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue((int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_name, $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()} - {GlobalOptionSetConstants.res_opt_integrationactionValues.Clienti.ToString()} - {DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")}");
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationdata, customersFileDataUri);
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, "Esportazione effettuata con successo");
                        crmServiceProvider.Service.Create(enDataIntegration);
                        #endregion

                        #region Salvo il file nel log DataIntegration
                        PluginRegion = "Salvo il file nel log DataIntegration";

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
        }
    }
}
