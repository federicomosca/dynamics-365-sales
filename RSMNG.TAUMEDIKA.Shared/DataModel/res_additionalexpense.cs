namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Spesa accessoria constants.
	/// </summary>
	public sealed class res_additionalexpense : EntityGenericConstants
	{
		/// <summary>
		/// res_additionalexpense
		/// </summary>
		public static string logicalName => "res_additionalexpense";

		/// <summary>
		/// Spesa accessoria
		/// </summary>
		public static string displayName => "Spesa accessoria";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Tasso di cambio per la valuta associata all'entità rispetto alla valuta di base.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Spesa accessoria,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_additionalexpenseid => "res_additionalexpenseid";

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
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_description => "res_description";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

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
