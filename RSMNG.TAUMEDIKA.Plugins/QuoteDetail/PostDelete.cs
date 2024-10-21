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
    public class PostDelete : RSMNG.BaseClass
    {
        public PostDelete(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Delete";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];

            //----------------------------------< CAMPI OFFERTA DA AGGIORNARE >----------------------------------//
            decimal parentImportoTotale = 0;                                    //totale imponibile + totale iva

            //-------------------------------------< CAMPI PER IL CALCOLO >--------------------------------------//
            decimal parentTotaleImponibile;                                     // totale prodotti + importo spesa accessoria
            decimal parentTotaleIva;                                            // S totale iva righe + iva calcolata su spesa accessoria

            decimal parentTotaleProdotti;                                       // S totale imponibile righe
            decimal parentImportoSpesaAccessoria;
            decimal parentAliquota;
            decimal parentIvaSpesaAccessoria;                                   // importo spesa accessoria * (aliquota/100)

            decimal detailsTotaleIVA;                                           // aggregato

            //--------------------------------------< CALCOLO DEI CAMPI >---------------------------------------//

            EntityReference erParent = preImage.GetAttributeValue<EntityReference>(quotedetail.quoteid) ?? null;
            if (PluginActiveTrace) crmServiceProvider.TracingService.Trace("Parent entity reference is not null", erParent != null);

            if (erParent != null)
            {
                Guid quoteId = erParent.Id;
                var fetchAggregatoRighe = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch aggregate=""true"">
                                      <entity name=""{quotedetail.logicalName}"">
                                        <attribute name=""{quotedetail.res_taxableamount}"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""{quotedetail.tax}"" alias=""TotaleIva"" aggregate=""sum"" />
                                        <filter>
                                          <condition attribute=""{quotedetail.quoteid}"" operator=""eq"" value=""{quoteId}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";
                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace(fetchAggregatoRighe);
                EntityCollection aggregatoRigheCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAggregatoRighe));

                if (aggregatoRigheCollection.Entities.Count > 0)
                {
                    Entity aggregato = aggregatoRigheCollection.Entities[0];

                    parentTotaleProdotti = aggregato.GetAliasedValue<Money>("TotaleImponibile")?.Value is decimal taxableamount ? taxableamount : 0;
                    detailsTotaleIVA = aggregato.GetAliasedValue<Money>("TotaleIva")?.Value is decimal totaltax ? totaltax : 0;

                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentTotaleProdotti {parentTotaleProdotti}");
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"detailsTotaleIVA {detailsTotaleIVA}");

                    var fetchSpesaAccessoria = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""{quote.logicalName}"">
                                    <attribute name=""{quote.freightamount}"" alias=""ImportoSpesaAccessoria"" />
                                    <filter>
                                      <condition attribute=""{quote.quoteid}"" operator=""eq"" value=""{quoteId}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""IVA"">
                                      <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace(fetchSpesaAccessoria);
                    EntityCollection spesaAccessoriaCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchSpesaAccessoria));

                    if (spesaAccessoriaCollection.Entities.Count > 0)
                    {
                        Entity spesaAccessoria = spesaAccessoriaCollection.Entities[0];

                        parentImportoSpesaAccessoria = spesaAccessoria.GetAliasedValue<Money>("ImportoSpesaAccessoria")?.Value is decimal freightamount ? freightamount : 0;
                        parentAliquota = spesaAccessoria.GetAttributeValue<AliasedValue>("Aliquota")?.Value is decimal rate ? rate : 0;

                        parentIvaSpesaAccessoria = parentImportoSpesaAccessoria * (parentAliquota / 100);
                        parentTotaleIva = detailsTotaleIVA + parentIvaSpesaAccessoria;

                        parentTotaleImponibile = parentTotaleProdotti + parentIvaSpesaAccessoria;
                        parentImportoTotale = parentTotaleImponibile + parentTotaleIva;

                        if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentImportoSpesaAccessoria {parentImportoSpesaAccessoria}");
                        if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentAliquota {parentAliquota}");
                        if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentIvaSpesaAccessoria {parentIvaSpesaAccessoria}");
                        if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentTotaleIva {parentTotaleIva}");
                        if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentTotaleImponibile {parentTotaleImponibile}");
                        if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"parentImportoTotale {parentImportoTotale}");
                    }
                }
                //----------------------------------------< AGGIORNAMENTO >-----------------------------------------//

                Entity enParent = new Entity(quote.logicalName, quoteId);
                enParent[quote.totalamount] = parentImportoTotale != 0 ? new Money(parentImportoTotale) : null;

                crmServiceProvider.Service.Update(enParent);
            }
        }
    }
}

