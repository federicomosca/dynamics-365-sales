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
    public class PostUpdate : RSMNG.BaseClass
    {
        public PostUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Update";
            PluginPrimaryEntityName = salesorderdetail.logicalName;
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

                EntityReference erSalesOrder = target.Contains(salesorderdetail.salesorderid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid) : preImage.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid);

                if (target.Contains(salesorderdetail.tax) || target.Contains(salesorderdetail.manualdiscountamount) || target.Contains(salesorderdetail.res_taxableamount))
                {
                    var fetchData = new
                    {
                        id = erSalesOrder.Id
                    };
                    var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch aggregate=""true"">
                                      <entity name=""salesorderdetail"">
                                        <attribute name=""manualdiscountamount"" alias=""ScontoTotale"" aggregate=""sum"" />
                                        <attribute name=""res_taxableamount"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""tax"" alias=""TotaleIva"" aggregate=""sum"" />
                                        <filter>
                                          <condition attribute=""quoteid"" operator=""eq"" value=""{fetchData.id}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";

                    EntityCollection results = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                    if (results.Entities.Count > 0)
                    {

                        decimal scontoTotale = results.Entities[0].ContainsAliasNotNull("ScontoTotale") ? results.Entities[0].GetAliasedValue<Money>("ScontoTotale").Value : 0;
                        decimal totaleImponibile = results.Entities[0].ContainsAliasNotNull("TotaleImponibile") ? results.Entities[0].GetAliasedValue<Money>("TotaleImponibile").Value : 0;
                        decimal totaleIva = results.Entities[0].ContainsAliasNotNull("TotaleIva") ? results.Entities[0].GetAliasedValue<Money>("TotaleIva").Value : 0;

                        Entity enSalesOrder = new Entity(salesorder.logicalName, erSalesOrder.Id);

                        enSalesOrder[quote.totallineitemamount] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                        enSalesOrder[quote.totaldiscountamount] = scontoTotale != 0 ? new Money(scontoTotale) : null;
                        enSalesOrder[quote.totaltax] = totaleIva != 0 ? new Money(totaleIva) : null;

                        crmServiceProvider.Service.Update(enSalesOrder);


                    }

                }

            }


        }
    }
}

