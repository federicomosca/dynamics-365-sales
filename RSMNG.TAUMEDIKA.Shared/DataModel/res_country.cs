namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Nazione constants.
	/// </summary>
	public sealed class res_country : EntityGenericConstants
	{
		/// <summary>
		/// res_country
		/// </summary>
		public static string logicalName => "res_country";

		/// <summary>
		/// Nazione
		/// </summary>
		public static string displayName => "Nazione";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Nazione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_countryid => "res_countryid";

		/// <summary>
		/// Display Name: Codice ISO (Apha3),
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_isonumber => "res_isonumber";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";


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
