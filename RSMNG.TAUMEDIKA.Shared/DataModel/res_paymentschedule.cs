namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Scadenziario Pagamenti constants.
	/// </summary>
	public sealed class res_paymentschedule : EntityGenericConstants
	{
		/// <summary>
		/// res_paymentschedule
		/// </summary>
		public static string logicalName => "res_paymentschedule";

		/// <summary>
		/// Scadenziario Pagamenti
		/// </summary>
		public static string displayName => "Scadenziario Pagamenti";

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
		/// Display Name: Importo,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_amount => "res_amount";

		/// <summary>
		/// Display Name: Importo (base),
		/// Type: Money,
		/// Description: Valore di Importo nella valuta di base.
		/// </summary>
		public static string res_amount_base => "res_amount_base";

		/// <summary>
		/// Display Name: Coordinata bancaria,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_bankdetails => "res_bankdetails";

		/// <summary>
		/// Display Name: Cliente,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: 
		/// </summary>
		public static string res_clientid => "res_clientid";

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
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_description => "res_description";

		/// <summary>
		/// Display Name: Importo Documento,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_documentamount => "res_documentamount";

		/// <summary>
		/// Display Name: Importo Documento (base),
		/// Type: Money,
		/// Description: Valore di Importo Documento nella valuta di base.
		/// </summary>
		public static string res_documentamount_base => "res_documentamount_base";

		/// <summary>
		/// Display Name: Commento documento,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_documentcomment => "res_documentcomment";

		/// <summary>
		/// Display Name: Data documento,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_documentdate => "res_documentdate";

		/// <summary>
		/// Display Name: N. Protocollo documento,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_documentprotocolnumber => "res_documentprotocolnumber";

		/// <summary>
		/// Display Name: Data scadenza,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_expirationdate => "res_expirationdate";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_nome => "res_nome";

		/// <summary>
		/// Display Name: Modalità pagamento (testo),
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_paymentmethod => "res_paymentmethod";

		/// <summary>
		/// Display Name: Modalità pagamento,
		/// Type: Lookup,
		/// Related entities: res_paymentmethod,
		/// Description: 
		/// </summary>
		public static string res_paymentmethodid => "res_paymentmethodid";

		/// <summary>
		/// Display Name: Rif. Pagamento,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_paymentreference => "res_paymentreference";

		/// <summary>
		/// Display Name: Scadenziario Pagamenti,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_paymentscheduleid => "res_paymentscheduleid";

		/// <summary>
		/// Display Name: Condizione di pagamento,
		/// Type: Lookup,
		/// Related entities: res_paymentterm,
		/// Description: 
		/// </summary>
		public static string res_paymenttermid => "res_paymenttermid";

		/// <summary>
		/// Display Name: Data sollecito,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_reminderdate => "res_reminderdate";

		/// <summary>
		/// Display Name: Descrizione sollecito,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_reminderdescription => "res_reminderdescription";

		/// <summary>
		/// Display Name: Risorsa,
		/// Type: Lookup,
		/// Related entities: res_bankdetails,
		/// Description: 
		/// </summary>
		public static string res_resourceid => "res_resourceid";

		/// <summary>
		/// Display Name: Soggetto,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_subject => "res_subject";

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
	};
}
