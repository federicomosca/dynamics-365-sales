using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.Shared.Country;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Quote
{
    public class PostCreate : RSMNG.BaseClass
    {
        public PostCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.POST;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.quote.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Valorizzazione campo data [DISABLED]
            PluginRegion = "Valorizzazione campo data";

            //DateTime rcDate = (DateTime)target[DataModel.quote.createdon];

            //target[DataModel.quote.res_date] = rcDate;

            //crmServiceProvider.Service.Update(target);
            #endregion

            #region Creo l'ordine con Motivo Stato = "Approvato"
            PluginRegion = "Creo l'ordine con Motivo Stato = \"Approvato\"";

            EntityReference erQuote = target.ToEntityReference();

            ColumnSet quoteColumnSet = new ColumnSet(
                DataModel.quote.name,
                DataModel.quote.transactioncurrencyid,
                DataModel.quote.pricelevelid,
                DataModel.quote.res_date,
                DataModel.quote.res_isinvoicerequested,
                "res_paymenttermid",
                DataModel.quote.res_deposit,
                DataModel.quote.res_vatnumberid,
                DataModel.quote.res_additionalexpenseid,
                DataModel.quote.willcall,
                DataModel.quote.res_shippingreference,
                DataModel.quote.shipto_line1,
                DataModel.quote.shipto_postalcode,
                DataModel.quote.shipto_city,
                DataModel.quote.res_location,
                DataModel.quote.shipto_stateorprovince,
                DataModel.quote.res_countryid,
                DataModel.quote.totallineitemamount,
                DataModel.quote.totalamountlessfreight,
                DataModel.quote.freightamount,
                DataModel.quote.totaltax,
                DataModel.quote.totalamount,
                DataModel.quote.totaldiscountamount,
                DataModel.quote.opportunityid,
                DataModel.quote.customerid,
                DataModel.quote.description,
                DataModel.quote.res_internalusecomment
                );

            Entity salesorder = new Entity(DataModel.salesorder.logicalName);

            foreach (string column in quoteColumnSet.Columns)
            {
                if (target.Contains(column) && target.GetAttributeValue<object>(column) != null)
                {
                    salesorder[column] = target.GetAttributeValue<object>(column);
                }
            }

            salesorder[DataModel.salesorder.quoteid] = erQuote;
            salesorder[DataModel.salesorder.statecode] = new OptionSetValue((int)DataModel.salesorder.statecodeValues.Attivo);
            salesorder[DataModel.salesorder.statuscode] = new OptionSetValue((int)DataModel.salesorder.statuscodeValues.Approvato_StateAttivo);

            crmServiceProvider.Service.Create(salesorder);
            #endregion
        }
    }
}

