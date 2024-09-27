using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RSMNG.TAUMEDIKA.Bot.CustomApi.Model.ExportCustomers
{


    [DataContract]
    public class Customer
    {
        [DataMember(Name = "Cod.")]
        public string Cod { get; set; }

        [DataMember(Name = "Codice fiscale")]
        public string CodiceFiscale { get; set; }

        [DataMember(Name = "Partita Iva")]
        public string PartitaIva { get; set; }

        [DataMember(Name = "Denominazione")]
        public string Denominazione { get; set; }

        [DataMember(Name = "Indirizzo")]
        public string Indirizzo { get; set; }

        [DataMember(Name = "Cap")]
        public string Cap { get; set; }

        [DataMember(Name = "Città")]
        public string Citta { get; set; }

        [DataMember(Name = "Prov.")]
        public string Prov { get; set; }

        [DataMember(Name = "Regione")]
        public string Regione { get; set; }

        [DataMember(Name = "Nazione")]
        public string Nazione { get; set; }

        [DataMember(Name = "Cod. destinatario Fatt. elettr.")]
        public string CodDestinatarioFattElettr { get; set; }

        [DataMember(Name = "Referente")]
        public string Referente { get; set; }

        [DataMember(Name = "Tel.")]
        public string Tel { get; set; }

        [DataMember(Name = "Cell")]
        public string Cell { get; set; }

        [DataMember(Name = "Fax")]
        public string Fax { get; set; }

        [DataMember(Name = "e-mail")]
        public string Email { get; set; }

        [DataMember(Name = "Pec")]
        public string Pec { get; set; }

        [DataMember(Name = "Agente")]
        public string Agente { get; set; }

        [DataMember(Name = "Pagamento")]
        public string Pagamento { get; set; }

        [DataMember(Name = "Ns Banca")]
        public string NsBanca { get; set; }

        [DataMember(Name = "Extra 6")]
        public string Extra6 { get; set; }

        [DataMember(Name = "Note")]
        public string Note { get; set; }
    }

    public sealed class ParametersIn
    {
        public static string debug => "debug";
        public static string filters => "filters";
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
        public static string statecodes => "statecodes";
    }

    public sealed class StateCodes
    {
        public static string statecode => "statecode";
    }
}
