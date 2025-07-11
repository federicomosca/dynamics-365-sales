﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Runtime.Remoting.Services;
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

            bool omaggio = target.ContainsAttributeNotNull(quotedetail.res_ishomage) && target.GetAttributeValue<bool>(quotedetail.res_ishomage);

            EntityReference codiceIva = null;
            decimal? aliquota = null;
            decimal scontoTotale;
            decimal importo;
            decimal totaleImponibile = 0;
            decimal totaleIva = 0;
            decimal importoTotale = 0;

            if (PluginActiveTrace)
            {
                crmServiceProvider.TracingService.Trace($"target contains codice iva: {target.Contains(quotedetail.res_vatnumberid)}");
                crmServiceProvider.TracingService.Trace($"preimage contains codice iva: {preImage.Contains(quotedetail.res_vatnumberid)}");
                if (target.Contains(quotedetail.res_vatnumberid)) crmServiceProvider.TracingService.Trace($"target codice iva: {target.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid).Name ?? null}");
                if (preImage.Contains(quotedetail.res_vatnumberid)) crmServiceProvider.TracingService.Trace($"preimage codice iva: {preImage.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid).Name ?? null}");
                crmServiceProvider.TracingService.Trace($"target contains quantità: {target.Contains(quotedetail.quantity)}");
                crmServiceProvider.TracingService.Trace($"target contains sconto totale: {target.Contains(quotedetail.manualdiscountamount)}");
                crmServiceProvider.TracingService.Trace($"target contains prezzo unitario: {target.Contains(quotedetail.priceperunit)}");
            }

            if (target.Contains(quotedetail.res_vatnumberid) ||
                target.Contains(quotedetail.quantity) ||
                target.Contains(quotedetail.manualdiscountamount) ||
                target.Contains(quotedetail.priceperunit) ||
                (preImage.Contains(quotedetail.res_isfromcanvas) && preImage.Contains(quotedetail.res_vatnumberid))
                )
            {
                if (PluginActiveTrace)
                {
                    crmServiceProvider.TracingService.Trace($"Codice IVA è stato selezionato dall'utente");
                }

                if (target.Contains(quotedetail.res_vatnumberid))
                {
                    codiceIva = target.ContainsAttributeNotNull(quotedetail.res_vatnumberid) ? target.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid) : null;
                }
                else if (preImage.ContainsAttributeNotNull(quotedetail.res_vatnumberid))
                {
                    codiceIva = preImage.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid) ?? null;
                }
                Entity enCodiceIva = codiceIva != null ? crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, codiceIva.Id, new ColumnSet(res_vatnumber.res_rate)) : null;

                aliquota = enCodiceIva?.GetAttributeValue<decimal>(res_vatnumber.res_rate) ?? null;
                scontoTotale = postImage.ContainsAttributeNotNull(quotedetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                importo = postImage.ContainsAttributeNotNull(quotedetail.baseamount) ? postImage.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;

                totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                totaleIva = omaggio ? 0 : (totaleImponibile * (aliquota == null ? 1 : aliquota.Value)) / 100;
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

                    string fetchProdotto = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch>
                                      <entity name=""{product.logicalName}"">
                                        <attribute name=""{product.productnumber}"" />
                                        <attribute name=""{product.res_vatnumberid}"" />
                                        <filter>
                                          <condition attribute=""{product.statecode}"" operator=""in""><value>{(int)product.statecodeValues.Attivo}</value><value>{(int)product.statecodeValues.Inaggiornamento}</value></condition>"" />
                                          <condition attribute=""{product.productid}"" operator=""eq"" value=""{erProduct.Id}"" />
                                        </filter>
                                        <link-entity name=""{res_vatnumber.logicalName}"" from=""{res_vatnumber.res_vatnumberid}"" to=""{res_vatnumber.res_vatnumberid}"" alias=""{res_vatnumber.logicalName}"" link-type=""outer"">
                                          <attribute name=""{res_vatnumber.res_rate}"" />
                                        </link-entity>
                                      </entity>
                                    </fetch>";
                    if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace(fetchProdotto); }

                    EntityCollection collectionProdotti = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

                    if (collectionProdotti?.Entities?.Count > 0)
                    {
                        if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"La fetch ha prodotto risultati"); }
                        Entity prodotto = collectionProdotti.Entities[0];

                        #region Valorizzo Codice articolo
                        PluginRegion = "Valorizzo Codice articolo";
                        string codiceArticolo = prodotto.ContainsAttributeNotNull(product.productnumber) ? prodotto.GetAttributeValue<string>(product.productnumber) : null;
                        target[quotedetail.res_itemcode] = codiceArticolo;
                        #endregion

                        codiceIva = prodotto.ContainsAttributeNotNull(product.res_vatnumberid) ? prodotto.GetAttributeValue<EntityReference>(product.res_vatnumberid) : null;

                        aliquota = prodotto.ContainsAliasNotNull($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") ? (decimal)prodotto.GetAliasedValue<decimal>($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") : 0m;
                        scontoTotale = postImage.ContainsAttributeNotNull(quotedetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(quotedetail.manualdiscountamount).Value : 0;
                        importo = postImage.ContainsAttributeNotNull(quotedetail.baseamount) ? postImage.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;

                        totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                        totaleIva = omaggio ? 0 : (totaleImponibile * (aliquota == null ? 1 : aliquota.Value)) / 100;
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
            }
            target[quotedetail.res_vatnumberid] = codiceIva;
            target[quotedetail.res_vatrate] = aliquota;
            target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
            target[quotedetail.tax] = new Money(totaleIva);
            target[quotedetail.extendedamount] = new Money(importoTotale);
            #endregion

            #region Gestisco il campo Prezzo unitario modificato da Canvas App [DISABLED]
            //PluginRegion = "Gestisco il campo Prezzo unitario modificato da Canvas App";

            //bool isFromCanvas = postImage.ContainsAttributeNotNull("res_isfromcanvas") && postImage.GetAttributeValue<bool>("res_isfromcanvas");

            //if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"From Canvas? {isFromCanvas}"); }

            //if (isFromCanvas)
            //{
            //    EntityReference preImageCodiceIva = preImage.ContainsAttributeNotNull(quotedetail.res_vatnumberid) ? preImage.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid) : null;
            //    decimal preImagePrezzoUnitario = preImage.ContainsAttributeNotNull(quotedetail.priceperunit) ? preImage.GetAttributeValue<Money>(quotedetail.priceperunit).Value : 0;
            //    decimal preImageQuantità = preImage.ContainsAttributeNotNull(quotedetail.quantity) ? preImage.GetAttributeValue<decimal>(quotedetail.quantity) : 0;
            //    decimal baseamount = preImagePrezzoUnitario * preImageQuantità;

            //    Entity enCodiceIva = crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, preImageCodiceIva.Id, new ColumnSet(res_vatnumber.res_rate));
            //    decimal aliquotaIva = enCodiceIva?.GetAttributeValue<decimal>(res_vatnumber.res_rate) ?? 0;
            //    crmServiceProvider.TracingService.Trace($"PreImage Codice IVA: {enCodiceIva?.GetAttributeValue<decimal>(res_vatnumber.res_rate)}");
            //    crmServiceProvider.TracingService.Trace($"PreImage Importo: {baseamount}");

            //    target[quotedetail.res_vatnumberid] = new EntityReference(res_vatnumber.logicalName, preImageCodiceIva.Id);
            //    target[quotedetail.baseamount] = new Money(baseamount);
            //    target[quotedetail.res_vatrate] = aliquotaIva;
            //}
            #endregion
        }
    }
}

