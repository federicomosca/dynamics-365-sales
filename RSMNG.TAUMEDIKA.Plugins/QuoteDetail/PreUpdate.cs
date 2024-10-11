using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.QuoteDetail
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            #region Trace Activation Method
            bool isFirstExecute = true;
            void Trace(string key, object value)
            {
                bool isTraceActive = true;
                if (isFirstExecute)
                {
                    crmServiceProvider.TracingService.Trace($"TRACE IS ACTIVE: {isTraceActive}");

                    isFirstExecute = false;
                }
                if (isTraceActive) crmServiceProvider.TracingService.Trace($"{key.ToUpper()}: {value.ToString()}");
            }
            #endregion

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            Guid targetId = target.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Codice Articolo
            PluginRegion = "Valorizzo il campo Codice Articolo";

            postImage.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);

            if (erProduct == null) throw new ApplicationException("Product entity reference not found");

            Entity prodotto = crmServiceProvider.Service.Retrieve(product.logicalName, erProduct.Id, new ColumnSet(product.productnumber));
            prodotto.TryGetAttributeValue<string>(product.productnumber, out string productNumber);

            target[quotedetail.res_itemcode] = productNumber != null ? productNumber : string.Empty;
            #endregion

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA";

            var fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""{product.logicalName}"">
                                        <filter>
                                          <condition attribute=""{product.statecode}"" operator=""eq"" value=""{product.statecodeValues.Attivo}"" />
                                          <condition attribute=""{product.productid}"" operator=""eq"" value=""{erProduct.Id}"" />
                                        </filter>
                                        <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""CodiceIVA"">
                                          <attribute name=""{res_vatnumber.res_vatnumberid}"" alias=""CodiceIVAGuid"" />
                                          <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                        </link-entity>
                                      </entity>
                                    </fetch>";

            Trace("fetch", fetchProdotto);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

            if (collection.Entities.Count > 0)
            {
                prodotto = collection.Entities[0];

                //---------------------------- Riga Offerta ----------------------------//

                //dalla fetch
                Guid codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                decimal codiceIvaAliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m; Trace("aliquota_iva", codiceIvaAliquota);

                //dal target
                decimal prezzoUnitario = postImage.GetAttributeValue<Money>(quotedetail.baseamount)?.Value ?? 0m; Trace("prezzo_unitario", prezzoUnitario);
                decimal quantità = postImage.GetAttributeValue<decimal>(quotedetail.quantity); Trace("quantità", quantità);
                decimal scontoTotale = postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m; Trace("sconto_totale", scontoTotale);

                if (codiceIvaGuid == Guid.Empty) throw new ApplicationException("Vat Number not found");

                EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceIvaGuid);

                //calcolo l'importo [riga offerta]
                decimal importo = prezzoUnitario * quantità; Trace("importo", importo);

                //calcolo il totale imponibile [riga offerta]
                decimal totaleImponibile = importo - scontoTotale; Trace("totale_imponibile", totaleImponibile);

                //calcolo il totale iva [riga offerta]
                decimal totaleIva = totaleImponibile * (codiceIvaAliquota / 100); Trace("totale_iva", totaleIva);

                //aggiorno i campi di riga offerta
                target[quotedetail.res_vatnumberid] = erCodiceIVA;
                target[quotedetail.res_vatrate] = codiceIvaAliquota;
                target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
                target[quotedetail.tax] = new Money(totaleIva);
            }
            #endregion
        }
    }
}

