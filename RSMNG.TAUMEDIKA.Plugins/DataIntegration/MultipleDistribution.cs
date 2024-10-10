using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

                #region Controllo il tipo di distribuzione da fare in base all'azione
                PluginRegion = "Controllo il tipo di distribuzione da fare in base all'azione";
                switch (eDataIntegration.GetAttributeValue<OptionSetValue>(res_dataintegration.res_integrationaction).Value)
                {
                    case (int)res_dataintegration.res_integrationactionValues.Articoli:
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
                                if (importProductDanea.Categoria != null)
                                {
                                    KeyAttributeCollection productFamilyKeys = new KeyAttributeCollection();
                                    productFamilyKeys.Add(product.productnumber, importProductDanea.Categoria.Codice);
                                    Entity enProductFamily = new Entity(product.logicalName, productFamilyKeys);
                                    enProductFamily.Attributes.Add(product.name, importProductDanea.Categoria.Nome);
                                    enProductFamily.Attributes.Add(product.productnumber, importProductDanea.Categoria.Codice);
                                    enProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                    UpsertRequest upsertRequestProductFamily = new UpsertRequest()
                                    {
                                        Target = enProductFamily
                                    };
                                    UpsertResponse upsertResponseProductFamily = (UpsertResponse)crmServiceProvider.Service.Execute(upsertRequestProductFamily);
                                    erProductFamily = upsertResponseProductFamily.Target;
                                }
                                if (importProductDanea.EntitaPrincipale != null)
                                {
                                    KeyAttributeCollection subProductFamilyKeys = new KeyAttributeCollection();
                                    subProductFamilyKeys.Add(product.productnumber, importProductDanea.EntitaPrincipale.Codice);
                                    if (erProductFamily != null)
                                    {
                                        subProductFamilyKeys.Add(product.parentproductid, erProductFamily);
                                    }
                                    Entity enSubProductFamily = new Entity(product.logicalName, subProductFamilyKeys);
                                    enSubProductFamily.Attributes.Add(product.name, importProductDanea.EntitaPrincipale.Nome);
                                    enSubProductFamily.Attributes.Add(product.productnumber, importProductDanea.EntitaPrincipale.Codice);
                                    if (erProductFamily != null)
                                    {
                                        enSubProductFamily.Attributes.Add(product.parentproductid, erProductFamily);
                                    }
                                    enSubProductFamily.Attributes.Add(product.productstructure, new OptionSetValue((int)product.productstructureValues.Famigliadiprodotti));
                                    UpsertRequest upsertRequestSubProductFamily = new UpsertRequest()
                                    {
                                        Target = enSubProductFamily
                                    };
                                    UpsertResponse upsertResponseSubProductFamily = (UpsertResponse)crmServiceProvider.Service.Execute(upsertRequestSubProductFamily);
                                    erProductFamily = upsertResponseSubProductFamily.Target;
                                }
                                #endregion

                                #region Creo Prodotto sotto una famiglia o una sottofamiglia di prodotti
                                try
                                {
                                    KeyAttributeCollection productKeys = new KeyAttributeCollection();
                                    productKeys.Add(product.productnumber, importProductDanea.Codice);
                                    if (erProductFamily != null)
                                    {
                                        productKeys.Add(product.parentproductid, erProductFamily);
                                    }
                                    Entity enProduct = new Entity(product.logicalName);
                                    enProduct.Attributes.Add(product.res_origincode, importProductDanea.Origine.Value != null ? new OptionSetValue((int)importProductDanea.Origine.Value) : null);
                                    enProduct.Attributes.Add(product.name, importProductDanea.Nome);
                                    enProduct.Attributes.Add(product.productnumber, importProductDanea.Codice);
                                    if (erProductFamily != null)
                                    {
                                        enProduct.Attributes.Add(product.parentproductid, erProductFamily);
                                    }
                                    enProduct.Attributes.Add(product.description, importProductDanea.Descrizione);
                                    enProduct.Attributes.Add(product.defaultuomscheduleid, new EntityReference(importProductDanea.UnitaDiVendita.Entity, importProductDanea.UnitaDiVendita.Id));
                                    enProduct.Attributes.Add(product.defaultuomid, new EntityReference(importProductDanea.UnitaPredefinita.Entity, importProductDanea.UnitaPredefinita.Id));
                                    enProduct.Attributes.Add(product.quantitydecimal, importProductDanea.DecimaliSupportati);
                                    enProduct.Attributes.Add(product.res_vatnumberid, importProductDanea.CodiceIVA != null ? new EntityReference(importProductDanea.CodiceIVA.Entity, importProductDanea.CodiceIVA.Id) : null);
                                    enProduct.Attributes.Add(product.price, importProductDanea.PrezzoDiListino != null ? new Money((decimal)importProductDanea.PrezzoDiListino) : null);
                                    OptionMetadata producttypecode = Helper.GetOptionSetUserLocalized(crmServiceProvider.Service, product.logicalName, product.producttypecode, importProductDanea.Tipologia.ExternalValue);
                                    enProduct.Attributes.Add(product.producttypecode, producttypecode != null ? new OptionSetValue((int)producttypecode.Value) : null);
                                    enProduct.Attributes.Add(product.productstructure, importProductDanea.StrutturaProdotto != null ? new OptionSetValue((int)importProductDanea.StrutturaProdotto.Value) : null);
                                    enProduct.Attributes.Add(product.res_manufacturer, importProductDanea.Produttore);
                                    enProduct.Attributes.Add(product.suppliername, importProductDanea.Fornitore);
                                    enProduct.Attributes.Add(product.statecode, new OptionSetValue((int)importProductDanea.Stato.Value));
                                    enProduct.Attributes.Add(product.statuscode, new OptionSetValue((int)importProductDanea.MotivoStato.Value));
                                    enProduct.Attributes.Add(product.res_barcode, importProductDanea.CodiceABarre);
                                    enProduct.Attributes.Add(product.res_grossweight, importProductDanea.PesoLordo);
                                    enProduct.Attributes.Add(product.stockweight, importProductDanea.PesoNetto);
                                    enProduct.Attributes.Add(product.stockvolume, importProductDanea.VolumeCm3);
                                    enProduct.Attributes.Add(product.res_uomweightid, importProductDanea.UnitaDimisuraPeso != null ? new EntityReference(importProductDanea.UnitaDimisuraPeso.Entity, importProductDanea.UnitaDimisuraPeso.Id) : null);
                                    UpsertRequest upsertRequestProduct = new UpsertRequest()
                                    {
                                        Target = enProduct
                                    };
                                    UpsertResponse upsertResponseProduct = (UpsertResponse)crmServiceProvider.Service.Execute(upsertRequestProduct);
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
