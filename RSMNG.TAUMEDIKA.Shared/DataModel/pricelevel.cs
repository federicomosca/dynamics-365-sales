namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Listino prezzi constants.
	/// </summary>
	public sealed class pricelevel : EntityGenericConstants
	{
		/// <summary>
		/// pricelevel
		/// </summary>
		public static string logicalName => "pricelevel";

		/// <summary>
		/// Listino prezzi
		/// </summary>
		public static string displayName => "Listino prezzi";

		/// <summary>
		/// Display Name: Data di inizio,
		/// Type: DateTime,
		/// Description: Primo giorno di validità del listino prezzi.
		/// </summary>
		public static string begindate => "begindate";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Descrizione del listino prezzi.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Data di fine,
		/// Type: DateTime,
		/// Description: Ultimo giorno di validità del listino prezzi.
		/// </summary>
		public static string enddate => "enddate";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Mostra il tasso di conversione della valuta del record. Il tasso di cambio è usato per convertire tutti i campi di tipo money nel record dalla valuta locale alla valuta predefinita del sistema.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Condizioni di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Condizioni di spedizione per il listino prezzi.
		/// </summary>
		public static string freighttermscode => "freighttermscode";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Nome del listino prezzi.
		/// </summary>
		public static string name => "name";

		/// <summary>
		/// Display Name: Organization Id,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Unique identifier for the organization
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Modalità di pagamento ,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Condizioni di pagamento da utilizzare nel listino prezzi.
		/// </summary>
		public static string paymentmethodcode => "paymentmethodcode";

		/// <summary>
		/// Display Name: Listino prezzi,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco del listino prezzi.
		/// </summary>
		public static string pricelevelid => "pricelevelid";

		/// <summary>
		/// Display Name: Default per agenti,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isdefaultforagents => "res_isdefaultforagents";

		/// <summary>
		/// Display Name: Default per sito web,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isdefaultforwebsite => "res_isdefaultforwebsite";

		/// <summary>
		/// Display Name: Import ERP,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_iserpimport => "res_iserpimport";

		/// <summary>
		/// Display Name: Ambito,
		/// Type: Virtual,
		/// Values:
		/// Agenti: 100000000,
		/// Sito Web: 100000001,
		/// Description: 
		/// </summary>
		public static string res_scopetypecodes => "res_scopetypecodes";

		/// <summary>
		/// Display Name: Metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Metodo di spedizione per i prodotti nel listino prezzi.
		/// </summary>
		public static string shippingmethodcode => "shippingmethodcode";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Identificatore univoco della valuta associata al listino prezzi.
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
			Attivo_StateAttivo = 100001,
			Inattivo_StateInattivo = 100002
		}

		/// <summary>
		/// Values for field Condizioni di spedizione
		/// <summary>
		public enum freighttermscodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Modalità di pagamento 
		/// <summary>
		public enum paymentmethodcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Default per agenti
		/// <summary>
		public enum res_isdefaultforagentsValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Default per sito web
		/// <summary>
		public enum res_isdefaultforwebsiteValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Import ERP
		/// <summary>
		public enum res_iserpimportValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Ambito
		/// <summary>
		public enum res_scopetypecodesValues
		{
			Agenti = 100000000,
			SitoWeb = 100000001
		}

		/// <summary>
		/// Values for field Metodo di spedizione
		/// <summary>
		public enum shippingmethodcodeValues
		{
			Valorepredefinito = 1
		}
	};
}
