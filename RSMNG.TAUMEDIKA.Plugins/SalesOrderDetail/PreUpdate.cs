using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Quote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = DataModel.salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            bool isTrace = false;
            var service = crmServiceProvider.Service;
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            
            EntityReference erProduct = target.Contains(salesorderdetail.productid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.productid) : preImage.GetAttributeValue<EntityReference>(salesorderdetail.productid);

            if (erProduct != null) {

                #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA
                PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA";

                var fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""{product.logicalName}"">
                                        <attribute name=""{product.productnumber}"" />
                                        <filter>
                                          <condition attribute=""{product.statecode}"" operator=""eq"" value=""{(int)product.statecodeValues.Attivo}"" />
                                          <condition attribute=""{product.productid}"" operator=""eq"" value=""{erProduct.Id}"" />
                                        </filter>
                                        <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIVA"">
                                          <attribute name=""{res_vatnumber.res_vatnumberid}"" alias=""CodiceIVAGuid"" />
                                          <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                        </link-entity>
                                      </entity>
                                    </fetch>";


                EntityCollection results = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

                if (results.Entities.Count > 0)
                {
                    Entity prodotto = results.Entities[0];


                    Guid codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                    decimal codiceIvaAliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m;

                    decimal prezzoUnitario = postImage.GetAttributeValue<Money>(quotedetail.baseamount)?.Value ?? 0m;
                    decimal quantità = postImage.GetAttributeValue<decimal>(quotedetail.quantity);
                    decimal scontoTotale = postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m;

                    EntityReference erCodiceIVA = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;

                    decimal importo = prezzoUnitario * quantità;
                    decimal totaleImponibile = importo - scontoTotale;
                    decimal totaleIva = totaleImponibile * (codiceIvaAliquota / 100);

                    target[salesorderdetail.res_vatnumberid] = erCodiceIVA;
                    target[salesorderdetail.res_vatrate] = codiceIvaAliquota;
                    target[salesorderdetail.res_taxableamount] = new Money(totaleImponibile);
                    target[salesorderdetail.tax] = new Money(totaleIva);
                    target[salesorderdetail.res_itemcode] = prodotto.GetAttributeValue<string>(product.productnumber);
                }
                #endregion


            }
        }
    }
}

