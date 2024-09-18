using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.PriceLevel
{
    public class PreCreate : RSMNG.BaseClass
    {
        public PreCreate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Create";
            PluginPrimaryEntityName = pricelevel.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];

            #region Controllo univocità DEFAULT PER AGENTI
            PluginRegion = "controllo univocità DEFAULT PER AGENTI";

            bool isDefaultForAgents = target.GetAttributeValue<bool>(pricelevel.res_isdefaultforagents);

            if (isDefaultForAgents) {
                var fetchData = new
                {
                    res_isdefaultforagents = (int)pricelevel.res_isdefaultforagentsValues.Si,
                    statecode = (int)pricelevel.statecodeValues.Attivo
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                    <fetch top=""1"">
                      <entity name=""pricelevel"">
                        <attribute name=""res_isdefaultforagents"" />
                        <filter>
                          <condition attribute=""res_isdefaultforagents"" operator=""eq"" value=""{fetchData.res_isdefaultforagents/*1*/}"" />
                          <condition attribute=""statecode"" operator=""eq"" value=""{fetchData.statecode/*0*/}"" />
                        </filter>
                      </entity>
                    </fetch>";

                EntityCollection ec = crmServiceProvider.Service.RetrieveMultiple(new FetchExpression(fetchXml));

                if(ec.Entities.Count > 0)
                {
                    throw new ApplicationException("Solo un listino prezzi per volta può avere 'Default per agenti' impostato a si");
                }
            }
            #endregion


        }
    }
}
