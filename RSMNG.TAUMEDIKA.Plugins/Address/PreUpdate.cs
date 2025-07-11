﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using RSMNG.TAUMEDIKA.DataModel;
using RSMNG.TAUMEDIKA.Shared.Address;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMNG.TAUMEDIKA.Plugins.Address
{
    public class PreUpdate : RSMNG.BaseClass
    {
        public PreUpdate(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.PRE;
            PluginMessage = "Update";
            PluginPrimaryEntityName = res_address.logicalName;
            PluginRegion = "";
            PluginActiveTrace = false;
        }
        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            Entity target = (Entity)crmServiceProvider.PluginContext.InputParameters["Target"];
            Entity preImage = crmServiceProvider.PluginContext.PreEntityImages["PreImage"];
            Entity postImage = target.GetPostImage(preImage);
            postImage.TryGetAttributeValue<EntityReference>(res_address.res_customerid, out EntityReference erCustomer);

            #region Controllo campo Indirizzo scheda cliente [DISABLED]
            //PluginRegion = "Controllo campo Indirizzo scheda cliente";

            //target.TryGetAttributeValue<bool>(res_address.res_iscustomeraddress, out bool isCustomerAddressModified);



            //if (isCustomerAddressModified)
            //{
            //    Trace("Check", "Il campo Indirizzo scheda cliente è stato modificato"); /** <------------< TRACE >------------ */

            //    throw new ApplicationException("Il campo Indirizzo scheda cliente non è modificabile dall'utente");
            //}
            #endregion

            #region Controllo campo Nome [DISABLED]
            //PluginRegion = "Controllo campo Nome";

            //target.TryGetAttributeValue<string>(res_address.res_name, out string nome);
            //if (nome != null)
            //{
            //    Trace("Check", "Il campo Nome è stato modificato"); /** <------------< TRACE >------------ */
            //    Trace("nome", nome); /** <------------< TRACE >------------ */

            //    throw new ApplicationException("Il campo nome non è modificabile dall'utente");
            //}
            #endregion

            #region Controllo Indirizzo scheda cliente [DISABLED]
            //PluginRegion = "Controllo Indirizzo scheda cliente";

            //preImage.TryGetAttributeValue<bool>(res_address.res_iscustomeraddress, out bool isCustomerAddress);

            ////se è un indirizzo scheda cliente
            //if (isCustomerAddress)
            //{
            //    Trace("Check", "È un indirizzo scheda cliente"); /** <------------< TRACE >------------ */

            //    //campi non modificabili dall'utente se Indirizzo scheda cliente = SI
            //    List<string> campiSchedaCliente = new List<string>
            //{
            //    res_address.res_customerid,
            //    res_address.res_addressField,
            //    res_address.res_postalcode,
            //    res_address.res_city,
            //    res_address.res_province,
            //    res_address.res_location,
            //    res_address.res_countryid,
            //};

            //    foreach (string campoModificato in campiSchedaCliente)
            //    {
            //        if (target.Contains(campoModificato) && target.GetAttributeValue<object>(campoModificato) != null)
            //        {
            //            Trace("Check", "È stato modificato un campo scheda cliente"); /** <------------< TRACE >------------ */

            //            throw new ApplicationException("I record con il campo Indirizzo scheda cliente = SI non sono modificabili a eccezione del campo Default");
            //        }
            //    }
            //}
            #endregion

            #region Controllo campi obbligatori
            PluginRegion = "Controllo campi obbligatori";
            crmServiceProvider.VerifyMandatoryField(Utility.mandatoryFields);
            #endregion

            #region Genera nome
            PluginRegion = "Genera nome";

            if (target.Contains(res_address.res_customerid) || target.Contains(res_address.res_city) || target.Contains(res_address.res_addressField))
            {

                string addressName;
                string customerName = string.Empty;
                string addressCity;
                string addressStreet;


                if (erCustomer != null)
                {
                    if (erCustomer.LogicalName == contact.logicalName)
                    {
                        Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(contact.fullname));
                        customerName = customer.GetAttributeValue<string>(contact.fullname) ?? string.Empty;
                    }
                    if (erCustomer.LogicalName == account.logicalName)
                    {
                        Entity customer = crmServiceProvider.Service.Retrieve(erCustomer.LogicalName, erCustomer.Id, new ColumnSet(account.name));
                        customerName = customer.GetAttributeValue<string>(account.name) ?? string.Empty;
                    }
                }

                addressCity = postImage.GetAttributeValue<string>(res_address.res_city) ?? string.Empty;
                addressStreet = postImage.GetAttributeValue<string>(res_address.res_addressField) ?? string.Empty;

                addressName = $"{customerName} - {addressCity} - {addressStreet}";

                target[res_address.res_name] = addressName;
            }
            #endregion

            #region Controllo duplicati Default = SI
            PluginRegion = "Controllo duplicati Default = SI";

            target.TryGetAttributeValue<bool>(res_address.res_isdefault, out bool isDefault);

            //se aggiorno il record e imposto Default = SI
            if (isDefault)
            {
                if (erCustomer.Id != null)
                {
                    //recupero eventuali record con Default = SI
                    EntityCollection linkedAddresses = Utility.GetAddresses(crmServiceProvider, erCustomer.Id);

                    if (linkedAddresses.Entities.Count > 0)
                    {
                        foreach (Entity linkedAddress in linkedAddresses.Entities)
                        {
                            //aggiorno a Default = NO tutti i record meno questo in update
                            Entity linkedAddressUpt = new Entity(linkedAddress.LogicalName, linkedAddress.Id);
                            linkedAddressUpt.Attributes.Add(res_address.res_isdefault, false);
                            crmServiceProvider.Service.Update(linkedAddressUpt);
                        }
                    }
                }
            }
            #endregion
        }
    }
}

