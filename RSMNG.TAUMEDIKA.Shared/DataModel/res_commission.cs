namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Provvigione constants.
	/// </summary>
	public sealed class res_commission : EntityGenericConstants
	{
		/// <summary>
		/// res_commission
		/// </summary>
		public static string logicalName => "res_commission";

		/// <summary>
		/// Provvigione
		/// </summary>
		public static string displayName => "Provvigione";

		/// <summary>
		/// Display Name: Dettaglio calcolo provvigioni,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_commissioncalculationdetail => "res_commissioncalculationdetail";

		/// <summary>
		/// Display Name: Provvigione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_commissionid => "res_commissionid";

		/// <summary>
		/// Display Name: Data fine,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_enddate => "res_enddate";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Data inizio,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_startdate => "res_startdate";


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
			Bozza_StateAttivo = 1,
			Calcolata_StateAttivo = 100000002,
			Calcolataerrori_StateAttivo = 100000003,
			Calcoloincorso_StateAttivo = 100000001,
			Inattivo_StateInattivo = 2
		}
	};
}
