namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Entity Generic constants.
	/// </summary>
	public class EntityGenericConstants
	{
		/// <summary>
		/// Display Name: Autore,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Mostra chi ha creato il record.
		/// </summary>
		public static string createdby => "createdby";

		/// <summary>
		/// Display Name: Data creazione,
		/// Type: DateTime,
		/// Description: Mostra la data e l'ora di creazione del record. Data e ora sono visualizzate in base al fuso orario selezionato nelle opzioni di Microsoft Dynamics 365.
		/// </summary>
		public static string createdon => "createdon";

		/// <summary>
		/// Display Name: Autore (delegato),
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Mostra chi ha creato il record per conto di un altro utente.
		/// </summary>
		public static string createdonbehalfby => "createdonbehalfby";

		/// <summary>
		/// Display Name: Numero sequenza importazione,
		/// Type: Integer,
		/// Description: Identificatore univoco dell'importazione di dati o migrazione di dati che ha creato il record.
		/// </summary>
		public static string importsequencenumber => "importsequencenumber";

		/// <summary>
		/// Display Name: Autore modifica,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Mostra chi ha aggiornato il record per ultimo.
		/// </summary>
		public static string modifiedby => "modifiedby";

		/// <summary>
		/// Display Name: Data modifica,
		/// Type: DateTime,
		/// Description: Mostra la data e l'ora dell'ultimo aggiornamento del record. Data e ora sono visualizzate in base al fuso orario selezionato nelle opzioni di Microsoft Dynamics 365.
		/// </summary>
		public static string modifiedon => "modifiedon";

		/// <summary>
		/// Display Name: Autore modifica (delegato),
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Mostra chi ha creato il record per conto di un altro utente.
		/// </summary>
		public static string modifiedonbehalfby => "modifiedonbehalfby";

		/// <summary>
		/// Display Name: Data creazione record,
		/// Type: DateTime,
		/// Description: Data e ora di migrazione del record.
		/// </summary>
		public static string overriddencreatedon => "overriddencreatedon";

		/// <summary>
		/// Display Name: Proprietario,
		/// Type: Owner,
		/// Description: Immettere l'utente o il team a cui è assegnata la gestione del record. Questo campo viene aggiornato ogni volta che il record viene assegnato a un utente diverso.
		/// </summary>
		public static string ownerid => "ownerid";

		/// <summary>
		/// Display Name: Business Unit proprietaria,
		/// Type: Lookup,
		/// Related entities: businessunit,
		/// Description: Mostra la Business Unit a cui appartiene il proprietario del record.
		/// </summary>
		public static string owningbusinessunit => "owningbusinessunit";

		/// <summary>
		/// Display Name: Team proprietario,
		/// Type: Lookup,
		/// Related entities: team,
		/// Description: Identificatore univoco del team a cui appartiene l'account.
		/// </summary>
		public static string owningteam => "owningteam";

		/// <summary>
		/// Display Name: Utente proprietario,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Identificatore univoco dell'utente a cui appartiene l'account.
		/// </summary>
		public static string owninguser => "owninguser";

		/// <summary>
		/// Display Name: Stato,
		/// Type: State,
		/// Values:
		/// Attiva: 0,
		/// Inattiva: 1,
		/// Description: Mostra se l'account è attivo o inattivo. Gli account inattivi sono di sola lettura e non possono essere modificati a meno che non vengano riattivati.
		/// </summary>
		public static string statecode => "statecode";

		/// <summary>
		/// Display Name: Motivo stato,
		/// Type: Status,
		/// Values:
		/// Attiva: 0,
		/// Inattiva: 1,
		/// Description: Selezionare lo stato dell'account.
		/// </summary>
		public static string statuscode => "statuscode";

		/// <summary>
		/// Display Name: Numero di versione regola fuso orario,
		/// Type: Integer,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string timezoneruleversionnumber => "timezoneruleversionnumber";

		/// <summary>
		/// Display Name: Codice fuso orario conversione UTC,
		/// Type: Integer,
		/// Description: Codice di fuso orario utilizzato al momento della creazione del record.
		/// </summary>
		public static string utcconversiontimezonecode => "utcconversiontimezonecode";

		/// <summary>
		/// Display Name: Numero versione,
		/// Type: BigInt,
		/// Description: Numero di versione dell'account.
		/// </summary>
		public static string versionnumber => "versionnumber";


		/// <summary>
		/// Values for field Stato
		/// <summary>
		public new enum statecodeValues
		{
			Attiva = 0,
			Inattiva = 1
		}

		/// <summary>
		/// Values for field Motivo stato
		/// <summary>
		public new enum statuscodeValues
		{
			Attiva_StateAttiva = 1,
			Inattiva_StateInattiva = 2
		}
	};
}
