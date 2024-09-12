namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Coordinata Bancaria constants.
	/// </summary>
	public sealed class res_bankdetails : EntityGenericConstants
	{
		/// <summary>
		/// res_bankdetails
		/// </summary>
		public static string logicalName => "res_bankdetails";

		/// <summary>
		/// Coordinata Bancaria
		/// </summary>
		public static string displayName => "Coordinata Bancaria";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Coordinata Bancaria,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_bankdetailsid => "res_bankdetailsid";

		/// <summary>
		/// Display Name: IBAN,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_iban => "res_iban";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Sede,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_site => "res_site";


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
