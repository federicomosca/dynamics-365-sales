using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.PaymentSchedule
{
    public class Utility
    {
        public static string DeleteAllPaymentSchedule(IOrganizationService service)
        {
            Model.BasicOutput basicOutput = new Model.BasicOutput() { result = 0, message = "Ok, cancellazione effettuata." };
            string jsonDataOutput = string.Empty;

            try
            {
                // Creazione della richiesta ExecuteMultiple
                ExecuteMultipleRequest executeMultipleRequest = new ExecuteMultipleRequest()
                {
                    Requests = new OrganizationRequestCollection(),
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    }
                };

                // Lista del pagamenti da cancellare
                var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                <fetch>
                  <entity name=""res_paymentschedule"" />
                </fetch>";
                List<Entity> lPaymentsSchedule = service.RetrieveAll(fetchXml);

                foreach (Entity ePaymentSchedule in lPaymentsSchedule)
                {
                    DeleteRequest deleteRequest = new DeleteRequest
                    {
                        Target = new EntityReference(res_paymentschedule.logicalName, ePaymentSchedule.Id)
                    };
                    executeMultipleRequest.Requests.Add(deleteRequest);
                }

                // Eseguire il batch
                ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse)service.Execute(executeMultipleRequest);

                // Controlla le risposte
                foreach (var responseItem in executeMultipleResponse.Responses)
                {
                    if (responseItem.Fault != null)
                    {
                        throw new Exception(responseItem.Fault.Message);
                    }
                }
            }
            catch (Exception exception)
            {
                basicOutput.result = 2;
                basicOutput.message = exception.Message;
            }
            finally
            {
                jsonDataOutput = RSMNG.Plugins.Controller.Serialize<Model.BasicOutput>(basicOutput, typeof(Model.BasicOutput));
            }
            return jsonDataOutput;

        }
    }
}
