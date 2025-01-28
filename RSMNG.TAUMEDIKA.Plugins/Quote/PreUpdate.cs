using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Country;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Quote
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.quote.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            crmServiceProvider.PluginContext.PreEntityImages.TryGetValue("PreImage", out Entity preImage);
            if (preImage == null) { return; }

            Entity postImage = target.GetPostImage(preImage);

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.Quote.Utility.mandatoryFields);
            target.TryGetAttributeValue<EntityReference>(quote.res_additionalexpenseid, out EntityReference additionalExpense);
            if (additionalExpense != null)
            {
                target.TryGetAttributeValue<EntityReference>(quote.res_vatnumberid, out EntityReference vatNumber);
                if (vatNumber == null) { throw new ApplicationException($"Il campo Codice IVA Spesa Accessoria è obbligatorio"); }
            }
            #endregion

            #region Popolo in automatico il Destinatario
            string destination = string.Empty;
            if (postImage.ContainsAttributeNotNull(quote.res_shippingreference))
            {
                destination = postImage.GetAttributeValue<string>(quote.res_shippingreference);
            }
            if (string.IsNullOrEmpty(destination) && postImage.ContainsAttributeNotNull(quote.customerid))
            {
                destination = Shared.Account.Utility.GetName(crmServiceProvider.Service, postImage.GetAttributeValue<EntityReference>(quote.customerid).Id);
            }
            target.AddWithRemove(quote.res_recipient, destination);
            #endregion

            #region Valorizzo il campo Nome
            PluginRegion = "Valorizzo il campo Nome";

            string nomeCliente = string.Empty;

            string nOfferta = preImage.ContainsAttributeNotNull(quote.quotenumber) ? preImage.GetAttributeValue<string>(quote.quotenumber) : string.Empty;
            if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"numero offerta: {preImage.GetAttributeValue<string>(quote.quotenumber)}");

            if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"cliente è stato modificato? {target.Contains(quote.customerid)}");

            //recupero il nome cliente dalla lookup polimorfica
            EntityReference erCliente = target.Contains(quote.customerid) ? target.GetAttributeValue<EntityReference>(quote.customerid) :
                preImage.Contains(quote.customerid) ? preImage.GetAttributeValue<EntityReference>(quote.customerid) : null;

            if (erCliente != null)
            {
                bool isAccount = erCliente.LogicalName == account.logicalName;

                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"customer is account? {isAccount}");

                //columnset relativo alla natura della lookup polimorfica
                ColumnSet columnSetCliente = isAccount ? new ColumnSet(account.name) : new ColumnSet(contact.fullname);
                Entity cliente = crmServiceProvider.Service.Retrieve(isAccount ? account.logicalName : contact.logicalName, erCliente.Id, columnSetCliente);

                if (cliente != null)
                {
                    nomeCliente = cliente.ContainsAttributeNotNull(isAccount ? account.name : contact.fullname) ?
                        cliente.GetAttributeValue<string>(isAccount ? account.name : contact.fullname) : string.Empty;
                }
            }

            string nomeOfferta = !string.IsNullOrEmpty(nOfferta) ? $"{nOfferta} - {nomeCliente}" : nomeCliente;

            target[quote.name] = nomeOfferta;
            #endregion

            #region Valorizzazione automatica del campo Motivo Stato Precedente
            PluginRegion = "Valorizzazione automatica del campo Motivo Stato Precedente";

            //recupero il motivo stato dalla preimage e lo salvo nel campo motivo stato precedente
            preImage.TryGetAttributeValue<OptionSetValue>(quote.statuscode, out var previousStatusCode);
            if (previousStatusCode != null)
            {
                target["res_oldstatuscode"] = previousStatusCode;
            }
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";
            postImage.TryGetAttributeValue<EntityReference>(DataModel.quote.res_countryid, out EntityReference erCountry);
            string countryName = erCountry != null ? Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

            target[DataModel.quote.shipto_country] = countryName;
            #endregion

            #region Ricalcolo di Totale imponibile, Importo totale, Totale IVA
            PluginRegion = "Ricalcolo di Totale imponibile, Importo totale, Totale IVA";

            if (target.Contains(quote.totalamountlessfreight) &&
                target.Contains(quote.totaltax) &&
                target.Contains(quote.totalamount) &&
                target.Contains(quote.totaldiscountamount) &&
                target.Contains(quote.totallineitemamount)
                )
            {
                decimal totaleIva,
                    totaleProdotti;

                totaleIva = postImage.GetAttributeValue<Money>(quote.totaltax)?.Value ?? 0;
                totaleProdotti = postImage.GetAttributeValue<Money>(quote.totallineitemamount)?.Value ?? 0;

                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_prodotti {totaleProdotti}");
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_iva {totaleIva}");

                decimal totaleImponibile, importoTotale;


                var fetchQuote = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""{quote.logicalName}"">
                                    <attribute name=""{quote.freightamount}"" />
                                    <filter>
                                      <condition attribute=""{quote.quoteid}"" operator=""eq"" value=""{postImage.Id}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""{res_vatnumber.logicalName}"">
                                      <attribute name=""{res_vatnumber.res_rate}"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace(fetchQuote);

                EntityCollection quoteCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchQuote));

                if (quoteCollection.Entities.Count > 0)
                {
                    Entity enQuote = quoteCollection.Entities[0];

                    decimal importoSpesaAccessoria = enQuote.ContainsAttributeNotNull(quote.freightamount) ? enQuote.GetAttributeValue<Money>(quote.freightamount).Value : 0;
                    decimal aliquota = enQuote.ContainsAliasNotNull($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") ? enQuote.GetAliasedValue<decimal>($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") : 1;
                    decimal aliquotaImportoSpesaAccessoria = importoSpesaAccessoria * ((aliquota == 0 ? 1 : aliquota) / 100);

                    totaleIva += aliquotaImportoSpesaAccessoria;
                    totaleImponibile = totaleProdotti + importoSpesaAccessoria;
                    importoTotale = totaleImponibile + totaleIva;

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"importo_totale {importoTotale}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_iva {totaleIva}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_imponibile {totaleImponibile}");

                    target[quote.totaltax] = totaleIva != 0 ? new Money(totaleIva) : null;
                    target[quote.totalamountlessfreight] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                    target[quote.totalamount] = importoTotale != 0 ? new Money(importoTotale) : null;
                }
            }
            #endregion
        }
    }
}

