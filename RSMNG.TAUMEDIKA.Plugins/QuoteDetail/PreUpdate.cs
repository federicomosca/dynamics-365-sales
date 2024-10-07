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
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            Guid targetId = target.Id;

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, TAUMEDIKA.Shared.QuoteDetail.Utility.mandatoryFields);
            #endregion

            #region Valorizzo il campo Codice Articolo
            PluginRegion = "Aggiorno il campo Codice Articolo";

            //product -> product number
            string productNumber = string.Empty;

            target.TryGetAttributeValue<EntityReference>(quotedetail.productid, out EntityReference erProduct);
            if (erProduct != null)
            {
                Entity product = crmServiceProvider.Service.Retrieve(DataModel.product.logicalName, erProduct.Id, new ColumnSet(DataModel.product.productnumber));
                if (product != null) { product.TryGetAttributeValue<string>(DataModel.product.productnumber, out productNumber); }
            }
            if (productNumber != string.Empty)
            {
                target[quotedetail.res_itemcode] = productNumber;
            }
            #endregion

            #region Valorizzo il campo Totale imponibile
            PluginRegion = "Valorizzo il campo Totale imponibile";

            //totale imponibile = importo - sconto totale
            decimal taxableamount = 0m;     //totale imponibile
            decimal baseamount = 0m;        //importo

            /**
             * recupero l'importo per sottrarvi il totale sconto
             * e calcolare il totale imponibile
             */
            var fetchQuoteDetail = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                            <fetch>
                              <entity name=""quotedetail"">
                                <attribute name=""baseamount"" alias=""importo"" />
                                <filter>
                                  <condition attribute=""quotedetailid"" operator=""eq"" value=""{targetId}"" />
                                </filter>
                              </entity>
                            </fetch>";

            EntityCollection quoteDetailCollection = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchQuoteDetail));

            if (quoteDetailCollection.Entities.Count > 0)
            {
                Entity enQuoteDetail = quoteDetailCollection.Entities[0];

                if (enQuoteDetail != null)
                {
                    baseamount = enQuoteDetail.GetAttributeValue<AliasedValue>("importo")?.Value is Money importo ? importo.Value : 0m;

                    decimal manualdiscountamount = target.GetAttributeValue<Money>(quotedetail.manualdiscountamount)?.Value ?? 0m;

                    //calcolo il totale imponibile
                    taxableamount = baseamount - manualdiscountamount;

                    //valorizzo il campo totale imponibile
                    target[quotedetail.res_taxableamount] = new Money(taxableamount);
                }
            }
            #endregion
        }
    }
}

