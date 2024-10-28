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
            PluginActiveTrace = true;
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
                DataModel.quote.name,                       //nome
                DataModel.quote.transactioncurrencyid,      //valuta
                DataModel.quote.pricelevelid,               //listino prezzi [en]
                DataModel.quote.res_date,                   //data
                DataModel.quote.res_isinvoicerequested,     //richiesta fattura
                "res_paymenttermid",                        //condizioni di pagamento
                DataModel.quote.res_deposit,                //acconto
                DataModel.quote.res_vatnumberid,            //spesa accessoria
                DataModel.quote.res_additionalexpenseid,    //codice iva spesa accessoria
                DataModel.quote.willcall,                   //spedizione (flag)
                DataModel.quote.res_shippingreference,
                DataModel.quote.shipto_line1,
                DataModel.quote.shipto_postalcode,
                DataModel.quote.shipto_city,
                DataModel.quote.res_location,
                DataModel.quote.shipto_stateorprovince,
                DataModel.quote.res_countryid,
                DataModel.quote.totallineitemamount,        //totale prodotti
                DataModel.quote.totalamountlessfreight,     //totale imponibile
                DataModel.quote.freightamount,              //importo spesa accessoria
                DataModel.quote.totaltax,                   //totale iva
                DataModel.quote.totalamount,                //importo totale
                DataModel.quote.totaldiscountamount,        //scontototale
                DataModel.quote.opportunityid,              //opportunità
                DataModel.quote.customerid,                 //potenziale cliente
                DataModel.quote.description,                //descrizione
                DataModel.quote.res_internalusecomment      //commento uso interno
                );

            Entity enSalesOrder = new Entity(DataModel.salesorder.logicalName);

            foreach (string column in quoteColumnSet.Columns)
            {
                if (target.ContainsAttributeNotNull(column))
                {
                    var attribute = target.GetAttributeValue<object>(column) ?? null;
                    if (PluginActiveTrace) { crmServiceProvider.TracingService.Trace($"{column}: {attribute}"); }
                    enSalesOrder[column] = attribute;
                }
            }

            //lookup
            enSalesOrder[DataModel.salesorder.quoteid] = erQuote;

            //stato
            enSalesOrder[DataModel.salesorder.statecode] = new OptionSetValue((int)DataModel.salesorder.statecodeValues.Attivo);
            enSalesOrder[DataModel.salesorder.statuscode] = new OptionSetValue((int)DataModel.salesorder.statuscodeValues.Approvato_StateAttivo);

            crmServiceProvider.Service.Create(enSalesOrder);
            #endregion
        }
    }
}

