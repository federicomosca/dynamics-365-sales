using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RSMNG.TAUMEDIKA.Shared.PaymentMethod
{
    public class Utility
    {
    }

    [DataContract]
    public class ImportPaymentDanea
    {
        [DataMember] public string CodCliente { get; set; }
        [DataMember] public LookUp Cliente { get; set; }
        [DataMember] public string Soggetto { get; set; }
        [DataMember] public string Data { get; set; }
        [DataMember] public string DataScadenza { get; set; }
        [DataMember] public string NProtDoc { get; set; }
        [DataMember] public string Descrizione { get; set; }
        [DataMember] public string DataDocumento { get; set; }
        [DataMember] public decimal? ImportoDoc { get; set; }
        [DataMember] public LookUp Pagamento { get; set; }
        [DataMember] public string CoordBancarie { get; set; }
        [DataMember] public string CodAgente { get; set; }
        [DataMember] public LookUp Agente { get; set; }
        [DataMember] public string Commento { get; set; }
        [DataMember] public decimal? Importo { get; set; }
        [DataMember] public bool Saldato { get; set; }
        [DataMember] public LookUp ModPagamento { get; set; }
        [DataMember] public LookUp Risorsa { get; set; }
        [DataMember] public string RifPagamento { get; set; }
        [DataMember] public string DataSollecito { get; set; }
        [DataMember] public string DescrSollecito { get; set; }
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
