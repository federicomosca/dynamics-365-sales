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

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"I campi obbligatori sono stati verificati"); }
            #endregion

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Codice Articolo
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Codice Articolo";

            bool omaggio = target.ContainsAttributeNotNull(quotedetail.res_ishomage) ? target.GetAttributeValue<bool>(quotedetail.res_ishomage) : false;

            EntityReference codiceIva = null;
            decimal aliquota = 0;
            decimal scontoTotale;
            decimal importo;
            decimal totaleImponibile = 0;
            decimal totaleIva = 0;
            decimal importoTotale = 0;


            if (target.ContainsAttributeNotNull(quotedetail.res_vatnumberid) ||
                target.Contains(quotedetail.quantity) ||
                target.Contains(quotedetail.manualdiscountamount) ||
                target.Contains(quotedetail.priceperunit)
                )
            {
                if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Codice IVA è stato selezionato dall'utente"); }

                codiceIva = target.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid) ?? null;

                Entity enCodiceIva = codiceIva != null ? crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, codiceIva.Id, new ColumnSet(res_vatnumber.res_rate)) : null;

                aliquota = enCodiceIva?.GetAttributeValue<decimal>(res_vatnumber.res_rate) ?? 0;
                scontoTotale = postImage.ContainsAttributeNotNull(quotedetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                importo = postImage.ContainsAttributeNotNull(quotedetail.baseamount) ? postImage.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;

                totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                totaleIva = omaggio ? 0 : (totaleImponibile * aliquota) / 100;
                importoTotale = totaleImponibile + totaleIva;
            }
            else
            {
                if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Codice IVA non è stato selezionato dall'utente"); }
                //se il codice iva non è stato selezionato dall'utente, lo recupero dal prodotto correlato
                postImage.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);

                if (erProduct != null)
                {
                    if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Il prodotto è stato selezionato"); }

                    var fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                            <fetch>
                                                <entity name=""{product.logicalName}"">
                                                <attribute name=""{product.productnumber}"" />
                                                <attribute name=""{product.res_vatnumberid}"" alias=""CodiceIva"" />
                                                <filter>
                                                    <condition attribute=""{product.productid}"" operator=""eq"" value=""{erProduct.Id}"" />
                                                    <condition attribute=""{product.statecode}"" operator=""eq"" value=""{product.statecodeValues.Attivo}"" />
                                                </filter>
                                                </entity>
                                            </fetch>";
                    if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace(fetchProdotto); }

                    EntityCollection collectionProdotti = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

                    if (collectionProdotti.Entities.Count > 0)
                    {
                        if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"La fetch ha prodotto risultati"); }
                        Entity prodotto = collectionProdotti.Entities[0];

                        #region Valorizzo Codice articolo
                        PluginRegion = "Valorizzo Codice articolo";
                        string codiceArticolo = prodotto.GetAttributeValue<AliasedValue>("CodiceArticolo")?.Value is string productNumber ? productNumber : null;
                        target[quotedetail.res_itemcode] = codiceArticolo;
                        #endregion

                        Guid codiceIvaGuid = prodotto.GetAttributeValue<AliasedValue>("CodiceIva")?.Value is Guid vatnumberid ? vatnumberid : Guid.Empty;
                        codiceIva = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;

                        /**
                         * eseguo una seconda chiamata al server per recuperare il codice iva correlato al prodotto
                         * ho tentato di fare una sola chiamata con link-entity ma riuscivo ad avere come risultati
                         * soltanto prodotti con codice iva o, alternativamente, soltanto prodotti senza codice iva,
                         * mai entrambi i casi (ho tentato link-type=outer ma non restituiva il risultato sperato)
                         */

                        var fetchCodiceIva = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                                <fetch>
                                                  <entity name=""{res_vatnumber.logicalName}"">
                                                    <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                                    <filter>
                                                      <condition attribute=""{res_vatnumber.res_vatnumberid}"" operator=""eq"" value=""{codiceIvaGuid}"" />
                                                    </filter>
                                                  </entity>
                                                </fetch>";

                        EntityCollection collectionCodiceIva = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchCodiceIva));

                        if (collectionCodiceIva.Entities.Count > 0)
                        {
                            Entity enCodiceIva = collectionCodiceIva.Entities[0]; 

                            aliquota = enCodiceIva.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0;
                        } 

                        scontoTotale = postImage.ContainsAttributeNotNull(quotedetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                        importo = postImage.ContainsAttributeNotNull(quotedetail.baseamount) ? postImage.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;

                        totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                        totaleIva = omaggio ? 0 : (totaleImponibile * aliquota) / 100;
                        importoTotale = totaleImponibile + totaleIva;

                        if (PluginActiveTrace)
                        {
                            crmServiceProvider.TracingService.Trace($"aliquota: {aliquota}");
                            crmServiceProvider.TracingService.Trace($"scontoTotale: {scontoTotale}");
                            crmServiceProvider.TracingService.Trace($"importo: {importo}");
                            crmServiceProvider.TracingService.Trace($"totaleImponibile: {totaleImponibile}");
                            crmServiceProvider.TracingService.Trace($"totaleIva: {totaleIva}");
                            crmServiceProvider.TracingService.Trace($"importoTotale: {importoTotale}");
                        }
                    }
                }
                target[quotedetail.res_vatnumberid] = codiceIva;
                target[quotedetail.res_vatrate] = aliquota;
                target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
                target[quotedetail.tax] = new Money(totaleIva);
                target[quotedetail.extendedamount] = new Money(importoTotale);
            }
            #endregion

            #region Gestisco il campo Prezzo unitario modificato da Canvas App
            PluginRegion = "Gestisco il campo Prezzo unitario modificato da Canvas App";

            bool isFromCanvas = target.ContainsAttributeNotNull("res_isfromcanvas") && target.GetAttributeValue<bool>("res_isfromcanvas");

            if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"From Canvas? {isFromCanvas}"); }

            if (isFromCanvas)
            {
                decimal preImagePriceperunit = preImage.ContainsAttributeNotNull(quotedetail.priceperunit) ? preImage.GetAttributeValue<Money>(quotedetail.priceperunit).Value : 0;
                decimal preImageBaseamount = preImage.ContainsAttributeNotNull(quotedetail.baseamount) ? preImage.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;

                if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"PreImage Prezzo Unitario: {preImagePriceperunit}, PreImage Importo: {preImageBaseamount}"); }

                target[quotedetail.priceperunit] = new Money(preImagePriceperunit);
                target[quotedetail.baseamount] = new Money(preImageBaseamount);
            }
            #endregion
        }
    }
}

