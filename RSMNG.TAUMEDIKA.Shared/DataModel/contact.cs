namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Contatto constants.
	/// </summary>
	public sealed class contact : EntityGenericConstants
	{
		/// <summary>
		/// contact
		/// </summary>
		public static string logicalName => "contact";

		/// <summary>
		/// Contatto
		/// </summary>
		public static string displayName => "Contatto";

		/// <summary>
		/// Display Name: Account,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Identificatore univoco dell'account a cui è associato il contatto.
		/// </summary>
		public static string accountid => "accountid";

		/// <summary>
		/// Display Name: Ruolo,
		/// Type: Picklist,
		/// Values:
		/// Decisore: 1,
		/// Dipendente: 2,
		/// Influenzatore: 3,
		/// Description: Selezionare il ruolo del contatto nella società o nel processo di vendita, ad esempio decisore, dipendente o influenzatore.
		/// </summary>
		public static string accountrolecode => "accountrolecode";

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
		/// Fatturazione: 1,
		/// Spedizione: 2,
		/// Primario: 3,
		/// Altro: 4,
		/// Description: Selezionare il tipo di indirizzo primario.
		/// </summary>
		public static string address1_addresstypecode => "address1_addresstypecode";

		/// <summary>
		/// Display Name: Città,
		/// Type: String,
		/// Description: Digitare la città per l'indirizzo primario.
		/// </summary>
		public static string address1_city => "address1_city";

		/// <summary>
		/// Display Name: Indirizzo 1,
		/// Type: Memo,
		/// Description: Mostra l'indirizzo primario completo.
		/// </summary>
		public static string address1_composite => "address1_composite";

		/// <summary>
		/// Display Name: Nazione (testo),
		/// Type: String,
		/// Description: Digitare il paese per l'indirizzo primario.
		/// </summary>
		public static string address1_country => "address1_country";

		/// <summary>
		/// Display Name: Indirizzo 1: regione,
		/// Type: String,
		/// Description: Digitare la regione per l'indirizzo primario.
		/// </summary>
		public static string address1_county => "address1_county";

		/// <summary>
		/// Display Name: Indirizzo 1: fax,
		/// Type: String,
		/// Description: Digitare il numero di fax associato all'indirizzo primario.
		/// </summary>
		public static string address1_fax => "address1_fax";

		/// <summary>
		/// Display Name: Indirizzo 1: condizioni di spedizione,
		/// Type: Picklist,
		/// Values:
		/// FOB (Franco a bordo): 1,
		/// Gratis: 2,
		/// CFR (Costo e nolo): 3,
		/// CIF (Costo ass. e nolo): 4,
		/// CIP (Trasp. e ass. pagati fino a): 5,
		/// CPT (Trasporto pagato fino a): 6,
		/// DAF (Reso frontiera): 7,
		/// DEQ (Reso banchina): 8,
		/// DES (Reso ex ship): 9,
		/// DDP (Reso sdoganato): 10,
		/// DDU (Reso non sdoganato): 11,
		/// EXW (Franco fabbrica): 12,
		/// FAS (Franco lungo bordo): 13,
		/// FCA (Franco vettore): 14,
		/// Description: Selezionare le condizioni di spedizione per l'indirizzo primario per assicurare che gli ordini di spedizione vengano elaborati correttamente.
		/// </summary>
		public static string address1_freighttermscode => "address1_freighttermscode";

		/// <summary>
		/// Display Name: Indirizzo 1: latitudine,
		/// Type: Double,
		/// Description: Digitare la latitudine per l'indirizzo primario da usare nella creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address1_latitude => "address1_latitude";

		/// <summary>
		/// Display Name: Indirizzo 1: via 1,
		/// Type: String,
		/// Description: Digitare la prima riga dell'indirizzo primario.
		/// </summary>
		public static string address1_line1 => "address1_line1";

		/// <summary>
		/// Display Name: Indirizzo 1: via 2,
		/// Type: String,
		/// Description: Digitare la seconda riga dell'indirizzo primario.
		/// </summary>
		public static string address1_line2 => "address1_line2";

		/// <summary>
		/// Display Name: Indirizzo 1: via 3,
		/// Type: String,
		/// Description: Digitare la terza riga dell'indirizzo primario.
		/// </summary>
		public static string address1_line3 => "address1_line3";

		/// <summary>
		/// Display Name: Indirizzo 1: longitudine,
		/// Type: Double,
		/// Description: Digitare la longitudine per l'indirizzo primario da usare nella creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address1_longitude => "address1_longitude";

		/// <summary>
		/// Display Name: Indirizzo,
		/// Type: String,
		/// Description: Digitare un nome descrittivo per l'indirizzo primario, ad esempio Sede centrale.
		/// </summary>
		public static string address1_name => "address1_name";

		/// <summary>
		/// Display Name: CAP,
		/// Type: String,
		/// Description: Digitare un codice postale per l'indirizzo primario.
		/// </summary>
		public static string address1_postalcode => "address1_postalcode";

		/// <summary>
		/// Display Name: Indirizzo 1: casella postale,
		/// Type: String,
		/// Description: Digitare il numero di casella postale dell'indirizzo primario.
		/// </summary>
		public static string address1_postofficebox => "address1_postofficebox";

		/// <summary>
		/// Display Name: Indirizzo 1: nome contatto primario,
		/// Type: String,
		/// Description: Digitare il nome del contatto principale presso l'indirizzo primario dell'account.
		/// </summary>
		public static string address1_primarycontactname => "address1_primarycontactname";

		/// <summary>
		/// Display Name: Indirizzo 1: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Trasporto aereo: 1,
		/// Spedizione Postale: 5,
		/// Trasporto stradale: 9,
		/// Trasporto ferroviario: 10,
		/// Trasporto marittimo: 11,
		/// Trasporto intermodale: 18,
		/// Corriere espresso: 28,
		/// Description: Selezionare un metodo di spedizione per le consegne inviate a questo indirizzo.
		/// </summary>
		public static string address1_shippingmethodcode => "address1_shippingmethodcode";

		/// <summary>
		/// Display Name: Provincia,
		/// Type: String,
		/// Description: Digitare la provincia dell'indirizzo primario.
		/// </summary>
		public static string address1_stateorprovince => "address1_stateorprovince";

		/// <summary>
		/// Display Name: Indirizzo 1: telefono,
		/// Type: String,
		/// Description: Digitare il numero di telefono principale associato all'indirizzo primario.
		/// </summary>
		public static string address1_telephone1 => "address1_telephone1";

		/// <summary>
		/// Display Name: Indirizzo 1: telefono 2,
		/// Type: String,
		/// Description: Digitare un secondo numero di telefono associato all'indirizzo primario.
		/// </summary>
		public static string address1_telephone2 => "address1_telephone2";

		/// <summary>
		/// Display Name: Indirizzo 1: telefono 3,
		/// Type: String,
		/// Description: Digitare un terzo numero di telefono associato all'indirizzo primario.
		/// </summary>
		public static string address1_telephone3 => "address1_telephone3";

		/// <summary>
		/// Display Name: Indirizzo 1: zona UPS,
		/// Type: String,
		/// Description: Digitare l'area UPS dell'indirizzo primario per assicurare che le spese di spedizione vengano calcolate correttamente e che le consegne vengano eseguite rapidamente, in caso di spedizione tramite UPS.
		/// </summary>
		public static string address1_upszone => "address1_upszone";

		/// <summary>
		/// Display Name: Indirizzo 1: differenza UTC,
		/// Type: Integer,
		/// Description: Selezionare il fuso orario o la differenza UTC per questo indirizzo in modo che altre persone possano farvi riferimento quando contattano qualcuno presso questo indirizzo.
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
		/// Description: Selezionare il tipo di indirizzo secondario.
		/// </summary>
		public static string address2_addresstypecode => "address2_addresstypecode";

		/// <summary>
		/// Display Name: Indirizzo 2: città,
		/// Type: String,
		/// Description: Digitare la città per l'indirizzo secondario.
		/// </summary>
		public static string address2_city => "address2_city";

		/// <summary>
		/// Display Name: Indirizzo 2,
		/// Type: Memo,
		/// Description: Mostra l'indirizzo secondario completo.
		/// </summary>
		public static string address2_composite => "address2_composite";

		/// <summary>
		/// Display Name: Indirizzo 2: paese,
		/// Type: String,
		/// Description: Digitare il paese per l'indirizzo secondario.
		/// </summary>
		public static string address2_country => "address2_country";

		/// <summary>
		/// Display Name: Indirizzo 2: regione,
		/// Type: String,
		/// Description: Digitare la regione per l'indirizzo secondario.
		/// </summary>
		public static string address2_county => "address2_county";

		/// <summary>
		/// Display Name: Indirizzo 2: fax,
		/// Type: String,
		/// Description: Digitare il numero di fax associato all'indirizzo secondario.
		/// </summary>
		public static string address2_fax => "address2_fax";

		/// <summary>
		/// Display Name: Indirizzo 2: condizioni di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare le condizioni di spedizione per l'indirizzo secondario per assicurare che gli ordini di spedizione vengano elaborati correttamente.
		/// </summary>
		public static string address2_freighttermscode => "address2_freighttermscode";

		/// <summary>
		/// Display Name: Indirizzo 2: latitudine,
		/// Type: Double,
		/// Description: Digitare la latitudine per l'indirizzo secondario da usare nella creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address2_latitude => "address2_latitude";

		/// <summary>
		/// Display Name: Indirizzo 2: via 1,
		/// Type: String,
		/// Description: Digitare la prima riga dell'indirizzo secondario.
		/// </summary>
		public static string address2_line1 => "address2_line1";

		/// <summary>
		/// Display Name: Indirizzo 2: via 2,
		/// Type: String,
		/// Description: Digitare la seconda riga dell'indirizzo secondario.
		/// </summary>
		public static string address2_line2 => "address2_line2";

		/// <summary>
		/// Display Name: Indirizzo 2: via 3,
		/// Type: String,
		/// Description: Digitare la terza riga dell'indirizzo secondario.
		/// </summary>
		public static string address2_line3 => "address2_line3";

		/// <summary>
		/// Display Name: Indirizzo 2: longitudine,
		/// Type: Double,
		/// Description: Digitare la longitudine per l'indirizzo secondario da usare per la creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address2_longitude => "address2_longitude";

		/// <summary>
		/// Display Name: Indirizzo 2: nome,
		/// Type: String,
		/// Description: Digitare un nome descrittivo per l'indirizzo secondario, ad esempio Sede centrale.
		/// </summary>
		public static string address2_name => "address2_name";

		/// <summary>
		/// Display Name: Indirizzo 2: CAP,
		/// Type: String,
		/// Description: Digitare un codice postale per l'indirizzo secondario.
		/// </summary>
		public static string address2_postalcode => "address2_postalcode";

		/// <summary>
		/// Display Name: Indirizzo 2: casella postale,
		/// Type: String,
		/// Description: Digitare il numero di casella postale dell'indirizzo secondario.
		/// </summary>
		public static string address2_postofficebox => "address2_postofficebox";

		/// <summary>
		/// Display Name: Indirizzo 2: nome contatto primario,
		/// Type: String,
		/// Description: Digitare il nome del contatto principale presso l'indirizzo secondario dell'account.
		/// </summary>
		public static string address2_primarycontactname => "address2_primarycontactname";

		/// <summary>
		/// Display Name: Indirizzo 2: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un metodo di spedizione per le consegne inviate a questo indirizzo.
		/// </summary>
		public static string address2_shippingmethodcode => "address2_shippingmethodcode";

		/// <summary>
		/// Display Name: Indirizzo 2: provincia,
		/// Type: String,
		/// Description: Digitare la provincia dell'indirizzo secondario.
		/// </summary>
		public static string address2_stateorprovince => "address2_stateorprovince";

		/// <summary>
		/// Display Name: Indirizzo 2: telefono 1,
		/// Type: String,
		/// Description: Digitare il numero di telefono principale associato all'indirizzo secondario.
		/// </summary>
		public static string address2_telephone1 => "address2_telephone1";

		/// <summary>
		/// Display Name: Indirizzo 2: telefono 2,
		/// Type: String,
		/// Description: Digitare un secondo numero di telefono associato all'indirizzo secondario.
		/// </summary>
		public static string address2_telephone2 => "address2_telephone2";

		/// <summary>
		/// Display Name: Indirizzo 2: telefono 3,
		/// Type: String,
		/// Description: Digitare un terzo numero di telefono associato all'indirizzo secondario.
		/// </summary>
		public static string address2_telephone3 => "address2_telephone3";

		/// <summary>
		/// Display Name: Indirizzo 2: zona UPS,
		/// Type: String,
		/// Description: Digitare l'area UPS dell'indirizzo secondario per assicurare che le spese di spedizione vengano calcolate correttamente e che le consegne vengano eseguite rapidamente, in caso di spedizione tramite UPS.
		/// </summary>
		public static string address2_upszone => "address2_upszone";

		/// <summary>
		/// Display Name: Indirizzo 2: differenza UTC,
		/// Type: Integer,
		/// Description: Selezionare il fuso orario o la differenza UTC per questo indirizzo in modo che altre persone possano farvi riferimento quando contattano qualcuno presso questo indirizzo.
		/// </summary>
		public static string address2_utcoffset => "address2_utcoffset";

		/// <summary>
		/// Display Name: Indirizzo 3: ID,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'indirizzo 3.
		/// </summary>
		public static string address3_addressid => "address3_addressid";

		/// <summary>
		/// Display Name: Indirizzo 3: tipo di indirizzo,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Seleziona il tipo del terzo indirizzo.
		/// </summary>
		public static string address3_addresstypecode => "address3_addresstypecode";

		/// <summary>
		/// Display Name: Indirizzo 3: città,
		/// Type: String,
		/// Description: Digita la città per il terzo indirizzo.
		/// </summary>
		public static string address3_city => "address3_city";

		/// <summary>
		/// Display Name: Indirizzo 3,
		/// Type: Memo,
		/// Description: Mostra il terzo indirizzo completo.
		/// </summary>
		public static string address3_composite => "address3_composite";

		/// <summary>
		/// Display Name: Indirizzo 3: paese,
		/// Type: String,
		/// Description: paese per il terzo indirizzo.
		/// </summary>
		public static string address3_country => "address3_country";

		/// <summary>
		/// Display Name: Indirizzo 3: regione,
		/// Type: String,
		/// Description: Digita la regione per il terzo indirizzo.
		/// </summary>
		public static string address3_county => "address3_county";

		/// <summary>
		/// Display Name: Indirizzo 3: fax,
		/// Type: String,
		/// Description: Digita il numero di fax associato al terzo indirizzo.
		/// </summary>
		public static string address3_fax => "address3_fax";

		/// <summary>
		/// Display Name: Indirizzo 3: condizioni di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Seleziona le condizioni di spedizione per il terzo indirizzo per assicurare che gli ordini di spedizione vengano elaborati correttamente.
		/// </summary>
		public static string address3_freighttermscode => "address3_freighttermscode";

		/// <summary>
		/// Display Name: Indirizzo 3: latitudine,
		/// Type: Double,
		/// Description: Digita la latitudine per il terzo indirizzo da usare nella creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address3_latitude => "address3_latitude";

		/// <summary>
		/// Display Name: Indirizzo 3: via 1,
		/// Type: String,
		/// Description: la prima riga del terzo indirizzo.
		/// </summary>
		public static string address3_line1 => "address3_line1";

		/// <summary>
		/// Display Name: Indirizzo 3: via 2,
		/// Type: String,
		/// Description: la seconda riga del terzo indirizzo.
		/// </summary>
		public static string address3_line2 => "address3_line2";

		/// <summary>
		/// Display Name: Indirizzo 3: via 3,
		/// Type: String,
		/// Description: la terza riga del terzo indirizzo.
		/// </summary>
		public static string address3_line3 => "address3_line3";

		/// <summary>
		/// Display Name: Indirizzo 3: longitudine,
		/// Type: Double,
		/// Description: Digita la longitudine per il terzo indirizzo da usare nella creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address3_longitude => "address3_longitude";

		/// <summary>
		/// Display Name: Indirizzo 3: nome,
		/// Type: String,
		/// Description: Digita un nome descrittivo per il terzo indirizzo, ad esempio Sede centrale.
		/// </summary>
		public static string address3_name => "address3_name";

		/// <summary>
		/// Display Name: Indirizzo 3: CAP,
		/// Type: String,
		/// Description: il codice postale per il terzo indirizzo.
		/// </summary>
		public static string address3_postalcode => "address3_postalcode";

		/// <summary>
		/// Display Name: Indirizzo 3: casella postale,
		/// Type: String,
		/// Description: il numero di casella postale del terzo indirizzo.
		/// </summary>
		public static string address3_postofficebox => "address3_postofficebox";

		/// <summary>
		/// Display Name: Indirizzo 3: nome contatto primario,
		/// Type: String,
		/// Description: Digita il nome del contatto principale presso il terzo indirizzo dell'account.
		/// </summary>
		public static string address3_primarycontactname => "address3_primarycontactname";

		/// <summary>
		/// Display Name: Indirizzo 3: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Seleziona un metodo di spedizione per le consegne inviate a questo indirizzo.
		/// </summary>
		public static string address3_shippingmethodcode => "address3_shippingmethodcode";

		/// <summary>
		/// Display Name: Indirizzo 3: provincia,
		/// Type: String,
		/// Description: la provincia del terzo indirizzo.
		/// </summary>
		public static string address3_stateorprovince => "address3_stateorprovince";

		/// <summary>
		/// Display Name: Indirizzo 3: telefono 1,
		/// Type: String,
		/// Description: Digita il numero di telefono principale associato al terzo indirizzo.
		/// </summary>
		public static string address3_telephone1 => "address3_telephone1";

		/// <summary>
		/// Display Name: Indirizzo 3: telefono 2,
		/// Type: String,
		/// Description: Digita un secondo numero di telefono associato al terzo indirizzo.
		/// </summary>
		public static string address3_telephone2 => "address3_telephone2";

		/// <summary>
		/// Display Name: Indirizzo 3: telefono 3,
		/// Type: String,
		/// Description: Digita un terzo numero di telefono associato all'indirizzo primario.
		/// </summary>
		public static string address3_telephone3 => "address3_telephone3";

		/// <summary>
		/// Display Name: Indirizzo 3: zona UPS,
		/// Type: String,
		/// Description: Digita l'area UPS del terzo indirizzo per assicurare che le spese di spedizione vengano calcolate correttamente e che le consegne vengano eseguite rapidamente, in caso di spedizione tramite UPS.
		/// </summary>
		public static string address3_upszone => "address3_upszone";

		/// <summary>
		/// Display Name: Indirizzo 3: differenza UTC,
		/// Type: Integer,
		/// Description: Seleziona il fuso orario o la differenza UTC per questo indirizzo in modo che altre persone possano farvi riferimento quando contattano qualcuno presso questo indirizzo.
		/// </summary>
		public static string address3_utcoffset => "address3_utcoffset";

		/// <summary>
		/// Display Name: Conferma rimozione password,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string adx_confirmremovepassword => "adx_confirmremovepassword";

		/// <summary>
		/// Display Name: Creato da Indirizzo IP,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_createdbyipaddress => "adx_createdbyipaddress";

		/// <summary>
		/// Display Name: Creato da Nome utente,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_createdbyusername => "adx_createdbyusername";

		/// <summary>
		/// Display Name: Numero accessi non riusciti,
		/// Type: Integer,
		/// Description: Mostra il numero corrente di tentativi di immissione password non riusciti per il contatto.
		/// </summary>
		public static string adx_identity_accessfailedcount => "adx_identity_accessfailedcount";

		/// <summary>
		/// Display Name: Indirizzo e-mail confermato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Determina se l'indirizzo e-mail è confermato dal contatto.
		/// </summary>
		public static string adx_identity_emailaddress1confirmed => "adx_identity_emailaddress1confirmed";

		/// <summary>
		/// Display Name: Ultimo accesso completato,
		/// Type: DateTime,
		/// Description: Indica la data e l'ora dell'ultimo accesso a un portale completato dall'utente.
		/// </summary>
		public static string adx_identity_lastsuccessfullogin => "adx_identity_lastsuccessfullogin";

		/// <summary>
		/// Display Name: Accesso locale disabilitato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica che il contatto non potrà più accedere al portale utilizzando l'account locale.
		/// </summary>
		public static string adx_identity_locallogindisabled => "adx_identity_locallogindisabled";

		/// <summary>
		/// Display Name: Blocco abilitato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Determina se per questo contatto vengono contati i tentativi di accesso non riusciti e se viene bloccato dopo un determinato numero di tentativi non riusciti. Per evitare che il contatto venga bloccato, puoi disabilitare questa impostazione.
		/// </summary>
		public static string adx_identity_lockoutenabled => "adx_identity_lockoutenabled";

		/// <summary>
		/// Display Name: Data fine blocco,
		/// Type: DateTime,
		/// Description: Mostra il momento in cui il contatto bloccato viene sbloccato.
		/// </summary>
		public static string adx_identity_lockoutenddate => "adx_identity_lockoutenddate";

		/// <summary>
		/// Display Name: Accesso abilitato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Determina se l'autenticazione Web è abilitata per il contatto.
		/// </summary>
		public static string adx_identity_logonenabled => "adx_identity_logonenabled";

		/// <summary>
		/// Display Name: Telefono cellulare confermato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Determina se il numero di telefono è confermato dal contatto.
		/// </summary>
		public static string adx_identity_mobilephoneconfirmed => "adx_identity_mobilephoneconfirmed";

		/// <summary>
		/// Display Name: Nuovo input password,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_identity_newpassword => "adx_identity_newpassword";

		/// <summary>
		/// Display Name: Hash password,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_identity_passwordhash => "adx_identity_passwordhash";

		/// <summary>
		/// Display Name: Indicatore sicurezza,
		/// Type: String,
		/// Description: Token usato per gestire la sessione di autenticazione Web.
		/// </summary>
		public static string adx_identity_securitystamp => "adx_identity_securitystamp";

		/// <summary>
		/// Display Name: Autenticazione a due fattori abilitata,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Determina se l'autenticazione a due fattori è abilitata per il contatto.
		/// </summary>
		public static string adx_identity_twofactorenabled => "adx_identity_twofactorenabled";

		/// <summary>
		/// Display Name: Nome utente,
		/// Type: String,
		/// Description: Mostra l'identità utente per l'autenticazione Web locale.
		/// </summary>
		public static string adx_identity_username => "adx_identity_username";

		/// <summary>
		/// Display Name: Modificato da Indirizzo IP,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_modifiedbyipaddress => "adx_modifiedbyipaddress";

		/// <summary>
		/// Display Name: Modificato da Nome utente,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_modifiedbyusername => "adx_modifiedbyusername";

		/// <summary>
		/// Display Name: Organization Name,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_organizationname => "adx_organizationname";

		/// <summary>
		/// Display Name: LCID preferito (deprecato),
		/// Type: Integer,
		/// Description: LCID del portale preferito dell'utente
		/// </summary>
		public static string adx_preferredlcid => "adx_preferredlcid";

		/// <summary>
		/// Display Name: Avviso profilo,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string adx_profilealert => "adx_profilealert";

		/// <summary>
		/// Display Name: Data avviso profilo,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string adx_profilealertdate => "adx_profilealertdate";

		/// <summary>
		/// Display Name: Istruzioni avviso profilo,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string adx_profilealertinstructions => "adx_profilealertinstructions";

		/// <summary>
		/// Display Name: Profilo anonimo,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string adx_profileisanonymous => "adx_profileisanonymous";

		/// <summary>
		/// Display Name: Ultimo impegno profilo,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string adx_profilelastactivity => "adx_profilelastactivity";

		/// <summary>
		/// Display Name: Data modifica profilo,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string adx_profilemodifiedon => "adx_profilemodifiedon";

		/// <summary>
		/// Display Name: Copia profilo pubblico,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string adx_publicprofilecopy => "adx_publicprofilecopy";

		/// <summary>
		/// Display Name: Time Zone,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string adx_timezone => "adx_timezone";

		/// <summary>
		/// Display Name: Scadenza 30,
		/// Type: Money,
		/// Description: Solo per uso di sistema.
		/// </summary>
		public static string aging30 => "aging30";

		/// <summary>
		/// Display Name: Scadenza 30 (base),
		/// Type: Money,
		/// Description: Mostra il campo Scadenza 30 convertito nella valuta di base predefinita del sistema. I calcoli usano il tasso di cambio specificato nell'area Valute.
		/// </summary>
		public static string aging30_base => "aging30_base";

		/// <summary>
		/// Display Name: Scadenza 60,
		/// Type: Money,
		/// Description: Solo per uso di sistema.
		/// </summary>
		public static string aging60 => "aging60";

		/// <summary>
		/// Display Name: Scadenza 60 (base),
		/// Type: Money,
		/// Description: Mostra il campo Scadenza 60 convertito nella valuta di base predefinita del sistema. I calcoli usano il tasso di cambio specificato nell'area Valute.
		/// </summary>
		public static string aging60_base => "aging60_base";

		/// <summary>
		/// Display Name: Scadenza 90,
		/// Type: Money,
		/// Description: Solo per uso di sistema.
		/// </summary>
		public static string aging90 => "aging90";

		/// <summary>
		/// Display Name: Scadenza 90 (base),
		/// Type: Money,
		/// Description: Mostra il campo Scadenza 90 convertito nella valuta di base predefinita del sistema. I calcoli usano il tasso di cambio specificato nell'area Valute.
		/// </summary>
		public static string aging90_base => "aging90_base";

		/// <summary>
		/// Display Name: Anniversario,
		/// Type: DateTime,
		/// Description: Immettere la data dell'anniversario di nozze o del servizio del contatto da usare nei programmi di omaggi ai clienti o in altre comunicazioni.
		/// </summary>
		public static string anniversary => "anniversary";

		/// <summary>
		/// Display Name: Reddito annuale,
		/// Type: Money,
		/// Description: Digitare il reddito annuale del contatto da usare nella definizione di profili e nell'analisi finanziaria.
		/// </summary>
		public static string annualincome => "annualincome";

		/// <summary>
		/// Display Name: Reddito annuale (base),
		/// Type: Money,
		/// Description: Mostra il campo Reddito annuale convertito nella valuta di base predefinita del sistema. I calcoli usano il tasso di cambio specificato nell'area Valute.
		/// </summary>
		public static string annualincome_base => "annualincome_base";

		/// <summary>
		/// Display Name: Assistente,
		/// Type: String,
		/// Description: Digitare il nome dell'assistente del contatto.
		/// </summary>
		public static string assistantname => "assistantname";

		/// <summary>
		/// Display Name: Telefono assistente,
		/// Type: String,
		/// Description: Digitare il numero di telefono dell'assistente del contatto.
		/// </summary>
		public static string assistantphone => "assistantphone";

		/// <summary>
		/// Display Name: Compleanno,
		/// Type: DateTime,
		/// Description: Immettere il compleanno del contatto da usare nei programmi di omaggi ai clienti o in altre comunicazioni.
		/// </summary>
		public static string birthdate => "birthdate";

		/// <summary>
		/// Display Name: Telefono ufficio 2,
		/// Type: String,
		/// Description: Digita un secondo numero di telefono ufficio per questo contatto.
		/// </summary>
		public static string business2 => "business2";

		/// <summary>
		/// Display Name: Biglietto da visita,
		/// Type: Memo,
		/// Description: Archivia l'immagine del biglietto da visita
		/// </summary>
		public static string businesscard => "businesscard";

		/// <summary>
		/// Display Name: BusinessCardAttributes,
		/// Type: String,
		/// Description: Archivia le proprietà del controllo Biglietto da visita.
		/// </summary>
		public static string businesscardattributes => "businesscardattributes";

		/// <summary>
		/// Display Name: Numero richiamata,
		/// Type: String,
		/// Description: Digita un numero di telefono di richiamata per questo contatto.
		/// </summary>
		public static string callback => "callback";

		/// <summary>
		/// Display Name: Nomi figli,
		/// Type: String,
		/// Description: Digitare i nomi dei figli del contatto, come riferimento nelle comunicazioni e nei programmi per i clienti.
		/// </summary>
		public static string childrensnames => "childrensnames";

		/// <summary>
		/// Display Name: Telefono società,
		/// Type: String,
		/// Description: Digita il numero di telefono aziendale del contatto.
		/// </summary>
		public static string company => "company";

		/// <summary>
		/// Display Name: Contatto,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco del contatto.
		/// </summary>
		public static string contactid => "contactid";

		/// <summary>
		/// Display Name: Autore (parte esterna),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Mostra la parte esterna che ha creato il record.
		/// </summary>
		public static string createdbyexternalparty => "createdbyexternalparty";

		/// <summary>
		/// Display Name: Limite di credito,
		/// Type: Money,
		/// Description: Digitare il limite di credito del contatto, come riferimento per la risoluzione di problemi contabili e di fatturazione con il cliente.
		/// </summary>
		public static string creditlimit => "creditlimit";

		/// <summary>
		/// Display Name: Limite di credito (base),
		/// Type: Money,
		/// Description: Mostra il campo Limite di credito convertito nella valuta di base predefinita del sistema per scopi di report. I calcoli usano il tasso di cambio specificato nell'area Valute.
		/// </summary>
		public static string creditlimit_base => "creditlimit_base";

		/// <summary>
		/// Display Name: Blocco del credito,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Selezionare se il contatto presenta un blocco del credito, come riferimento per la risoluzione di problemi contabili e di fatturazione con il cliente.
		/// </summary>
		public static string creditonhold => "creditonhold";

		/// <summary>
		/// Display Name: Dimensioni cliente,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare le dimensioni della società del contatto per scopi di segmentazione e report.
		/// </summary>
		public static string customersizecode => "customersizecode";

		/// <summary>
		/// Display Name: Tipo relazione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare la categoria che meglio descrive la relazione tra il contatto e l'organizzazione.
		/// </summary>
		public static string customertypecode => "customertypecode";

		/// <summary>
		/// Display Name: Listino prezzi,
		/// Type: Lookup,
		/// Related entities: pricelevel,
		/// Description: Scegli il listino prezzi predefinito associato al contatto per assicurare che vengano applicati i prezzi dei prodotti corretti per il cliente in opportunità di vendita, offerte e ordini.
		/// </summary>
		public static string defaultpricelevelid => "defaultpricelevelid";

		/// <summary>
		/// Display Name: Reparto,
		/// Type: String,
		/// Description: Digitare il reparto o la Business Unit in cui il contatto lavora nella società o azienda padre.
		/// </summary>
		public static string department => "department";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Digitare informazioni aggiuntive per descrivere il contatto, ad esempio un estratto del sito Web della società.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Non consentire invio di messaggi e-mail in blocco,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se il contatto accetta l'invio di e-mail in blocco tramite campagne di marketing o mini-campagne. Se si seleziona Non consentire, il contatto potrà essere aggiunto agli elenchi marketing ma sarà escluso dalle e-mail.
		/// </summary>
		public static string donotbulkemail => "donotbulkemail";

		/// <summary>
		/// Display Name: Non consentire posta inviata in blocco,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Selezionare se il contatto accetta l'invio di posta in blocco tramite campagne di marketing o mini-campagne. Se si seleziona Non consentire, il contatto potrà essere aggiunto agli elenchi marketing ma sarà escluso dalle lettere.
		/// </summary>
		public static string donotbulkpostalmail => "donotbulkpostalmail";

		/// <summary>
		/// Display Name: Non consentire invio di messaggi e-mail,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Seleziona se il contatto consente l'invio di e-mail dirette da Microsoft Dynamics 365. Se si seleziona Non consentire, Microsoft Dynamics 365 non invierà e-mail.
		/// </summary>
		public static string donotemail => "donotemail";

		/// <summary>
		/// Display Name: Non consentire fax,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se il contatto consente i fax. Se si seleziona Non consentire, il contatto sarà escluso da qualsiasi impegno di tipo fax distribuito nelle campagne di marketing.
		/// </summary>
		public static string donotfax => "donotfax";

		/// <summary>
		/// Display Name: Non consentire telefonate,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se il contatto accetta le telefonate. Se si seleziona Non consentire, il contatto sarà escluso da qualsiasi impegno di tipo telefonata distribuito nelle campagne di marketing.
		/// </summary>
		public static string donotphone => "donotphone";

		/// <summary>
		/// Display Name: Non consentire posta,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se il contatto consente la posta diretta. Se si seleziona Non consentire, il contatto sarà escluso dagli impegni di tipo lettera distribuiti nelle campagne di marketing.
		/// </summary>
		public static string donotpostalmail => "donotpostalmail";

		/// <summary>
		/// Display Name: Consenti invio materiale marketing,
		/// Type: Boolean,
		/// Values:
		/// Non inviare: 1,
		/// Invia: 0,
		/// Description: Selezionare se il contatto accetta materiale di marketing come brochure o cataloghi. I contatti che rifiutano esplicitamente possono essere esclusi dalle iniziative di marketing.
		/// </summary>
		public static string donotsendmm => "donotsendmm";

		/// <summary>
		/// Display Name: Titolo di studio,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare il livello di istruzione più elevato del contatto da usare nella segmentazione e nell'analisi.
		/// </summary>
		public static string educationcode => "educationcode";

		/// <summary>
		/// Display Name: E-mail,
		/// Type: String,
		/// Description: Digitare l'indirizzo e-mail primario per il contatto.
		/// </summary>
		public static string emailaddress1 => "emailaddress1";

		/// <summary>
		/// Display Name: Indirizzo e-mail 2,
		/// Type: String,
		/// Description: Digitare l'indirizzo e-mail secondario per il contatto.
		/// </summary>
		public static string emailaddress2 => "emailaddress2";

		/// <summary>
		/// Display Name: Indirizzo e-mail 3,
		/// Type: String,
		/// Description: Digitare un indirizzo e-mail alternativo per il contatto.
		/// </summary>
		public static string emailaddress3 => "emailaddress3";

		/// <summary>
		/// Display Name: Dipendente,
		/// Type: String,
		/// Description: Digitare un numero o ID dipendente per il contatto, come riferimento negli ordini, nei casi di servizio o in altre comunicazioni con l'organizzazione del contatto.
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
		/// Description: Mostra il tasso di conversione della valuta del record. Il tasso di cambio è usato per convertire tutti i campi di tipo money nel record dalla valuta locale alla valuta predefinita del sistema.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Identificatore utente esterno,
		/// Type: String,
		/// Description: Identificatore di un utente esterno.
		/// </summary>
		public static string externaluseridentifier => "externaluseridentifier";

		/// <summary>
		/// Display Name: Stato civile,
		/// Type: Picklist,
		/// Values:
		/// Celibe/Nubile: 1,
		/// Coniugato: 2,
		/// Divorziato: 3,
		/// Vedovo: 4,
		/// Description: Selezionare lo stato civile del contatto, come riferimento nelle telefonate di completamento e in altre comunicazioni.
		/// </summary>
		public static string familystatuscode => "familystatuscode";

		/// <summary>
		/// Display Name: Fax,
		/// Type: String,
		/// Description: Digitare il numero di fax per il contatto.
		/// </summary>
		public static string fax => "fax";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Digitare il nome del contatto per assicurare che il contatto sia indicato correttamente nelle chiamate di vendita, nelle e-mail e nelle campagne di marketing.
		/// </summary>
		public static string firstname => "firstname";

		/// <summary>
		/// Display Name: Segui impegno e-mail,
		/// Type: Boolean,
		/// Values:
		/// Consenti: 1,
		/// Non consentire: 0,
		/// Description: Specifica se consentire o meno che gli impegni dei messaggi e-mail inviati al contatto vengano seguiti fornendo informazioni quali: numero di volte in cui un messaggio viene aperto, numero di visualizzazioni degli allegati e numero di clic sul collegamento.
		/// </summary>
		public static string followemail => "followemail";

		/// <summary>
		/// Display Name: Sito FTP,
		/// Type: String,
		/// Description: Digitare l'URL per il sito FTP del contatto per consentire agli utenti di accedere ai dati e di condividere documenti.
		/// </summary>
		public static string ftpsiteurl => "ftpsiteurl";

		/// <summary>
		/// Display Name: Nome completo,
		/// Type: String,
		/// Description: Combina e mostra nome e cognome del contatto per consentire di mostrare il nome completo in visualizzazioni e report.
		/// </summary>
		public static string fullname => "fullname";

		/// <summary>
		/// Display Name: Sesso,
		/// Type: Picklist,
		/// Values:
		/// Maschile: 1,
		/// Femminile: 2,
		/// Description: Selezionare il sesso del contatto per assicurare che il contatto sia indicato correttamente nelle chiamate di vendita, nelle e-mail e nelle campagne di marketing.
		/// </summary>
		public static string gendercode => "gendercode";

		/// <summary>
		/// Display Name: Codice fiscale/partita IVA,
		/// Type: String,
		/// Description: Digitare il numero di passaporto oppure codice fiscale o partita IVA del contatto da usare in documenti o report.
		/// </summary>
		public static string governmentid => "governmentid";

		/// <summary>
		/// Display Name: Figli,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare se il contatto ha figli, come riferimento nelle telefonate di completamento e in altre comunicazioni.
		/// </summary>
		public static string haschildrencode => "haschildrencode";

		/// <summary>
		/// Display Name: Telefono abitazione 2,
		/// Type: String,
		/// Description: Digita un secondo numero di telefono abitazione per questo contatto.
		/// </summary>
		public static string home2 => "home2";

		/// <summary>
		/// Display Name: Creato automaticamente,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il contatto è stato creato automaticamente durante la conversione di un messaggio e-mail o di un appuntamento.
		/// </summary>
		public static string isautocreate => "isautocreate";

		/// <summary>
		/// Display Name: Cliente back office,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Selezionare se il contatto è presente in un sistema di contabilità separato o in un altro sistema, ad esempio Microsoft Dynamics GP o un altro database ERP, da usare per scopi di integrazione.
		/// </summary>
		public static string isbackofficecustomer => "isbackofficecustomer";

		/// <summary>
		/// Display Name: Posizione,
		/// Type: String,
		/// Description: Digitare la posizione del contatto per assicurare che il contatto sia indicato correttamente nelle chiamate di vendita, nelle e-mail e nelle campagne di marketing.
		/// </summary>
		public static string jobtitle => "jobtitle";

		/// <summary>
		/// Display Name: Cognome,
		/// Type: String,
		/// Description: Digitare il cognome del contatto per assicurare che il contatto sia indicato correttamente nelle chiamate di vendita, nelle e-mail e nelle campagne di marketing.
		/// </summary>
		public static string lastname => "lastname";

		/// <summary>
		/// Display Name: Ultimo periodo sospensione,
		/// Type: DateTime,
		/// Description: Contiene l'indicatore di data e ora dell'ultimo periodo di sospensione.
		/// </summary>
		public static string lastonholdtime => "lastonholdtime";

		/// <summary>
		/// Display Name: Data ultima inclusione in campagna,
		/// Type: DateTime,
		/// Description: Mostra la data dell'ultima volta in cui il contatto è stato incluso in una campagna di marketing o in una mini-campagna.
		/// </summary>
		public static string lastusedincampaign => "lastusedincampaign";

		/// <summary>
		/// Display Name: Fonte,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare l'origine di marketing primaria che ha diretto il contatto all'organizzazione.
		/// </summary>
		public static string leadsourcecode => "leadsourcecode";

		/// <summary>
		/// Display Name: Responsabile,
		/// Type: String,
		/// Description: Digitare il nome del responsabile del contatto da usare nella riassegnazione dei problemi o in altre comunicazioni di completamento con il contatto.
		/// </summary>
		public static string managername => "managername";

		/// <summary>
		/// Display Name: Telefono responsabile,
		/// Type: String,
		/// Description: Digitare il numero di telefono del responsabile del contatto.
		/// </summary>
		public static string managerphone => "managerphone";

		/// <summary>
		/// Display Name: Solo marketing,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Specifica se è solo per marketing
		/// </summary>
		public static string marketingonly => "marketingonly";

		/// <summary>
		/// Display Name: ID master,
		/// Type: Lookup,
		/// Related entities: contact,
		/// Description: Identificatore univoco del contatto master per l'unione.
		/// </summary>
		public static string masterid => "masterid";

		/// <summary>
		/// Display Name: Unito,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Mostra se l'account è stato unito a un contatto master.
		/// </summary>
		public static string merged => "merged";

		/// <summary>
		/// Display Name: Secondo nome,
		/// Type: String,
		/// Description: Digitare il secondo nome o la relativa iniziale del contatto per assicurare che il contatto sia indicato correttamente.
		/// </summary>
		public static string middlename => "middlename";

		/// <summary>
		/// Display Name: Cellulare,
		/// Type: String,
		/// Description: Digitare il numero di cellulare del contatto.
		/// </summary>
		public static string mobilephone => "mobilephone";

		/// <summary>
		/// Display Name: Autore modifica (parte esterna),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Mostra la parte esterna che ha modificato il record.
		/// </summary>
		public static string modifiedbyexternalparty => "modifiedbyexternalparty";

		/// <summary>
		/// Display Name: Partner responsabile,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Identificatore univoco per l'account associato al contatto.
		/// </summary>
		public static string msa_managingpartnerid => "msa_managingpartnerid";

		/// <summary>
		/// Display Name: KPI,
		/// Type: Lookup,
		/// Related entities: msdyn_contactkpiitem,
		/// Description: Mapping ai record KPI contatti
		/// </summary>
		public static string msdyn_contactkpiid => "msdyn_contactkpiid";

		/// <summary>
		/// Display Name: Etichette influenza decisione,
		/// Type: Picklist,
		/// Values:
		/// Decisore: 0,
		/// Influencer: 1,
		/// Blocco: 2,
		/// Sconosciuto: 3,
		/// Description: Indica l'influenza di acquisto utilizzando le etichette
		/// </summary>
		public static string msdyn_decisioninfluencetag => "msdyn_decisioninfluencetag";

		/// <summary>
		/// Display Name: Disabilita registrazione Web,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica che il contatto ha rifiutato esplicitamente la registrazione Web.
		/// </summary>
		public static string msdyn_disablewebtracking => "msdyn_disablewebtracking";

		/// <summary>
		/// Display Name: Rifiuto esplicito GDPR,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il contatto è stato rifiutato esplicitamente o meno
		/// </summary>
		public static string msdyn_gdproptout => "msdyn_gdproptout";

		/// <summary>
		/// Display Name: Assistente,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il contatto è un assistente nell'organigramma
		/// </summary>
		public static string msdyn_isassistantinorgchart => "msdyn_isassistantinorgchart";

		/// <summary>
		/// Display Name: È un minore,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica che il contatto è considerato un minore nella giurisdizione di competenza.
		/// </summary>
		public static string msdyn_isminor => "msdyn_isminor";

		/// <summary>
		/// Display Name: Minore con consenso dei genitori,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica che il contatto è considerato un minore nella giurisdizione di competenza ma i genitori hanno fornito il consenso.
		/// </summary>
		public static string msdyn_isminorwithparentalconsent => "msdyn_isminorwithparentalconsent";

		/// <summary>
		/// Display Name: Flag Non in azienda,
		/// Type: Picklist,
		/// Values:
		/// Nessun commento: 0,
		/// Non in azienda: 1,
		/// Ignora: 2,
		/// Description: Definisce se il contatto appartiene o meno all'account associato
		/// </summary>
		public static string msdyn_orgchangestatus => "msdyn_orgchangestatus";

		/// <summary>
		/// Display Name: Data di accettazione delle condizioni del portale,
		/// Type: DateTime,
		/// Description: Indica la data e l'ora in cui la persona ha accettato le condizioni del portale.
		/// </summary>
		public static string msdyn_portaltermsagreementdate => "msdyn_portaltermsagreementdate";

		/// <summary>
		/// Display Name: Fuso orario principale,
		/// Type: Integer,
		/// Description: Indica il fuso orario primario in cui lavora il contatto.
		/// </summary>
		public static string msdyn_primarytimezone => "msdyn_primarytimezone";

		/// <summary>
		/// Display Name: Lingua preferita,
		/// Type: Picklist,
		/// Values:
		/// Arabo: 1025,
		/// Basco - Province basche: 1069,
		/// Bulgaro - Bulgaria: 1026,
		/// Catalano - Catalogna: 1027,
		/// Cinese - Cina: 2052,
		/// Cinese – RAS di Hong Kong: 3076,
		/// Cinese - Tradizionale: 1028,
		/// Croato - Croazia: 1050,
		/// Ceco - Repubblica Ceca: 1029,
		/// Danese - Danimarca: 1030,
		/// Olandese - Paesi Bassi: 1043,
		/// Inglese: 1033,
		/// Estone - Estonia: 1061,
		/// Finlandese - Finlandia: 1035,
		/// Francese - Francia: 1036,
		/// Gallego - Spagna: 1110,
		/// Tedesco - Germania: 1031,
		/// Greco - Grecia: 1032,
		/// Ebraico: 1037,
		/// Hindi – India: 1081,
		/// Ungherese - Ungheria: 1038,
		/// Indonesiano - Indonesia: 1057,
		/// Italiano - Italia: 1040,
		/// Giapponese - Giappone: 1041,
		/// Kazako - Kazakistan: 1087,
		/// Coreano - Corea del Sud: 1042,
		/// Lettone - Lettonia: 1062,
		/// Lituano - Lituania: 1063,
		/// Malese - Malaysia: 1086,
		/// Norvegese (Bokmål) - Norvegia: 1044,
		/// Polacco - Polonia: 1045,
		/// Portoghese - Brasile: 1046,
		/// Portoghese - Portogallo: 2070,
		/// Rumeno - Romania: 1048,
		/// Russo - Russia: 1049,
		/// Serbo (alfabeto cirillico) - Serbia: 3098,
		/// Serbo (alfabeto latino) - Serbia: 2074,
		/// Slovacco - Slovacchia: 1051,
		/// Sloveno - Slovenia: 1060,
		/// Spagnolo (ordinamento tradizionale) - Spagna: 3082,
		/// Svedese - Svezia: 1053,
		/// Thai - Thailandia: 1054,
		/// Turco - Turchia: 1055,
		/// Ucraino - Ucraina: 1058,
		/// Vietnamita - Vietnam: 1066,
		/// Description: Lingua portale preferita dell'utente
		/// </summary>
		public static string mspp_userpreferredlcid => "mspp_userpreferredlcid";

		/// <summary>
		/// Display Name: Nome alternativo,
		/// Type: String,
		/// Description: Digitare il nome alternativo del contatto.
		/// </summary>
		public static string nickname => "nickname";

		/// <summary>
		/// Display Name: N. figli,
		/// Type: Integer,
		/// Description: Digitare il numero di figli del contatto, come riferimento nelle telefonate di completamento e in altre comunicazioni.
		/// </summary>
		public static string numberofchildren => "numberofchildren";

		/// <summary>
		/// Display Name: Periodo di sospensione (minuti),
		/// Type: Integer,
		/// Description: Mostra la durata della sospensione del record in minuti.
		/// </summary>
		public static string onholdtime => "onholdtime";

		/// <summary>
		/// Display Name: Lead di origine,
		/// Type: Lookup,
		/// Related entities: lead,
		/// Description: Mostra il lead da cui è stato creato il contatto, se è stato creato convertendo un lead in Microsoft Dynamics 365. Usato per associare il contatto ai dati sul lead di origine a scopo di report e analisi.
		/// </summary>
		public static string originatingleadid => "originatingleadid";

		/// <summary>
		/// Display Name: Cellulare 2,
		/// Type: String,
		/// Description: Digitare il numero di cercapersone per il contatto.
		/// </summary>
		public static string pager => "pager";

		/// <summary>
		/// Display Name: Contatto padre,
		/// Type: Lookup,
		/// Related entities: contact,
		/// Description: Identificatore univoco del contatto padre.
		/// </summary>
		public static string parentcontactid => "parentcontactid";

		/// <summary>
		/// Display Name: Nome società,
		/// Type: Customer,
		/// Description: Selezionare l'account padre o il contatto padre per il contatto per fornire un collegamento rapido a dettagli aggiuntivi, ad esempio informazioni finanziarie, impegni e opportunità.
		/// </summary>
		public static string parentcustomerid => "parentcustomerid";

		/// <summary>
		/// Display Name: Tipo di cliente padre,
		/// Type: EntityName,
		/// Description: 
		/// </summary>
		public static string parentcustomeridtype => "parentcustomeridtype";

		/// <summary>
		/// Display Name: Partecipa al flusso di lavoro,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Mostra se il contatto partecipa alle regole del flusso di lavoro.
		/// </summary>
		public static string participatesinworkflow => "participatesinworkflow";

		/// <summary>
		/// Display Name: Condizioni di pagamento,
		/// Type: Picklist,
		/// Values:
		/// 30 gg.: 1,
		/// 60 gg.: 4,
		/// 90 gg.: 9,
		/// Pagamento in contanti: 10,
		/// Description: Selezionare le condizioni di pagamento per indicare quando il cliente deve pagare l'importo totale.
		/// </summary>
		public static string paymenttermscode => "paymenttermscode";

		/// <summary>
		/// Display Name: Giorno preferito,
		/// Type: Picklist,
		/// Values:
		/// Domenica: 0,
		/// Lunedì: 1,
		/// Martedì: 2,
		/// Mercoledì: 3,
		/// Giovedì: 4,
		/// Venerdì: 5,
		/// Sabato: 6,
		/// Description: Selezionare il giorno della settimana preferito per gli appuntamenti di tipo servizio.
		/// </summary>
		public static string preferredappointmentdaycode => "preferredappointmentdaycode";

		/// <summary>
		/// Display Name: Orario preferito,
		/// Type: Picklist,
		/// Values:
		/// Mattino: 1,
		/// Pomeriggio: 2,
		/// Sera: 3,
		/// Description: Selezionare l'ora del giorno preferita per gli appuntamenti di tipo servizio.
		/// </summary>
		public static string preferredappointmenttimecode => "preferredappointmenttimecode";

		/// <summary>
		/// Display Name: Metodo di contatto preferito,
		/// Type: Picklist,
		/// Values:
		/// Tutti: 1,
		/// E-mail: 2,
		/// Telefono: 3,
		/// Fax: 4,
		/// Posta: 5,
		/// Description: Selezionare il metodo di contatto preferito.
		/// </summary>
		public static string preferredcontactmethodcode => "preferredcontactmethodcode";

		/// <summary>
		/// Display Name: Struttura/attrezzature preferite,
		/// Type: Lookup,
		/// Related entities: equipment,
		/// Description: Scegli le attrezzature o la struttura di servizio preferita del contatto per assicurare che i servizi siano pianificati correttamente per il cliente.
		/// </summary>
		public static string preferredequipmentid => "preferredequipmentid";

		/// <summary>
		/// Display Name: Servizio preferito,
		/// Type: Lookup,
		/// Related entities: service,
		/// Description: Scegli il servizio preferito del contatto per assicurare che i servizi siano pianificati correttamente per il cliente.
		/// </summary>
		public static string preferredserviceid => "preferredserviceid";

		/// <summary>
		/// Display Name: Utente preferito,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Scegliere il rappresentante del servizio clienti normale o preferito, come riferimento durante la pianificazione degli impegni di tipo servizio per il contatto.
		/// </summary>
		public static string preferredsystemuserid => "preferredsystemuserid";

		/// <summary>
		/// Display Name: Processo,
		/// Type: Uniqueidentifier,
		/// Description: Mostra l'ID del processo.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Nazione,
		/// Type: Lookup,
		/// Related entities: res_country,
		/// Description: 
		/// </summary>
		public static string res_countryid => "res_countryid";

		/// <summary>
		/// Display Name: Località,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_location => "res_location";

		/// <summary>
		/// Display Name: Titolo,
		/// Type: String,
		/// Description: Digitare il titolo del contatto per assicurare che il contatto sia indicato correttamente nelle chiamate di vendita, nei messaggi e-mail e nelle campagne di marketing.
		/// </summary>
		public static string salutation => "salutation";

		/// <summary>
		/// Display Name: Metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un metodo di spedizione per le consegne inviate a questo indirizzo.
		/// </summary>
		public static string shippingmethodcode => "shippingmethodcode";

		/// <summary>
		/// Display Name: CONTRATTO DI SERVIZIO,
		/// Type: Lookup,
		/// Related entities: sla,
		/// Description: Scegli il contratto di servizio da applicare al record del contatto.
		/// </summary>
		public static string slaid => "slaid";

		/// <summary>
		/// Display Name: Ultimo contratto di servizio applicato,
		/// Type: Lookup,
		/// Related entities: sla,
		/// Description: Ultimo contratto di servizio applicato al caso. Questo campo è solo per uso interno.
		/// </summary>
		public static string slainvokedid => "slainvokedid";

		/// <summary>
		/// Display Name: Nome coniuge/partner,
		/// Type: String,
		/// Description: Digitare il nome del coniuge o del partner del contatto, come riferimento durante chiamate, eventi o altre comunicazioni.
		/// </summary>
		public static string spousesname => "spousesname";

		/// <summary>
		/// Display Name: (Deprecata) Fase del processo,
		/// Type: Uniqueidentifier,
		/// Description: Mostra l'ID della fase.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Sottoscrizione,
		/// Type: Uniqueidentifier,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string subscriptionid => "subscriptionid";

		/// <summary>
		/// Display Name: Suffisso,
		/// Type: String,
		/// Description: Digitare il suffisso usato nel nome del contatto, ad esempio Jr. o Sr., per assicurare che il contatto sia indicato correttamente nelle chiamate di vendita, nelle e-mail e nelle campagne di marketing.
		/// </summary>
		public static string suffix => "suffix";

		/// <summary>
		/// Display Name: TeamsFollowed,
		/// Type: Integer,
		/// Description: Numero di utenti o conversazioni che hanno seguito il record
		/// </summary>
		public static string teamsfollowed => "teamsfollowed";

		/// <summary>
		/// Display Name: Telefono ufficio,
		/// Type: String,
		/// Description: Digitare il numero di telefono principale per questo contatto.
		/// </summary>
		public static string telephone1 => "telephone1";

		/// <summary>
		/// Display Name: Telefono abitazione,
		/// Type: String,
		/// Description: Digitare un secondo numero di telefono per questo contatto.
		/// </summary>
		public static string telephone2 => "telephone2";

		/// <summary>
		/// Display Name: Telefono 3,
		/// Type: String,
		/// Description: Digitare un terzo numero di telefono per questo contatto.
		/// </summary>
		public static string telephone3 => "telephone3";

		/// <summary>
		/// Display Name: Area,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un'area per il contatto da usare nella segmentazione e nell'analisi.
		/// </summary>
		public static string territorycode => "territorycode";

		/// <summary>
		/// Display Name: Tempo dedicato personalmente,
		/// Type: String,
		/// Description: Tempo totale dedicato personalmente ai messaggi e-mail (lettura e scrittura) e alle riunioni relativamente al record del contatto.
		/// </summary>
		public static string timespentbymeonemailandmeetings => "timespentbymeonemailandmeetings";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Scegliere la valuta locale per il record per assicurare che i preventivi vengano espressi nella valuta corretta.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";

		/// <summary>
		/// Display Name: (Deprecata) Percorso incrociato,
		/// Type: String,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string traversedpath => "traversedpath";

		/// <summary>
		/// Display Name: Sito Web,
		/// Type: String,
		/// Description: Digitare l'URL del sito Web professionale o personale o del blog del contatto.
		/// </summary>
		public static string websiteurl => "websiteurl";

		/// <summary>
		/// Display Name: Nome Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del nome del contatto, se specificato in giapponese, per assicurare che il nome venga pronunciato correttamente nelle telefonate con il contatto.
		/// </summary>
		public static string yomifirstname => "yomifirstname";

		/// <summary>
		/// Display Name: Nome completo Yomi,
		/// Type: String,
		/// Description: Mostra il nome e cognome Yomi combinati del contatto per consentire di mostrare il nome fonetico completo in visualizzazioni e report.
		/// </summary>
		public static string yomifullname => "yomifullname";

		/// <summary>
		/// Display Name: Cognome Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del cognome del contatto, se specificato in giapponese, per assicurare che il nome venga pronunciato correttamente nelle telefonate con il contatto.
		/// </summary>
		public static string yomilastname => "yomilastname";

		/// <summary>
		/// Display Name: Secondo nome Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del secondo nome del contatto, se specificato in giapponese, per assicurare che il nome venga pronunciato correttamente nelle telefonate con il contatto.
		/// </summary>
		public static string yomimiddlename => "yomimiddlename";


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

		/// <summary>
		/// Values for field Ruolo
		/// <summary>
		public enum accountrolecodeValues
		{
			Decisore = 1,
			Dipendente = 2,
			Influenzatore = 3
		}

		/// <summary>
		/// Values for field Indirizzo 1: tipo di indirizzo
		/// <summary>
		public enum address1_addresstypecodeValues
		{
			Altro = 4,
			Fatturazione = 1,
			Primario = 3,
			Spedizione = 2
		}

		/// <summary>
		/// Values for field Indirizzo 1: condizioni di spedizione
		/// <summary>
		public enum address1_freighttermscodeValues
		{
			CFRCostoenolo = 3,
			CIFCostoassenolo = 4,
			CIPTraspeasspagatifinoa = 5,
			CPTTrasportopagatofinoa = 6,
			DAFResofrontiera = 7,
			DDPResosdoganato = 10,
			DDUResononsdoganato = 11,
			DEQResobanchina = 8,
			DESResoexship = 9,
			EXWFrancofabbrica = 12,
			FASFrancolungobordo = 13,
			FCAFrancovettore = 14,
			FOBFrancoabordo = 1,
			Gratis = 2
		}

		/// <summary>
		/// Values for field Indirizzo 1: metodo di spedizione
		/// <summary>
		public enum address1_shippingmethodcodeValues
		{
			Corriereespresso = 28,
			SpedizionePostale = 5,
			Trasportoaereo = 1,
			Trasportoferroviario = 10,
			Trasportointermodale = 18,
			Trasportomarittimo = 11,
			Trasportostradale = 9
		}

		/// <summary>
		/// Values for field Indirizzo 2: tipo di indirizzo
		/// <summary>
		public enum address2_addresstypecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Indirizzo 2: condizioni di spedizione
		/// <summary>
		public enum address2_freighttermscodeValues
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
		/// Values for field Indirizzo 3: tipo di indirizzo
		/// <summary>
		public enum address3_addresstypecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Indirizzo 3: condizioni di spedizione
		/// <summary>
		public enum address3_freighttermscodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Indirizzo 3: metodo di spedizione
		/// <summary>
		public enum address3_shippingmethodcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Conferma rimozione password
		/// <summary>
		public enum adx_confirmremovepasswordValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Indirizzo e-mail confermato
		/// <summary>
		public enum adx_identity_emailaddress1confirmedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Accesso locale disabilitato
		/// <summary>
		public enum adx_identity_locallogindisabledValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Blocco abilitato
		/// <summary>
		public enum adx_identity_lockoutenabledValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Accesso abilitato
		/// <summary>
		public enum adx_identity_logonenabledValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Telefono cellulare confermato
		/// <summary>
		public enum adx_identity_mobilephoneconfirmedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Autenticazione a due fattori abilitata
		/// <summary>
		public enum adx_identity_twofactorenabledValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Avviso profilo
		/// <summary>
		public enum adx_profilealertValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Profilo anonimo
		/// <summary>
		public enum adx_profileisanonymousValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Blocco del credito
		/// <summary>
		public enum creditonholdValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Dimensioni cliente
		/// <summary>
		public enum customersizecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Tipo relazione
		/// <summary>
		public enum customertypecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Non consentire invio di messaggi e-mail in blocco
		/// <summary>
		public enum donotbulkemailValues
		{
			Consenti = 0,
			Nonconsentire = 1
		}

		/// <summary>
		/// Values for field Non consentire posta inviata in blocco
		/// <summary>
		public enum donotbulkpostalmailValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Non consentire invio di messaggi e-mail
		/// <summary>
		public enum donotemailValues
		{
			Consenti = 0,
			Nonconsentire = 1
		}

		/// <summary>
		/// Values for field Non consentire fax
		/// <summary>
		public enum donotfaxValues
		{
			Consenti = 0,
			Nonconsentire = 1
		}

		/// <summary>
		/// Values for field Non consentire telefonate
		/// <summary>
		public enum donotphoneValues
		{
			Consenti = 0,
			Nonconsentire = 1
		}

		/// <summary>
		/// Values for field Non consentire posta
		/// <summary>
		public enum donotpostalmailValues
		{
			Consenti = 0,
			Nonconsentire = 1
		}

		/// <summary>
		/// Values for field Consenti invio materiale marketing
		/// <summary>
		public enum donotsendmmValues
		{
			Invia = 0,
			Noninviare = 1
		}

		/// <summary>
		/// Values for field Titolo di studio
		/// <summary>
		public enum educationcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Stato civile
		/// <summary>
		public enum familystatuscodeValues
		{
			CelibeNubile = 1,
			Coniugato = 2,
			Divorziato = 3,
			Vedovo = 4
		}

		/// <summary>
		/// Values for field Segui impegno e-mail
		/// <summary>
		public enum followemailValues
		{
			Consenti = 1,
			Nonconsentire = 0
		}

		/// <summary>
		/// Values for field Sesso
		/// <summary>
		public enum gendercodeValues
		{
			Femminile = 2,
			Maschile = 1
		}

		/// <summary>
		/// Values for field Figli
		/// <summary>
		public enum haschildrencodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Creato automaticamente
		/// <summary>
		public enum isautocreateValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Cliente back office
		/// <summary>
		public enum isbackofficecustomerValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Fonte
		/// <summary>
		public enum leadsourcecodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Solo marketing
		/// <summary>
		public enum marketingonlyValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Unito
		/// <summary>
		public enum mergedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Etichette influenza decisione
		/// <summary>
		public enum msdyn_decisioninfluencetagValues
		{
			Blocco = 2,
			Decisore = 0,
			Influencer = 1,
			Sconosciuto = 3
		}

		/// <summary>
		/// Values for field Disabilita registrazione Web
		/// <summary>
		public enum msdyn_disablewebtrackingValues
		{
			No = 0,
			Si = 1
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
		/// Values for field Assistente
		/// <summary>
		public enum msdyn_isassistantinorgchartValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field È un minore
		/// <summary>
		public enum msdyn_isminorValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Minore con consenso dei genitori
		/// <summary>
		public enum msdyn_isminorwithparentalconsentValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Flag Non in azienda
		/// <summary>
		public enum msdyn_orgchangestatusValues
		{
			Ignora = 2,
			Nessuncommento = 0,
			Noninazienda = 1
		}

		/// <summary>
		/// Values for field Lingua preferita
		/// <summary>
		public enum mspp_userpreferredlcidValues
		{
			Arabo = 1025,
			BascoProvincebasche = 1069,
			BulgaroBulgaria = 1026,
			CatalanoCatalogna = 1027,
			CecoRepubblicaCeca = 1029,
			CineseCina = 2052,
			CineseRASdiHongKong = 3076,
			CineseTradizionale = 1028,
			CoreanoCoreadelSud = 1042,
			CroatoCroazia = 1050,
			DaneseDanimarca = 1030,
			Ebraico = 1037,
			EstoneEstonia = 1061,
			FinlandeseFinlandia = 1035,
			FranceseFrancia = 1036,
			GallegoSpagna = 1110,
			GiapponeseGiappone = 1041,
			GrecoGrecia = 1032,
			HindiIndia = 1081,
			IndonesianoIndonesia = 1057,
			Inglese = 1033,
			ItalianoItalia = 1040,
			KazakoKazakistan = 1087,
			LettoneLettonia = 1062,
			LituanoLituania = 1063,
			MaleseMalaysia = 1086,
			NorvegeseBokmalNorvegia = 1044,
			OlandesePaesiBassi = 1043,
			PolaccoPolonia = 1045,
			PortogheseBrasile = 1046,
			PortoghesePortogallo = 2070,
			RumenoRomania = 1048,
			RussoRussia = 1049,
			SerboalfabetocirillicoSerbia = 3098,
			SerboalfabetolatinoSerbia = 2074,
			SlovaccoSlovacchia = 1051,
			SlovenoSlovenia = 1060,
			SpagnoloordinamentotradizionaleSpagna = 3082,
			SvedeseSvezia = 1053,
			TedescoGermania = 1031,
			ThaiThailandia = 1054,
			TurcoTurchia = 1055,
			UcrainoUcraina = 1058,
			UnghereseUngheria = 1038,
			VietnamitaVietnam = 1066
		}

		/// <summary>
		/// Values for field Partecipa al flusso di lavoro
		/// <summary>
		public enum participatesinworkflowValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Condizioni di pagamento
		/// <summary>
		public enum paymenttermscodeValues
		{
			_30gg = 1,
			_60gg = 4,
			_90gg = 9,
			Pagamentoincontanti = 10
		}

		/// <summary>
		/// Values for field Giorno preferito
		/// <summary>
		public enum preferredappointmentdaycodeValues
		{
			Domenica = 0,
			Giovedi = 4,
			Lunedi = 1,
			Martedi = 2,
			Mercoledi = 3,
			Sabato = 6,
			Venerdi = 5
		}

		/// <summary>
		/// Values for field Orario preferito
		/// <summary>
		public enum preferredappointmenttimecodeValues
		{
			Mattino = 1,
			Pomeriggio = 2,
			Sera = 3
		}

		/// <summary>
		/// Values for field Metodo di contatto preferito
		/// <summary>
		public enum preferredcontactmethodcodeValues
		{
			Email = 2,
			Fax = 4,
			Posta = 5,
			Telefono = 3,
			Tutti = 1
		}

		/// <summary>
		/// Values for field Metodo di spedizione
		/// <summary>
		public enum shippingmethodcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Area
		/// <summary>
		public enum territorycodeValues
		{
			Valorepredefinito = 1
		}
	};
}
