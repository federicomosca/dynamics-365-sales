using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.DataIntegration
{
    public class MultipleDistribution : RSMNG.BaseClass
    {
        public MultipleDistribution(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "res_MultipleDistribution";
            PluginPrimaryEntityName = res_dataintegration.logicalName;
            PluginRegion = "";
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            #region Dichiarazioni variabili
            PluginRegion = "Dichiarazioni variabili";
            EntityReference erDataIntegration = (EntityReference)crmServiceProvider.PluginContext.InputParameters["Target"];
            //Input
            int numberRows = (int)crmServiceProvider.PluginContext.InputParameters["NumberRows"];

            //Output
            int statusCode = (int)res_dataintegration.statuscodeValues.InElaborazione_StateAttivo;
            string detailMessage = string.Empty;
            int integrationsNumber = 0;

            //Altro
            EntityCollection ecDataIntegrationDetail = null;
            Entity eDataIntegration = crmServiceProvider.Service.Retrieve(res_dataintegration.logicalName, erDataIntegration.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { res_dataintegration.res_integrationaction, res_dataintegration.res_integrationresult, res_dataintegration.res_integrationsnumber }));
            #endregion

            #region Distribuisco i DataIntegrationDetail
            PluginRegion = "Distribuisco i DataIntegrationDetail";
            try
            {
                #region Imposto il dettaglio dei messaggi di errore per eventualmente increntarli
                if (eDataIntegration.ContainsAttributeNotNull(res_dataintegration.res_integrationresult))
                {
                    detailMessage = eDataIntegration.GetAttributeValue<string>(res_dataintegration.res_integrationresult);
                }
                #endregion

                #region Imposto il numero di integrazioni per determinare lo stato padre
                if (eDataIntegration.ContainsAttributeNotNull(res_dataintegration.res_integrationsnumber))
                {
                    integrationsNumber = eDataIntegration.GetAttributeValue<int>(res_dataintegration.res_integrationsnumber);
                }
                #endregion

                #region Distribuisco i dettagli
                PluginRegion = "Prelevo i DataIntegrationdetail da distribuire";
                ecDataIntegrationDetail = Shared.DataIntegrationDetail.Utility.GetDataIntegrationDetails(crmServiceProvider.Service, erDataIntegration.Id, numberRows, (int)res_dataintegrationdetail.statuscodeValues.Creato_StateAttivo);
                if (ecDataIntegrationDetail?.Entities?.Count > 0)
                {
                    #region Dichiaro variabili globali che potranno servire per l'integrazione
                    EntityReference erPriceLevelERP = null;
                    Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>> dCategory = null;
                    int integrationAction = eDataIntegration.GetAttributeValue<OptionSetValue>(res_dataintegration.res_integrationaction).Value;
                    #endregion

                    #region Definisco dei dati a seconda dell'azione di integrazione che sto affrontando
                    #region recupero il listino prezzi predefinito
                    erPriceLevelERP = Shared.PriceLevel.Utility.GetPriceLevelERP(crmServiceProvider.Service);
                    #endregion

                    #region Recupero le categorie/sottocategorie
                    dCategory = Shared.Product.Utility.GetProductFamily(crmServiceProvider.Service);
                    #endregion
                    #endregion

                    #region Ciclo i DataIntegrationDetail trovati
                    PluginRegion = "Ciclo i DataIntegrationDetail trovati";
                    foreach (Entity enDataIntegrationDetail in ecDataIntegrationDetail.Entities)
                    {
                        string res_integrationrow = enDataIntegrationDetail.GetAttributeValue<string>(res_dataintegrationdetail.res_integrationrow);

                        switch (integrationAction)
                        {
                            case (int)res_dataintegration.res_integrationactionValues.Articoli:
                                PluginRegion = "Deserializzo l'integrationRow dei prodotti";
                                Shared.Product.ImportProductDanea importProductDanea = RSMNG.Plugins.Controller.Deserialize<Shared.Product.ImportProductDanea>(res_integrationrow);

                                #region Creo le famiglie di Prodotti in base alla Categoria e Entita pricipale
                                PluginRegion = "Creo le famiglie di Prodotti in base alla Categoria e Entita pricipale";
                                EntityReference erProductFamily = null;
                                KeyValuePair<string, Guid> categoryKey = default(KeyValuePair<string, Guid>);
                                KeyValuePair<string, Guid> subCategoryKey = default(KeyValuePair<string, Guid>);
                                if (importProductDanea.Categoria != null)
                                {
                                    var foundCategory = dCategory
                                        .Where(entry => entry.Key.Key.Equals(importProductDanea.Categoria.Codice, StringComparison.OrdinalIgnoreCase))
                                        .FirstOrDefault();
                                    if (foundCategory.Equals(default(KeyValuePair<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>>)))
                                    {
                                        Entity enProductFamily = new Entity(product.logicalName);
                                        enProductFamily.Attributes.Add(product.name, importProductDanea.Categoria.Nome);
                                        enProductFamily.Attributes.Add(product.productnumber, importProductDanea.Categoria.Codice);
                                        enProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                        erProductFamily = new EntityReference(product.logicalName, crmServiceProvider.Service.Create(enProductFamily));

                                        //Pubblico la categoria
                                        Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, erProductFamily.Id, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);

                                        //Inserisco la nuova categoria del dictionary
                                        categoryKey = new KeyValuePair<string, Guid>(importProductDanea.Categoria.Codice, erProductFamily.Id);
                                        dCategory[categoryKey] = new List<KeyValuePair<string, Guid>>();
                                    }
                                    else
                                    {
                                        erProductFamily = new EntityReference(product.logicalName, foundCategory.Key.Value);
                                        categoryKey = foundCategory.Key;
                                    }
                                }
                                if (importProductDanea.EntitaPrincipale != null)
                                {
                                    if (erProductFamily != null && !categoryKey.Equals(default(KeyValuePair<string, Guid>)))
                                    {
                                        //Cerco la sotto categoria
                                        var foundSubCategory = dCategory
                                            .Where(entry => entry.Key.Key.Equals(categoryKey.Key, StringComparison.OrdinalIgnoreCase)) // Filtra per nome del padre
                                            .SelectMany(entry => entry.Value.Select(child => new { Parent = entry.Key, Child = child }))
                                            .FirstOrDefault(x => x.Child.Key.Equals(importProductDanea.EntitaPrincipale.Codice, StringComparison.OrdinalIgnoreCase)); // Filtra per nome del figlio

                                        if (foundSubCategory == null)
                                        {
                                            //Creo la sotto categoria legata alla categoria
                                            Entity enSubProductFamily = new Entity(product.logicalName);
                                            enSubProductFamily.Attributes.Add(product.name, importProductDanea.EntitaPrincipale.Nome);
                                            enSubProductFamily.Attributes.Add(product.productnumber, importProductDanea.EntitaPrincipale.Codice);
                                            enSubProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                            enSubProductFamily.Attributes.Add(product.parentproductid, erProductFamily);
                                            erProductFamily = new EntityReference(product.logicalName, crmServiceProvider.Service.Create(enSubProductFamily));

                                            //Pubblico la sotto categoria
                                            Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, erProductFamily.Id, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);

                                            //Inserisco la nuova categoria del dictionary
                                            subCategoryKey = new KeyValuePair<string, Guid>(importProductDanea.EntitaPrincipale.Codice, erProductFamily.Id);
                                            dCategory[categoryKey].Add(subCategoryKey);
                                        }
                                        else
                                        {
                                            erProductFamily = new EntityReference(product.logicalName, foundSubCategory.Child.Value);
                                            subCategoryKey = foundSubCategory.Child;
                                        }
                                    }
                                    else
                                    {
                                        //Cerco la catagoria
                                        var foundCategory = dCategory
                                            .Where(entry => entry.Key.Key.Equals(importProductDanea.EntitaPrincipale.Codice, StringComparison.OrdinalIgnoreCase))
                                            .FirstOrDefault();
                                        if (foundCategory.Equals(default(KeyValuePair<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>>)))
                                        {
                                            Entity enProductFamily = new Entity(product.logicalName);
                                            enProductFamily.Attributes.Add(product.name, importProductDanea.EntitaPrincipale.Nome);
                                            enProductFamily.Attributes.Add(product.productnumber, importProductDanea.EntitaPrincipale.Codice);
                                            enProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                            erProductFamily = new EntityReference(product.logicalName, crmServiceProvider.Service.Create(enProductFamily));

                                            //Pubblico la categoria
                                            Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, erProductFamily.Id, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);

                                            //Inserisco la nuova categoria del dictionary
                                            categoryKey = new KeyValuePair<string, Guid>(importProductDanea.EntitaPrincipale.Codice, erProductFamily.Id);
                                            dCategory[categoryKey] = new List<KeyValuePair<string, Guid>>();
                                        }
                                        else
                                        {
                                            categoryKey = foundCategory.Key;
                                            erProductFamily = new EntityReference(product.logicalName, foundCategory.Key.Value);
                                        }
                                    }
                                }
                                #endregion

                                #region Creo Prodotto sotto una famiglia o una sottofamiglia di prodotti
                                try
                                {
                                    PluginRegion = "Creo Prodotto sotto una famiglia o una sottofamiglia di prodotti";
                                    //Cerco il prodotto
                                    Entity enProduct = Shared.Product.Utility.GetProduct(crmServiceProvider.Service, importProductDanea.Codice);

                                    Entity enProductUpt = new Entity(product.logicalName);
                                    enProductUpt.Attributes.Add(product.res_origincode, importProductDanea.Origine.Value != null ? new OptionSetValue((int)importProductDanea.Origine.Value) : null);
                                    enProductUpt.Attributes.Add(product.name, importProductDanea.Nome);
                                    if (!categoryKey.Equals(default(KeyValuePair<string, Guid>)))
                                    {
                                        enProductUpt.Attributes.Add(product.res_parentcategoryid, new EntityReference(product.logicalName, categoryKey.Value));
                                    }
                                    enProductUpt.Attributes.Add(product.description, importProductDanea.Descrizione);
                                    enProductUpt.Attributes.Add(product.defaultuomscheduleid, new EntityReference(importProductDanea.UnitaDiVendita.Entity, importProductDanea.UnitaDiVendita.Id));
                                    enProductUpt.Attributes.Add(product.defaultuomid, new EntityReference(importProductDanea.UnitaPredefinita.Entity, importProductDanea.UnitaPredefinita.Id));
                                    enProductUpt.Attributes.Add(product.quantitydecimal, importProductDanea.DecimaliSupportati);
                                    enProductUpt.Attributes.Add(product.res_vatnumberid, importProductDanea.CodiceIVA != null ? new EntityReference(importProductDanea.CodiceIVA.Entity, importProductDanea.CodiceIVA.Id) : null);
                                    enProductUpt.Attributes.Add(product.price, importProductDanea.PrezzoDiListino != null ? new Money((decimal)importProductDanea.PrezzoDiListino) : null);
                                    OptionMetadata producttypecode = Helper.GetOptionSetUserLocalized(crmServiceProvider.Service, product.logicalName, product.producttypecode, importProductDanea.Tipologia.ExternalValue);
                                    enProductUpt.Attributes.Add(product.producttypecode, producttypecode != null ? new OptionSetValue((int)producttypecode.Value) : null);
                                    enProductUpt.Attributes.Add(product.productstructure, importProductDanea.StrutturaProdotto != null ? new OptionSetValue((int)importProductDanea.StrutturaProdotto.Value) : null);
                                    enProductUpt.Attributes.Add(product.res_manufacturer, importProductDanea.Produttore);
                                    enProductUpt.Attributes.Add(product.suppliername, importProductDanea.Fornitore);
                                    enProductUpt.Attributes.Add(product.res_barcode, importProductDanea.CodiceABarre);
                                    enProductUpt.Attributes.Add(product.res_grossweight, importProductDanea.PesoLordo);
                                    enProductUpt.Attributes.Add(product.stockweight, importProductDanea.PesoNetto);
                                    enProductUpt.Attributes.Add(product.stockvolume, importProductDanea.VolumeCm3);
                                    enProductUpt.Attributes.Add(product.res_uomweightid, importProductDanea.UnitaDimisuraPeso != null ? new EntityReference(importProductDanea.UnitaDimisuraPeso.Entity, importProductDanea.UnitaDimisuraPeso.Id) : null);

                                    if (enProduct == null)
                                    {
                                        if (erProductFamily != null)
                                        {
                                            enProductUpt.Attributes.Add(product.parentproductid, erProductFamily);
                                        }
                                        enProductUpt.Attributes.Add(product.productnumber, importProductDanea.Codice);
                                        Guid enProductUptId = crmServiceProvider.Service.Create(enProductUpt);

                                        //Aggiorno il listino prezzi
                                        enProductUpt.Attributes.Add(product.pricelevelid, erPriceLevelERP);
                                        enProductUpt.Id = enProductUptId;
                                        crmServiceProvider.Service.Update(enProductUpt);

                                        //Attivo il prodotto
                                        Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, enProductUptId, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);


                                    }
                                    else
                                    {
                                        enProductUpt.Id = enProduct.Id;
                                        crmServiceProvider.Service.Update(enProductUpt);
                                    }
                                    integrationsNumber++;

                                    #region Aggiorno lo stato del DataIntegrationDetail in distribuito
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegrationdetail.statecodeValues.Inattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue((int)res_dataintegrationdetail.statuscodeValues.Distribuito_StateInattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegrationdetail.res_integrationresult, "Ok, distribuito correttamente");
                                    crmServiceProvider.Service.Update(enDataIntegrationDetail);
                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    #region Aggiorno lo stato del DataIntegrationDetail in non distribuito
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegrationdetail.statecodeValues.Inattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue((int)res_dataintegrationdetail.statuscodeValues.NotDistribuito_StateInattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegrationdetail.res_integrationresult, $@"Errore: {e.Message}");
                                    crmServiceProvider.Service.Update(enDataIntegrationDetail);
                                    #endregion

                                    detailMessage += $@"{Environment.NewLine}- Errore: {e.Message}";
                                }
                                #endregion
                                break;
                            case (int)res_dataintegration.res_integrationactionValues.DocumentiRicevute:
                                PluginRegion = "Deserializzo l'integrationRow delle ricevute";
                                Shared.Document.ImportReceiptDanea importReceiptDanea = RSMNG.Plugins.Controller.Deserialize<Shared.Document.ImportReceiptDanea>(res_integrationrow);

                                #region Creo la ricevuta
                                PluginRegion = "Creo la ricevuta";
                                KeyAttributeCollection idx_DocumentReceipt = new KeyAttributeCollection {
                                    new KeyValuePair<string, object> (res_document.res_documenttypecode,importReceiptDanea.TipoDoc.Value),
                                    new KeyValuePair<string, object> (res_document.res_documentnumber,importReceiptDanea.NDoc)
                                };
                                Entity eDocumentReceiptUpt = new Entity(res_document.logicalName, idx_DocumentReceipt);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_documenttypecode, new OptionSetValue((int)importReceiptDanea.TipoDoc.Value));
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_customerid, importReceiptDanea.Cliente != null ? new EntityReference(importReceiptDanea.Cliente.Entity, importReceiptDanea.Cliente.Id) : null);
                                if (importReceiptDanea.Data != null)
                                {
                                    eDocumentReceiptUpt.Attributes.Add(res_document.res_date, Convert.ToDateTime(importReceiptDanea.Data));
                                }
                                else
                                {
                                    eDocumentReceiptUpt.Attributes.Add(res_document.res_date, null);
                                }
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_documentnumber, importReceiptDanea.NDoc);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_customernumber, importReceiptDanea.CodCliente);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_agent, importReceiptDanea.CodAgente);
                                if (importReceiptDanea.Agente != null)
                                {
                                    eDocumentReceiptUpt.Attributes.Add(res_document.ownerid, new EntityReference(importReceiptDanea.Agente.Entity, importReceiptDanea.Agente.Id));
                                }
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_nettotalexcludingvat, importReceiptDanea.TotNettoIva != null ? new Money((decimal)importReceiptDanea.TotNettoIva) : null);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_vat, importReceiptDanea.Iva != null ? new Money((decimal)importReceiptDanea.Iva) : null);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_documenttotal, importReceiptDanea.TotDoc != null ? new Money((decimal)importReceiptDanea.TotDoc) : null);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_ispendingpayment, importReceiptDanea.DaSaldare != null ? new Money((decimal)importReceiptDanea.DaSaldare) : null);
                                if (importReceiptDanea.DataUltimoPag != null)
                                {
                                    eDocumentReceiptUpt.Attributes.Add(res_document.res_lastpaymentdate, Convert.ToDateTime(importReceiptDanea.DataUltimoPag));
                                }
                                else
                                {
                                    eDocumentReceiptUpt.Attributes.Add(res_document.res_lastpaymentdate, null);
                                }
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_paymenttermid, importReceiptDanea.Pagamento != null ? new EntityReference(importReceiptDanea.Pagamento.Entity, importReceiptDanea.Pagamento.Id) : null);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_bankdetailsid, importReceiptDanea.CoordBancarie != null ? new EntityReference(importReceiptDanea.CoordBancarie.Entity, importReceiptDanea.CoordBancarie.Id) : null);
                                eDocumentReceiptUpt.Attributes.Add(res_document.res_note, importReceiptDanea.Commento);

                                //Effettuo l'upsert del documento - Ricevute
                                UpsertRequest requestDocumentReceiptUpt = new UpsertRequest()
                                {
                                    Target = eDocumentReceiptUpt
                                };
                                UpsertResponse responseDocumentReceiptUpt = (UpsertResponse)crmServiceProvider.Service.Execute(requestDocumentReceiptUpt);
                                #endregion
                                break;
                            case (int)res_dataintegration.res_integrationactionValues.DocumentiFatture:
                                PluginRegion = "Deserializzo l'integrationRow delle fatture";
                                Shared.Document.ImportInvoiceDanea importInvoiceDanea = RSMNG.Plugins.Controller.Deserialize<Shared.Document.ImportInvoiceDanea>(res_integrationrow);

                                #region Creo la fattura
                                PluginRegion = "Creo la fattura";
                                KeyAttributeCollection idx_DocumentInvoice = new KeyAttributeCollection {
                                    new KeyValuePair<string, object> (res_document.res_documenttypecode,importInvoiceDanea.TipoDoc.Value),
                                    new KeyValuePair<string, object> (res_document.res_documentnumber,importInvoiceDanea.NDoc)
                                };
                                Entity eDocumentInvoiceUpt = new Entity(res_document.logicalName, idx_DocumentInvoice);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_documenttypecode, new OptionSetValue((int)importInvoiceDanea.TipoDoc.Value));
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_customerid, importInvoiceDanea.Cliente != null ? new EntityReference(importInvoiceDanea.Cliente.Entity, importInvoiceDanea.Cliente.Id) : null);
                                if (importInvoiceDanea.Data != null)
                                {
                                    eDocumentInvoiceUpt.Attributes.Add(res_document.res_date, Convert.ToDateTime(importInvoiceDanea.Data));
                                }
                                else
                                {
                                    eDocumentInvoiceUpt.Attributes.Add(res_document.res_date, null);
                                }
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_documentnumber, importInvoiceDanea.NDoc);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_customernumber, importInvoiceDanea.CodCliente);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_agent, importInvoiceDanea.CodAgente);
                                if (importInvoiceDanea.Agente != null)
                                {
                                    eDocumentInvoiceUpt.Attributes.Add(res_document.ownerid, new EntityReference(importInvoiceDanea.Agente.Entity, importInvoiceDanea.Agente.Id));
                                }
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_nettotalexcludingvat, importInvoiceDanea.TotNettoIva != null ? new Money((decimal)importInvoiceDanea.TotNettoIva) : null);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_vat, importInvoiceDanea.Iva != null ? new Money((decimal)importInvoiceDanea.Iva) : null);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_documenttotal, importInvoiceDanea.TotDoc != null ? new Money((decimal)importInvoiceDanea.TotDoc) : null);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_ispendingpayment, importInvoiceDanea.DaSaldare != null ? new Money((decimal)importInvoiceDanea.DaSaldare) : null);
                                if (importInvoiceDanea.DataUltimoPag != null)
                                {
                                    eDocumentInvoiceUpt.Attributes.Add(res_document.res_lastpaymentdate, Convert.ToDateTime(importInvoiceDanea.DataUltimoPag));
                                }
                                else
                                {
                                    eDocumentInvoiceUpt.Attributes.Add(res_document.res_lastpaymentdate, null);
                                }
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_paymenttermid, importInvoiceDanea.Pagamento != null ? new EntityReference(importInvoiceDanea.Pagamento.Entity, importInvoiceDanea.Pagamento.Id) : null);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_bankdetailsid, importInvoiceDanea.CoordBancarie != null ? new EntityReference(importInvoiceDanea.CoordBancarie.Entity, importInvoiceDanea.CoordBancarie.Id) : null);
                                eDocumentInvoiceUpt.Attributes.Add(res_document.res_note, importInvoiceDanea.Commento);

                                //Effettuo l'upsert del documento - Fatture
                                UpsertRequest requestDocumentInvoiceUpt = new UpsertRequest()
                                {
                                    Target = eDocumentInvoiceUpt
                                };
                                UpsertResponse responseDocumentInvoiceUpt = (UpsertResponse)crmServiceProvider.Service.Execute(requestDocumentInvoiceUpt);
                                #endregion
                                break;
                        }
                    }
                    #endregion

                    #region Definisco lo status code che viene fuori dalla action e aggiorno lo stato
                    statusCode = integrationsNumber == 0 ? (int)res_dataintegration.statuscodeValues.NonDistribuito_StateInattivo : integrationsNumber.Equals(ecDataIntegrationDetail.Entities.Count) ? (int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo : (int)res_dataintegration.statuscodeValues.Distribuitoparzialmente_StateInattivo;
                    eDataIntegration.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegration.statecodeValues.Inattivo));
                    eDataIntegration.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue(statusCode));
                    #endregion

                    #region Aggiorno il numero di integrazioni e dettaglio
                    eDataIntegration.AddWithRemove(res_dataintegration.res_integrationsnumber, integrationsNumber);
                    eDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, detailMessage);
                    crmServiceProvider.Service.Update(eDataIntegration);
                    #endregion
                }
                else
                {
                    statusCode = string.IsNullOrEmpty(detailMessage) ? (int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo : (int)res_dataintegration.statuscodeValues.Distribuitoparzialmente_StateInattivo;
                    eDataIntegration.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegration.statecodeValues.Inattivo));
                    eDataIntegration.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue(statusCode));
                    eDataIntegration.AddWithRemove(res_dataintegration.res_integrationsnumber, integrationsNumber);
                    eDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, detailMessage);
                    crmServiceProvider.Service.Update(eDataIntegration);
                }
                #endregion








                #region Controllo il tipo di distribuzione da fare in base all'azione
                PluginRegion = "Controllo il tipo di distribuzione da fare in base all'azione";
                switch (eDataIntegration.GetAttributeValue<OptionSetValue>(res_dataintegration.res_integrationaction).Value)
                {
                    case (int)res_dataintegration.res_integrationactionValues.Articoli:

                        #region recupero il listino prezzi predefinito
                        EntityReference erPriceLevelERP = Shared.PriceLevel.Utility.GetPriceLevelERP(crmServiceProvider.Service);
                        #endregion

                        #region Recupero le categorie/sottocategorie
                        Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>> dCategory = Shared.Product.Utility.GetProductFamily(crmServiceProvider.Service);
                        #endregion

                        #region Distribuisco gli articoli
                        PluginRegion = "Prelevo i DataIntegrationdetail da distribuire";
                        ecDataIntegrationDetail = Shared.DataIntegrationDetail.Utility.GetDataIntegrationDetails(crmServiceProvider.Service, erDataIntegration.Id, numberRows, (int)res_dataintegrationdetail.statuscodeValues.Creato_StateAttivo);
                        if (ecDataIntegrationDetail?.Entities?.Count > 0)
                        {
                            #region Ciclo i DataIntegrationDetail trovati
                            PluginRegion = "Ciclo i DataIntegrationDetail trovati";
                            foreach (Entity enDataIntegrationDetail in ecDataIntegrationDetail.Entities)
                            {
                                PluginRegion = "Deserializzo l'integrationRow";
                                string res_integrationrow = enDataIntegrationDetail.GetAttributeValue<string>(res_dataintegrationdetail.res_integrationrow);
                                Shared.Product.ImportProductDanea importProductDanea = RSMNG.Plugins.Controller.Deserialize<Shared.Product.ImportProductDanea>(res_integrationrow);

                                #region Creo le famiglie di Prodotti in base alla Categoria e Entita pricipale
                                PluginRegion = "Creo le famiglie di Prodotti in base alla Categoria e Entita pricipale";
                                EntityReference erProductFamily = null;
                                KeyValuePair<string, Guid> categoryKey = default(KeyValuePair<string, Guid>);
                                KeyValuePair<string, Guid> subCategoryKey = default(KeyValuePair<string, Guid>);
                                if (importProductDanea.Categoria != null)
                                {
                                    var foundCategory = dCategory
                                        .Where(entry => entry.Key.Key.Equals(importProductDanea.Categoria.Codice, StringComparison.OrdinalIgnoreCase))
                                        .FirstOrDefault();
                                    if (foundCategory.Equals(default(KeyValuePair<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>>)))
                                    {
                                        Entity enProductFamily = new Entity(product.logicalName);
                                        enProductFamily.Attributes.Add(product.name, importProductDanea.Categoria.Nome);
                                        enProductFamily.Attributes.Add(product.productnumber, importProductDanea.Categoria.Codice);
                                        enProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                        erProductFamily = new EntityReference(product.logicalName, crmServiceProvider.Service.Create(enProductFamily));

                                        //Pubblico la categoria
                                        Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, erProductFamily.Id, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);

                                        //Inserisco la nuova categoria del dictionary
                                        categoryKey = new KeyValuePair<string, Guid>(importProductDanea.Categoria.Codice, erProductFamily.Id);
                                        dCategory[categoryKey] = new List<KeyValuePair<string, Guid>>();
                                    }
                                    else
                                    {
                                        erProductFamily = new EntityReference(product.logicalName, foundCategory.Key.Value);
                                        categoryKey = foundCategory.Key;
                                    }
                                }
                                if (importProductDanea.EntitaPrincipale != null)
                                {
                                    if (erProductFamily != null && !categoryKey.Equals(default(KeyValuePair<string, Guid>)))
                                    {
                                        //Cerco la sotto categoria
                                        var foundSubCategory = dCategory
                                            .Where(entry => entry.Key.Key.Equals(categoryKey.Key, StringComparison.OrdinalIgnoreCase)) // Filtra per nome del padre
                                            .SelectMany(entry => entry.Value.Select(child => new { Parent = entry.Key, Child = child }))
                                            .FirstOrDefault(x => x.Child.Key.Equals(importProductDanea.EntitaPrincipale.Codice, StringComparison.OrdinalIgnoreCase)); // Filtra per nome del figlio

                                        if (foundSubCategory == null)
                                        {
                                            //Creo la sotto categoria legata alla categoria
                                            Entity enSubProductFamily = new Entity(product.logicalName);
                                            enSubProductFamily.Attributes.Add(product.name, importProductDanea.EntitaPrincipale.Nome);
                                            enSubProductFamily.Attributes.Add(product.productnumber, importProductDanea.EntitaPrincipale.Codice);
                                            enSubProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                            enSubProductFamily.Attributes.Add(product.parentproductid, erProductFamily);
                                            erProductFamily = new EntityReference(product.logicalName, crmServiceProvider.Service.Create(enSubProductFamily));

                                            //Pubblico la sotto categoria
                                            Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, erProductFamily.Id, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);

                                            //Inserisco la nuova categoria del dictionary
                                            subCategoryKey = new KeyValuePair<string, Guid>(importProductDanea.EntitaPrincipale.Codice, erProductFamily.Id);
                                            dCategory[categoryKey].Add(subCategoryKey);
                                        }
                                        else
                                        {
                                            erProductFamily = new EntityReference(product.logicalName, foundSubCategory.Child.Value);
                                            subCategoryKey = foundSubCategory.Child;
                                        }
                                    }
                                    else
                                    {
                                        //Cerco la catagoria
                                        var foundCategory = dCategory
                                            .Where(entry => entry.Key.Key.Equals(importProductDanea.EntitaPrincipale.Codice, StringComparison.OrdinalIgnoreCase))
                                            .FirstOrDefault();
                                        if (foundCategory.Equals(default(KeyValuePair<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>>)))
                                        {
                                            Entity enProductFamily = new Entity(product.logicalName);
                                            enProductFamily.Attributes.Add(product.name, importProductDanea.EntitaPrincipale.Nome);
                                            enProductFamily.Attributes.Add(product.productnumber, importProductDanea.EntitaPrincipale.Codice);
                                            enProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                            erProductFamily = new EntityReference(product.logicalName, crmServiceProvider.Service.Create(enProductFamily));

                                            //Pubblico la categoria
                                            Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, erProductFamily.Id, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);

                                            //Inserisco la nuova categoria del dictionary
                                            categoryKey = new KeyValuePair<string, Guid>(importProductDanea.EntitaPrincipale.Codice, erProductFamily.Id);
                                            dCategory[categoryKey] = new List<KeyValuePair<string, Guid>>();
                                        }
                                        else
                                        {
                                            categoryKey = foundCategory.Key;
                                            erProductFamily = new EntityReference(product.logicalName, foundCategory.Key.Value);
                                        }
                                    }
                                }
                                #endregion

                                #region Creo Prodotto sotto una famiglia o una sottofamiglia di prodotti
                                try
                                {
                                    //Cerco il prodotto
                                    Entity enProduct = Shared.Product.Utility.GetProduct(crmServiceProvider.Service, importProductDanea.Codice);

                                    Entity enProductUpt = new Entity(product.logicalName);
                                    enProductUpt.Attributes.Add(product.res_origincode, importProductDanea.Origine.Value != null ? new OptionSetValue((int)importProductDanea.Origine.Value) : null);
                                    enProductUpt.Attributes.Add(product.name, importProductDanea.Nome);
                                    if (!categoryKey.Equals(default(KeyValuePair<string, Guid>)))
                                    {
                                        enProductUpt.Attributes.Add(product.res_parentcategoryid, new EntityReference(product.logicalName, categoryKey.Value));
                                    }
                                    enProductUpt.Attributes.Add(product.description, importProductDanea.Descrizione);
                                    enProductUpt.Attributes.Add(product.defaultuomscheduleid, new EntityReference(importProductDanea.UnitaDiVendita.Entity, importProductDanea.UnitaDiVendita.Id));
                                    enProductUpt.Attributes.Add(product.defaultuomid, new EntityReference(importProductDanea.UnitaPredefinita.Entity, importProductDanea.UnitaPredefinita.Id));
                                    enProductUpt.Attributes.Add(product.quantitydecimal, importProductDanea.DecimaliSupportati);
                                    enProductUpt.Attributes.Add(product.res_vatnumberid, importProductDanea.CodiceIVA != null ? new EntityReference(importProductDanea.CodiceIVA.Entity, importProductDanea.CodiceIVA.Id) : null);
                                    enProductUpt.Attributes.Add(product.price, importProductDanea.PrezzoDiListino != null ? new Money((decimal)importProductDanea.PrezzoDiListino) : null);
                                    OptionMetadata producttypecode = Helper.GetOptionSetUserLocalized(crmServiceProvider.Service, product.logicalName, product.producttypecode, importProductDanea.Tipologia.ExternalValue);
                                    enProductUpt.Attributes.Add(product.producttypecode, producttypecode != null ? new OptionSetValue((int)producttypecode.Value) : null);
                                    enProductUpt.Attributes.Add(product.productstructure, importProductDanea.StrutturaProdotto != null ? new OptionSetValue((int)importProductDanea.StrutturaProdotto.Value) : null);
                                    enProductUpt.Attributes.Add(product.res_manufacturer, importProductDanea.Produttore);
                                    enProductUpt.Attributes.Add(product.suppliername, importProductDanea.Fornitore);
                                    enProductUpt.Attributes.Add(product.res_barcode, importProductDanea.CodiceABarre);
                                    enProductUpt.Attributes.Add(product.res_grossweight, importProductDanea.PesoLordo);
                                    enProductUpt.Attributes.Add(product.stockweight, importProductDanea.PesoNetto);
                                    enProductUpt.Attributes.Add(product.stockvolume, importProductDanea.VolumeCm3);
                                    enProductUpt.Attributes.Add(product.res_uomweightid, importProductDanea.UnitaDimisuraPeso != null ? new EntityReference(importProductDanea.UnitaDimisuraPeso.Entity, importProductDanea.UnitaDimisuraPeso.Id) : null);

                                    if (enProduct == null)
                                    {
                                        if (erProductFamily != null)
                                        {
                                            enProductUpt.Attributes.Add(product.parentproductid, erProductFamily);
                                        }
                                        enProductUpt.Attributes.Add(product.productnumber, importProductDanea.Codice);
                                        Guid enProductUptId = crmServiceProvider.Service.Create(enProductUpt);

                                        //Aggiorno il listino prezzi
                                        enProductUpt.Attributes.Add(product.pricelevelid, erPriceLevelERP);
                                        enProductUpt.Id = enProductUptId;
                                        crmServiceProvider.Service.Update(enProductUpt);

                                        //Attivo il prodotto
                                        Helper.SetStateCode(crmServiceProvider.Service, product.logicalName, enProductUptId, (int)product.statecodeValues.Attivo, (int)product.statuscodeValues.Attivo_StateAttivo);


                                    }
                                    else
                                    {
                                        enProductUpt.Id = enProduct.Id;
                                        crmServiceProvider.Service.Update(enProductUpt);
                                    }
                                    integrationsNumber++;

                                    #region Aggiorno lo stato del DataIntegrationDetail in distribuito
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegrationdetail.statecodeValues.Inattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue((int)res_dataintegrationdetail.statuscodeValues.Distribuito_StateInattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegrationdetail.res_integrationresult, "Ok, distribuito correttamente");
                                    crmServiceProvider.Service.Update(enDataIntegrationDetail);
                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    #region Aggiorno lo stato del DataIntegrationDetail in non distribuito
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegrationdetail.statecodeValues.Inattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue((int)res_dataintegrationdetail.statuscodeValues.NotDistribuito_StateInattivo));
                                    enDataIntegrationDetail.AddWithRemove(res_dataintegrationdetail.res_integrationresult, $@"Errore: {e.Message}");
                                    crmServiceProvider.Service.Update(enDataIntegrationDetail);
                                    #endregion

                                    detailMessage += $@"{Environment.NewLine}- Errore: {e.Message}";
                                }
                                #endregion
                            }
                            #endregion

                            #region Definisco lo status code che viene fuori dalla action e aggiorno lo stato
                            statusCode = integrationsNumber == 0 ? (int)res_dataintegration.statuscodeValues.NonDistribuito_StateInattivo : integrationsNumber.Equals(ecDataIntegrationDetail.Entities.Count) ? (int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo : (int)res_dataintegration.statuscodeValues.Distribuitoparzialmente_StateInattivo;
                            eDataIntegration.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegration.statecodeValues.Inattivo));
                            eDataIntegration.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue(statusCode));
                            #endregion

                            #region Aggiorno il numero di integrazioni e dettaglio
                            eDataIntegration.AddWithRemove(res_dataintegration.res_integrationsnumber, integrationsNumber);
                            eDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, detailMessage);
                            crmServiceProvider.Service.Update(eDataIntegration);
                            #endregion
                        }
                        else
                        {
                            statusCode = string.IsNullOrEmpty(detailMessage) ? (int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo : (int)res_dataintegration.statuscodeValues.Distribuitoparzialmente_StateInattivo;
                            eDataIntegration.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegration.statecodeValues.Inattivo));
                            eDataIntegration.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue(statusCode));
                            eDataIntegration.AddWithRemove(res_dataintegration.res_integrationsnumber, integrationsNumber);
                            eDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, detailMessage);
                            crmServiceProvider.Service.Update(eDataIntegration);
                        }
                        #endregion
                        break;
                    case (int)res_dataintegration.res_integrationactionValues.DocumentiRicevute:
                        break;
                    case (int)res_dataintegration.res_integrationactionValues.DocumentiFatture:
                        break;
                }
                #endregion
            }
            catch (Exception e)
            {
                statusCode = integrationsNumber == 0 ? (int)res_dataintegration.statuscodeValues.NonDistribuito_StateInattivo : (int)res_dataintegration.statuscodeValues.Distribuitoparzialmente_StateInattivo;
                detailMessage += $@"{Environment.NewLine}- Errore: {e.Message}";
                eDataIntegration.AddWithRemove(res_dataintegration.statecode, new OptionSetValue((int)res_dataintegration.statecodeValues.Inattivo));
                eDataIntegration.AddWithRemove(res_dataintegration.statuscode, new OptionSetValue(statusCode));
                eDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, detailMessage);
                crmServiceProvider.Service.Update(eDataIntegration);
            }
            finally
            {
                crmServiceProvider.PluginContext.OutputParameters["StatusCode"] = statusCode;
                crmServiceProvider.PluginContext.OutputParameters["DetailMessage"] = detailMessage;
            }
            #endregion
        }
    }
}
