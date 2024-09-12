namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Condizione di pagamento constants.
	/// </summary>
	public sealed class res_paymentterm : EntityGenericConstants
	{
		/// <summary>
		/// res_paymentterm
		/// </summary>
		public static string logicalName => "res_paymentterm";

		/// <summary>
		/// Condizione di pagamento
		/// </summary>
		public static string displayName => "Condizione di pagamento";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Abilita visibilità banca,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isbankvisible => "res_isbankvisible";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Condizione di pagamento,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_paymenttermid => "res_paymenttermid";


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
		/// Values for field Abilita visibilità banca
		/// <summary>
		public enum res_isbankvisibleValues
		{
			No = 0,
			Si = 1
		}
	};
}
