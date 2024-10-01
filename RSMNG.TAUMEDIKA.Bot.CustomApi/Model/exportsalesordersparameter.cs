using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ExportSalesOrders
{

    [XmlRoot(ElementName = "Company")]
    public class Company
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "CostVatCode")]
    public class CostVatCode
    {
        [XmlAttribute(AttributeName = "Perc")]
        public string Perc { get; set; }

        [XmlAttribute(AttributeName = "Class")]
        public string Class { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "VatCode")]
    public class VatCode
    {
        [XmlAttribute(AttributeName = "Perc")]
        public string Perc { get; set; }

        [XmlAttribute(AttributeName = "Class")]
        public string Class { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }
    }

    [XmlRoot(ElementName = "Row")]
    public class Row
    {
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "Qty")]
        public string Qty { get; set; }

        [XmlElement(ElementName = "Um")]
        public string Um { get; set; }

        [XmlElement(ElementName = "Price")]
        public string Price { get; set; }

        [XmlElement(ElementName = "Discounts")]
        public string Discounts { get; set; }

        [XmlElement(ElementName = "EcoFee")]
        public string EcoFee { get; set; }

        [XmlElement(ElementName = "VatCode")]
        public VatCode VatCode { get; set; }

        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }

        [XmlElement(ElementName = "Stock")]
        public string Stock { get; set; }

        [XmlElement(ElementName = "Notes")]
        public string Notes { get; set; }
    }

    [XmlRoot(ElementName = "Rows")]
    public class Rows
    {
        [XmlElement(ElementName = "Row")]
        public List<Row> Row { get; set; }
    }

    [XmlRoot(ElementName = "Document")]
    public class Document
    {
        [XmlElement(ElementName = "CustomerCode")]
        public string CustomerCode { get; set; }

        [XmlElement(ElementName = "CustomerWebLogin")]
        public string CustomerWebLogin { get; set; }

        [XmlElement(ElementName = "CustomerName")]
        public string CustomerName { get; set; }

        [XmlElement(ElementName = "CustomerAddress")]
        public string CustomerAddress { get; set; }

        [XmlElement(ElementName = "CustomerPostcode")]
        public string CustomerPostcode { get; set; }

        [XmlElement(ElementName = "CustomerCity")]
        public string CustomerCity { get; set; }

        [XmlElement(ElementName = "CustomerProvince")]
        public string CustomerProvince { get; set; }

        [XmlElement(ElementName = "CustomerCountry")]
        public string CustomerCountry { get; set; }

        [XmlElement(ElementName = "CustomerFiscalCode")]
        public string CustomerFiscalCode { get; set; }

        [XmlElement(ElementName = "CustomerReference")]
        public string CustomerReference { get; set; }

        [XmlElement(ElementName = "CustomerTel")]
        public string CustomerTel { get; set; }

        [XmlElement(ElementName = "CustomerCellPhone")]
        public string CustomerCellPhone { get; set; }

        [XmlElement(ElementName = "CustomerFax")]
        public string CustomerFax { get; set; }

        [XmlElement(ElementName = "CustomerEmail")]
        public string CustomerEmail { get; set; }

        [XmlElement(ElementName = "CustomerPec")]
        public string CustomerPec { get; set; }

        [XmlElement(ElementName = "CustomerEInvoiceDestCode")]
        public string CustomerEInvoiceDestCode { get; set; }

        [XmlElement(ElementName = "DeliveryName")]
        public string DeliveryName { get; set; }

        [XmlElement(ElementName = "DeliveryAddress")]
        public string DeliveryAddress { get; set; }

        [XmlElement(ElementName = "DeliveryPostcode")]
        public string DeliveryPostcode { get; set; }

        [XmlElement(ElementName = "DeliveryCity")]
        public string DeliveryCity { get; set; }

        [XmlElement(ElementName = "DeliveryProvince")]
        public string DeliveryProvince { get; set; }

        [XmlElement(ElementName = "DeliveryCountry")]
        public string DeliveryCountry { get; set; }

        [XmlElement(ElementName = "DocumentType")]
        public string DocumentType { get; set; }

        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }

        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }

        [XmlElement(ElementName = "Numbering")]
        public string Numbering { get; set; }

        [XmlElement(ElementName = "CostDescription")]
        public string CostDescription { get; set; }

        [XmlElement(ElementName = "CostVatCode")]
        public CostVatCode CostVatCode { get; set; }

        [XmlElement(ElementName = "CostAmount")]
        public string CostAmount { get; set; }

        [XmlElement(ElementName = "ContribDescription")]
        public string ContribDescription { get; set; }

        [XmlElement(ElementName = "ContribPerc")]
        public string ContribPerc { get; set; }

        [XmlElement(ElementName = "ContribSubjectToWithholdingTax")]
        public string ContribSubjectToWithholdingTax { get; set; }

        [XmlElement(ElementName = "ContribAmount")]
        public string ContribAmount { get; set; }

        [XmlElement(ElementName = "ContribVatCode")]
        public string ContribVatCode { get; set; }

        [XmlElement(ElementName = "TotalWithoutTax")]
        public string TotalWithoutTax { get; set; }

        [XmlElement(ElementName = "VatAmount")]
        public string VatAmount { get; set; }

        [XmlElement(ElementName = "WithholdingTaxAmount")]
        public string WithholdingTaxAmount { get; set; }

        [XmlElement(ElementName = "WithholdingTaxAmountB")]
        public string WithholdingTaxAmountB { get; set; }

        [XmlElement(ElementName = "WithholdingTaxNameB")]
        public string WithholdingTaxNameB { get; set; }

        [XmlElement(ElementName = "Total")]
        public string Total { get; set; }

        [XmlElement(ElementName = "PriceList")]
        public string PriceList { get; set; }

        [XmlElement(ElementName = "PricesIncludeVat")]
        public string PricesIncludeVat { get; set; }

        [XmlElement(ElementName = "TotalSubjectToWithholdingTax")]
        public string TotalSubjectToWithholdingTax { get; set; }

        [XmlElement(ElementName = "WithholdingTaxPerc")]
        public string WithholdingTaxPerc { get; set; }

        [XmlElement(ElementName = "WithholdingTaxPerc2")]
        public string WithholdingTaxPerc2 { get; set; }

        [XmlElement(ElementName = "PaymentName")]
        public string PaymentName { get; set; }

        [XmlElement(ElementName = "PaymentBank")]
        public string PaymentBank { get; set; }

        [XmlElement(ElementName = "PaymentAdvanceAmount")]
        public string PaymentAdvanceAmount { get; set; }

        [XmlElement(ElementName = "Carrier")]
        public string Carrier { get; set; }

        [XmlElement(ElementName = "TransportReason")]
        public string TransportReason { get; set; }

        [XmlElement(ElementName = "GoodsAppearance")]
        public string GoodsAppearance { get; set; }

        [XmlElement(ElementName = "NumOfPieces")]
        public string NumOfPieces { get; set; }

        [XmlElement(ElementName = "TransportDateTime")]
        public string TransportDateTime { get; set; }

        [XmlElement(ElementName = "ShipmentTerms")]
        public string ShipmentTerms { get; set; }

        [XmlElement(ElementName = "TransportedWeight")]
        public string TransportedWeight { get; set; }

        [XmlElement(ElementName = "TrackingNumber")]
        public string TrackingNumber { get; set; }

        [XmlElement(ElementName = "InternalComment")]
        public string InternalComment { get; set; }

        [XmlElement(ElementName = "CustomField1")]
        public string CustomField1 { get; set; }

        [XmlElement(ElementName = "CustomField2")]
        public string CustomField2 { get; set; }

        [XmlElement(ElementName = "CustomField3")]
        public string CustomField3 { get; set; }

        [XmlElement(ElementName = "CustomField4")]
        public string CustomField4 { get; set; }

        [XmlElement(ElementName = "FootNotes")]
        public string FootNotes { get; set; }

        [XmlElement(ElementName = "ExpectedConclusionDate")]
        public string ExpectedConclusionDate { get; set; }

        [XmlElement(ElementName = "SalesAgent")]
        public string SalesAgent { get; set; }

        [XmlElement(ElementName = "Rows")]
        public Rows Rows { get; set; }
    }

    [XmlRoot(ElementName = "Documents")]
    public class Documents
    {
        [XmlElement(ElementName = "Document")]
        public List<Document> Document { get; set; }
    }

    [XmlRoot(ElementName = "EasyfattDocuments")]
    public class EasyfattDocuments
    {
        [XmlElement(ElementName = "Company")]
        public Company Company { get; set; }

        [XmlElement(ElementName = "Documents")]
        public Documents Documents { get; set; }

        [XmlAttribute(AttributeName = "AppVersion")]
        public string AppVersion { get; set; }

        [XmlAttribute(AttributeName = "Creator")]
        public string Creator { get; set; }

        [XmlAttribute(AttributeName = "CreatorUrl")]
        public string CreatorUrl { get; set; }

        [XmlAttribute(AttributeName = "xmlns:xsi")]
        public string XmlnsXsi { get; set; }

        [XmlAttribute(AttributeName = "xsi:noNamespaceSchemaLocation")]
        public string XsiNoNamespaceSchemaLocation { get; set; }
    }

    public sealed class ParametersIn
    {
        public static string debug => "debug";
        public static string filters => "filters";
        public static string configurations => "configurations";
    }

    public sealed class ParametersOut
    {
        public static string result => "result";
        public static string error => "error";
        public static string file => "file";
    }

    public sealed class Filters
    {
        public static string lastxdays => "lastxdays";
        public static string statuscodes => "statuscodes";
    }

    public sealed class Configurations
    {
        public static string appversion => "appversion";
        public static string creator => "creator";
        public static string creatorurl => "creatorurl";
        public static string xmlnsxsi => "xmlns:xsi";
        public static string xsinonamespaceschemalocation => "xsinonamespaceschemalocation";
        public static string companyname => "companyname";
    }

    public sealed class StatusCodes
    {
        public static string statuscode => "statuscode";
    }

}
