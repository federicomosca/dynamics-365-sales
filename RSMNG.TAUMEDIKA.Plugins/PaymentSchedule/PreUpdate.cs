using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.PaymentSchedule
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = res_paymentschedule.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            crmServiceProvider.TracingService.Trace("Trace attivo.");

            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);

            #region Re-imposta nome
            PluginRegion = "Re-imposta nome";


            // Codice Cliente - Cliente - Data - Importo

            if (target.Contains(res_paymentschedule.res_clientid) ||
                target.Contains(res_paymentschedule.res_customernumber) ||
                target.Contains(res_paymentschedule.res_date) ||
                target.Contains(res_paymentschedule.res_amount))
            {
                string nome = string.Empty;
                string cliente = null;
                EntityReference erClient = postImage.GetAttributeValue<EntityReference>(res_paymentschedule.res_clientid);
                if (erClient != null)
                {
                    Entity enClient = crmServiceProvider.Service.Retrieve(erClient.LogicalName, erClient.Id, new ColumnSet(erClient.LogicalName == contact.logicalName ? contact.fullname : account.name));
                    cliente = enClient.GetAttributeValue<string>(erClient.LogicalName == contact.logicalName ? contact.fullname : account.name);
                }

                string data = postImage.ContainsAttributeNotNull(res_paymentschedule.res_date) ? postImage.GetAttributeValue<DateTime>(res_paymentschedule.res_date).ToString("dd/MM/yyyy") : null;
                string codiceCliente = postImage.GetAttributeValue<string>(res_paymentschedule.res_customernumber);
                string importo = postImage.ContainsAttributeNotNull(res_paymentschedule.res_amount) ? (postImage.GetAttributeValue<Money>(res_paymentschedule.res_amount).Value).ToString("F2") : "0";

                nome = !string.IsNullOrEmpty(codiceCliente) ? codiceCliente + " - " : null;
                nome += !string.IsNullOrEmpty(cliente) ? cliente : null;
                nome += !string.IsNullOrEmpty(data) ? " - " + data : null;
                nome += !string.IsNullOrEmpty(importo) ? " - " + importo : null;

                target[res_paymentschedule.res_nome] = nome;
            }


            #endregion
        }
    }
}

