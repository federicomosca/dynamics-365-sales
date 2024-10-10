namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Integrazione Dati constants.
	/// </summary>
	public sealed class res_dataintegration : EntityGenericConstants
	{
		/// <summary>
		/// res_dataintegration
		/// </summary>
		public static string logicalName => "res_dataintegration";

		/// <summary>
		/// Integrazione Dati
		/// </summary>
		public static string displayName => "Integrazione Dati";

		/// <summary>
		/// Display Name: DataIntegration,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_dataintegrationid => "res_dataintegrationid";

		/// <summary>
		/// Display Name: File di Distribuzione,
		/// Type: Virtual,
		/// Description: 
		/// </summary>
		public static string res_distributionfile => "res_distributionfile";

		/// <summary>
		/// Display Name: Azione dell'Integrazione,
		/// Type: Picklist,
		/// Values:
		/// Clienti: 100000000,
		/// Ordini: 100000001,
		/// Articoli: 100000002,
		/// Documenti: 100000003,
		/// Pagamenti: 100000004,
		/// Description: 
		/// </summary>
		public static string res_integrationaction => "res_integrationaction";

		/// <summary>
		/// Display Name: Dati dell'Integrazione,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_integrationdata => "res_integrationdata";

		/// <summary>
		/// Display Name: File di Integrazione,
		/// Type: Virtual,
		/// Description: 
		/// </summary>
		public static string res_integrationfile => "res_integrationfile";

		/// <summary>
		/// Display Name: Risultato dell'Integrazione,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_integrationresult => "res_integrationresult";

		/// <summary>
		/// Display Name: Numero di integrazioni,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string res_integrationsnumber => "res_integrationsnumber";

		/// <summary>
		/// Display Name: Tipo di Integrazione,
		/// Type: Picklist,
		/// Values:
		/// Import: 100000000,
		/// Export: 100000001,
		/// Description: 
		/// </summary>
		public static string res_integrationtype => "res_integrationtype";

		/// <summary>
		/// Display Name: Name,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";


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
			Distribuitoparzialmente_StateInattivo = 100000004,
			InElaborazione_StateAttivo = 100000002,
			InValidazione_StateAttivo = 100000001,
			NonDistribuito_StateInattivo = 100000003
		}

		/// <summary>
		/// Values for field Azione dell'Integrazione
		/// <summary>
		public enum res_integrationactionValues
		{
			Articoli = 100000002,
			Clienti = 100000000,
			Documenti = 100000003,
			Ordini = 100000001,
			Pagamenti = 100000004
		}

		/// <summary>
		/// Values for field Tipo di Integrazione
		/// <summary>
		public enum res_integrationtypeValues
		{
			Export = 100000001,
			Import = 100000000
		}
	};
}
