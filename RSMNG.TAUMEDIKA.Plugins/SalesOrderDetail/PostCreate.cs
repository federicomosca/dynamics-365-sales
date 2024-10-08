using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PostCreate : RSMNG.BaseClass
    {
        public PostCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Create";
            PluginPrimaryEntityName = salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Guid targetId = target.Id;

            #region Valorizzo i campi Codice IVA e Aliquota IVA
            PluginRegion = "Valorizzo i campi Codice IVA e Aliquota IVA";

            var fetchCodiceIVA = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                    <entity name=""salesorderdetail"">
                                    <filter>
                                        <condition attribute=""salesorderdetailid"" operator=""eq"" value=""{targetId}"" />
                                    </filter>
                                    <link-entity name=""product"" from=""productid"" to=""productid"" alias=""product"">
                                        <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""codiceivalookup"">
                                        <attribute name=""res_rate"" alias=""aliquota"" />
                                        <attribute name=""res_vatnumberid"" alias=""codiceiva"" />
                                        </link-entity>
                                    </link-entity>
                                    </entity>
                                </fetch>";
            crmServiceProvider.TracingService.Trace(fetchCodiceIVA);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchCodiceIVA));

            if (collection.Entities.Count > 0)
            {
                Entity salesorderdetail = collection.Entities[0];

                Guid codiceiva = salesorderdetail.Contains("codiceiva") ? (Guid)salesorderdetail.GetAttributeValue<AliasedValue>("codiceiva").Value : Guid.Empty;
                Decimal? aliquota = salesorderdetail.Contains("aliquota") ? (Decimal?)salesorderdetail.GetAttributeValue<AliasedValue>("aliquota").Value : null;
                if (codiceiva != null && aliquota.HasValue)
                {
                    EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceiva);

                    target[DataModel.salesorderdetail.res_vatnumberid] = erCodiceIVA;
                    target[DataModel.salesorderdetail.res_vatrate] = aliquota;
                    crmServiceProvider.Service.Update(target);
                }
                else throw new ApplicationException("Codice IVA non trovato");
            }
            #endregion

            #region Valorizzo il campo Totale imponibile
            PluginRegion = "Valorizzo il campo Totale imponibile";

            //totale imponibile = importo - sconto totale
            decimal taxableamount = 0m;
            decimal baseamount = 0m;

            /**
             * recupero il prezzo unitario dalla voce di listino associata
             * recupero la quantità dal target e moltiplico i due valori
             * al risultato si sottrae lo sconto totale per ottenere il totale imponibile
             */
            var fetchSalesOrderDetail = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch top=""1"">
                                        <entity name=""salesorder"">
                                        <link-entity name=""pricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""listino"">
                                            <link-entity name=""productpricelevel"" from=""pricelevelid"" to=""pricelevelid"" alias=""vocedilistino"">
                                            <attribute name=""amount"" alias=""importo"" />
                                            <link-entity name=""product"" from=""productid"" to=""productid"" alias=""prodotto"">
                                                <link-entity name=""salesorderdetail"" from=""productid"" to=""productid"" alias=""rigaordine"">
                                                <filter>
                                                    <condition attribute=""salesorderdetailid"" operator=""eq"" value=""{targetId}"" uitype=""salesorderdetail"" />
                                                </filter>
                                                </link-entity>
                                            </link-entity>
                                            </link-entity>
                                        </link-entity>
                                        </entity>
                                    </fetch>";

            EntityCollection salesOrderDetailCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchSalesOrderDetail));

            if (salesOrderDetailCollection.Entities.Count > 0)
            {
                Entity enSalesOrderDetail = salesOrderDetailCollection.Entities[0];

                if (enSalesOrderDetail != null)
                {

                    Entity salesOrderDetailToUpdate = new Entity(DataModel.salesorderdetail.logicalName, targetId);

                    decimal prezzounitario = enSalesOrderDetail.GetAttributeValue<AliasedValue>("importo")?.Value is Money importo ? importo.Value : 0m;

                    decimal quantità = target.GetAttributeValue<decimal?>(salesorderdetail.quantity) ?? 0m;
                    decimal manualdiscountamount = target.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount)?.Value ?? 0m;

                    //calcolo l'importo
                    baseamount = prezzounitario * quantità;

                    //calcolo il totale imponibile
                    taxableamount = baseamount - manualdiscountamount;

                    //valorizzo il campo totale imponibile
                    salesOrderDetailToUpdate[salesorderdetail.res_taxableamount] = taxableamount;
                    crmServiceProvider.Service.Update(salesOrderDetailToUpdate);
                }
            }
            #endregion
        }
    }
}

