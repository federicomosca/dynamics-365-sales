using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using RSMNG.TAUMEDIKA.DataModel;
using System.Runtime.Serialization;
using static RSMNG.TAUMEDIKA.Model;
using RSMNG.Plugins;
using RSMNG.TAUMEDIKA.Shared.Quote.Model;
using static RSMNG.TAUMEDIKA.Shared.AgentCommission.Model;

namespace RSMNG.TAUMEDIKA.Shared.AgentCommission
{
    public class Utility
    {
        public static List<string> mandatoryFields = new List<string> {
                res_agentcommission.ownerid,
                res_agentcommission.res_commissionid
            };
        public static string GetName(IOrganizationService service, Guid entityId)
        {
            string ret = string.Empty;
            if (entityId.Equals(Guid.Empty))
            {
                return ret;
            }
            Entity enEntity = service.Retrieve(DataModel.res_agentcommission.logicalName, entityId, new ColumnSet(new string[] { DataModel.res_agentcommission.res_agentcommissionid, DataModel.res_agentcommission.res_name }));
            if (enEntity.Attributes.Contains(DataModel.res_agentcommission.res_name) && enEntity.Attributes[DataModel.res_agentcommission.res_name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.res_agentcommission.res_name);
            }

            return ret;
        }
        public static string AgentCommissionCalculation(ITracingService tracingService, IOrganizationService service, string jsonDataInput)
        {
            string actionMsg = string.Empty;
            string jsonDataOutput = string.Empty;
            Model.AgentCommissionCalculationOutput agentCommissionCalculationOutput = new Model.AgentCommissionCalculationOutput() { result = 0, message = "Ok Calcolo effettuato con successo." };
            try
            {
                #region Deserializzo il json in input
                actionMsg = "Deserializzo il json in input";
                tracingService.Trace(jsonDataInput);
                Model.AgentCommissionCalculationInput agentCommissionCalculationInput = Controller.Deserialize<Model.AgentCommissionCalculationInput>(jsonDataInput, typeof(Model.AgentCommissionCalculationInput));
                #endregion

                #region Aggiorno i documenti e Cancello le provvigioni agente della provvigione
                if (agentCommissionCalculationInput.DeleteAgentCommission)
                {
                    actionMsg = "Aggiorno i documenti togliendo la lookup provvigione agente e azzero la provvigione";
                    var fetchDataD = new
                    {
                        res_commissionid = agentCommissionCalculationInput.CommissionId
                    };
                    var fetchXmlD = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                    <fetch>
                      <entity name=""{res_document.logicalName}"">
                        <link-entity name=""{res_agentcommission.logicalName}"" from=""{res_document.res_agentcommissionid}"" to=""{res_agentcommission.res_agentcommissionid}"">
                          <filter>
                            <condition attribute=""{res_agentcommission.res_commissionid}"" operator=""eq"" value=""{fetchDataD.res_commissionid}"" />
                          </filter>
                        </link-entity>
                      </entity>
                    </fetch>";
                    List<Entity> lDocumentDelete = service.RetrieveAll(fetchXmlD);
                    foreach (Entity entity in lDocumentDelete)
                    {
                        entity[res_document.res_calculatedcommission] = null;
                        entity[res_document.res_agentcommissionid] = null;
                        service.Update(entity);
                    }

                    actionMsg = "Cancello le Provvigioni agenti legate alla provvigione";
                    var fetchDataAC = new
                    {
                        res_commissionid = agentCommissionCalculationInput.CommissionId
                    };
                    var fetchXmlAC = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                    <fetch>
                      <entity name=""{res_agentcommission.logicalName}"">
                        <filter>
                          <condition attribute=""{res_agentcommission.res_commissionid}"" operator=""eq"" value=""{fetchDataAC.res_commissionid}"" />
                        </filter>
                      </entity>
                    </fetch>";
                    List<Entity> lAgentCommission = service.RetrieveAll(fetchXmlAC);
                    foreach (Entity entity in lAgentCommission)
                    {
                        service.Delete(entity.LogicalName, entity.Id);
                    }

                    actionMsg = "Avvio il calcolo delle provvigioni cambiando di stato alla provvigione";
                    Helper.SetStateCode(service, res_commission.logicalName, new Guid(agentCommissionCalculationInput.CommissionId), (int)res_commission.statecodeValues.Attivo, (int)res_commission.statuscodeValues.Calcoloincorso_StateAttivo);
                }
                #endregion

                #region Prelevo le date della provvigione
                actionMsg = "Prelevo le date della provvigione";
                Entity enCommission = service.Retrieve(res_commission.logicalName, new Guid(agentCommissionCalculationInput.CommissionId), new ColumnSet(new string[] { res_commission.res_startdate, res_commission.res_enddate }));
                string startDate = enCommission.ContainsAttributeNotNull(res_commission.res_startdate) ? enCommission.GetAttributeValue<DateTime>(res_commission.res_startdate).ToString("yyyy-MM-dd") : DateTime.MinValue.ToString("yyyy-MM-dd");
                string endDate = enCommission.ContainsAttributeNotNull(res_commission.res_enddate) ? enCommission.GetAttributeValue<DateTime>(res_commission.res_enddate).ToString("yyyy-MM-dd") : string.Empty;
                if (string.IsNullOrEmpty(endDate))
                {
                    throw new Exception("La data di fine della provvigione deve essere obbligatoria.");
                }
                #endregion

                #region Prelevo il codice dell'agente
                actionMsg = "Prelevo il codice dell'agente";
                Entity enAgente = service.Retrieve(systemuser.logicalName, new Guid(agentCommissionCalculationInput.AgentId), new ColumnSet(new string[] { systemuser.res_agentnumber, systemuser.res_commissionpercentage }));
                string agentNumber = enAgente.ContainsAttributeNotNull(systemuser.res_agentnumber) ? enAgente.GetAttributeValue<string>(systemuser.res_agentnumber) : string.Empty;
                decimal commissionPercentage = enAgente.ContainsAttributeNotNull(systemuser.res_commissionpercentage) ? enAgente.GetAttributeValue<decimal>(systemuser.res_commissionpercentage) : 0;
                if (string.IsNullOrEmpty(agentNumber))
                {
                    throw new Exception("Il codice dell'Agente deve esser obbligatorio.");
                }
                #endregion

                #region Prelevo i documenti in base all'agente in esame
                actionMsg = "Prelevo i documenti in base all'agente in esame";
                var fetchData = new
                {
                    res_ispendingpayment = "0",
                    res_agent = agentNumber,
                    res_isexcludedfromcalculation = "0",
                    res_startdate = startDate,
                    res_enddate = endDate
                };
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                <fetch>
                  <entity name=""{res_document.logicalName}"">
                    <attribute name=""{res_document.res_nettotalexcludingvat}"" />
                    <filter>
                      <condition attribute=""{res_document.res_ispendingpayment}"" operator=""eq"" value=""{fetchData.res_ispendingpayment}"" />
                      <condition attribute=""{res_document.res_agent}"" operator=""eq"" value=""{fetchData.res_agent}"" />
                      <condition attribute=""{res_document.res_lastpaymentdate}"" operator=""between"">
                        <value>{fetchData.res_startdate}</value>
                        <value>{fetchData.res_enddate}</value>
                      </condition>
                      <condition attribute=""{res_document.res_agentcommissionid}"" operator=""null"" />
                      <condition attribute=""{res_document.res_isexcludedfromcalculation}"" operator=""eq"" value=""{fetchData.res_isexcludedfromcalculation}"" />
                    </filter>
                  </entity>
                </fetch>";
                List<Entity> lDocument = service.RetrieveAll(fetchXml);
                #endregion

                #region Creo la provvigione agente
                actionMsg = "Creo la provvigione agente";
                Entity enAgentCommission = new Entity(res_agentcommission.logicalName);
                enAgentCommission.Attributes.Add(res_agentcommission.res_agentid, enAgente.ToEntityReference());
                enAgentCommission.Attributes.Add(res_agentcommission.ownerid, enAgente.ToEntityReference());
                enAgentCommission.Attributes.Add(res_agentcommission.res_commissionid, new EntityReference(res_commission.logicalName, new Guid(agentCommissionCalculationInput.CommissionId)));
                enAgentCommission.Attributes.Add(res_agentcommission.statecode, new OptionSetValue((int)res_agentcommission.statecodeValues.Attivo));
                enAgentCommission.Attributes.Add(res_agentcommission.statuscode, new OptionSetValue((int)res_agentcommission.statuscodeValues.Calcolato_StateAttivo));
                Guid enAgentCommissionId = service.Create(enAgentCommission);
                #endregion

                #region Aggiorno i documenti associando la provvigione agente e avviando di fatto il calcolo della provvigione
                actionMsg = "Aggiorno i documenti associando la provvigione agente e avviando di fatto il calcolo della provvigione";
                foreach (Entity enDocument in lDocument)
                {
                    decimal netTotalExcludingVat = enDocument.ContainsAttributeNotNull(res_document.res_nettotalexcludingvat) ? enDocument.GetAttributeValue<Money>(res_document.res_nettotalexcludingvat).Value : 0;

                    Entity enDocumentUpt = new Entity(enDocument.LogicalName, enDocument.Id);
                    enDocumentUpt.Attributes.Add(res_document.res_agentcommissionid, new EntityReference(res_agentcommission.logicalName, enAgentCommissionId));
                    enDocumentUpt.Attributes.Add(res_document.res_calculatedcommission, new Money((netTotalExcludingVat * commissionPercentage) / 100));
                    service.Update(enDocumentUpt);
                }
                #endregion
            }
            catch (Exception ex)
            {
                agentCommissionCalculationOutput.result = 2;
                agentCommissionCalculationOutput.message = $"{actionMsg} - {ex.Message}";
            }
            finally
            {
                jsonDataOutput = RSMNG.Plugins.Controller.Serialize<Model.AgentCommissionCalculationOutput>(agentCommissionCalculationOutput, typeof(Model.AgentCommissionCalculationOutput));
            }
            return jsonDataOutput;
        }
        public static void UpdateTotalCommission(IOrganizationService service, Guid entityId)
        {
            var fetchData = new
            {
                res_agentcommissionid = entityId
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch aggregate=""true"">
                <entity name=""{res_document.logicalName}"">
                <attribute name=""{res_document.res_calculatedcommission}"" alias=""res_calculatedcommission"" aggregate=""sum"" />
                <attribute name=""{res_document.res_nettotalexcludingvat}"" alias=""res_nettotalexcludingvat"" aggregate=""sum"" />
                <filter>
                    <condition attribute=""res_agentcommissionid"" operator=""eq"" value=""{fetchData.res_agentcommissionid}"" />
                </filter>
                </entity>
            </fetch>";
            List<Entity> lDocument = service.RetrieveAll(fetchXml);
            if (lDocument.Count > 0)
            {
                Entity enDocument = lDocument[0];
                Entity enAgentCommission = new Entity(res_agentcommission.logicalName, entityId);
                enAgentCommission.Attributes.Add(res_agentcommission.res_soldtotalamount, enDocument.GetAliasedValue<Money>(res_document.res_nettotalexcludingvat));
                decimal res_calculatedcommission = enDocument.GetAliasedValue<Money>(res_document.res_calculatedcommission).Value;
                enAgentCommission.Attributes.Add(res_agentcommission.res_calculatedcommission, res_calculatedcommission < 0 ? new Money(0) : enDocument.GetAliasedValue<Money>(res_document.res_calculatedcommission));
                service.Update(enAgentCommission);
            }
        }

    }
    public class Model
    {
        [DataContract]
        public class AgentCommissionCalculationInput
        {
            [DataMember] public bool DeleteAgentCommission { get; set; }
            [DataMember] public string CommissionId { get; set; }
            [DataMember] public string AgentId { get; set; }
            [DataMember] public bool LastAgent { get; set; }
        }

        [DataContract]
        public class AgentCommissionCalculationOutput : TAUMEDIKA.Model.BasicOutput
        {

        }
    }
}
