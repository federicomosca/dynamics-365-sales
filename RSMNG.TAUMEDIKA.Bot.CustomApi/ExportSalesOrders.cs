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
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using static RSMNG.TAUMEDIKA.Model;

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
                        PluginRegion = "Estraggo le spese accessorie";
                        var fetchXmlAE = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_additionalexpense.logicalName}"">
                            <attribute name=""{res_additionalexpense.res_additionalexpenseid}"" />
                            <attribute name=""{res_additionalexpense.res_amount}"" />
                            <attribute name=""{res_additionalexpense.res_name}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lAdditionalExpense = crmServiceProvider.Service.RetrieveAll(fetchXmlAE);
                        if (lAdditionalExpense == null)
                        {
                            lAdditionalExpense = new List<Entity>();
                        }
                        #endregion

                        #region Estraggo i codici Iva
                        PluginRegion = "Estraggo i codici Iva";
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
                        if (lVatNumber == null)
                        {
                            lVatNumber = new List<Entity>();
                        }
                        #endregion

                        #region Estraggo le codizioni di pagamento
                        PluginRegion = "Estraggo le codizioni di pagamento";
                        var fetchXmlPT = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{res_paymentterm.logicalName}"">
                            <attribute name=""{res_paymentterm.res_isbankvisible}"" />
                            <attribute name=""{res_paymentterm.res_name}"" />
                            <attribute name=""{res_paymentterm.res_paymenttermid}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lPaymentTerm = crmServiceProvider.Service.RetrieveAll(fetchXmlPT);
                        if (lPaymentTerm == null)
                        {
                            lPaymentTerm = new List<Entity>();
                        }
                        #endregion

                        #region Estraggo le banche
                        PluginRegion = "Estraggo le banche";
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
                        if (lBankDetail == null)
                        {
                            lBankDetail = new List<Entity>();
                        }
                        #endregion

                        #region Estraggo le unità di misura
                        PluginRegion = "Estraggo le unità di misura";
                        var fetchXmlU = $@"<?xml version=""1.0"" encoding=""utf-16""?>
                        <fetch>
                          <entity name=""{uom.logicalName}"">
                            <attribute name=""{uom.uomid}"" />
                            <attribute name=""{uom.name}"" />
                          </entity>
                        </fetch>";
                        List<Entity> lUom = crmServiceProvider.Service.RetrieveAll(fetchXmlU);
                        if (lUom == null)
                        {
                            lUom = new List<Entity>();
                        }
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
                        List<Guid> lSalesOrdersId = lSaleOrders.Select(e => e.Id).Distinct().ToList();
                        crmServiceProvider.TracingService.Trace($"lSalesOrdersId:{lSalesOrdersId.Count}");
                        foreach (Guid salesOrderId in lSalesOrdersId)
                        {
                            //prelevo ogni singolo ordine
                            Entity eSalesOrder = lSaleOrders.FirstOrDefault(f => f.Id.Equals(salesOrderId));
                            if (eSalesOrder != null)
                            {
                                //Prelevo il dettaglio dell'account legato al salesorder
                                Entity eAccount = lSaleOrders.FirstOrDefault(f => f.Id.Equals(salesOrderId) && f.ContainsAttributeNotNull(salesorder.customerid));

                                if (eAccount == null)
                                {
                                    eAccount = new Entity(salesorder.logicalName);
                                }

                                //Prelevo i dettaglio prima di aggiungere il document alla collection documents
                                List<Entity> lSalesOrderDetails = lSaleOrders.Where(w => w.Id.Equals(salesOrderId) && w.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.salesorderdetailid}")).ToList();
                                crmServiceProvider.TracingService.Trace($"lSalesOrderDetails:{lSalesOrderDetails?.Count}");

                                PluginRegion = "Popolo l'ordine";
                                Document document = new Document
                                {
                                    CustomerCode = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.accountnumber}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.accountnumber}") : "",
                                    CustomerWebLogin = "",
                                    CustomerName = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.name}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.name}") : "",
                                    CustomerAddress = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_line1}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_line1}").Replace(Environment.NewLine, " ") : "",
                                    CustomerPostcode = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_postalcode}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_postalcode}") : "",
                                    CustomerCity = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_city}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_city}") : "",
                                    CustomerProvince = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_stateorprovince}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_stateorprovince}") : "",
                                    CustomerCountry = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_country}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_country}") : "",
                                    CustomerFiscalCode = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.res_taxcode}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.res_taxcode}") : "",
                                    CustomerReference = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.primarycontactid}") ? Shared.Contact.Utility.GetName(crmServiceProvider.Service, eAccount.GetAliasedValue<EntityReference>($"{account.logicalName}.{account.primarycontactid}").Id) : "",
                                    CustomerTel = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.telephone1}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.telephone1}") : "",
                                    CustomerCellPhone = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.res_mobilenumber}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.res_mobilenumber}") : "",
                                    CustomerFax = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.fax}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.fax}") : "",
                                    CustomerEmail = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.emailaddress1}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.emailaddress1}") : "",
                                    CustomerPec = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.emailaddress3}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.emailaddress3}") : "",
                                    CustomerEInvoiceDestCode = eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.res_sdi}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.res_sdi}") : "",
                                    DeliveryName = eSalesOrder.ContainsAttributeNotNull(salesorder.res_shippingreference) ? eSalesOrder.GetAttributeValue<string>(salesorder.res_shippingreference) : eSalesOrder.ContainsAttributeNotNull(salesorder.customerid) ? Shared.Account.Utility.GetName(crmServiceProvider.Service, eSalesOrder.GetAttributeValue<EntityReference>(salesorder.customerid).Id) : "",
                                    DeliveryAddress = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_composite) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_composite).Replace(Environment.NewLine, " ") : eSalesOrder.ContainsAttributeNotNull(salesorder.customerid) && eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_line1}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_line1}").Replace(Environment.NewLine, " ") : "",
                                    DeliveryPostcode = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_postalcode) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_postalcode) : eSalesOrder.ContainsAttributeNotNull(salesorder.customerid) && eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_postalcode}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_postalcode}") : "",
                                    DeliveryCity = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_city) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_city) : eSalesOrder.ContainsAttributeNotNull(salesorder.customerid) && eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_city}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_city}") : "",
                                    DeliveryProvince = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_stateorprovince) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_stateorprovince) : eSalesOrder.ContainsAttributeNotNull(salesorder.customerid) && eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_stateorprovince}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_stateorprovince}") : "",
                                    DeliveryCountry = eSalesOrder.ContainsAttributeNotNull(salesorder.shipto_country) ? eSalesOrder.GetAttributeValue<string>(salesorder.shipto_country) : eSalesOrder.ContainsAttributeNotNull(salesorder.customerid) && eAccount.ContainsAliasNotNull($"{account.logicalName}.{account.address1_country}") ? eAccount.GetAliasedValue<string>($"{account.logicalName}.{account.address1_country}") : "",
                                    DocumentType = "C",
                                    Date = eSalesOrder.ContainsAttributeNotNull(salesorder.res_date) ? eSalesOrder.GetAttributeValue<DateTime>(salesorder.res_date).ToString("yyyy-MM-dd") : "",
                                    Number = eSalesOrder.ContainsAttributeNotNull(salesorder.ordernumber) ? eSalesOrder.GetAttributeValue<string>(salesorder.ordernumber) : "",
                                    Numbering = "",
                                    CostDescription = "",
                                    CostVatCode = new CostVatCode { Text = "", Class = "", Perc = "", Description = "" },
                                    CostAmount = eSalesOrder.ContainsAttributeNotNull(salesorder.freightamount) ? eSalesOrder.GetAttributeValue<Money>(salesorder.freightamount).Value.ToString("F2") : "",
                                    ContribDescription = "",
                                    ContribPerc = "",
                                    ContribSubjectToWithholdingTax = "",
                                    ContribAmount = "",
                                    ContribVatCode = "",
                                    TotalWithoutTax = eSalesOrder.ContainsAttributeNotNull(salesorder.totalamountlessfreight) ? eSalesOrder.GetAttributeValue<Money>(salesorder.totalamountlessfreight).Value.ToString("F2") : "",
                                    VatAmount = eSalesOrder.ContainsAttributeNotNull(salesorder.totaltax) ? eSalesOrder.GetAttributeValue<Money>(salesorder.totaltax).Value.ToString("F2") : "",
                                    WithholdingTaxAmount = "",
                                    WithholdingTaxAmountB = "",
                                    WithholdingTaxNameB = "",
                                    Total = eSalesOrder.ContainsAttributeNotNull(salesorder.totalamount) ? eSalesOrder.GetAttributeValue<Money>(salesorder.totalamount).Value.ToString("F2") : "",
                                    PriceList = "Listino1",
                                    PricesIncludeVat = "false",
                                    TotalSubjectToWithholdingTax = "",
                                    WithholdingTaxPerc = "",
                                    WithholdingTaxPerc2 = "",
                                    PaymentName = "",
                                    PaymentBank = "",
                                    PaymentAdvanceAmount = eSalesOrder.ContainsAttributeNotNull(salesorder.res_deposit) ? eSalesOrder.GetAttributeValue<Money>(salesorder.res_deposit).Value.ToString("F2") : "",
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
                                    SalesAgent = eSalesOrder.ContainsAttributeNotNull(salesorder.ownerid) && eSalesOrder.GetAttributeValue<EntityReference>(salesorder.ownerid).LogicalName == systemuser.logicalName ? Shared.SystemUser.Utility.GetAgentNumber(crmServiceProvider.Service, eSalesOrder.GetAttributeValue<EntityReference>(salesorder.ownerid).Id) : "",
                                    Rows = new Rows()
                                    {
                                        Row = new System.Collections.Generic.List<Row>()
                                    }
                                };

                                PluginRegion = "Popolo il Codice IVA";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_vatnumberid))
                                {
                                    Entity res_vatnumberid = lVatNumber.FirstOrDefault(vn => vn.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_vatnumberid).Id));
                                    if (res_vatnumberid != null)
                                    {
                                        document.CostVatCode.Text = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_code) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_code) : "";
                                        document.CostVatCode.Perc = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_rate) ? res_vatnumberid.GetAttributeValue<decimal>(res_vatnumber.res_rate).ToString("F2") : "";
                                        document.CostVatCode.Class = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_vattype) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_vattype).ToString() : "";
                                        document.CostVatCode.Description = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_description) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_description).ToString() : "";
                                    }
                                }

                                PluginRegion = "Popolo le spese accessorie";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_additionalexpenseid))
                                {
                                    Entity res_additionalexpenseid = lAdditionalExpense.FirstOrDefault(ae => ae.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_additionalexpenseid).Id));
                                    if (res_additionalexpenseid != null)
                                    {
                                        document.CostDescription = res_additionalexpenseid.ContainsAttributeNotNull(res_additionalexpense.res_name) ? res_additionalexpenseid.GetAttributeValue<string>(res_additionalexpense.res_name) : "";
                                    }
                                }

                                PluginRegion = "Popolo la codizione di pagamento";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_paymenttermid))
                                {
                                    Entity res_paymenttermid = lPaymentTerm.FirstOrDefault(pt => pt.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_paymenttermid).Id));
                                    if (res_paymenttermid != null)
                                    {
                                        document.PaymentName = res_paymenttermid.ContainsAttributeNotNull(res_paymentterm.res_name) ? res_paymenttermid.GetAttributeValue<string>(res_paymentterm.res_name) : "";
                                    }
                                }

                                PluginRegion = "Popolo la coordinata bancaria";
                                if (eSalesOrder.ContainsAttributeNotNull(salesorder.res_bankdetailsid))
                                {
                                    Entity res_bankdetailsid = lBankDetail.FirstOrDefault(bd => bd.Id.Equals(eSalesOrder.GetAttributeValue<EntityReference>(salesorder.res_bankdetailsid).Id));
                                    if (res_bankdetailsid != null)
                                    {
                                        document.PaymentBank = res_bankdetailsid.ContainsAttributeNotNull(res_bankdetails.res_name) ? res_bankdetailsid.GetAttributeValue<string>(res_bankdetails.res_name) : "";
                                    }
                                }

                                PluginRegion = "Ciclo i Prodotti Ordine";
                                if (lSalesOrderDetails?.Count() > 0)
                                {
                                    //Aggiungo i dettagli all'ordine
                                    foreach (Entity eSalesOrderDetail in lSalesOrderDetails)
                                    {
                                        PluginRegion = "Prelevo l'unità di misura corretta";
                                        Entity uomid = lUom.FirstOrDefault(u => u.Id.Equals(eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.uomid}") ? eSalesOrderDetail.GetAliasedValue<EntityReference>($"{salesorderdetail.logicalName}.{salesorderdetail.uomid}").Id : Guid.Empty));

                                        PluginRegion = "Popolo la riga dell'ordine";
                                        Row row = new Row
                                        {
                                            Code = eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.res_itemcode}") ? eSalesOrderDetail.GetAliasedValue<string>($"{salesorderdetail.logicalName}.{salesorderdetail.res_itemcode}") : "",
                                            Description = eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.productdescription}") ? eSalesOrderDetail.GetAliasedValue<string>($"{salesorderdetail.logicalName}.{salesorderdetail.productdescription}") : eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.productid}") ? Shared.Product.Utility.GetName(crmServiceProvider.Service, eSalesOrderDetail.GetAliasedValue<EntityReference>($"{salesorderdetail.logicalName}.{salesorderdetail.productid}").Id) : "",
                                            Qty = eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.quantity}") ? eSalesOrderDetail.GetAliasedValue<decimal>($"{salesorderdetail.logicalName}.{salesorderdetail.quantity}").ToString("F2") : "",
                                            Um = uomid != null ? uomid.ContainsAttributeNotNull(uom.name) ? uomid.GetAttributeValue<string>(uom.name) : "" : "",
                                            Price = eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.priceperunit}") ? eSalesOrderDetail.GetAliasedValue<Money>($"{salesorderdetail.logicalName}.{salesorderdetail.priceperunit}").Value.ToString("F2") : "",
                                            Discounts = "",
                                            EcoFee = "",
                                            VatCode = new VatCode { Text = "", Class = "", Perc = "", Description = "" },
                                            Total = eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.res_taxableamount}") ? eSalesOrderDetail.GetAliasedValue<Money>($"{salesorderdetail.logicalName}.{salesorderdetail.res_taxableamount}").Value.ToString() : "",
                                            Notes = "",
                                            Stock = ""
                                        };

                                        PluginRegion = "Popolo lo sconto della riga dell'ordine";
                                        string res_discountpercentage = string.Empty;
                                        if (eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.res_discountpercentage1}"))
                                        {
                                            res_discountpercentage += eSalesOrderDetail.GetAliasedValue<decimal>($"{salesorderdetail.logicalName}.{salesorderdetail.res_discountpercentage1}").ToString("F2");
                                        }
                                        if (eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.res_discountpercentage2}"))
                                        {
                                            res_discountpercentage += (!string.IsNullOrEmpty(res_discountpercentage) ? "+" : "") + eSalesOrderDetail.GetAliasedValue<decimal>($"{salesorderdetail.logicalName}.{salesorderdetail.res_discountpercentage2}").ToString("F2");
                                        }
                                        if (eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.res_discountpercentage3}"))
                                        {
                                            res_discountpercentage += (!string.IsNullOrEmpty(res_discountpercentage) ? "+" : "") + eSalesOrderDetail.GetAliasedValue<decimal>($"{salesorderdetail.logicalName}.{salesorderdetail.res_discountpercentage3}").ToString("F2");
                                        }
                                        if (!string.IsNullOrEmpty(res_discountpercentage))
                                        {
                                            row.Discounts = res_discountpercentage + "%";
                                        }

                                        PluginRegion = "Popolo il codice Iva della riga dell'ordine";
                                        if (eSalesOrderDetail.ContainsAliasNotNull($"{salesorderdetail.logicalName}.{salesorderdetail.res_vatnumberid}"))
                                        {
                                            Entity res_vatnumberid = lVatNumber.FirstOrDefault(vn => vn.Id.Equals(eSalesOrderDetail.GetAliasedValue<EntityReference>($"{salesorderdetail.logicalName}.{salesorderdetail.res_vatnumberid}").Id));
                                            if (res_vatnumberid != null)
                                            {
                                                row.VatCode.Text = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_code) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_code) : "";
                                                row.VatCode.Perc = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_rate) ? res_vatnumberid.GetAttributeValue<decimal>(res_vatnumber.res_rate).ToString("F2") : "";
                                                row.VatCode.Class = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_vattype) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_vattype).ToString() : "";
                                                row.VatCode.Description = res_vatnumberid.ContainsAttributeNotNull(res_vatnumber.res_description) ? res_vatnumberid.GetAttributeValue<string>(res_vatnumber.res_description).ToString() : "";
                                            }
                                        }

                                        document.Rows.Row.Add(row);
                                    }
                                }
                                easyfattDocuments.Documents.Document.Add(document);
                            }
                        }
                        #endregion

                        #region Serializzo l'oggetto EasyFattDocuments
                        PluginRegion = "Serializzo l'oggetto EasyFattDocuments";
                        XmlSerializer serializer = null;
                        try
                        {
                            serializer = new XmlSerializer(typeof(EasyfattDocuments));
                            // serializzazione o deserializzazione
                        }
                        catch (InvalidOperationException ex)
                        {
                            throw ex;
                        }
                        string xmlOutput = string.Empty;
                        using (StringWriter stringWriter = new CustomStringWriter(Encoding.UTF8))
                        {
                            serializer = new XmlSerializer(typeof(EasyfattDocuments));

                            // Crea le impostazioni per l'XmlWriter
                            var settings = new XmlWriterSettings
                            {
                                Indent = true,
                                OmitXmlDeclaration = false // Include la dichiarazione XML
                            };

                            using (var writer = XmlWriter.Create(stringWriter, settings))
                            {
                                // Serializza l'oggetto
                                serializer.Serialize(writer, easyfattDocuments);
                            }

                            //serializer.Serialize(stringWriter, easyfattDocuments);
                            xmlOutput = stringWriter.ToString();
                            Console.WriteLine(xmlOutput);
                        }
                        #endregion

                        #region Serializzo l'oggetto EasyFattDocuments in Json
                        string jsonOutput = JsonSerializer.Serialize(easyfattDocuments);
                        #endregion

                        #region Creo il file on memoria per XML
                        PluginRegion = "Creo il file on memoria";
                        // Convertiamo la stringa del StringWriter in byte usando la codifica UTF-8
                        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlOutput);

                        // Convertiamo i byte in Base64
                        string base64Xml = Convert.ToBase64String(xmlBytes);

                        // Creiamo il Data URI per il file XML
                        string dataUriXml = $"data:text/xml;base64,{base64Xml}";
                        #endregion

                        #region Creo il file on memoria per XML
                        PluginRegion = "Creo il file on memoria";
                        // Convertiamo la stringa del StringWriter in byte usando la codifica UTF-8
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonOutput);

                        // Convertiamo i byte in Base64
                        string base64Json = Convert.ToBase64String(jsonBytes);

                        // Creiamo il Data URI per il file XML
                        string dataUriJson = $"data:text/json;base64,{base64Json}";
                        #endregion

                        #region definisco la data corretta
                        PluginRegion = "Definisco la data corretta";
                        TimeZoneInfo europeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, europeTimeZone);
                        #endregion

                        #region Creo il log DataIntegration
                        PluginRegion = "Creo il log DataIntegration";
                        Entity enDataIntegration = new Entity(res_dataintegration.logicalName);
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationtype, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationtypeValues.Export));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationaction, new OptionSetValue((int)GlobalOptionSetConstants.res_opt_integrationactionValues.Ordini));
                        enDataIntegration.AddWithRemove(res_dataintegration.res_name, $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()} - {GlobalOptionSetConstants.res_opt_integrationactionValues.Ordini.ToString()} - {localTime.ToString("dd/MM/yyyy HH:mm:ss")}");
                        enDataIntegration.AddWithRemove(res_dataintegration.res_integrationresult, "Esportazione effettuata con successo");
                        Guid enDataIntegrationId = crmServiceProvider.Service.Create(enDataIntegration);
                        #endregion

                        #region Salvo il file nel log DataIntegration XML
                        PluginRegion = "Salvo il file nel log DataIntegration XML";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Input uploadFile_Input_Xml = new RSMNG.TAUMEDIKA.Model.UploadFile_Input()
                        {
                            MimeType = "text/xml",
                            FileName = $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()}_{GlobalOptionSetConstants.res_opt_integrationactionValues.Ordini.ToString()}_{localTime.ToString("dd_MM_yyyy_HH_mm_ss")}.defxml",
                            Id = enDataIntegrationId.ToString(),
                            FileSize = xmlBytes.Length,
                            Content = base64Xml
                        };
                        #endregion

                        string resultUploadXml = Helper.UploadFile(crmServiceProvider.TracingService, crmServiceProvider.Service, res_dataintegration.res_integrationfile, res_dataintegration.logicalName, RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.UploadFile_Input>(uploadFile_Input_Xml, typeof(RSMNG.TAUMEDIKA.Model.UploadFile_Input)));

                        #region Salvo il file nel log DataIntegration JSON
                        PluginRegion = "Salvo il file nel log DataIntegration JSON";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Input uploadFile_Input_Json = new RSMNG.TAUMEDIKA.Model.UploadFile_Input()
                        {
                            MimeType = "text/json",
                            FileName = $"{GlobalOptionSetConstants.res_opt_integrationtypeValues.Export.ToString()}_{GlobalOptionSetConstants.res_opt_integrationactionValues.Ordini.ToString()}_{localTime.ToString("dd_MM_yyyy_HH_mm_ss")}.json",
                            Id = enDataIntegrationId.ToString(),
                            FileSize = jsonBytes.Length,
                            Content = base64Json
                        };

                        string resultUploadJson = Helper.UploadFile(crmServiceProvider.TracingService, crmServiceProvider.Service, res_dataintegration.res_distributionfile, res_dataintegration.logicalName, RSMNG.Plugins.Controller.Serialize<RSMNG.TAUMEDIKA.Model.UploadFile_Input>(uploadFile_Input_Json, typeof(RSMNG.TAUMEDIKA.Model.UploadFile_Input)));
                        #endregion

                        #region Controllo l'esito del salvataggio del file
                        PluginRegion = "Controllo l'esito del salvataggio del file XML";
                        RSMNG.TAUMEDIKA.Model.UploadFile_Output uploadFile_Output = RSMNG.Plugins.Controller.Deserialize<RSMNG.TAUMEDIKA.Model.UploadFile_Output>(resultUploadXml);

                        if (uploadFile_Output?.result != 0)
                        {
                            outResult = "KO";
                            outErrorCode = "02";
                            outMessage = $"{uploadFile_Output.message}";
                        }
                        #endregion

                        if (outResult == "OK")
                        {
                            #region Cambio di stato al log DataIntegration
                            PluginRegion = "Cambio di stato al log DataIntegration";
                            Helper.SetStateCode(crmServiceProvider.Service, res_dataintegration.logicalName, enDataIntegrationId, (int)res_dataintegration.statecodeValues.Inattivo, (int)res_dataintegration.statuscodeValues.Distribuito_StateInattivo);
                            #endregion

                            #region Popolo il parametro di output file
                            PluginRegion = "Popolo il parametro di output file";
                            outFile.Attributes.Add("mimetype", uploadFile_Input_Xml.MimeType);
                            outFile.Attributes.Add("name", uploadFile_Input_Xml.FileName);
                            outFile.Attributes.Add("size", uploadFile_Input_Xml.FileSize);
                            outFile.Attributes.Add("content", uploadFile_Input_Xml.Content);
                            outFile.Attributes.Add("datauri", dataUriXml);
                            #endregion
                        }

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
