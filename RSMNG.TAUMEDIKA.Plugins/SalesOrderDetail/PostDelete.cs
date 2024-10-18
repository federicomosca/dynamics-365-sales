using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrderDetails
{
    public class PostDelete : RSMNG.BaseClass
    {
        public PostDelete(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Delete";
            PluginPrimaryEntityName = salesorderdetail.logicalName;
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

            EntityReference erParent = preImage.GetAttributeValue<EntityReference>(salesorderdetail.salesorderid) ?? null;
            crmServiceProvider.TracingService.Trace("Parent entity reference is not null", erParent != null ? true : false);

            if (erParent != null)
            {
                Guid parentId = erParent.Id;
                var fetchAggregatoRighe = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                    <fetch aggregate=""true"">
                                      <entity name=""{salesorderdetail.logicalName}"">
                                        <attribute name=""{salesorderdetail.res_taxableamount}"" alias=""TotaleImponibile"" aggregate=""sum"" />
                                        <attribute name=""{salesorderdetail.tax}"" alias=""TotaleIva"" aggregate=""sum"" />
                                        <filter>
                                          <condition attribute=""{salesorderdetail.salesorderid}"" operator=""eq"" value=""{parentId}"" />
                                        </filter>
                                      </entity>
                                    </fetch>";
                crmServiceProvider.TracingService.Trace("fetch_Aggregato_Righe", fetchAggregatoRighe);
                EntityCollection aggregatoRigheCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchAggregatoRighe));

                if (aggregatoRigheCollection.Entities.Count > 0)
                {
                    Entity aggregato = aggregatoRigheCollection.Entities[0];

                    parentTotaleProdotti = aggregato.GetAliasedValue<Money>("TotaleImponibile")?.Value is decimal taxableamount ? taxableamount : 0;
                    detailsTotaleIVA = aggregato.GetAliasedValue<Money>("TotaleIva")?.Value is decimal totaltax ? totaltax : 0;

                    crmServiceProvider.TracingService.Trace("parentTotaleProdotti", parentTotaleProdotti);
                    crmServiceProvider.TracingService.Trace("detailsTotaleIVA ", detailsTotaleIVA);

                    var fetchSpesaAccessoria = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                  <entity name=""{salesorder.logicalName}"">
                                    <attribute name=""{salesorder.freightamount}"" alias=""ImportoSpesaAccessoria"" />
                                    <filter>
                                      <condition attribute=""{salesorder.salesorderid}"" operator=""eq"" value=""{parentId}"" />
                                    </filter>
                                    <link-entity name=""{res_vatnumber.logicalName}"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""IVA"">
                                      <attribute name=""{res_vatnumber.res_rate}"" alias=""Aliquota"" />
                                    </link-entity>
                                  </entity>
                                </fetch>";
                    crmServiceProvider.TracingService.Trace("fetch_Spesa_Accessoria", fetchSpesaAccessoria);
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

                        crmServiceProvider.TracingService.Trace("parentImportoSpesaAccessoria", parentImportoSpesaAccessoria);
                        crmServiceProvider.TracingService.Trace("parentAliquota ", parentAliquota);
                        crmServiceProvider.TracingService.Trace("parentIvaSpesaAccessoria", parentIvaSpesaAccessoria);
                        crmServiceProvider.TracingService.Trace("parentTotaleIva ", parentTotaleIva);
                        crmServiceProvider.TracingService.Trace("parentTotaleImponibile", parentTotaleImponibile);
                        crmServiceProvider.TracingService.Trace("parentImportoTotale", parentImportoTotale);
                    }
                }
                //----------------------------------------< AGGIORNAMENTO >-----------------------------------------//

                Entity enParent = new Entity(salesorder.logicalName, parentId);
                enParent[salesorder.totalamount] = parentImportoTotale != 0 ? new Money(parentImportoTotale) : null;

                crmServiceProvider.Service.Update(enParent);
            }
        }
    }
}

