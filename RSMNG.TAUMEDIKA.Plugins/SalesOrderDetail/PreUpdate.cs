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

            #region Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Codice Articolo
            PluginRegion = "Valorizzo i campi Codice IVA, Aliquota IVA, Totale IVA e Codice Articolo";

            bool omaggio = target.ContainsAttributeNotNull(salesorderdetail.res_ishomage) ? target.GetAttributeValue<bool>(salesorderdetail.res_ishomage) : false;


            EntityReference codiceIva = null;
            decimal? aliquota = null;
            decimal scontoTotale;
            decimal importo;
            decimal totaleImponibile = 0;
            decimal totaleIva = 0;
            decimal importoTotale = 0;


            if (target.Contains(salesorderdetail.res_vatnumberid) ||
                target.Contains(salesorderdetail.quantity) ||
                target.Contains(salesorderdetail.manualdiscountamount) ||
                target.Contains(salesorderdetail.priceperunit)
                )
            {
                if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"Codice IVA è stato selezionato dall'utente"); }

                if (target.Contains(salesorderdetail.res_vatnumberid))
                {
                    codiceIva = target.ContainsAttributeNotNull(salesorderdetail.res_vatnumberid) ? target.GetAttributeValue<EntityReference>(salesorderdetail.res_vatnumberid) : null;
                }
                else
                {
                    codiceIva = preImage.GetAttributeValue<EntityReference>(salesorderdetail.res_vatnumberid) ?? null;
                }
                Entity enCodiceIva = codiceIva != null ? crmServiceProvider.Service.Retrieve(res_vatnumber.logicalName, codiceIva.Id, new ColumnSet(res_vatnumber.res_rate)) : null;

                aliquota = enCodiceIva?.GetAttributeValue<decimal>(res_vatnumber.res_rate) ?? null;
                scontoTotale = postImage.ContainsAttributeNotNull(salesorderdetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount).Value : 0;
                importo = postImage.ContainsAttributeNotNull(salesorderdetail.baseamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.baseamount).Value : 0;

                
                totaleImponibile = omaggio ? 0 : importo - scontoTotale;
                totaleIva = omaggio ? 0 : (totaleImponibile * (aliquota == null ? 1 : aliquota.Value)) / 100;
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
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace(fetchProdotto);
                    EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchProdotto));

                    if (collection?.Entities?.Count > 0)
                    {
                        if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"La fetch ha prodotto risultati"); }
                        Entity prodotto = collection.Entities[0];

                        #region Valorizzo Codice articolo
                        PluginRegion = "Valorizzo Codice articolo";
                        string codiceArticolo = prodotto.ContainsAttributeNotNull(product.productnumber) ? prodotto.GetAttributeValue<string>(product.productnumber) : null;
                        target[salesorderdetail.res_itemcode] = codiceArticolo;
                        #endregion


                        codiceIva = prodotto.ContainsAttributeNotNull(product.res_vatnumberid) ? prodotto.GetAttributeValue<EntityReference>(product.res_vatnumberid) : null;

                        aliquota = prodotto.ContainsAliasNotNull($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") ? (decimal)prodotto.GetAliasedValue<decimal>($"{res_vatnumber.logicalName}.{res_vatnumber.res_rate}") : 0m;
                        
                        scontoTotale = postImage.ContainsAttributeNotNull(salesorderdetail.manualdiscountamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.manualdiscountamount).Value : 0;
                        importo = postImage.ContainsAttributeNotNull(salesorderdetail.baseamount) ? postImage.GetAttributeValue<Money>(salesorderdetail.baseamount).Value : 0;

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
            
            target[salesorderdetail.res_vatnumberid] = codiceIva;
            target[salesorderdetail.res_vatrate] = aliquota;
            target[salesorderdetail.res_taxableamount] = new Money(totaleImponibile);
            target[salesorderdetail.tax] = new Money(totaleIva);
            target[salesorderdetail.extendedamount] = new Money(importoTotale);
            
            
            #endregion

        }
    }
}

