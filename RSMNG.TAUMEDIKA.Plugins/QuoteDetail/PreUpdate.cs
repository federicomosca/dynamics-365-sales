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
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            //traccio quali attributi vengono modificati alla "creazione" della riga
            foreach (var attribute in target.Attributes)
            {
                string column = attribute.Key;
                crmServiceProvider.TracingService.Trace(column, column);
            }

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA";

            postImage.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);
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
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace("fetch", fetchProdotto);

                EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));
                if (collection.Entities.Count > 0)
                {
                    Entity prodotto = collection.Entities[0];

                    #region Valorizzo Codice Articolo
                    PluginRegion = "Valorizzo Codice Articolo";

                    string codiceArticolo = prodotto.GetAttributeValue<AliasedValue>("CodiceArticolo")?.Value is string productNumber ? productNumber : null;
                    target[quotedetail.res_itemcode] = codiceArticolo;
                    #endregion

                    //dalla fetch
                    Guid codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIVAGuid")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                    decimal codiceIvaAliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m;
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"aliquota_iva {codiceIvaAliquota}");


                    //dal target
                    decimal importo = postImage.GetAttributeValue<Money>(quotedetail.baseamount)?.Value ?? 0m;
                    decimal quantità = postImage.GetAttributeValue<decimal>(quotedetail.quantity);
                    decimal scontoTotale = postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m;

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"prezzo_unitario {importo}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"quantità {quantità}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"sconto_totale {scontoTotale}");


                    if (codiceIvaGuid != Guid.Empty)
                    {
                        EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceIvaGuid);

                        //calcolo il totale imponibile [riga offerta]
                        decimal totaleImponibile = importo - scontoTotale;

                        //calcolo il totale iva [riga offerta]
                        decimal totaleIva = totaleImponibile * (codiceIvaAliquota / 100);

                        if(PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_imponibile {totaleImponibile}");
                        if(PluginActiveTrace) crmServiceProvider.TracingService.Trace($"totale_iva {totaleIva}");

                        //aggiorno i campi di riga offerta
                        target[quotedetail.res_vatnumberid] = erCodiceIVA;
                        target[quotedetail.res_vatrate] = codiceIvaAliquota;
                        target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
                        target[quotedetail.tax] = new Money(totaleIva);
                    }
                }
            }
            #endregion
        }
    }
}

