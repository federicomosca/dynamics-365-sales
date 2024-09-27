namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Indirizzo constants.
	/// </summary>
	public sealed class res_address : EntityGenericConstants
	{
		/// <summary>
		/// res_address
		/// </summary>
		public static string logicalName => "res_address";

		/// <summary>
		/// Indirizzo
		/// </summary>
		public static string displayName => "Indirizzo";

		/// <summary>
		/// Display Name: ID processo,
		/// Type: Uniqueidentifier,
		/// Description: Contiene l'ID del processo associato all'entità.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Indirizzo,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_addressField => "res_address";

		/// <summary>
		/// Display Name: Indirizzo,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_addressid => "res_addressid";

		/// <summary>
		/// Display Name: Città,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_city => "res_city";

		/// <summary>
		/// Display Name: Nazione,
		/// Type: Lookup,
		/// Related entities: res_country,
		/// Description: 
		/// </summary>
		public static string res_countryid => "res_countryid";

		/// <summary>
		/// Display Name: Cliente,
		/// Type: Customer,
		/// Description: 
		/// </summary>
		public static string res_customerid => "res_customerid";

		/// <summary>
		/// Display Name: Indirizzo scheda cliente,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_iscustomeraddress => "res_iscustomeraddress";

		/// <summary>
		/// Display Name: Default,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isdefault => "res_isdefault";

		/// <summary>
		/// Display Name: Località,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_location => "res_location";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: CAP,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_postalcode => "res_postalcode";

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

		/// <summary>
		/// Values for field Indirizzo scheda cliente
		/// <summary>
		public enum res_iscustomeraddressValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Default
		/// <summary>
		public enum res_isdefaultValues
		{
			No = 0,
			Si = 1
		}
	};
}
