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
    public class PostCreate : RSMNG.BaseClass
    {
        public PostCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Create";
            PluginPrimaryEntityName = quotedetail.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Valorizzo i campi Codice IVA e Aliquota IVA
            PluginRegion = "Valorizzo i campi Codice IVA e Aliquota IVA";
            Guid targetId = target.Id;

            var fetchCodiceIVA = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                                <fetch>
                                    <entity name=""quotedetail"">
                                    <filter>
                                        <condition attribute=""quotedetailid"" operator=""eq"" value=""{targetId}"" />
                                    </filter>
                                    <link-entity name=""product"" from=""productid"" to=""productid"" alias=""product"">
                                        <link-entity name=""res_vatnumber"" from=""res_vatnumberid"" to=""res_vatnumberid"" alias=""codiceivalookup"">
                                        <attribute name=""res_rate"" alias=""aliquota"" />
                                        <attribute name=""res_vatnumberid"" alias=""codiceiva"" />
                                        </link-entity>
                                    </link-entity>
                                    </entity>
                                </fetch>";

            crmServiceProvider.TracingService.Trace(fetchCodiceIVA);
            EntityCollection collection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchCodiceIVA));

            if (collection.Entities.Count > 0)
            {
                Entity quotedetail = collection.Entities[0];

                Guid codiceiva = quotedetail.Contains("codiceiva") ? (Guid)quotedetail.GetAttributeValue<AliasedValue>("codiceiva").Value : Guid.Empty;
                Decimal? aliquota = quotedetail.Contains("aliquota") ? (Decimal?)quotedetail.GetAttributeValue<AliasedValue>("aliquota").Value : null;
                if (codiceiva != null && aliquota.HasValue)
                {
                    EntityReference erCodiceIVA = new EntityReference(res_vatnumber.logicalName, codiceiva);

                    target[DataModel.quotedetail.res_vatnumberid] = erCodiceIVA;
                    target[DataModel.quotedetail.res_vatrate] = aliquota;
                    crmServiceProvider.Service.Update(target);
                }
                else throw new ApplicationException("Codice IVA non trovato");
            }
        }
        #endregion
    }
}

