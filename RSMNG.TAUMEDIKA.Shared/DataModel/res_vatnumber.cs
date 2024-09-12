namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Codice IVA constants.
	/// </summary>
	public sealed class res_vatnumber : EntityGenericConstants
	{
		/// <summary>
		/// res_vatnumber
		/// </summary>
		public static string logicalName => "res_vatnumber";

		/// <summary>
		/// Codice IVA
		/// </summary>
		public static string displayName => "Codice IVA";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Codice,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_code => "res_code";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_description => "res_description";

		/// <summary>
		/// Display Name: Name,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Aliquota,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_rate => "res_rate";

		/// <summary>
		/// Display Name: Codice IVA,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_vatnumberid => "res_vatnumberid";

		/// <summary>
		/// Display Name: Natura,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_vattype => "res_vattype";


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
