namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Unità constants.
	/// </summary>
	public sealed class uom : EntityGenericConstants
	{
		/// <summary>
		/// uom
		/// </summary>
		public static string logicalName => "uom";

		/// <summary>
		/// Unità
		/// </summary>
		public static string displayName => "Unità";

		/// <summary>
		/// Display Name: Unità di base,
		/// Type: Lookup,
		/// Related entities: uom,
		/// Description: Scegli l'unità di base o primaria su cui si basa l'unità.
		/// </summary>
		public static string baseuom => "baseuom";

		/// <summary>
		/// Display Name: Created By (External Party),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Shows the external party who created the record.
		/// </summary>
		public static string createdbyexternalparty => "createdbyexternalparty";

		/// <summary>
		/// Display Name: Unità di base dell'unità di vendita,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se l'unità è l'unità di base per l'unità di vendita associato.
		/// </summary>
		public static string isschedulebaseuom => "isschedulebaseuom";

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
		/// Description: Digitare un titolo o un nome descrittivo per l'unità di misura.
		/// </summary>
		public static string name => "name";

		/// <summary>
		/// Display Name: Organizzazione ,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'organizzazione associata all'unità di misura.
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Quantità,
		/// Type: Decimal,
		/// Description: Quantità di unità per il prodotto.
		/// </summary>
		public static string quantity => "quantity";

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
		/// Display Name: Unità,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'unità.
		/// </summary>
		public static string uomid => "uomid";

		/// <summary>
		/// Display Name: Unità di vendita,
		/// Type: Lookup,
		/// Related entities: uomschedule,
		/// Description: Scegli l'ID dell'unità di vendita a cui l'unità è associata.
		/// </summary>
		public static string uomscheduleid => "uomscheduleid";


		/// <summary>
		/// Values for field Unità di base dell'unità di vendita
		/// <summary>
		public enum isschedulebaseuomValues
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
