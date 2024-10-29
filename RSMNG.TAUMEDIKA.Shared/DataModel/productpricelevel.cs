namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Voce di listino constants.
	/// </summary>
	public sealed class productpricelevel : EntityGenericConstants
	{
		/// <summary>
		/// productpricelevel
		/// </summary>
		public static string logicalName => "productpricelevel";

		/// <summary>
		/// Voce di listino
		/// </summary>
		public static string displayName => "Voce di listino";

		/// <summary>
		/// Display Name: Importo,
		/// Type: Money,
		/// Description: Importo in valuta per il listino prezzi.
		/// </summary>
		public static string amount => "amount";

		/// <summary>
		/// Display Name: Importo (Base),
		/// Type: Money,
		/// Description: Value of the Importo in base currency.
		/// </summary>
		public static string amount_base => "amount_base";

		/// <summary>
		/// Display Name: Elenco sconti,
		/// Type: Lookup,
		/// Related entities: discounttype,
		/// Description: Identificatore univoco dell'elenco di sconti associato al listino prezzi.
		/// </summary>
		public static string discounttypeid => "discounttypeid";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Mostra il tasso di conversione della valuta del record. Il tasso di cambio è usato per convertire tutti i campi di tipo money nel record dalla valuta locale alla valuta predefinita del sistema.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Organizzazione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'organizzazione associata al listino prezzi.
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Percentuale,
		/// Type: Decimal,
		/// Description: Percentuale per il listino prezzi.
		/// </summary>
		public static string percentage => "percentage";

		/// <summary>
		/// Display Name: Listino prezzi,
		/// Type: Lookup,
		/// Related entities: pricelevel,
		/// Description: Identificatore univoco del listino prezzi associato alla voce del listino prezzi.
		/// </summary>
		public static string pricelevelid => "pricelevelid";

		/// <summary>
		/// Display Name: Metodo di determinazione dei prezzi,
		/// Type: Picklist,
		/// Values:
		/// Importo forfettario: 1,
		/// % prezzo di listino: 2,
		/// Costo corrente con % ricarico (mark up): 3,
		/// Costo corrente con % margine: 4,
		/// Costo medio con % ricarico (mark up): 5,
		/// Costo medio con % margine: 6,
		/// Description: Metodo di determinazione dei prezzi applicato al listino.
		/// </summary>
		public static string pricingmethodcode => "pricingmethodcode";

		/// <summary>
		/// Display Name: Process Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the process associated with the entity.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Prodotto,
		/// Type: Lookup,
		/// Related entities: product,
		/// Description: Prodotto associato al listino prezzi.
		/// </summary>
		public static string productid => "productid";

		/// <summary>
		/// Display Name: ID prodotto,
		/// Type: String,
		/// Description: Numero definito dall'utente del prodotto.
		/// </summary>
		public static string productnumber => "productnumber";

		/// <summary>
		/// Display Name: Listino prezzi prodotto,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco del listino prezzi.
		/// </summary>
		public static string productpricelevelid => "productpricelevelid";

		/// <summary>
		/// Display Name: Quantità minima di vendita,
		/// Type: Picklist,
		/// Values:
		/// Non definita: 1,
		/// Intera: 2,
		/// Intera e frazionaria: 3,
		/// Description: Quantità minima di prodotto che deve essere venduta per un determinato listino prezzi.
		/// </summary>
		public static string quantitysellingcode => "quantitysellingcode";

		/// <summary>
		/// Display Name: Origine,
		/// Type: Picklist,
		/// Values:
		/// Dynamics: 100000000,
		/// ERP: 100000001,
		/// Description: 
		/// </summary>
		public static string res_origine => "res_origine";

		/// <summary>
		/// Display Name: Valore di arrotondamento,
		/// Type: Money,
		/// Description: Valore di arrotondamento per il listino prezzi.
		/// </summary>
		public static string roundingoptionamount => "roundingoptionamount";

		/// <summary>
		/// Display Name: Valore di arrotondamento (Base),
		/// Type: Money,
		/// Description: Value of the Valore di arrotondamento in base currency.
		/// </summary>
		public static string roundingoptionamount_base => "roundingoptionamount_base";

		/// <summary>
		/// Display Name: Opzione di arrotondamento,
		/// Type: Picklist,
		/// Values:
		/// Termina con: 1,
		/// Multiplo di: 2,
		/// Description: Opzione di arrotondamento per il listino prezzi.
		/// </summary>
		public static string roundingoptioncode => "roundingoptioncode";

		/// <summary>
		/// Display Name: Regola di arrotondamento,
		/// Type: Picklist,
		/// Values:
		/// Nessuno: 1,
		/// Per eccesso: 2,
		/// Per difetto: 3,
		/// Al valore più vicino: 4,
		/// Description: Regola per gli arrotondamenti del listino prezzi.
		/// </summary>
		public static string roundingpolicycode => "roundingpolicycode";

		/// <summary>
		/// Display Name: (Deprecated) Stage Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the stage where the entity is located.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Scegli la valuta locale per il record per assicurarti che i budget vengano espressi nella valuta corretta.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";

		/// <summary>
		/// Display Name: (Deprecated) Traversed Path,
		/// Type: String,
		/// Description: A comma separated list of string values representing the unique identifiers of stages in a Business Process Flow Instance in the order that they occur.
		/// </summary>
		public static string traversedpath => "traversedpath";

		/// <summary>
		/// Display Name: Unità,
		/// Type: Lookup,
		/// Related entities: uom,
		/// Description: Identificatore univoco dell'unità per il listino prezzi.
		/// </summary>
		public static string uomid => "uomid";

		/// <summary>
		/// Display Name: ID unità di vendita,
		/// Type: Lookup,
		/// Related entities: uomschedule,
		/// Description: Identificatore univoco dell'unità di vendita per il listino prezzi.
		/// </summary>
		public static string uomscheduleid => "uomscheduleid";


		/// <summary>
		/// Values for field Metodo di determinazione dei prezzi
		/// <summary>
		public enum pricingmethodcodeValues
		{
			Costocorrenteconmargine = 4,
			Costocorrenteconricaricomarkup = 3,
			Costomedioconmargine = 6,
			Costomedioconricaricomarkup = 5,
			Importoforfettario = 1,
			prezzodilistino = 2
		}

		/// <summary>
		/// Values for field Quantità minima di vendita
		/// <summary>
		public enum quantitysellingcodeValues
		{
			Intera = 2,
			Interaefrazionaria = 3,
			Nondefinita = 1
		}

		/// <summary>
		/// Values for field Origine
		/// <summary>
		public enum res_origineValues
		{
			Dynamics = 100000000,
			ERP = 100000001
		}

		/// <summary>
		/// Values for field Opzione di arrotondamento
		/// <summary>
		public enum roundingoptioncodeValues
		{
			Multiplodi = 2,
			Terminacon = 1
		}

		/// <summary>
		/// Values for field Regola di arrotondamento
		/// <summary>
		public enum roundingpolicycodeValues
		{
			Alvalorepiuvicino = 4,
			Nessuno = 1,
			Perdifetto = 3,
			Pereccesso = 2
		}
	};
}
