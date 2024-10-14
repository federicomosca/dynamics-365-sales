using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Quote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Web;
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
            var trace = crmServiceProvider.TracingService;

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);


            // Ricalcolo necessario per aggiornare i campi che non sono nativamente aggiornati in automatico dal sistema
            // Alla creazione di una riga importo risulta essere 0, poi il sistema effettua calcoli automatici
            // e aggiorna il valore di importo effettuando un update.
            if (target.Contains(salesorderdetail.productid))
            {
                Guid codiceIvaGuid = Guid.Empty;
                decimal importo = 0;
                decimal totaleImponibile = 0;
                decimal totaleIva = 0;
                

                decimal prezzoUnitario = postImage.GetAttributeValue<Money>(salesorderdetail.baseamount)?.Value ?? 0m;
                decimal quantità = postImage.GetAttributeValue<decimal?>(salesorderdetail.quantity) ?? 0m;
                decimal scontoTotale = postImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount)?.Value ?? 0m;
                decimal aliquota = postImage.GetAttributeValue<decimal?>(salesorderdetail.res_vatnumberid) ?? 0m;
                string productNumber = postImage.GetAttributeValue<string>(salesorderdetail.productnumber);
                

                EntityReference erProduct = target.Contains(salesorderdetail.productid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.productid) : preImage.GetAttributeValue<EntityReference>(salesorderdetail.productid);

                #region Recupera dati product
                PluginRegion = "Recupera dati product";

                if (erProduct != null)
                {
                    // preso solo i valori che non sono presi nativamente.
                    // Al cambio del prodotto altri valori sono presi nativamente dal sistema e sono presenti nel target
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

                    EntityCollection results = service.RetrieveMultiple(new FetchExpression(fetchProdotto));

                    if (results.Entities.Count > 0)
                    {
                        codiceIvaGuid = results.Entities[0].GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                        aliquota = results.Entities[0].GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m;
                        productNumber = results.Entities[0].GetAttributeValue<string>(product.productnumber);

                    }                    
                }
                #endregion

                importo = prezzoUnitario * quantità;
                totaleImponibile = importo - scontoTotale;
                totaleIva = totaleImponibile * (aliquota / 100);


                target[salesorderdetail.res_vatnumberid] = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;
                target[salesorderdetail.res_taxableamount] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
                target[salesorderdetail.tax] = totaleIva != 0 ? new Money(totaleIva) : null;
                target[salesorderdetail.res_itemcode] = productNumber;
                
                if (aliquota != 0) {
                    target[salesorderdetail.res_vatrate] = aliquota; 
                } else
                {
                    target[salesorderdetail.res_vatrate] = null;
                }

            }
        }
    }
}

