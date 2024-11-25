using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.Document
{
    public class Utility
    {
    }

    [DataContract]
    public class ImportReceiptDanea
    {
        [DataMember] public Option TipoDoc { get; set; } = new Option() { Text = "Ricevuta Fiscale", Value = 100000001, ExternalValue = null };
        [DataMember] public string CodCliente { get; set; }
        [DataMember] public string NomeCliente { get; set; }
        [DataMember] public LookUp Cliente { get; set; }
        [DataMember] public string Data { get; set; }
        [DataMember] public string Anno { get; set; }
        [DataMember] public string NDoc { get; set; }
        [DataMember] public decimal? DaSaldare { get; set; }
        [DataMember] public decimal? TotNettoIva { get; set; }
        [DataMember] public string CodAgente { get; set; }
        [DataMember] public LookUp Agente { get; set; }
        [DataMember] public string DataUltimoPag { get; set; }
        [DataMember] public decimal? Iva { get; set; }
        [DataMember] public decimal? TotDoc { get; set; }
        [DataMember] public LookUp Pagamento { get; set; }
        [DataMember] public LookUp CoordBancarie { get; set; }
        [DataMember] public string Commento { get; set; }
    }

    [DataContract]
    public class ImportInvoiceDanea
    {
        [DataMember] public Option TipoDoc { get; set; } = new Option() { Text = "Fattura", Value = 100000000, ExternalValue = null };
        [DataMember] public string CodCliente { get; set; }
        [DataMember] public string NomeCliente { get; set; }
        [DataMember] public LookUp Cliente { get; set; }
        [DataMember] public string Data { get; set; }
        [DataMember] public string Anno { get; set; }
        [DataMember] public string NDoc { get; set; }
        [DataMember] public decimal? DaSaldare { get; set; }
        [DataMember] public decimal? TotNettoIva { get; set; }
        [DataMember] public string CodAgente { get; set; }
        [DataMember] public LookUp Agente { get; set; }
        [DataMember] public string DataUltimoPag { get; set; }
        [DataMember] public decimal? Iva { get; set; }
        [DataMember] public decimal? TotDoc { get; set; }
        [DataMember] public LookUp Pagamento { get; set; }
        [DataMember] public LookUp CoordBancarie { get; set; }
        [DataMember] public string Commento { get; set; }
    }

    [DataContract]
    public class Option
    {
        [DataMember] public int? Value { get; set; }
        [DataMember] public string ExternalValue { get; set; }
        [DataMember] public string Text { get; set; }
    }

    [DataContract]
    public class LookUp
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Text { get; set; }
        [DataMember] public string Entity { get; set; }
    }

}
