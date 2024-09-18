using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Crm.Sdk.Messages;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA.Shared.PriceLevel
{
    public class Utility
    {
        public static void CheckDefaultForAgents(IOrganizationService service)
        {
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

            EntityCollection ec = service.RetrieveMultiple(new FetchExpression(fetchXml));

            if (ec.Entities.Count > 0)
            {
                throw new ApplicationException("Solo un listino prezzi per volta può avere 'Default per agenti' impostato a si");
            }


        }
    }
}
