using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Document
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = res_document.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);


            #region Effettuo il calcolo della provvigione
            PluginRegion = "Effettuo il calcolo della provvigione";
            if (target.ContainsAttributeNotNull(res_document.res_agentcommissionid))
            {
                if (target.NotContainsAttributeOrNull(res_document.res_calculatedcommission)
                    && postImage.ContainsAttributeNotNull(res_document.res_agent)
                    && postImage.ContainsAttributeNotNull(res_document.res_nettotalexcludingvat))
                {
                    decimal calculatedCommission = 0;
                    PluginRegion = "Prendo la percentuale di commissione da applicare";
                    decimal? commissionPercentage = Shared.SystemUser.Utility.GetCommissionPercentage(crmServiceProvider.Service, postImage.GetAttributeValue<string>(res_document.res_agent));
                    if (!commissionPercentage.HasValue)
                    {
                        throw new ApplicationException("La percentuale commissione dell'agente deve essere obbligatoria.");
                    }
                    calculatedCommission = (postImage.GetAttributeValue<Money>(res_document.res_nettotalexcludingvat).Value * commissionPercentage.Value) / 100;
                    target.AddWithRemove(res_document.res_calculatedcommission, new Money(calculatedCommission));
                }
            }
            #endregion

            #region Valorizzo il campo Nome
            PluginRegion = "Valorizzo il campo Nome";

            //campi per valorizzazione del nome
            EntityReference erCliente;
            string codiceCliente;
            string nomeCliente = string.Empty;
            string data;
            decimal totaleDocumento;

            string nome;

            //--------------------< codice cliente >----------------------//
            codiceCliente = target.Contains(res_document.res_customernumber) ?
                target.GetAttributeValue<string>(res_document.res_customernumber) : preImage.GetAttributeValue<string>(res_document.res_customernumber);

            //--------------------< nome cliente >----------------------//
            erCliente = target.Contains(res_document.res_customerid) ?
                target.GetAttributeValue<EntityReference>(res_document.res_customerid) : preImage.GetAttributeValue<EntityReference>(res_document.res_customerid);

            if (erCliente != null)
            {
                if (erCliente.LogicalName == contact.logicalName)
                {
                    Entity cliente = crmServiceProvider.Service.Retrieve(erCliente.LogicalName, erCliente.Id, new ColumnSet(DataModel.contact.fullname));
                    nomeCliente = cliente.GetAttributeValue<string>(contact.fullname) ?? string.Empty;
                }
                if (erCliente.LogicalName == DataModel.account.logicalName)
                {
                    Entity cliente = crmServiceProvider.Service.Retrieve(erCliente.LogicalName, erCliente.Id, new ColumnSet(DataModel.account.name));
                    nomeCliente = cliente.GetAttributeValue<string>(account.name) ?? string.Empty;
                }
            }

            //--------------------< data >----------------------//
            data = target.Contains(res_document.res_date) ?
                target.GetAttributeValue<string>(res_document.res_date) : preImage.GetAttributeValue<string>(res_document.res_date);

            //--------------------< totale documento >----------------------//
            totaleDocumento = target.Contains(res_document.res_documenttotal) ?
                target.GetAttributeValue<Money>(res_document.res_documenttotal).Value : preImage.GetAttributeValue<Money>(res_document.res_documenttotal).Value;

            //--------------------< valorizzo il campo nome >----------------------//
            nome = $"{codiceCliente} - {nomeCliente} - {data} - {totaleDocumento}";
            target[res_document.res_nome] = nome;
            #endregion

        }
    }
}
