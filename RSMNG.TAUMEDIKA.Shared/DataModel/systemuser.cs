namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Utente constants.
	/// </summary>
	public sealed class systemuser : EntityGenericConstants
	{
		/// <summary>
		/// systemuser
		/// </summary>
		public static string logicalName => "systemuser";

		/// <summary>
		/// Utente
		/// </summary>
		public static string displayName => "Utente";

		/// <summary>
		/// Display Name: Modalità di accesso,
		/// Type: Picklist,
		/// Values:
		/// Lettura-scrittura: 0,
		/// Amministrativa: 1,
		/// Lettura: 2,
		/// Utente di supporto: 3,
		/// Non interattivo: 4,
		/// Amministratore con delega: 5,
		/// Description: Tipo di utente.
		/// </summary>
		public static string accessmode => "accessmode";

		/// <summary>
		/// Display Name: GUID Active Directory,
		/// Type: Uniqueidentifier,
		/// Description: GUID oggetto di Active Directory per l'utente di sistema.
		/// </summary>
		public static string activedirectoryguid => "activedirectoryguid";

		/// <summary>
		/// Display Name: Indirizzo 1: ID,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'indirizzo 1.
		/// </summary>
		public static string address1_addressid => "address1_addressid";

		/// <summary>
		/// Display Name: Indirizzo 1: tipo di indirizzo,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Tipo di indirizzo per l'indirizzo 1, ad esempio, di fatturazione, di spedizione o primario.
		/// </summary>
		public static string address1_addresstypecode => "address1_addresstypecode";

		/// <summary>
		/// Display Name: Città,
		/// Type: String,
		/// Description: Nome di città per l'indirizzo 1.
		/// </summary>
		public static string address1_city => "address1_city";

		/// <summary>
		/// Display Name: Indirizzo,
		/// Type: Memo,
		/// Description: Mostra l'indirizzo primario completo.
		/// </summary>
		public static string address1_composite => "address1_composite";

		/// <summary>
		/// Display Name: Paese,
		/// Type: String,
		/// Description: Nome di paese per l'indirizzo 1.
		/// </summary>
		public static string address1_country => "address1_country";

		/// <summary>
		/// Display Name: Indirizzo 1: regione,
		/// Type: String,
		/// Description: Nome di regione per l'indirizzo 1.
		/// </summary>
		public static string address1_county => "address1_county";

		/// <summary>
		/// Display Name: Indirizzo 1: fax,
		/// Type: String,
		/// Description: Numero di fax per l'indirizzo 1.
		/// </summary>
		public static string address1_fax => "address1_fax";

		/// <summary>
		/// Display Name: Indirizzo 1: latitudine,
		/// Type: Double,
		/// Description: Latitudine per l'indirizzo 1.
		/// </summary>
		public static string address1_latitude => "address1_latitude";

		/// <summary>
		/// Display Name: Via 1,
		/// Type: String,
		/// Description: Prima riga per l'immissione di informazioni sull'indirizzo 1.
		/// </summary>
		public static string address1_line1 => "address1_line1";

		/// <summary>
		/// Display Name: Via 2,
		/// Type: String,
		/// Description: Seconda riga per l'immissione di informazioni sull'indirizzo 1.
		/// </summary>
		public static string address1_line2 => "address1_line2";

		/// <summary>
		/// Display Name: Via 3,
		/// Type: String,
		/// Description: Terza riga per l'immissione di informazioni sull'indirizzo 1.
		/// </summary>
		public static string address1_line3 => "address1_line3";

		/// <summary>
		/// Display Name: Indirizzo 1: longitudine,
		/// Type: Double,
		/// Description: Longitudine per l'indirizzo 1.
		/// </summary>
		public static string address1_longitude => "address1_longitude";

		/// <summary>
		/// Display Name: Indirizzo 1: nome,
		/// Type: String,
		/// Description: Nome da immettere per l'indirizzo 1.
		/// </summary>
		public static string address1_name => "address1_name";

		/// <summary>
		/// Display Name: CAP,
		/// Type: String,
		/// Description: Codice postale per l'indirizzo 1.
		/// </summary>
		public static string address1_postalcode => "address1_postalcode";

		/// <summary>
		/// Display Name: Indirizzo 1: casella postale,
		/// Type: String,
		/// Description: Numero di casella postale per l'indirizzo 1.
		/// </summary>
		public static string address1_postofficebox => "address1_postofficebox";

		/// <summary>
		/// Display Name: Indirizzo 1: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Metodo di spedizione per l'indirizzo 1.
		/// </summary>
		public static string address1_shippingmethodcode => "address1_shippingmethodcode";

		/// <summary>
		/// Display Name: Provincia,
		/// Type: String,
		/// Description: Provincia per l'indirizzo 1.
		/// </summary>
		public static string address1_stateorprovince => "address1_stateorprovince";

		/// <summary>
		/// Display Name: Telefono principale,
		/// Type: String,
		/// Description: Primo numero di telefono associato all'indirizzo 1.
		/// </summary>
		public static string address1_telephone1 => "address1_telephone1";

		/// <summary>
		/// Display Name: Altro telefono,
		/// Type: String,
		/// Description: Secondo numero di telefono associato all'indirizzo 1.
		/// </summary>
		public static string address1_telephone2 => "address1_telephone2";

		/// <summary>
		/// Display Name: Cellulare 2,
		/// Type: String,
		/// Description: Terzo numero di telefono associato all'indirizzo 1.
		/// </summary>
		public static string address1_telephone3 => "address1_telephone3";

		/// <summary>
		/// Display Name: Indirizzo 1: zona UPS,
		/// Type: String,
		/// Description: Zona trasporto ferroviario per l'indirizzo 1.
		/// </summary>
		public static string address1_upszone => "address1_upszone";

		/// <summary>
		/// Display Name: Indirizzo 1: differenza UTC,
		/// Type: Integer,
		/// Description: Differenza UTC per l'indirizzo 1, ovvero la differenza tra l'ora locale e l'ora UTC standard.
		/// </summary>
		public static string address1_utcoffset => "address1_utcoffset";

		/// <summary>
		/// Display Name: Indirizzo 2: ID,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'indirizzo 2.
		/// </summary>
		public static string address2_addressid => "address2_addressid";

		/// <summary>
		/// Display Name: Indirizzo 2: tipo di indirizzo,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Tipo di indirizzo per l'indirizzo 2, ad esempio, di fatturazione, di spedizione o primario.
		/// </summary>
		public static string address2_addresstypecode => "address2_addresstypecode";

		/// <summary>
		/// Display Name: Altra città,
		/// Type: String,
		/// Description: Nome di città per l'indirizzo 2.
		/// </summary>
		public static string address2_city => "address2_city";

		/// <summary>
		/// Display Name: Altro indirizzo,
		/// Type: Memo,
		/// Description: Mostra l'indirizzo secondario completo.
		/// </summary>
		public static string address2_composite => "address2_composite";

		/// <summary>
		/// Display Name: Altro paese,
		/// Type: String,
		/// Description: Nome di paese per l'indirizzo 2.
		/// </summary>
		public static string address2_country => "address2_country";

		/// <summary>
		/// Display Name: Indirizzo 2: regione,
		/// Type: String,
		/// Description: Nome di regione per l'indirizzo 2.
		/// </summary>
		public static string address2_county => "address2_county";

		/// <summary>
		/// Display Name: Indirizzo 2: fax,
		/// Type: String,
		/// Description: Numero di fax per l'indirizzo 2.
		/// </summary>
		public static string address2_fax => "address2_fax";

		/// <summary>
		/// Display Name: Indirizzo 2: latitudine,
		/// Type: Double,
		/// Description: Latitudine per l'indirizzo 2.
		/// </summary>
		public static string address2_latitude => "address2_latitude";

		/// <summary>
		/// Display Name: Via 1 altro indirizzo,
		/// Type: String,
		/// Description: Prima riga per l'immissione di informazioni sull'indirizzo 2.
		/// </summary>
		public static string address2_line1 => "address2_line1";

		/// <summary>
		/// Display Name: Via 2 altro indirizzo,
		/// Type: String,
		/// Description: Seconda riga per l'immissione di informazioni sull'indirizzo 2.
		/// </summary>
		public static string address2_line2 => "address2_line2";

		/// <summary>
		/// Display Name: Via 3 altro indirizzo,
		/// Type: String,
		/// Description: Terza riga per l'immissione di informazioni sull'indirizzo 2.
		/// </summary>
		public static string address2_line3 => "address2_line3";

		/// <summary>
		/// Display Name: Indirizzo 2: longitudine,
		/// Type: Double,
		/// Description: Longitudine per l'indirizzo 2.
		/// </summary>
		public static string address2_longitude => "address2_longitude";

		/// <summary>
		/// Display Name: Indirizzo 2: nome,
		/// Type: String,
		/// Description: Nome da immettere per l'indirizzo 2.
		/// </summary>
		public static string address2_name => "address2_name";

		/// <summary>
		/// Display Name: CAP altro indirizzo,
		/// Type: String,
		/// Description: Codice postale per l'indirizzo 2.
		/// </summary>
		public static string address2_postalcode => "address2_postalcode";

		/// <summary>
		/// Display Name: Indirizzo 2: casella postale,
		/// Type: String,
		/// Description: Numero di casella postale per l'indirizzo 2.
		/// </summary>
		public static string address2_postofficebox => "address2_postofficebox";

		/// <summary>
		/// Display Name: Indirizzo 2: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Metodo di spedizione per l'indirizzo 2.
		/// </summary>
		public static string address2_shippingmethodcode => "address2_shippingmethodcode";

		/// <summary>
		/// Display Name: Provincia altro indirizzo,
		/// Type: String,
		/// Description: Provincia per l'indirizzo 2.
		/// </summary>
		public static string address2_stateorprovince => "address2_stateorprovince";

		/// <summary>
		/// Display Name: Indirizzo 2: telefono 1,
		/// Type: String,
		/// Description: Primo numero di telefono associato all'indirizzo 2.
		/// </summary>
		public static string address2_telephone1 => "address2_telephone1";

		/// <summary>
		/// Display Name: Indirizzo 2: telefono 2,
		/// Type: String,
		/// Description: Secondo numero di telefono associato all'indirizzo 2.
		/// </summary>
		public static string address2_telephone2 => "address2_telephone2";

		/// <summary>
		/// Display Name: Indirizzo 2: telefono 3,
		/// Type: String,
		/// Description: Terzo numero di telefono associato all'indirizzo 2.
		/// </summary>
		public static string address2_telephone3 => "address2_telephone3";

		/// <summary>
		/// Display Name: Indirizzo 2: zona UPS,
		/// Type: String,
		/// Description: Zona trasporto ferroviario per l'indirizzo 2.
		/// </summary>
		public static string address2_upszone => "address2_upszone";

		/// <summary>
		/// Display Name: Indirizzo 2: differenza UTC,
		/// Type: Integer,
		/// Description: Differenza UTC per l'indirizzo 2, ovvero la differenza tra l'ora locale e l'ora UTC standard.
		/// </summary>
		public static string address2_utcoffset => "address2_utcoffset";

		/// <summary>
		/// Display Name: ID applicazione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore dell'applicazione. Utilizzato per accedere ai dati in un'altra applicazione.
		/// </summary>
		public static string applicationid => "applicationid";

		/// <summary>
		/// Display Name: URI ID applicazione,
		/// Type: String,
		/// Description: L'URI utilizzato come identificatore logico univoco per l'app esterna. Utilizzato per convalidare l'applicazione.
		/// </summary>
		public static string applicationiduri => "applicationiduri";

		/// <summary>
		/// Display Name: ID oggetto Azure AD,
		/// Type: Uniqueidentifier,
		/// Description: ID oggetto della directory dell'applicazione.
		/// </summary>
		public static string azureactivedirectoryobjectid => "azureactivedirectoryobjectid";

		/// <summary>
		/// Display Name: Data eliminazione Azure,
		/// Type: DateTime,
		/// Description: Data e ora in cui l'utente è stato impostato come eliminato temporaneamente in Azure.
		/// </summary>
		public static string azuredeletedon => "azuredeletedon";

		/// <summary>
		/// Display Name: Stato di Azure,
		/// Type: Picklist,
		/// Values:
		/// Esiste: 0,
		/// Eliminato temporaneamente: 1,
		/// Non trovato o eliminato definitivamente: 2,
		/// Description: Stato di Azure dell'utente
		/// </summary>
		public static string azurestate => "azurestate";

		/// <summary>
		/// Display Name: Business Unit,
		/// Type: Lookup,
		/// Related entities: businessunit,
		/// Description: Identificatore univoco della Business Unit a cui è associato l'utente.
		/// </summary>
		public static string businessunitid => "businessunitid";

		/// <summary>
		/// Display Name: Calendario,
		/// Type: Lookup,
		/// Related entities: calendar,
		/// Description: Calendario fiscale associato all'utente.
		/// </summary>
		public static string calendarid => "calendarid";

		/// <summary>
		/// Display Name: Tipo di licenza,
		/// Type: Picklist,
		/// Values:
		/// Professional: 0,
		/// Amministrativa: 1,
		/// Di base: 2,
		/// Per dispositivo professionale: 3,
		/// Device Basic: 4,
		/// Essential: 5,
		/// Device Essential: 6,
		/// Enterprise: 7,
		/// Per dispositivo Enterprise: 8,
		/// Vendite: 9,
		/// Servizio: 10,
		/// Field Service: 11,
		/// Project Service: 12,
		/// Description: Tipo di licenza dell'utente. Questa viene utilizzata nella versione locale del prodotto. Le licenze online vengono gestite tramite Il portale di Microsoft 365 Office
		/// </summary>
		public static string caltype => "caltype";

		/// <summary>
		/// Display Name: Filtri predefiniti popolati,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se i filtri predefiniti di Outlook sono stati popolati.
		/// </summary>
		public static string defaultfilterspopulated => "defaultfilterspopulated";

		/// <summary>
		/// Display Name: Cassetta postale,
		/// Type: Lookup,
		/// Related entities: mailbox,
		/// Description: Selezionare la cassetta postale associata all'utente.
		/// </summary>
		public static string defaultmailbox => "defaultmailbox";

		/// <summary>
		/// Display Name: Nome cartella predefinito di OneDrive for Business,
		/// Type: String,
		/// Description: Digita un nome di cartella predefinito per il percorso di OneDrive for Business dell'utente.
		/// </summary>
		public static string defaultodbfoldername => "defaultodbfoldername";

		/// <summary>
		/// Display Name: Stato Eliminato,
		/// Type: Picklist,
		/// Values:
		/// Non eliminato: 0,
		/// Eliminato temporaneamente: 1,
		/// Description: Stato eliminazione utente
		/// </summary>
		public static string deletedstate => "deletedstate";

		/// <summary>
		/// Display Name: Motivo disabilitazione,
		/// Type: String,
		/// Description: Motivo della disabilitazione dell'utente.
		/// </summary>
		public static string disabledreason => "disabledreason";

		/// <summary>
		/// Display Name: Mostra in visualizzazioni servizi,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Specifica se l'utente deve essere o meno mostrato nelle visualizzazioni dei servizi.
		/// </summary>
		public static string displayinserviceviews => "displayinserviceviews";

		/// <summary>
		/// Display Name: Nome utente,
		/// Type: String,
		/// Description: Dominio di Active Directory di cui è membro l'utente.
		/// </summary>
		public static string domainname => "domainname";

		/// <summary>
		/// Display Name: Stato indirizzo e-mail primario,
		/// Type: Picklist,
		/// Values:
		/// Vuoto: 0,
		/// Approvato: 1,
		/// In attesa di approvazione: 2,
		/// Rifiutato: 3,
		/// Description: Mostra lo stato dell'indirizzo e-mail primario.
		/// </summary>
		public static string emailrouteraccessapproval => "emailrouteraccessapproval";

		/// <summary>
		/// Display Name: Dipendente,
		/// Type: String,
		/// Description: Identificatore dipendente dell'utente.
		/// </summary>
		public static string employeeid => "employeeid";

		/// <summary>
		/// Display Name: Immagine entità,
		/// Type: Virtual,
		/// Description: Mostra l'immagine predefinita del record.
		/// </summary>
		public static string entityimage => "entityimage";

		/// <summary>
		/// Display Name: ID immagine entità,
		/// Type: Uniqueidentifier,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string entityimageid => "entityimageid";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Tasso di cambio per la valuta associata all'utente di sistema rispetto alla valuta di base.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Nome dell'utente.
		/// </summary>
		public static string firstname => "firstname";

		/// <summary>
		/// Display Name: Nome completo,
		/// Type: String,
		/// Description: Nome completo dell'utente.
		/// </summary>
		public static string fullname => "fullname";

		/// <summary>
		/// Display Name: Codice fiscale/partita IVA,
		/// Type: String,
		/// Description: Codice fiscale o partita IVA dell'utente.
		/// </summary>
		public static string governmentid => "governmentid";

		/// <summary>
		/// Display Name: Telefono abitazione,
		/// Type: String,
		/// Description: Numero di telefono dell'abitazione per l'utente.
		/// </summary>
		public static string homephone => "homephone";

		/// <summary>
		/// Display Name: ID identità dell'utente univoco,
		/// Type: Integer,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string identityid => "identityid";

		/// <summary>
		/// Display Name: Metodo di recapito messaggi e-mail in arrivo,
		/// Type: Picklist,
		/// Values:
		/// Nessuno: 0,
		/// Microsoft Dynamics 365 per Outlook: 1,
		/// Sincronizzazione lato server del router e-mail: 2,
		/// Cassetta postale di inoltro: 3,
		/// Description: Metodo di recapito dei messaggi e-mail in arrivo per l'utente.
		/// </summary>
		public static string incomingemaildeliverymethod => "incomingemaildeliverymethod";

		/// <summary>
		/// Display Name: Indirizzo e-mail primario,
		/// Type: String,
		/// Description: Indirizzo e-mail interno per l'utente.
		/// </summary>
		public static string internalemailaddress => "internalemailaddress";

		/// <summary>
		/// Display Name: Stato invito,
		/// Type: Picklist,
		/// Values:
		/// Invito non inviato: 0,
		/// Invito inviato: 1,
		/// Invito quasi scaduto: 2,
		/// Invito scaduto: 3,
		/// Invito accettato: 4,
		/// Invito rifiutato: 5,
		/// Invito revocato: 6,
		/// Description: Stato invito utente.
		/// </summary>
		public static string invitestatuscode => "invitestatuscode";

		/// <summary>
		/// Display Name: Utente di Active Directory,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Specifica se l'utente è un utente di Active Directory.
		/// </summary>
		public static string isactivedirectoryuser => "isactivedirectoryuser";

		/// <summary>
		/// Display Name: Stato,
		/// Type: Boolean,
		/// Values:
		/// Disabilitato: 1,
		/// Abilitato: 0,
		/// Description: Specifica se l'utente è abilitato.
		/// </summary>
		public static string isdisabled => "isdisabled";

		/// <summary>
		/// Display Name: Stato di approvazione dell'indirizzo e-mail da parte dell'amministratore O365,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Mostra lo stato di approvazione dell'indirizzo e-mail da parte dell'amministratore O365.
		/// </summary>
		public static string isemailaddressapprovedbyo365admin => "isemailaddressapprovedbyo365admin";

		/// <summary>
		/// Display Name: Modalità integrazione,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Determina se l'utente utilizza l'integrazione.
		/// </summary>
		public static string isintegrationuser => "isintegrationuser";

		/// <summary>
		/// Display Name: Utente con licenza,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Specifica se l'utente dispone di una licenza.
		/// </summary>
		public static string islicensed => "islicensed";

		/// <summary>
		/// Display Name: Utente sincronizzato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Informazioni sullo stato di sincronizzazione dell'utente con la directory.
		/// </summary>
		public static string issyncwithdirectory => "issyncwithdirectory";

		/// <summary>
		/// Display Name: Posizione,
		/// Type: String,
		/// Description: Posizione dell'utente.
		/// </summary>
		public static string jobtitle => "jobtitle";

		/// <summary>
		/// Display Name: Cognome,
		/// Type: String,
		/// Description: Cognome dell'utente.
		/// </summary>
		public static string lastname => "lastname";

		/// <summary>
		/// Display Name: Ora ultimo aggiornamento utente,
		/// Type: DateTime,
		/// Description: Timestamp dell'ultimo aggiornamento per l'utente
		/// </summary>
		public static string latestupdatetime => "latestupdatetime";

		/// <summary>
		/// Display Name: Secondo nome,
		/// Type: String,
		/// Description: Secondo nome dell'utente.
		/// </summary>
		public static string middlename => "middlename";

		/// <summary>
		/// Display Name: E-mail fuori sede,
		/// Type: String,
		/// Description: Indirizzo e-mail fuori sede per l'utente.
		/// </summary>
		public static string mobilealertemail => "mobilealertemail";

		/// <summary>
		/// Display Name: Profilo CRM Mobile Offline,
		/// Type: Lookup,
		/// Related entities: mobileofflineprofile,
		/// Description: Elementi presenti in un oggetto SystemUser specifico.
		/// </summary>
		public static string mobileofflineprofileid => "mobileofflineprofileid";

		/// <summary>
		/// Display Name: Cellulare,
		/// Type: String,
		/// Description: Numero di cellulare per l'utente.
		/// </summary>
		public static string mobilephone => "mobilephone";

		/// <summary>
		/// Display Name: Tipo di utente,
		/// Type: Picklist,
		/// Values:
		/// Utente dell'applicazione: 192350000,
		/// Utente dell'applicazione bot: 192350001,
		/// Description: Tipo di utente: utente dell'applicazione o utente dell'applicazione bot
		/// </summary>
		public static string msdyn_agentType => "msdyn_agentType";

		/// <summary>
		/// Display Name: ID applicazione bot,
		/// Type: String,
		/// Description: ID applicazione del bot.
		/// </summary>
		public static string msdyn_botapplicationid => "msdyn_botapplicationid";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Descrizione utente BOT
		/// </summary>
		public static string msdyn_botdescription => "msdyn_botdescription";

		/// <summary>
		/// Display Name: Endpoint,
		/// Type: String,
		/// Description: Endpoint utente bot
		/// </summary>
		public static string msdyn_botendpoint => "msdyn_botendpoint";

		/// <summary>
		/// Display Name: Handle bot,
		/// Type: String,
		/// Description: Handle bot
		/// </summary>
		public static string msdyn_bothandle => "msdyn_bothandle";

		/// <summary>
		/// Display Name: Provider di bot,
		/// Type: Picklist,
		/// Values:
		/// Agente virtuale: 192350000,
		/// Altro: 192350001,
		/// Nessuno: 192350002,
		/// Description: Indica il tipo di bot
		/// </summary>
		public static string msdyn_botprovider => "msdyn_botprovider";

		/// <summary>
		/// Display Name: Chiavi private,
		/// Type: String,
		/// Description: Chiavi private utente bot
		/// </summary>
		public static string msdyn_botsecretkeys => "msdyn_botsecretkeys";

		/// <summary>
		/// Display Name: Capacità,
		/// Type: Integer,
		/// Description: Capacità associata all'utente.
		/// </summary>
		public static string msdyn_capacity => "msdyn_capacity";

		/// <summary>
		/// Display Name: Presenza predefinita,
		/// Type: Lookup,
		/// Related entities: msdyn_presence,
		/// Description: Identificatore univoco per la presenza associata all'utente.
		/// </summary>
		public static string msdyn_defaultpresenceiduser => "msdyn_defaultpresenceiduser";

		/// <summary>
		/// Display Name: Rifiuto esplicito GDPR,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se l'utente stato rifiutato esplicitamente o meno
		/// </summary>
		public static string msdyn_gdproptout => "msdyn_gdproptout";

		/// <summary>
		/// Display Name: Campo controllo wrapper della griglia,
		/// Type: String,
		/// Description: Campo per associare il controllo wrapper della griglia
		/// </summary>
		public static string msdyn_gridwrappercontrolfield => "msdyn_gridwrappercontrolfield";

		/// <summary>
		/// Display Name: Aggregato abilitato per gli esperti,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Verifica che l'aggregato sia abilitato per gli esperti.
		/// </summary>
		public static string msdyn_isexpertenabledforswarm => "msdyn_isexpertenabledforswarm";

		/// <summary>
		/// Display Name: ID ambiente proprietario,
		/// Type: String,
		/// Description: ID ambiente dell'ambiente CDS a cui appartiene l'utente del bot.
		/// </summary>
		public static string msdyn_owningenvironmentid => "msdyn_owningenvironmentid";

		/// <summary>
		/// Display Name: Tipo,
		/// Type: Picklist,
		/// Values:
		/// Utente CRM: 192350000,
		/// Utente BOT: 192350001,
		/// Description: Tipo di utente - Utente CRM o BOT
		/// </summary>
		public static string msdyn_usertype => "msdyn_usertype";

		/// <summary>
		/// Display Name: Nome alternativo,
		/// Type: String,
		/// Description: Nome alternativo dell'utente.
		/// </summary>
		public static string nickname => "nickname";

		/// <summary>
		/// Display Name: Organizzazione ,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'organizzazione associata all'utente.
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Metodo di recapito messaggi e-mail in uscita,
		/// Type: Picklist,
		/// Values:
		/// Nessuno: 0,
		/// Microsoft Dynamics 365 per Outlook: 1,
		/// Sincronizzazione lato server del router e-mail: 2,
		/// Description: Metodo di recapito dei messaggi e-mail in uscita per l'utente.
		/// </summary>
		public static string outgoingemaildeliverymethod => "outgoingemaildeliverymethod";

		/// <summary>
		/// Display Name: Responsabile,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Identificatore univoco del responsabile dell'utente.
		/// </summary>
		public static string parentsystemuserid => "parentsystemuserid";

		/// <summary>
		/// Display Name: Passport alto,
		/// Type: Integer,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string passporthi => "passporthi";

		/// <summary>
		/// Display Name: Passport basso,
		/// Type: Integer,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string passportlo => "passportlo";

		/// <summary>
		/// Display Name: Indirizzo e-mail 2,
		/// Type: String,
		/// Description: Indirizzo e-mail personale dell'utente.
		/// </summary>
		public static string personalemailaddress => "personalemailaddress";

		/// <summary>
		/// Display Name: URL foto,
		/// Type: String,
		/// Description: URL del sito Web in cui si trova una foto dell'utente.
		/// </summary>
		public static string photourl => "photourl";

		/// <summary>
		/// Display Name: Posizione,
		/// Type: Lookup,
		/// Related entities: position,
		/// Description: Posizione dell'utente nel modello di sicurezza gerarchica.
		/// </summary>
		public static string positionid => "positionid";

		/// <summary>
		/// Display Name: Indirizzo preferito,
		/// Type: Picklist,
		/// Values:
		/// Indirizzo postale: 1,
		/// Altro indirizzo: 2,
		/// Description: Indirizzo preferito per l'utente.
		/// </summary>
		public static string preferredaddresscode => "preferredaddresscode";

		/// <summary>
		/// Display Name: Indirizzo e-mail preferito,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Indirizzo e-mail preferito per l'utente.
		/// </summary>
		public static string preferredemailcode => "preferredemailcode";

		/// <summary>
		/// Display Name: Telefono preferito,
		/// Type: Picklist,
		/// Values:
		/// Telefono principale: 1,
		/// Altro telefono: 2,
		/// Telefono abitazione: 3,
		/// Cellulare: 4,
		/// Description: Numero di telefono preferito per l'utente.
		/// </summary>
		public static string preferredphonecode => "preferredphonecode";

		/// <summary>
		/// Display Name: Processo,
		/// Type: Uniqueidentifier,
		/// Description: Mostra l'ID del processo.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Coda predefinita,
		/// Type: Lookup,
		/// Related entities: queue,
		/// Description: Identificatore univoco della coda predefinita per l'utente.
		/// </summary>
		public static string queueid => "queueid";

		/// <summary>
		/// Display Name: Codice agente,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_agentnumber => "res_agentnumber";

		/// <summary>
		/// Display Name: % Commissione,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_commissionpercentage => "res_commissionpercentage";

		/// <summary>
		/// Display Name: Agente,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isagente => "res_isagente";

		/// <summary>
		/// Display Name: Disabilita calcolo provvigioni,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_iscommissioncalculationdisabled => "res_iscommissioncalculationdisabled";

		/// <summary>
		/// Display Name: Titolo,
		/// Type: String,
		/// Description: Titolo per la corrispondenza con l'utente.
		/// </summary>
		public static string salutation => "salutation";

		/// <summary>
		/// Display Name: Modalità accesso limitato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Controlla se si tratta di un utente per l'installazione.
		/// </summary>
		public static string setupuser => "setupuser";

		/// <summary>
		/// Display Name: Indirizzo e-mail di SharePoint,
		/// Type: String,
		/// Description: Indirizzo e-mail di lavoro di SharePoint
		/// </summary>
		public static string sharepointemailaddress => "sharepointemailaddress";

		/// <summary>
		/// Display Name: Sito,
		/// Type: Lookup,
		/// Related entities: site,
		/// Description: Luogo dove si trova l'utente.
		/// </summary>
		public static string siteid => "siteid";

		/// <summary>
		/// Display Name: Competenze,
		/// Type: String,
		/// Description: Set di competenze dell'utente.
		/// </summary>
		public static string skills => "skills";

		/// <summary>
		/// Display Name: (Deprecata) Fase del processo,
		/// Type: Uniqueidentifier,
		/// Description: Mostra l'ID della fase.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Utente,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'utente.
		/// </summary>
		public static string systemuserid => "systemuserid";

		/// <summary>
		/// Display Name: Area,
		/// Type: Lookup,
		/// Related entities: territory,
		/// Description: Identificatore univoco dell'area a cui è assegnato l'utente.
		/// </summary>
		public static string territoryid => "territoryid";

		/// <summary>
		/// Display Name: Titolo,
		/// Type: String,
		/// Description: Posizione dell'utente.
		/// </summary>
		public static string title => "title";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Identificatore univoco della valuta associata all'utente di sistema.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";

		/// <summary>
		/// Display Name: (Deprecata) Percorso incrociato,
		/// Type: String,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string traversedpath => "traversedpath";

		/// <summary>
		/// Display Name: Tipo di licenza dell'utente,
		/// Type: Integer,
		/// Description: Mostra il tipo di licenza per l'utente.
		/// </summary>
		public static string userlicensetype => "userlicensetype";

		/// <summary>
		/// Display Name: PUID utente,
		/// Type: String,
		/// Description:  Informazioni personali dell'utente PUID utente
		/// </summary>
		public static string userpuid => "userpuid";

		/// <summary>
		/// Display Name: Windows Live ID,
		/// Type: String,
		/// Description: Windows Live ID
		/// </summary>
		public static string windowsliveid => "windowsliveid";

		/// <summary>
		/// Display Name: Indirizzo e-mail Yammer,
		/// Type: String,
		/// Description: Indirizzo e-mail di accesso a Yammer dell'utente
		/// </summary>
		public static string yammeremailaddress => "yammeremailaddress";

		/// <summary>
		/// Display Name: ID utente Yammer,
		/// Type: String,
		/// Description: ID Yammer dell'utente
		/// </summary>
		public static string yammeruserid => "yammeruserid";

		/// <summary>
		/// Display Name: Nome Yomi,
		/// Type: String,
		/// Description: Pronuncia del nome proprio dell'utente, scritto in caratteri hiragana o katakana fonetici.
		/// </summary>
		public static string yomifirstname => "yomifirstname";

		/// <summary>
		/// Display Name: Nome completo Yomi,
		/// Type: String,
		/// Description: Pronuncia del nome completo dell'utente, scritto in caratteri hiragana o katakana fonetici.
		/// </summary>
		public static string yomifullname => "yomifullname";

		/// <summary>
		/// Display Name: Cognome Yomi,
		/// Type: String,
		/// Description: Pronuncia del cognome dell'utente, scritto in caratteri hiragana o katakana fonetici.
		/// </summary>
		public static string yomilastname => "yomilastname";

		/// <summary>
		/// Display Name: Secondo nome Yomi,
		/// Type: String,
		/// Description: Pronuncia del secondo nome dell'utente, scritto in caratteri hiragana o katakana fonetici.
		/// </summary>
		public static string yomimiddlename => "yomimiddlename";


		/// <summary>
		/// Values for field Modalità di accesso
		/// <summary>
		public enum accessmodeValues
		{
			Amministrativa = 1,
			Amministratorecondelega = 5,
			Lettura = 2,
			Letturascrittura = 0,
			Noninterattivo = 4,
			Utentedisupporto = 3
		}

		/// <summary>
		/// Values for field Indirizzo 1: tipo di indirizzo
		/// <summary>
		public enum address1_addresstypecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Indirizzo 1: metodo di spedizione
		/// <summary>
		public enum address1_shippingmethodcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Indirizzo 2: tipo di indirizzo
		/// <summary>
		public enum address2_addresstypecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Indirizzo 2: metodo di spedizione
		/// <summary>
		public enum address2_shippingmethodcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Stato di Azure
		/// <summary>
		public enum azurestateValues
		{
			Eliminatotemporaneamente = 1,
			Esiste = 0,
			Nontrovatooeliminatodefinitivamente = 2
		}

		/// <summary>
		/// Values for field Tipo di licenza
		/// <summary>
		public enum caltypeValues
		{
			Amministrativa = 1,
			DeviceBasic = 4,
			DeviceEssential = 6,
			Dibase = 2,
			Enterprise = 7,
			Essential = 5,
			FieldService = 11,
			PerdispositivoEnterprise = 8,
			Perdispositivoprofessionale = 3,
			Professional = 0,
			ProjectService = 12,
			Servizio = 10,
			Vendite = 9
		}

		/// <summary>
		/// Values for field Filtri predefiniti popolati
		/// <summary>
		public enum defaultfilterspopulatedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Stato Eliminato
		/// <summary>
		public enum deletedstateValues
		{
			Eliminatotemporaneamente = 1,
			Noneliminato = 0
		}

		/// <summary>
		/// Values for field Mostra in visualizzazioni servizi
		/// <summary>
		public enum displayinserviceviewsValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Stato indirizzo e-mail primario
		/// <summary>
		public enum emailrouteraccessapprovalValues
		{
			Approvato = 1,
			Inattesadiapprovazione = 2,
			Rifiutato = 3,
			Vuoto = 0
		}

		/// <summary>
		/// Values for field Metodo di recapito messaggi e-mail in arrivo
		/// <summary>
		public enum incomingemaildeliverymethodValues
		{
			Cassettapostalediinoltro = 3,
			MicrosoftDynamics365perOutlook = 1,
			Nessuno = 0,
			Sincronizzazionelatoserverdelrouteremail = 2
		}

		/// <summary>
		/// Values for field Stato invito
		/// <summary>
		public enum invitestatuscodeValues
		{
			Invitoaccettato = 4,
			Invitoinviato = 1,
			Invitononinviato = 0,
			Invitoquasiscaduto = 2,
			Invitorevocato = 6,
			Invitorifiutato = 5,
			Invitoscaduto = 3
		}

		/// <summary>
		/// Values for field Utente di Active Directory
		/// <summary>
		public enum isactivedirectoryuserValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Stato
		/// <summary>
		public enum isdisabledValues
		{
			Abilitato = 0,
			Disabilitato = 1
		}

		/// <summary>
		/// Values for field Stato di approvazione dell'indirizzo e-mail da parte dell'amministratore O365
		/// <summary>
		public enum isemailaddressapprovedbyo365adminValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Modalità integrazione
		/// <summary>
		public enum isintegrationuserValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Utente con licenza
		/// <summary>
		public enum islicensedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Utente sincronizzato
		/// <summary>
		public enum issyncwithdirectoryValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Tipo di utente
		/// <summary>
		public enum msdyn_agentTypeValues
		{
			Utentedellapplicazione = 192350000,
			Utentedellapplicazionebot = 192350001
		}

		/// <summary>
		/// Values for field Provider di bot
		/// <summary>
		public enum msdyn_botproviderValues
		{
			Agentevirtuale = 192350000,
			Altro = 192350001,
			Nessuno = 192350002
		}

		/// <summary>
		/// Values for field Rifiuto esplicito GDPR
		/// <summary>
		public enum msdyn_gdproptoutValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Aggregato abilitato per gli esperti
		/// <summary>
		public enum msdyn_isexpertenabledforswarmValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Tipo
		/// <summary>
		public enum msdyn_usertypeValues
		{
			UtenteBOT = 192350001,
			UtenteCRM = 192350000
		}

		/// <summary>
		/// Values for field Metodo di recapito messaggi e-mail in uscita
		/// <summary>
		public enum outgoingemaildeliverymethodValues
		{
			MicrosoftDynamics365perOutlook = 1,
			Nessuno = 0,
			Sincronizzazionelatoserverdelrouteremail = 2
		}

		/// <summary>
		/// Values for field Indirizzo preferito
		/// <summary>
		public enum preferredaddresscodeValues
		{
			Altroindirizzo = 2,
			Indirizzopostale = 1
		}

		/// <summary>
		/// Values for field Indirizzo e-mail preferito
		/// <summary>
		public enum preferredemailcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Telefono preferito
		/// <summary>
		public enum preferredphonecodeValues
		{
			Altrotelefono = 2,
			Cellulare = 4,
			Telefonoabitazione = 3,
			Telefonoprincipale = 1
		}

		/// <summary>
		/// Values for field Agente
		/// <summary>
		public enum res_isagenteValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Disabilita calcolo provvigioni
		/// <summary>
		public enum res_iscommissioncalculationdisabledValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Modalità accesso limitato
		/// <summary>
		public enum setupuserValues
		{
			No = 0,
			Si = 1
		}
	};
}
