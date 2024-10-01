using Microsoft.Xrm.Sdk;
using RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ExportSalesOrders;
using RSMNG.TAUMEDIKA.DataModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi
{
    public class ExportSalesOrders : RSMNG.BaseClass
    {
        public ExportSalesOrders(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {
            PluginStage = Stage.TRANSACTION;
            PluginMessage = "res_ExportSalesOrders";
            PluginPrimaryEntityName = "none";
            PluginRegion = "ExportSalesOrders";
        }

        public override void ExecutePlugin(CrmServiceProvider crmServiceProvider)
        {
            crmServiceProvider.TracingService.Trace("Inizio");
            Response response = null;
            string outResult = "OK";
            string outMessage = "Nessun errore riscontrato";
            string outErrorCode = "00";
            Entity outFile = new Entity();
            bool debug = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.debug) ? (bool)crmServiceProvider.PluginContext.InputParameters[ParametersIn.debug] : false;
            Entity enFiltersRequest = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.filters) ? (Entity)crmServiceProvider.PluginContext.InputParameters[ParametersIn.filters] : null;
            Entity enConfigurationRequest = crmServiceProvider.PluginContext.InputParameters.ContainsAttributeNotNull(ParametersIn.configurations) ? (Entity)crmServiceProvider.PluginContext.InputParameters[ParametersIn.configurations] : null;

            if (!debug)
            {
                try
                {
                    #region Controllo i parametri obbligatori
                    PluginRegion = "Controllo i parametri obbligatori";
                    if (enFiltersRequest.NotContainsAttributeOrNull(Filters.statuscodes))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'Stati' obbligatorio";
                    }
                    else if (enFiltersRequest.NotContainsAttributeOrNull(Filters.lastxdays))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'Ultimi x giorni' obbligatorio";
                    }
                    else if (enConfigurationRequest.NotContainsAttributeOrNull(Configurations.appversion))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'AppVersion' obbligatorio per la generazione del file XML";
                    }
                    else if (enConfigurationRequest.NotContainsAttributeOrNull(Configurations.creator))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'Creator' obbligatorio per la generazione del file XML";
                    }
                    else if (enConfigurationRequest.NotContainsAttributeOrNull(Configurations.creatorurl))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'CreatorUrl' obbligatorio per la generazione del file XML";
                    }
                    else if (enConfigurationRequest.NotContainsAttributeOrNull(Configurations.xmlnsxsi))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'xmlns:xsi' obbligatorio per la generazione del file XML";
                    }
                    else if (enConfigurationRequest.NotContainsAttributeOrNull(Configurations.xsinonamespaceschemalocation))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'xsi:noNamespaceSchemaLocation' obbligatorio per la generazione del file XML";
                    }
                    else if (enConfigurationRequest.NotContainsAttributeOrNull(Configurations.companyname))
                    {
                        outResult = "KO";
                        outErrorCode = "01";
                        outMessage = $"Parametro 'CompanyName' obbligatorio per la generazione del file XML";
                    }
                    #endregion

                    if (outResult == "OK")
                    {
                        #region Crea un'istanza della classe EasyfattDocuments e popola i campi
                        PluginRegion = "Crea un'istanza della classe EasyfattDocuments e popola i campi";
                        EasyfattDocuments easyfattDocuments = new EasyfattDocuments
                        {
                            AppVersion = enConfigurationRequest.GetAttributeValue<string>(Configurations.appversion),
                            Creator = enConfigurationRequest.GetAttributeValue<string>(Configurations.creator),
                            CreatorUrl = enConfigurationRequest.GetAttributeValue<string>(Configurations.creatorurl),
                            Company = new Company
                            {
                                Name = enConfigurationRequest.GetAttributeValue<string>(Configurations.companyname)
                            },
                            XmlnsXsi = enConfigurationRequest.GetAttributeValue<string>(Configurations.xmlnsxsi),
                            XsiNoNamespaceSchemaLocation = enConfigurationRequest.GetAttributeValue<string>(Configurations.xsinonamespaceschemalocation),
                            Documents = new Documents
                            {
                                Document = new System.Collections.Generic.List<Document>()
                            }
                        };
                        #endregion

                        #region Estraggo le spese accessorie
                        var fetchXmlAE = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_additionalexpense.logicalName}"">
                            <attribute name=""{res_additionalexpense.res_additionalexpenseid}"" />
                            <attribute name=""{res_additionalexpense.res_amount}"" />
                            <attribute name=""{res_additionalexpense.res_name}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lAdditionalExpense = crmServiceProvider.Service.RetrieveAll(fetchXmlAE);
                        #endregion

                        #region Estraggo i codici Iva
                        var fetchXmlVN = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_vatnumber.logicalName}"">
                            <attribute name=""{res_vatnumber.res_code}"" />
                            <attribute name=""{res_vatnumber.res_name}"" />
                            <attribute name=""{res_vatnumber.res_rate}"" />
                            <attribute name=""{res_vatnumber.res_vatnumberid}"" />
                            <attribute name=""{res_vatnumber.res_vattype}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lVatNumber = crmServiceProvider.Service.RetrieveAll(fetchXmlVN);
                        #endregion

                        #region Estraggo le codizioni di pagamento
                        var fetchXmlPT = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_paymentterm.logicalName}"">
                            <attribute name=""{res_paymentterm.res_isbankvisible}"" />
                            <attribute name=""{res_paymentterm.res_name}"" />
                            <attribute name=""{res_paymentterm.res_paymenttermid}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lPaymentTerm = crmServiceProvider.Service.RetrieveAll(fetchXmlPT);
                        #endregion

                        #region Estraggo le banche
                        var fetchXmlBD = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_bankdetails.logicalName}"">
                            <attribute name=""{res_bankdetails.res_bankdetailsid}"" />
                            <attribute name=""{res_bankdetails.res_iban}"" />
                            <attribute name=""{res_bankdetails.res_name}"" />
                            <attribute name=""{res_bankdetails.res_site}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lBankDetail = crmServiceProvider.Service.RetrieveAll(fetchXmlBD);
                        #endregion

                        #region Estraggo gli ordini applicando i filtri indicati
                        PluginRegion = "Estraggo gli ordini applicando i filtri indicati";
                        var fetchData = new
                        {
                            modifiedon = (int)enFiltersRequest.Attributes[Filters.lastxdays],
                            statuscode = $"<value>{enFiltersRequest.GetAttributeValue<EntityCollection>(Filters.statuscodes).Entities.Select(sc => sc.GetAttributeValue<int>(StatusCodes.statuscode).ToString()).Aggregate((acc, next) => $"{acc}</value><value>{next}")}</value>"
                        };
                        var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                            <entity name=""{salesorder.logicalName}"">
                            <attribute name=""{salesorder.freightamount}"" />
                            <attribute name=""{salesorder.ordernumber}"" />
                            <attribute name=""{salesorder.ownerid}"" />
                            <attribute name=""{salesorder.res_additionalexpenseid}"" />
                            <attribute name=""{salesorder.res_bankdetailsid}"" />
                            <attribute name=""{salesorder.res_date}"" />
                            <attribute name=""{salesorder.res_deposit}"" />
                            <attribute name=""{salesorder.res_internalusecomment}"" />
                            <attribute name=""{salesorder.res_paymenttermid}"" />
                            <attribute name=""{salesorder.res_shippingreference}"" />
                            <attribute name=""{salesorder.res_vatnumberid}"" />
                            <attribute name=""{salesorder.shipto_city}"" />
                            <attribute name=""{salesorder.shipto_composite}"" />
                            <attribute name=""{salesorder.shipto_country}"" />
                            <attribute name=""{salesorder.shipto_postalcode}"" />
                            <attribute name=""{salesorder.shipto_stateorprovince}"" />
                            <attribute name=""{salesorder.totalamount}"" />
                            <attribute name=""{salesorder.totalamountlessfreight}"" />
                            <attribute name=""{salesorder.totaltax}"" />
                            <attribute name=""{salesorder.customerid}"" />
                            <filter>
                                <condition attribute=""{salesorder.statuscode}"" operator=""in"">
                                {fetchData.statuscode}
                                </condition>
                                <condition attribute=""{salesorder.modifiedon}"" operator=""last-x-days"" value=""{fetchData.modifiedon}"" />
                            </filter>
                            <link-entity name=""{account.logicalName}"" from=""{account.accountid}"" to=""{salesorder.customerid}"" link-type=""outer"" alias=""{account.logicalName}"">
                                <attribute name=""{account.accountnumber}"" />
                                <attribute name=""{account.address1_city}"" />
                                <attribute name=""{account.address1_country}"" />
                                <attribute name=""{account.address1_line1}"" />
                                <attribute name=""{account.address1_postalcode}"" />
                                <attribute name=""{account.address1_stateorprovince}"" />
                                <attribute name=""{account.emailaddress1}"" />
                                <attribute name=""{account.emailaddress3}"" />
                                <attribute name=""{account.fax}"" />
                                <attribute name=""{account.name}"" />
                                <attribute name=""{account.primarycontactid}"" />
                                <attribute name=""{account.res_mobilenumber}"" />
                                <attribute name=""{account.res_sdi}"" />
                                <attribute name=""{account.res_taxcode}"" />
                                <attribute name=""{account.telephone1}"" />
                            </link-entity>
                            <link-entity name=""{salesorderdetail.logicalName}"" from=""{salesorderdetail.salesorderid}"" to=""{salesorder.salesorderid}"" link-type=""outer"" alias=""{salesorderdetail.logicalName}"">
                                <attribute name=""{salesorderdetail.salesorderdetailid}"" />
                                <attribute name=""{salesorderdetail.res_itemcode}"" />
                                <attribute name=""{salesorderdetail.priceperunit}"" />
                                <attribute name=""{salesorderdetail.productdescription}"" />
                                <attribute name=""{salesorderdetail.productid}"" />
                                <attribute name=""{salesorderdetail.quantity}"" />
                                <attribute name=""{salesorderdetail.res_discountpercentage1}"" />
                                <attribute name=""{salesorderdetail.res_discountpercentage2}"" />
                                <attribute name=""{salesorderdetail.res_discountpercentage3}"" />
                                <attribute name=""{salesorderdetail.res_taxableamount}"" />
                                <attribute name=""{salesorderdetail.res_vatnumberid}"" />
                                <attribute name=""{salesorderdetail.uomid}"" />
                                <attribute name=""{salesorderdetail.description}"" />
                            </link-entity>
                            </entity>
                        </fetch>";
                        List<Entity> lSaleOrders = crmServiceProvider.Service.RetrieveAll(fetchXml);
                        crmServiceProvider.TracingService.Trace($"Query:{fetchXml}");
                        #endregion

                        #region Popolo la classe Document
                        PluginRegion = "Popolo la classe Document";
                        List<Guid> salesOrdersId = lSaleOrders.Select(e => e.Id).Distinct().ToList();
                        foreach (Guid salesOrderId in salesOrdersId)
                        {
                            //prelevo ogni singolo ordine
                            Entity eSalesOrder = lSaleOrders.FirstOrDefault(f => f.Id.Equals(salesOrderId));
                            if (eSalesOrder != null)
                            {
                                //Prelevo il dettaglio dell'account legato al salesorder
                                Entity eAccount = lSaleOrders.FirstOrDefault(f => f.Id.Equals(salesOrderId) && f.ContainsAttributeNotNull(account.accountnumber));

                                //Prelevo i dettaglio prima di aggiungere il document alla collection documents
                                List<Entity> lSalesOrderDetails = lSaleOrders.Where(w => w.Id.Equals(salesOrderId) && w.ContainsAttributeNotNull(salesorderdetail.salesorderdetailid)).ToList();

                                PluginRegion = "Popolo l'ordine";
                                Document document = new Document
                                {
                                    CustomerCode = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.accountnumber}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.accountnumber}") : "" ?? "",
                                    CustomerWebLogin = "",
                                    CustomerName = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.name}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.name}") : "" ?? "",
                                    CustomerAddress = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_line1}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_line1}") : "" ?? "",
                                    CustomerPostcode = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_postalcode}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_postalcode}") : "" ?? "",
                                    CustomerCity = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_city}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_city}") : "" ?? "",
                                    CustomerProvince = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_stateorprovince}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_stateorprovince}") : "" ?? "",
                                    CustomerCountry = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_country}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_country}") : "" ?? "",
                                    CustomerFiscalCode = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.res_taxcode}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.res_taxcode}") : "" ?? "",
                                    CustomerReference = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.primarycontactid}") ? Shared.Contact.Utility.GetName(crmServiceProvider.Service, (Guid)eAccount?.GetAliasedValue<EntityReference>($"{account.logicalName}.{account.primarycontactid}").Id) : "" ?? "",
                                    CustomerTel = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.telephone1}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.telephone1}") : "" ?? "",
                                    CustomerCellPhone = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.res_mobilenumber}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.res_mobilenumber}") : "" ?? "",
                                    CustomerFax = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.fax}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.fax}") : "" ?? "",
                                    CustomerEmail = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.emailaddress1}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.emailaddress1}") : "" ?? "",
                                    CustomerPec = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.emailaddress3}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.emailaddress3}") : "" ?? "",
                                    CustomerEInvoiceDestCode = (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.res_sdi}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.res_sdi}") : "" ?? "",
                                    DeliveryName = eSalesOrder.ContainsAttributeNotNull(salesorder.res_shippingreference) ? eSalesOrder.GetAttributeValue<string>(salesorder.res_shippingreference) : eSalesOrder.ContainsAliasNotNull(salesorder.customerid) ? Shared.Account.Utility.GetName(crmServiceProvider.Service, eSalesOrder.GetAttributeValue<EntityReference>(salesorder.customerid).Id) : "" ?? "",
                                    DeliveryAddress = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_composite) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_composite) : (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_line1}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_line1}") : "" ?? "",
                                    DeliveryPostcode = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_postalcode) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_postalcode) : (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_postalcode}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_postalcode}") : "" ?? "",
                                    DeliveryCity = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_city) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_city) : (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_city}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_city}") : "" ?? "",
                                    DeliveryProvince = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_stateorprovince) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_stateorprovince) : (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_stateorprovince}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_stateorprovince}") : "" ?? "",
                                    DeliveryCountry = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_country) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_country) : (bool)eAccount?.ContainsAliasNotNull($"{account.logicalName}.{account.address1_country}") ? eAccount?.GetAliasedValue<string>($"{account.logicalName}.{account.address1_country}") : "" ?? "",
                                    DocumentType = "C",
                                    Date = eSalesOrder.ContainsAttributeNotNull(salesorder.res_date) ? eSalesOrder.GetAttributeValue<DateTime>(salesorder.res_date).ToString("yyyy-MM-dd") : "",
                                    Number = eSalesOrder.ContainsAttributeNotNull(salesorder.ordernumber) ? eSalesOrder.GetAttributeValue<string>(salesorder.ordernumber) : "",
                                    Numbering = "",
                                    CostDescription = "",
                                    CostVatCode = new CostVatCode
                                    {
                                        Value = "",
                                        Class = "",
                                        Perc = "",
                                        Description = ""
                                    },
                                    CostAmount = eSalesOrder.ContainsAttributeNotNull(salesorder.freightamount) ? eSalesOrder.GetAttributeValue<Money>(salesorder.freightamount).Value.ToString("{0:C}") : "",
                                    ContribDescription = "",
                                    ContribPerc = "",
                                    ContribSubjectToWithholdingTax = "",
                                    ContribAmount = "",
                                    ContribVatCode = "",
                                    TotalWithoutTax = eSalesOrder.ContainsAttributeNotNull(salesorder.totalamountlessfreight) ? eSalesOrder.GetAttributeValue<Money>(salesorder.totalamountlessfreight).Value.ToString("{0:C}") : "",
                                    VatAmount = eSalesOrder.ContainsAttributeNotNull(salesorder.totaltax) ? eSalesOrder.GetAttributeValue<Money>(salesorder.totaltax).Value.ToString("{0:C}") : "",
                                    WithholdingTaxAmount = "",
                                    WithholdingTaxAmountB = "",
                                    WithholdingTaxNameB = "",
                                    Total = eSalesOrder.ContainsAttributeNotNull(salesorder.totalamount) ? eSalesOrder.GetAttributeValue<Money>(salesorder.totalamount).Value.ToString("{0:C}") : "",
                                    PriceList = "Listino1",
                                    PricesIncludeVat = "false",
                                    TotalSubjectToWithholdingTax = "",
                                    WithholdingTaxPerc = "",
                                    WithholdingTaxPerc2 = "",
                                    PaymentName = "",
                                    PaymentBank = "",
                                    PaymentAdvanceAmount = eSalesOrder.ContainsAttributeNotNull(salesorder.res_deposit) ? eSalesOrder.GetAttributeValue<Money>(salesorder.res_deposit).Value.ToString("{0:C}") : "",
                                    Carrier = "",
                                    TransportReason = "",
                                    GoodsAppearance = "",
                                    NumOfPieces = "",
                                    TransportDateTime = "",
                                    ShipmentTerms = "",
                                    TransportedWeight = "",
                                    TrackingNumber = "",
                                    InternalComment = eSalesOrder.ContainsAttributeNotNull(salesorder.res_internalusecomment) ? eSalesOrder.GetAttributeValue<string>(salesorder.res_internalusecomment) : "",
                                    CustomField1 = "",
                                    CustomField2 = "",
                                    CustomField3 = "",
                                    CustomField4 = "",
                                    FootNotes = "",
                                    ExpectedConclusionDate = "",
                                    SalesAgent = eSalesOrder.ContainsAttributeNotNull(salesorder.ownerid) && eSalesOrder.GetAttributeValue<EntityReference>(salesorder.ownerid).LogicalName == systemuser.logicalName ? Shared.SystemUser.Utility.GetAgentNumber(crmServiceProvider.Service, eSalesOrder.GetAttributeValue<EntityReference>(salesorder.ownerid).Id) : ""
                                    Rows = new Rows
                                    {
                                        Row = new System.Collections.Generic.List<Row>()
                                    }
                                };

                                PluginRegion = "Popolo il Codice IVA";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_vatnumberid))
                                {
                                    Entity res_vatnumberid = lVatNumber.FirstOrDefault(vn => vn.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_vatnumberid)));
                                    if (res_vatnumberid != null)
                                    {
                                        document.CostVatCode.Value = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_code) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_code) : "";
                                        document.CostVatCode.Perc = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_rate) ? res_vatnumberid.GetAttributeValue<int>(res_vatnumber.res_rate).ToString() : "";
                                        document.CostVatCode.Class = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_vattype) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_vattype).ToString() : "";
                                        document.CostVatCode.Description = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_description) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_description).ToString() : "";
                                    }
                                }

                                PluginRegion = "Popolo le spese accessorie";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_additionalexpenseid))
                                {
                                    Entity res_additionalexpenseid = lAdditionalExpense.FirstOrDefault(ae => ae.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_additionalexpenseid)));
                                    if (res_additionalexpenseid != null)
                                    {
                                        document.CostDescription = res_additionalexpenseid.ContainsAttributeNotNull(res_additionalexpense.res_name) ? res_additionalexpenseid.GetAttributeValue<string>(res_additionalexpense.res_name) : "";
                                    }
                                }

                                PluginRegion = "Popolo la codizione di pagamento";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_paymenttermid))
                                {
                                    Entity res_paymenttermid = lPaymentTerm.FirstOrDefault(pt => pt.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_paymenttermid)));
                                    if (res_paymenttermid != null)
                                    {
                                        document.PaymentName = res_paymenttermid.ContainsAttributeNotNull(res_paymentterm.res_name) ? res_paymenttermid.GetAttributeValue<string>(res_paymentterm.res_name) : "";
                                    }
                                }

                                PluginRegion = "Popolo la coordinata bancaria";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_bankdetailsid))
                                {
                                    Entity res_bankdetailsid = lBankDetail.FirstOrDefault(bd => bd.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_bankdetailsid)));
                                    if (res_bankdetailsid != null)
                                    {
                                        document.PaymentBank = res_bankdetailsid.ContainsAttributeNotNull(res_bankdetails.res_name) ? res_bankdetailsid.GetAttributeValue<string>(res_bankdetails.res_name) : "";
                                    }
                                }

                                if (lSalesOrderDetails?.Count() > 0)
                                {
                                    //Aggiungo i dettagli all'ordine
                                    foreach (Entity eSalesOrderDetail in lSalesOrderDetails)
                                    {
                                        document.Rows.Row.Add(
                                        new Row
                                        {
                                            Code = eSalesOrderDetail.ContainsAttributeNotNull(salesorderdetail.res_itemcode) ? eSalesOrderDetail.GetAttributeValue<string>(salesorderdetail.res_itemcode) : "",
                                            Description = eSalesOrderDetail.ContainsAttributeNotNull(salesorderdetail.description) ? eSalesOrderDetail.GetAttributeValue<string>(salesorderdetail.description) : "",
                                            Qty = eSalesOrderDetail.ContainsAttributeNotNull(salesorderdetail.quantity) ? eSalesOrderDetail.GetAttributeValue<int>(salesorderdetail.quantity).ToString() : "",
                                            Price = "1220",
                                            Total = "1043.1",
                                            
                                        }
                                        );
                                    }
                                    easyfattDocuments.Documents.Document.Add(document);
                                }
                            }
                        }
                        #endregion

                        //// Crea un'istanza della classe EasyfattDocuments e popola i campi
                        //EasyfattDocuments easyfattDocuments = new EasyfattDocuments
                        //{
                        //    AppVersion = "2",
                        //    Creator = "Danea Easyfatt Enterprise One 2024.57",
                        //    CreatorUrl = "http://www.danea.it/software/easyfatt",
                        //    Company = new Company
                        //    {
                        //        Name = "R&S"
                        //    },
                        //    XmlnsXsi = "",
                        //    XsiNoNamespaceSchemaLocation = "",
                        //    Documents = new Documents
                        //    {
                        //        Document = new System.Collections.Generic.List<Document>
                        //        {
                        //            new Document
                        //            {
                        //                CustomerCode = "0001",
                        //                CustomerName = "Giuseppe Giglio",
                        //                CustomerAddress = "Via Sandro Botticelli",
                        //                CustomerCity = "Napoli",
                        //                DocumentType = "C",
                        //                Date = "2024-07-17",
                        //                Number = "7000",
                        //                Rows = new Rows
                        //                {
                        //                    Row = new System.Collections.Generic.List<Row>
                        //                    {
                        //                        new Row
                        //                        {
                        //                            Code = "00002",
                        //                            Description = "Crema",
                        //                            Qty = "1",
                        //                            Price = "1220",
                        //                            Total = "1043.1"
                        //                        },
                        //                        new Row
                        //                        {
                        //                            Code = "",
                        //                            Description = "crema manuale test",
                        //                            Qty = "1",
                        //                            Price = "1000",
                        //                            Total = "1000"
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //};

                        //// Serializza l'oggetto in XML
                        //XmlSerializer serializer = new XmlSerializer(typeof(EasyfattDocuments));

                        //// Salva il risultato in un file XML
                        //using (StreamWriter writer = new StreamWriter("output.xml"))
                        //{
                        //    serializer.Serialize(writer, easyfattDocuments);
                        //}

                        //// Alternativamente, serializza in una stringa
                        //using (StringWriter stringWriter = new StringWriter())
                        //{
                        //    serializer.Serialize(stringWriter, easyfattDocuments);
                        //    string xmlOutput = stringWriter.ToString();
                        //    Console.WriteLine(xmlOutput);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    response = new Response(outErrorCode, outMessage);
                    crmServiceProvider.PluginContext.OutputParameters[ParametersOut.result] = outResult;
                    crmServiceProvider.PluginContext.OutputParameters[ParametersOut.error] = response.ErrorResponseEntity;
                    crmServiceProvider.PluginContext.OutputParameters[ParametersOut.file] = outFile;
                }
            }
            else
            {
                outResult = "OK";
                outErrorCode = "00";
                outMessage = "'DEBUG MODE' API eseguita correttamente";
                response = new Response(outErrorCode, outMessage);
                crmServiceProvider.PluginContext.OutputParameters[ParametersOut.result] = outResult;
                crmServiceProvider.PluginContext.OutputParameters[ParametersOut.error] = response.ErrorResponseEntity;
                crmServiceProvider.PluginContext.OutputParameters[ParametersOut.file] = outFile;
            }
            crmServiceProvider.TracingService.Trace("Fine");
        }
    }
}
