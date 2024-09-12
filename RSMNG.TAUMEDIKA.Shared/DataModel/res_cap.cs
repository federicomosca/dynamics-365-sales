namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// CAP constants.
	/// </summary>
	public sealed class res_cap : EntityGenericConstants
	{
		/// <summary>
		/// res_cap
		/// </summary>
		public static string logicalName => "res_cap";

		/// <summary>
		/// CAP
		/// </summary>
		public static string displayName => "CAP";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: CAP,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_capid => "res_capid";

		/// <summary>
		/// Display Name: Comune,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_city => "res_city";

		/// <summary>
		/// Display Name: Codice,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_code => "res_code";

		/// <summary>
		/// Display Name: Nazione,
		/// Type: Lookup,
		/// Related entities: res_country,
		/// Description: 
		/// </summary>
		public static string res_countryid => "res_countryid";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Provincia,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_province => "res_province";


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
