using Microsoft.Xrm.Sdk;
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

        }
    }
}
