namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Dettaglio Integrazione Dati constants.
	/// </summary>
	public sealed class res_dataintegrationdetail : EntityGenericConstants
	{
		/// <summary>
		/// res_dataintegrationdetail
		/// </summary>
		public static string logicalName => "res_dataintegrationdetail";

		/// <summary>
		/// Dettaglio Integrazione Dati
		/// </summary>
		public static string displayName => "Dettaglio Integrazione Dati";

		/// <summary>
		/// Display Name: Dettaglio Integrazione Dati,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_dataintegrationdetailid => "res_dataintegrationdetailid";

		/// <summary>
		/// Display Name: Integrazione Dati,
		/// Type: Lookup,
		/// Related entities: res_dataintegration,
		/// Description: 
		/// </summary>
		public static string res_dataintegrationid => "res_dataintegrationid";

		/// <summary>
		/// Display Name: Risultato dell'Integrazione,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_integrationresult => "res_integrationresult";

		/// <summary>
		/// Display Name: Riga di Integrazione,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_integrationrow => "res_integrationrow";

		/// <summary>
		/// Display Name: Name,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: N° Riga,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string res_rownum => "res_rownum";


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
			Creato_StateAttivo = 1,
			Distribuito_StateInattivo = 2,
			NotDistribuito_StateInattivo = 100000001
		}
	};
}
