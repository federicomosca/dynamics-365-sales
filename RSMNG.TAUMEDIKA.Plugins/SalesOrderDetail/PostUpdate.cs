using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.SalesOrderDetail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            Guid targetId = target.Id;

            var trace = crmServiceProvider.TracingService;

            if (target.Contains(salesorderdetail.res_taxableamount))
            {

                EntityReference enSalesOrder = target.Contains(salesorderdetail.salesorderid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid) : preImage.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid);

                if (enSalesOrder != null)
                {
                    // viene sovrascritto dalla logica nativa
                    //Utility.SetSalesOrder(crmServiceProvider.Service, trace, target, enSalesOrder.Id);
                }

            }

            #region Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale dell'Offerta correlata
            PluginRegion = "Aggiorno i campi Totale righe, Sconto totale, Totale imponibile, Totale IVA, Importo totale dell'Offerta correlata";

            /**
             * fetch di tutti i campi interessati nel calcolo di tutte le salesorderdetail
             * associate al salesorder che si deve aggiornare
             * 
             * in particolare recupero le seguenti entità correlate all'ordine:
             * salesorder detail > sconto totale, totale imponibile, totale iva
             * spesa accessoria > importo
             * codice IVA spesa accessoria > aliquota
             */
            var fetchSalesOrder = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                        <fetch aggregate=""true"">
                                          <entity name=""salesorder"">
                                            <attribute name=""salesorderid"" alias=""salesorderid"" groupby=""true"" />
                                            <link-entity name=""salesorderdetail"" from=""salesorderid"" to=""salesorderid"" link-type=""inner"" alias=""salesorderdetail"">
                                              <attribute name=""manualdiscountamount"" alias=""scontototale"" aggregate=""sum"" />
                                              <attribute name=""res_taxableamount"" alias=""totaleimponibile"" aggregate=""sum"" />
                                              <attribute name=""tax"" alias=""totaleiva"" aggregate=""sum"" />
                                            </link-entity>
                                            <link-entity name=""res_additionalexpense"" from=""res_additionalexpenseid"" to=""res_additionalexpenseid"" alias=""additionalexpense"">
                                              <attribute name=""res_amount"" alias=""importo"" groupby=""true"" />
                                            </link-entity>
                                            <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""vatnumber"">
                                              <attribute name=""res_rate"" alias=""aliquota"" groupby=""true"" />
                                            </link-entity>
                                          </entity>
                                        </fetch>";
            crmServiceProvider.TracingService.Trace(fetchSalesOrder);
            EntityCollection aggregateCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchSalesOrder));

            if (aggregateCollection.Entities.Count > 0)
            {
                //offerta da aggiornare
                Entity aggregate = aggregateCollection.Entities[0];

                /**
                 * campi dell'offerta che verranno aggiornati
                 */
                decimal totallineitemamount = 0m,    //totale righe        somma del totale imponibile di tutte le salesorderdetail
                    totaldiscountamount = 0m,        //sconto totale       somma dello sconto totale di tutte le salesorderdetail
                    totalamountlessfreight = 0m,     //totale imponibile   totale righe - sconto totale
                    totaltax = 0m,                   //totale iva          somma del totale iva di tutte le salesorderdetail + iva calcolata su importo spesa accessoria
                    totalamount = 0m;                //importo totale      totale imponibile + totale iva

                if (aggregate != null)
                {
                    //id dell'offerta da aggiornare
                    Guid salesorderid = aggregate.GetAttributeValue<AliasedValue>("salesorderid")?.Value as Guid? ?? Guid.Empty;

                    if (salesorderid != Guid.Empty)
                    {
                        //creo l'offerta da aggiornare
                        Entity salesorder = new Entity(DataModel.salesorder.logicalName, salesorderid);

                        //spesa accessoria e aliquota per il calcolo del totale iva
                        decimal spesaAccessoria = aggregate.GetAttributeValue<AliasedValue>("importo")?.Value is Money importo ? importo.Value : 0m;

                        decimal codiceIvaSpesaAccessoria = aggregate.GetAttributeValue<AliasedValue>("aliquota")?.Value is Money aliquota ? aliquota.Value : 0m;

                        /**
                         * inizio a valorizzare totale iva con l'aliquota applicata alla spesa accessoria
                         * a questo risultato andrà poi aggiunta la somma del totale iva di tutte le righe offerta
                         */
                        totaltax = spesaAccessoria * (codiceIvaSpesaAccessoria / 100);

                        //recupero totale imponibile, sconto totale e totale iva di tutte le righe di dettaglio
                        decimal aggrTotaleImponibile = aggregate.GetAttributeValue<AliasedValue>("totaleimponibile")?.Value is Money totaleimponibile ? totaleimponibile.Value : 0m;
                        decimal aggrScontoTotale = aggregate.GetAttributeValue<AliasedValue>("scontototale")?.Value is Money scontototale ? scontototale.Value : 0m;
                        decimal aggrTotaleIva = aggregate.GetAttributeValue<AliasedValue>("totaleiva")?.Value is Money totaleiva ? totaleiva.Value : 0m;

                        //calcolo il totale imponibile e l'importo totale
                        totalamountlessfreight = totallineitemamount - totaldiscountamount;
                        totalamount = totalamountlessfreight + totaltax;

                        salesorder[DataModel.salesorder.totallineitemamount] = new Money(aggrTotaleImponibile);
                        salesorder[DataModel.salesorder.totaldiscountamount] = new Money(aggrScontoTotale);
                        salesorder[DataModel.salesorder.totaltax] = new Money(aggrTotaleIva);
                        salesorder[DataModel.salesorder.totalamountlessfreight] = new Money(totalamountlessfreight);
                        salesorder[DataModel.salesorder.totalamount] = new Money(totalamount);

                        crmServiceProvider.Service.Update(salesorder);
                    }
                    else { throw new ApplicationException("SalesOrder ID is missing"); }
                }
            }
            else
            {
                throw new ApplicationException("Cannot find salesorder details");
            }
            #endregion
        }
    }
}

