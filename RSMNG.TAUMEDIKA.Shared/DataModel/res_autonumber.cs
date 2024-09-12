namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Auto Number constants.
	/// </summary>
	public sealed class res_autonumber : EntityGenericConstants
	{
		/// <summary>
		/// res_autonumber
		/// </summary>
		public static string logicalName => "res_autonumber";

		/// <summary>
		/// Auto Number
		/// </summary>
		public static string displayName => "Auto Number";

		/// <summary>
		/// Display Name: ID organizzazione,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Identificatore univoco dell'organizzazione
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Auto Number,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_autonumberid => "res_autonumberid";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_descrizione => "res_descrizione";

		/// <summary>
		/// Display Name: Dummy,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_dummy => "res_dummy";

		/// <summary>
		/// Display Name: Last Deploy,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_lastdeploy => "res_lastdeploy";

		/// <summary>
		/// Display Name: Last Number,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string res_lastnumber => "res_lastnumber";

		/// <summary>
		/// Display Name: Length,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string res_length => "res_length";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Prefix,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_prefix => "res_prefix";

		/// <summary>
		/// Display Name: Prefix Separator,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_prefixseparator => "res_prefixseparator";

		/// <summary>
		/// Display Name: Seed,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string res_seed => "res_seed";

		/// <summary>
		/// Display Name: Suffix,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_suffix => "res_suffix";

		/// <summary>
		/// Display Name: Suffix Random,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_suffixrandom => "res_suffixrandom";

		/// <summary>
		/// Display Name: Suffix Separator,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_suffixseparator => "res_suffixseparator";


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
		/// Values for field Suffix Random
		/// <summary>
		public enum res_suffixrandomValues
		{
			No = 0,
			Si = 1
		}
	};
}
