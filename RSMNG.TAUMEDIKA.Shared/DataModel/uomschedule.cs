namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Unità di vendita constants.
	/// </summary>
	public sealed class uomschedule : EntityGenericConstants
	{
		/// <summary>
		/// uomschedule
		/// </summary>
		public static string logicalName => "uomschedule";

		/// <summary>
		/// Unità di vendita
		/// </summary>
		public static string displayName => "Unità di vendita";

		/// <summary>
		/// Display Name: Nome unità di base,
		/// Type: String,
		/// Description: Nome dell'unità di base.
		/// </summary>
		public static string baseuomname => "baseuomname";

		/// <summary>
		/// Display Name: Created By (External Party),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Shows the external party who created the record.
		/// </summary>
		public static string createdbyexternalparty => "createdbyexternalparty";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Descrizione dell'unità di vendita.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Modified By (External Party),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Shows the external party who modified the record.
		/// </summary>
		public static string modifiedbyexternalparty => "modifiedbyexternalparty";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Nome dell'unità di vendita.
		/// </summary>
		public static string name => "name";

		/// <summary>
		/// Display Name: Organization Id,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Unique identifier for the organization
		/// </summary>
		public static string organizationid => "organizationid";

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
		/// Display Name: Unità di vendita,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'unità di vendita.
		/// </summary>
		public static string uomscheduleid => "uomscheduleid";


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
		/// Values for field Default
		/// <summary>
		public enum res_isdefaultValues
		{
			No = 0,
			Si = 1
		}
	};
}
