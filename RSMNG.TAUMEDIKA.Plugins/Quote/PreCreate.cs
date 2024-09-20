using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.Shared.Quote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Quote
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = DataModel.quote.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";

            VerifyMandatoryField(crmServiceProvider, Utility.mandatoryFields);
            #endregion

            #region Valorizzazione automatica del campo Importo spesa accessoria
            PluginRegion = "Valorizzazione automatica del campo Importo spesa accessoria"; 
            Money amount = null;
            target.TryGetAttributeValue<EntityReference>(DataModel.quote.res_additionalexpenseid, out EntityReference erAdditionalExpense);
            if (erAdditionalExpense != null)
            {
                Entity enAdditionalExpense = crmServiceProvider.Service.Retrieve(
                    DataModel.res_additionalexpense.logicalName,
                    erAdditionalExpense.Id,
                    new ColumnSet(DataModel.res_additionalexpense.res_amount));

                amount = enAdditionalExpense != null && enAdditionalExpense.Contains(DataModel.res_additionalexpense.res_amount) ?
                    enAdditionalExpense.GetAttributeValue<Money>(DataModel.res_additionalexpense.res_amount) : null;

                target[DataModel.quote.freightamount] = amount;
            }
            #endregion
        }
    }
}

