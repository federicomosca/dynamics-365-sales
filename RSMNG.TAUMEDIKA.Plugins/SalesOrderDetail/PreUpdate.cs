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
            PluginPrimaryEntityName = salesorderdetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA";

            bool omaggio = target.ContainsAttributeNotNull(salesorderdetail.res_ishomage) ? target.GetAttributeValue<bool>(salesorderdetail.res_ishomage) : false;


            EntityReference codiceIva = null;
            decimal aliquota = 0;
            decimal scontoTotale;
            decimal importo;
            decimal totaleImponibile = 0;
            decimal totaleIva = 0;
            decimal importoTotale = 0;

            if (target.ContainsAttributeNotNull(salesorderdetail.res_vatnumberid) ||
                target.Contains(salesorderdetail.quantity) ||
                target.Contains(salesorderdetail.manualdiscountamount) ||
                target.Contains(salesorderdetail.priceperunit)
                )
            {
                codiceIva = postImage.GetAttributeValue<EntityReference>(salesorderdetail.res_vatnumberid);

                Entity enCodiceIva = codiceIva != null ? crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, codiceIva.Id, new ColumnSet(res_vatnumber.res_rate)) : null;

                aliquota = enCodiceIva?.GetAttributeValue<decimal>(res_vatnumber.res_rate) ?? 0;
                scontoTotale = postImage.ContainsAttributeNotNull(salesorderdetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount).Value : 0;
                importo = postImage.ContainsAttributeNotNull(salesorderdetail.baseamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.baseamount).Value : 0;

                totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                totaleIva = omaggio ? 0 : (totaleImponibile * aliquota) / 100;
                importoTotale = totaleImponibile + totaleIva;
            }
            else
            {
                //se il codice iva non è stato selezionato dall'utente, lo recupero dal prodotto correlato
                postImage.TryGetAttributeValue<EntityReference>(salesorderdetail.productid, out EntityReference erProduct);

                if (erProduct != null)
                {
                    var fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""{product.logicalName}"">
                                        <attribute name=""{product.productnumber}"" alias=""CodiceArticolo"" />
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

                    EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

                    if (collection.Entities.Count > 0)
                    {
                        Entity prodotto = collection.Entities[0];


                        #region Valorizzo Codice articolo
                        PluginRegion = "Valorizzo Codice articolo";
                        string codiceArticolo = prodotto.GetAttributeValue<AliasedValue>("CodiceArticolo")?.Value is string productNumber ? productNumber : null;
                        target[salesorderdetail.res_itemcode] = codiceArticolo;
                        #endregion

                        //dalla fetch
                        Guid codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                        codiceIva = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;

                        aliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m;
                        scontoTotale = postImage.ContainsAttributeNotNull(salesorderdetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount).Value : 0;
                        importo = postImage.ContainsAttributeNotNull(salesorderdetail.baseamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.baseamount).Value : 0;

                        totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                        totaleIva = omaggio ? 0 : (totaleImponibile * aliquota) / 100;
                        importoTotale = totaleImponibile + totaleIva;
                    }
                }
            }
            target[salesorderdetail.res_vatnumberid] = codiceIva;
            target[salesorderdetail.res_vatrate] = aliquota;
            target[salesorderdetail.res_taxableamount] = new Money(totaleImponibile);
            target[salesorderdetail.tax] = new Money(totaleIva);
            target[salesorderdetail.extendedamount] = new Money(importoTotale);
            #endregion

            #region Recupera dati product [DISABLED]
            //PluginRegion = "Recupera dati product";
            ////if (target.Contains(salesorderdetail.productid)) qui non entra anche se modifico il prodotto dal modulo della riga ordine
            ////{

            //if (erProduct != null)
            //{
            //    // preso solo i valori che non sono presi nativamente.
            //    // Al cambio del prodotto altri valori sono presi nativamente dal sistema e sono presenti nel target
            //    var fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            //                        <fetch>
            //                          <entity name=""{product.logicalName}"">
            //                            <attribute name=""{product.productnumber}"" />
            //                            <filter>
            //                              <condition attribute=""{product.statecode}"" operator=""eq"" value=""{(int)product.statecodeValues.Attivo}"" />
            //                              <condition attribute=""{product.productid}"" operator=""eq"" value=""{erProduct.Id}"" />
            //                            </filter>
            //                            <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIVA"">
            //                              <attribute name=""{res_vatnumber.res_vatnumberid}"" alias=""CodiceIVAGuid"" />
            //                              <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
            //                            </link-entity>
            //                          </entity>
            //                        </fetch>";

            //    EntityCollection results = service.RetrieveMultiple(new FetchExpression(fetchProdotto));

            //    if (results.Entities.Count > 0)
            //    {
            //        Entity prodotto = results.Entities[0];
            //        crmServiceProvider.TracingService.Trace("041", 041);
            //        codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
            //        crmServiceProvider.TracingService.Trace("04", 04);
            //        aliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m; crmServiceProvider.TracingService.Trace("aliquota", aliquota);
            //        crmServiceProvider.TracingService.Trace("05", 05);
            //        productNumber = prodotto.GetAttributeValue<string>(product.productnumber);

            //        //creo qui entityreference di codice iva prima di passarla al target
            //    }
            //}

            ////}
            #endregion

            //totaleImponibile = importo - scontoTotale; crmServiceProvider.TracingService.Trace("totale_imponibile", totaleImponibile);
            //totaleIva = totaleImponibile * (aliquota / 100); crmServiceProvider.TracingService.Trace("totale_iva", totaleIva);

            //EntityReference erCodiceIVA = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;
            //crmServiceProvider.TracingService.Trace("06", 06);
            //target[salesorderdetail.res_vatnumberid] = erCodiceIVA;
            //target[salesorderdetail.res_taxableamount] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
            //target[salesorderdetail.tax] = totaleIva != 0 ? new Money((decimal)totaleIva) : null;
            //target[salesorderdetail.res_itemcode] = productNumber;
            //target[salesorderdetail.res_vatrate] = aliquota != 0 ? aliquota : null;
        }
    }
}

