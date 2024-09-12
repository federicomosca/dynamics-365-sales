namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Modalità di pagamento constants.
	/// </summary>
	public sealed class res_paymentmethod : EntityGenericConstants
	{
		/// <summary>
		/// res_paymentmethod
		/// </summary>
		public static string logicalName => "res_paymentmethod";

		/// <summary>
		/// Modalità di pagamento
		/// </summary>
		public static string displayName => "Modalità di pagamento";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_nome => "res_nome";

		/// <summary>
		/// Display Name: Modalità di pagamento,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_paymentmethodid => "res_paymentmethodid";


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
