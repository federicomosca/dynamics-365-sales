namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Documento constants.
	/// </summary>
	public sealed class res_document : EntityGenericConstants
	{
		/// <summary>
		/// res_document
		/// </summary>
		public static string logicalName => "res_document";

		/// <summary>
		/// Documento
		/// </summary>
		public static string displayName => "Documento";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Tasso di cambio per la valuta associata all'entità rispetto alla valuta di base.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Agente,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_agent => "res_agent";

		/// <summary>
		/// Display Name: Provvigione Agente,
		/// Type: Lookup,
		/// Related entities: res_agentcommission,
		/// Description: 
		/// </summary>
		public static string res_agentcommissionid => "res_agentcommissionid";

		/// <summary>
		/// Display Name: Coordinata Bancaria,
		/// Type: Lookup,
		/// Related entities: res_bankdetails,
		/// Description: 
		/// </summary>
		public static string res_bankdetailsid => "res_bankdetailsid";

		/// <summary>
		/// Display Name: Provvigione  Calcolata,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_calculatedcommission => "res_calculatedcommission";

		/// <summary>
		/// Display Name: Provvigione  Calcolata (base),
		/// Type: Money,
		/// Description: Valore di Provvigione  Calcolata nella valuta di base.
		/// </summary>
		public static string res_calculatedcommission_base => "res_calculatedcommission_base";

		/// <summary>
		/// Display Name: Cliente,
		/// Type: Customer,
		/// Description: 
		/// </summary>
		public static string res_customerid => "res_customerid";

		/// <summary>
		/// Display Name: Codice cliente,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_customernumber => "res_customernumber";

		/// <summary>
		/// Display Name: Data,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_date => "res_date";

		/// <summary>
		/// Display Name: Documento,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_documentid => "res_documentid";

		/// <summary>
		/// Display Name: N. Documento,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_documentnumber => "res_documentnumber";

		/// <summary>
		/// Display Name: Totale documento,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_documenttotal => "res_documenttotal";

		/// <summary>
		/// Display Name: Totale documento (base),
		/// Type: Money,
		/// Description: Valore di Totale documento nella valuta di base.
		/// </summary>
		public static string res_documenttotal_base => "res_documenttotal_base";

		/// <summary>
		/// Display Name: Tipo Documento,
		/// Type: Picklist,
		/// Values:
		/// Fattura: 100000000,
		/// Ricevuta Fiscale: 100000001,
		/// Description: 
		/// </summary>
		public static string res_documenttypecode => "res_documenttypecode";

		/// <summary>
		/// Display Name: Escluso dal calcolo provvigioni,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isexcludedfromcalculation => "res_isexcludedfromcalculation";

		/// <summary>
		/// Display Name: Ancora da saldare,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_ispendingpayment => "res_ispendingpayment";

		/// <summary>
		/// Display Name: Data ultimo pagamento,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_lastpaymentdate => "res_lastpaymentdate";

		/// <summary>
		/// Display Name: Tot. Netto IVA,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_nettotalexcludingvat => "res_nettotalexcludingvat";

		/// <summary>
		/// Display Name: Tot. Netto IVA (base),
		/// Type: Money,
		/// Description: Valore di Tot. Netto IVA nella valuta di base.
		/// </summary>
		public static string res_nettotalexcludingvat_base => "res_nettotalexcludingvat_base";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_nome => "res_nome";

		/// <summary>
		/// Display Name: Nota,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_note => "res_note";

		/// <summary>
		/// Display Name: Condizione di pagamento,
		/// Type: Lookup,
		/// Related entities: res_paymentterm,
		/// Description: 
		/// </summary>
		public static string res_paymenttermid => "res_paymenttermid";

		/// <summary>
		/// Display Name: IVA,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_vat => "res_vat";

		/// <summary>
		/// Display Name: IVA (base),
		/// Type: Money,
		/// Description: Valore di IVA nella valuta di base.
		/// </summary>
		public static string res_vat_base => "res_vat_base";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Identificatore univoco della valuta associata all'entità.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";


		/// <summary>
		/// Values for field Stato
		/// <summary>
		public new enum statecodeValues
		{
			Attivo = 0,
			Inattivo = 1
		}

		/// <summary>
		/// Values for field Motivo stato
		/// <summary>
		public new enum statuscodeValues
		{
			Attivo_StateAttivo = 1,
			Inattivo_StateInattivo = 2
		}

		/// <summary>
		/// Values for field Tipo Documento
		/// <summary>
		public enum res_documenttypecodeValues
		{
			Fattura = 100000000,
			RicevutaFiscale = 100000001
		}

		/// <summary>
		/// Values for field Escluso dal calcolo provvigioni
		/// <summary>
		public enum res_isexcludedfromcalculationValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Ancora da saldare
		/// <summary>
		public enum res_ispendingpaymentValues
		{
			No = 0,
			Si = 1
		}
	};
}
