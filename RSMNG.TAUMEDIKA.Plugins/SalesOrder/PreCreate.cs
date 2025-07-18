﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.SalesOrder
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.salesorder.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, RSMNG.TAUMEDIKA.Shared.SalesOrder.Utility.mandatoryFields);
            #endregion

            #region Popolo in automatico il Destinatario
            string destination = string.Empty;
            if (target.ContainsAttributeNotNull(salesorder.res_shippingreference))
            {
                destination = target.GetAttributeValue<string>(salesorder.res_shippingreference);
            }
            if (string.IsNullOrEmpty(destination) && target.ContainsAttributeNotNull(salesorder.customerid))
            {
                destination = Shared.Account.Utility.GetName(crmServiceProvider.Service, target.GetAttributeValue<EntityReference>(salesorder.customerid).Id);
            }
            target.AddWithRemove(salesorder.res_recipient, destination);
            #endregion

            #region Valorizzo il campo Nome
            PluginRegion = "Valorizzo il campo Nome";

            string nomeCliente = string.Empty;

            if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"target n. ordine: {target.GetAttributeValue<string>(salesorder.ordernumber)}");
            string nOrdine = target.ContainsAttributeNotNull(salesorder.ordernumber) ? target.GetAttributeValue<string>(salesorder.ordernumber) : string.Empty;

            //recupero il nome cliente dalla lookup polimorfica
            EntityReference erCliente = target.GetAttributeValue<EntityReference>(salesorder.customerid) ?? null;

            if (erCliente != null)
            {
                bool isAccount = erCliente.LogicalName == account.logicalName;

                if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"customer is account? {isAccount}");

                //columnset relativo alla natura della lookup polimorfica
                ColumnSet columnSetCliente = isAccount ? new ColumnSet(account.name) : new ColumnSet(contact.fullname);
                Entity cliente = crmServiceProvider.Service.Retrieve(isAccount ? account.logicalName : contact.logicalName, erCliente.Id, columnSetCliente);

                if (cliente != null)
                {
                    nomeCliente = cliente.ContainsAttributeNotNull(isAccount ? account.name : contact.fullname) ?
                        cliente.GetAttributeValue<string>(isAccount ? account.name : contact.fullname) : string.Empty;
                }
            }

            string nomeOrdine = !string.IsNullOrEmpty(nOrdine) ? nOrdine + " - " + nomeCliente : nomeCliente;

            target[salesorder.name] = nomeOrdine;
            #endregion

            #region Valorizzo il campo Nazione (testo)
            PluginRegion = "Valorizzo il campo Nazione (testo)";
            if (target.Contains(salesorder.res_countryid))
            {
                target.TryGetAttributeValue<EntityReference>(DataModel.salesorder.res_countryid, out EntityReference erCountry);
                string countryName = erCountry != null ? RSMNG.TAUMEDIKA.Shared.Country.Utility.GetName(crmServiceProvider.Service, erCountry.Id) : string.Empty;

                target[contact.address1_country] = countryName;
            }
            #endregion

            #region Imposto il motivo stato su Approvato
            PluginRegion = "Imposto il motivo stato su Approvato";

            bool isFromQuote = false;

            IPluginExecutionContext context = crmServiceProvider.PluginContext.ParentContext;
            IPluginExecutionContext parentContext = context?.ParentContext;
            if (parentContext != null)
            {
                isFromQuote = parentContext.MessageName == "ConvertQuoteToSalesOrder";
                if (isFromQuote)
                {
                    if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"Ordine generato dall'offerta? {isFromQuote}");
                    target[salesorder.statuscode] = new OptionSetValue((int)salesorder.statuscodeValues.Approvato_StateAttivo);
                }
            }
            else { if (PluginActiveTrace) crmServiceProvider.TracingService.Trace($"Ordine generato dall'offerta? {isFromQuote}"); }
            #endregion
        }
    }
}

