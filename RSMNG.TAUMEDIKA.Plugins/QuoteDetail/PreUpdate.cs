using Microsoft.Xrm.Sdk;
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
            PluginActiveTrace = true;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            crmServiceProvider.TracingService.Trace("quotedetail pre update");

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"I campi obbligatori sono stati verificati"); }
            #endregion
            crmServiceProvider.TracingService.Trace("01");
            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Codice Articolo
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Codice Articolo";


            ///TEST
            StringBuilder traceMessage = new StringBuilder();
            traceMessage.AppendLine("Attributes received in Target:");

            // Loop through each attribute in the entity
            foreach (var attribute in target.Attributes)
            {
                string attributeName = attribute.Key;
                object attributeValue = attribute.Value;

                // Append each attribute's name and value to the trace message
                traceMessage.AppendLine($"{attributeName}: {attributeValue}");
            }

            // Trace the final message
            crmServiceProvider.TracingService.Trace(traceMessage.ToString());


            //////////













            bool omaggio = target.ContainsAttributeNotNull(quotedetail.res_ishomage) ? target.GetAttributeValue<bool>(quotedetail.res_ishomage) : false;

            EntityReference codiceIva = null;
            decimal aliquota = 0;
            decimal scontoTotale;
            decimal importo;
            decimal totaleImponibile = 0;
            decimal totaleIva = 0;
            decimal importoTotale = 0;


            if (target.Contains(quotedetail.res_vatnumberid) ||
                target.Contains(quotedetail.quantity) ||
                target.Contains(quotedetail.manualdiscountamount) ||
                target.Contains(quotedetail.priceperunit)
                )
            {
                if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Codice IVA è stato selezionato dall'utente"); }

                if (target.Contains(quotedetail.res_vatnumberid))
                {
                    codiceIva = target.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid) ?? null;
                }
                else
                {
                    codiceIva = preImage.GetAttributeValue<EntityReference>(quotedetail.res_vatnumberid) ?? null;
                }
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

                        Guid codiceIvaGuid = prodotto.ContainsAliasNotNull("CodiceIVAGuid") ? prodotto.GetAliasedValue<Guid>("CodiceIVAGuid") : Guid.Empty;
                        codiceIva = codiceIvaGuid != Guid.Empty ? new EntityReference(res_vatnumber.logicalName, codiceIvaGuid) : null;
                        
                        crmServiceProvider.TracingService.Trace("codice iva guid: " + codiceIvaGuid.ToString());
                        string test = codiceIva != null ? "true" : "null";
                        crmServiceProvider.TracingService.Trace($"EntityReference: " + test);

                        aliquota = prodotto.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0m;
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
            }
            target[quotedetail.res_vatnumberid] = codiceIva;
            target[quotedetail.res_vatrate] = aliquota;
            target[quotedetail.res_taxableamount] = new Money(totaleImponibile);
            target[quotedetail.tax] = new Money(totaleIva);
            target[quotedetail.extendedamount] = new Money(importoTotale);
            #endregion
            
            /*
            #region Gestisco il campo Prezzo unitario modificato da Canvas App
            PluginRegion = "Gestisco il campo Prezzo unitario modificato da Canvas App";

            bool isFromCanvas = target.ContainsAttributeNotNull("res_isfromcanvas") && target.GetAttributeValue<bool>("res_isfromcanvas");

            if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"From Canvas? {isFromCanvas}"); }

            if (isFromCanvas)
            {
                decimal preImagePriceperunit = preImage.ContainsAttributeNotNull(quotedetail.priceperunit) ? preImage.GetAttributeValue<Money>(quotedetail.priceperunit).Value : 0;
                decimal preImageBaseamount = preImage.ContainsAttributeNotNull(quotedetail.baseamount) ? preImage.GetAttributeValue<Money>(quotedetail.baseamount).Value : 0;

                if (PluginActiveTrace) {
                    crmServiceProvider.TracingService.Trace($"PreImage Prezzo Unitario: {preImagePriceperunit}, PreImage Importo: {preImageBaseamount}");
                   
                }

                target[quotedetail.priceperunit] = new Money(preImagePriceperunit);
                target[quotedetail.baseamount] = new Money(preImageBaseamount);
                target[quotedetail.res_isfromcanvas] = false;
            }
            #endregion
            */
        }
    }
}

