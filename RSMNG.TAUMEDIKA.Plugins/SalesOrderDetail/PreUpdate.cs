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
            #region Trace Activation Method
            bool isFirstExecute = true;
            void Trace(string key, object value)
            {
                //TRACE TOGGLE
                bool isTraceActive = false;

                if (isFirstExecute)
                {
                    if (isTraceActive)
                    {
                        crmServiceProvider.TracingService.Trace($"TRACE IS ACTIVE: {isTraceActive}");
                        isFirstExecute = false;
                    }
                }
                if (isTraceActive)
                {
                    key = string.Concat(key.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToUpper();
                    value = value.ToString();
                    crmServiceProvider.TracingService.Trace($"{key}: {value}");
                }
            }
            #endregion

            var service = crmServiceProvider.Service;

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);


            // Ricalcolo necessario per aggiornare i campi che non sono nativamente aggiornati in automatico dal sistema
            // Alla creazione di una riga importo risulta essere 0, poi il sistema effettua calcoli automatici
            // e aggiorna il valore di importo effettuando un update.
            Guid codiceIvaGuid = Guid.Empty;
            decimal totaleImponibile;
            decimal? totaleIva;
            Trace("01", 01);
            decimal importo = postImage.GetAttributeValue<Money>(salesorderdetail.baseamount)?.Value ?? 0m;
            Trace("02", 02);
            decimal scontoTotale = postImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount)?.Value ?? 0m;
            Trace("03", 03);
            decimal? aliquota = postImage.GetAttributeValue<decimal?>(salesorderdetail.res_vatrate) ?? 0m;
            Trace("031", 031);
            string productNumber = postImage.GetAttributeValue<string>(salesorderdetail.productnumber);


            EntityReference erProduct = target.Contains(salesorderdetail.productid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.productid) : preImage.GetAttributeValue<EntityReference>(salesorderdetail.productid);

            #region Recupera dati product
            PluginRegion = "Recupera dati product";
            //if (target.Contains(salesorderdetail.productid)) qui non entra anche se modifico il prodotto dal modulo della riga ordine
            //{

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
                    Entity prodotto = results.Entities[0];
                    Trace("041", 041);
                    codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                    Trace("04", 04);
                    aliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m; Trace("aliquota", aliquota);
                    Trace("05", 05);
                    productNumber = prodotto.GetAttributeValue<string>(product.productnumber);

                    //creo qui entityreference di codice iva prima di passarla al target
                }
            }

            //}
            #endregion

            totaleImponibile = importo - scontoTotale; Trace("totale_imponibile", totaleImponibile);
            totaleIva = totaleImponibile * (aliquota / 100); Trace("totale_iva", totaleIva);

            EntityReference erCodiceIVA = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;
            Trace("06", 06);
            target[salesorderdetail.res_vatnumberid] = erCodiceIVA;
            target[salesorderdetail.res_taxableamount] = totaleImponibile != 0 ? new Money(totaleImponibile) : null;
            target[salesorderdetail.tax] = totaleIva != 0 ? new Money((decimal)totaleIva) : null;
            target[salesorderdetail.res_itemcode] = productNumber;
            target[salesorderdetail.res_vatrate] = aliquota != 0 ? aliquota : null;
        }
    }
}

