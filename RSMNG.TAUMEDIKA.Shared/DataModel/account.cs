namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Account constants.
	/// </summary>
	public sealed class account : EntityGenericConstants
	{
		/// <summary>
		/// account
		/// </summary>
		public static string logicalName => "account";

		/// <summary>
		/// Account
		/// </summary>
		public static string displayName => "Account";

		/// <summary>
		/// Display Name: Categoria,
		/// Type: Picklist,
		/// Values:
		/// Cliente preferito: 1,
		/// Standard: 2,
		/// Description: Selezionare una categoria per indicare se l'account cliente è standard o preferito.
		/// </summary>
		public static string accountcategorycode => "accountcategorycode";

		/// <summary>
		/// Display Name: Classificazione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un codice di classificazione per indicare il valore potenziale dell'account cliente in base all'utile sugli investimenti previsto, al livello di cooperazione, alla durata del ciclo di vendita o ad altri criteri.
		/// </summary>
		public static string accountclassificationcode => "accountclassificationcode";

		/// <summary>
		/// Display Name: Account,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'account.
		/// </summary>
		public static string accountid => "accountid";

		/// <summary>
		/// Display Name: Codice,
		/// Type: String,
		/// Description: Digitare un numero ID o un codice per l'account per cercare e identificare rapidamente l'account nelle visualizzazioni di sistema.
		/// </summary>
		public static string accountnumber => "accountnumber";

		/// <summary>
		/// Display Name: Livello di interesse account,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un livello di interesse per indicare il valore dell'account cliente.
		/// </summary>
		public static string accountratingcode => "accountratingcode";

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
		/// Display Name: Nazione (Testo),
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
		/// Display Name: Indirizzo,
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
		/// Display Name: Indirizzo 1: nome,
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
		/// Display Name: Telefono indirizzo,
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
		/// Display Name: Altro telefono,
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
		/// Display Name: Created By (IP Address),
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_createdbyipaddress => "adx_createdbyipaddress";

		/// <summary>
		/// Display Name: Created By (User Name),
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_createdbyusername => "adx_createdbyusername";

		/// <summary>
		/// Display Name: Modified By (IP Address),
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_modifiedbyipaddress => "adx_modifiedbyipaddress";

		/// <summary>
		/// Display Name: Modified By (User Name),
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string adx_modifiedbyusername => "adx_modifiedbyusername";

		/// <summary>
		/// Display Name: Scadenza 30,
		/// Type: Money,
		/// Description: Solo per uso di sistema.
		/// </summary>
		public static string aging30 => "aging30";

		/// <summary>
		/// Display Name: Scadenza 30 (base),
		/// Type: Money,
		/// Description: Equivalente nella valuta di base del campo di scadenza 30.
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
		/// Description: Equivalente nella valuta di base del campo di scadenza 60.
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
		/// Description: Equivalente nella valuta di base del campo di scadenza 90.
		/// </summary>
		public static string aging90_base => "aging90_base";

		/// <summary>
		/// Display Name: Tipo di azienda,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare la designazione giuridica o il tipo di società dell'account per contratti o per scopi di report.
		/// </summary>
		public static string businesstypecode => "businesstypecode";

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
		/// Description: Digitare il limite di credito dell'account. Questo riferimento è utile per risolvere problemi contabili e di fatturazione con il cliente.
		/// </summary>
		public static string creditlimit => "creditlimit";

		/// <summary>
		/// Display Name: Limite di credito (base),
		/// Type: Money,
		/// Description: Mostra il limite di credito convertito nella valuta di base predefinita del sistema per scopi di report.
		/// </summary>
		public static string creditlimit_base => "creditlimit_base";

		/// <summary>
		/// Display Name: Blocco del credito,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Selezionare se il credito per l'account è bloccato. Questo riferimento è utile per risolvere problemi contabili e di fatturazione con il cliente.
		/// </summary>
		public static string creditonhold => "creditonhold";

		/// <summary>
		/// Display Name: Dimensioni cliente,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare la categoria o l'intervallo di dimensioni dell'account per scopi di segmentazione e report.
		/// </summary>
		public static string customersizecode => "customersizecode";

		/// <summary>
		/// Display Name: Tipo relazione,
		/// Type: Picklist,
		/// Values:
		/// Concorrente: 1,
		/// Consulente: 2,
		/// Cliente: 3,
		/// Investitore: 4,
		/// Partner: 5,
		/// Influenzatore: 6,
		/// Stampa: 7,
		/// Potenziale cliente: 8,
		/// Rivenditore: 9,
		/// Produttore: 10,
		/// Fornitore: 11,
		/// Altro: 12,
		/// Description: Selezionare la categoria che meglio descrive la relazione tra l'account e l'organizzazione.
		/// </summary>
		public static string customertypecode => "customertypecode";

		/// <summary>
		/// Display Name: Listino prezzi,
		/// Type: Lookup,
		/// Related entities: pricelevel,
		/// Description: Scegli il listino prezzi predefinito associato all'account per assicurare che vengano applicati i prezzi dei prodotti corretti per questo cliente in opportunità di vendita, offerte e ordini.
		/// </summary>
		public static string defaultpricelevelid => "defaultpricelevelid";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Digitare informazioni aggiuntive per descrivere l'account, ad esempio un estratto del sito Web della società.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Non consentire invio di messaggi e-mail in blocco,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se l'account consente l'invio di e-mail in blocco tramite le campagne. Se si seleziona Non consentire, l'account potrà essere aggiunto agli elenchi marketing ma sarà escluso dall'e-mail.
		/// </summary>
		public static string donotbulkemail => "donotbulkemail";

		/// <summary>
		/// Display Name: Non consentire posta inviata in blocco,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Selezionare se l'account consente l'invio di posta in blocco tramite campagne di marketing o mini-campagne. Se si seleziona Non consentire, l'account potrà essere aggiunto agli elenchi marketing ma sarà escluso dalla posta.
		/// </summary>
		public static string donotbulkpostalmail => "donotbulkpostalmail";

		/// <summary>
		/// Display Name: Non consentire invio di messaggi e-mail,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Seleziona se l'account consente l'invio di e-mail dirette da Microsoft Dynamics 365.
		/// </summary>
		public static string donotemail => "donotemail";

		/// <summary>
		/// Display Name: Non consentire fax,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se l'account consente i fax. Se si seleziona Non consentire, l'account sarà escluso dagli impegni di tipo fax distribuiti nelle campagne di marketing.
		/// </summary>
		public static string donotfax => "donotfax";

		/// <summary>
		/// Display Name: Non consentire telefonate,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se l'account consente le telefonate. Se si seleziona Non consentire, l'account sarà escluso dagli impegni di tipo telefonata distribuiti nelle campagne di marketing.
		/// </summary>
		public static string donotphone => "donotphone";

		/// <summary>
		/// Display Name: Non consentire posta,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Selezionare se l'account consente la posta diretta. Se si seleziona Non consentire, l'account sarà escluso dagli impegni di tipo lettera distribuiti nelle campagne di marketing.
		/// </summary>
		public static string donotpostalmail => "donotpostalmail";

		/// <summary>
		/// Display Name: Consenti invio materiale marketing,
		/// Type: Boolean,
		/// Values:
		/// Non inviare: 1,
		/// Invia: 0,
		/// Description: Selezionare se l'account accetta materiale di marketing come brochure o cataloghi.
		/// </summary>
		public static string donotsendmm => "donotsendmm";

		/// <summary>
		/// Display Name: E-mail,
		/// Type: String,
		/// Description: Digitare l'indirizzo e-mail primario per l'account.
		/// </summary>
		public static string emailaddress1 => "emailaddress1";

		/// <summary>
		/// Display Name: E-mail secondaria,
		/// Type: String,
		/// Description: Digitare l'indirizzo e-mail secondario per l'account.
		/// </summary>
		public static string emailaddress2 => "emailaddress2";

		/// <summary>
		/// Display Name: PEC,
		/// Type: String,
		/// Description: Digitare un indirizzo e-mail alternativo per l'account.
		/// </summary>
		public static string emailaddress3 => "emailaddress3";

		/// <summary>
		/// Display Name: Immagine predefinita,
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
		/// Display Name: Fax,
		/// Type: String,
		/// Description: Digitare il numero di fax per l'account.
		/// </summary>
		public static string fax => "fax";

		/// <summary>
		/// Display Name: Segui impegno e-mail,
		/// Type: Boolean,
		/// Values:
		/// Consenti: 1,
		/// Non consentire: 0,
		/// Description: Specifica se consentire o meno che gli impegni dei messaggi e-mail inviati all'account vengano seguiti fornendo informazioni quali: numero di volte in cui un messaggio viene aperto, numero di visualizzazioni degli allegati e numero di clic sul collegamento.
		/// </summary>
		public static string followemail => "followemail";

		/// <summary>
		/// Display Name: Sito FTP,
		/// Type: String,
		/// Description: Digitare l'URL per il sito FTP dell'account per consentire agli utenti di accedere ai dati e di condividere documenti.
		/// </summary>
		public static string ftpsiteurl => "ftpsiteurl";

		/// <summary>
		/// Display Name: Settore,
		/// Type: Picklist,
		/// Values:
		/// Industrie alimentari, delle bevande e del tabacco: 3,
		/// Commercio all'ingrosso, al dettaglio, riparazioni: 16,
		/// Editoria e stampa: 20,
		/// Agricoltura, caccia, pesca e silvicoltura: 34,
		/// Altre industrie manifatturiere: 38,
		/// Altre attività di servizi: 43,
		/// Fabbricazione di prodotti chimici: 44,
		/// Edilizia: 53,
		/// Estrazione di minerali energetici e non energetici: 59,
		/// Prod. e distrib. di energia elettrica, gas e acqua: 61,
		/// Commercio all'ingrosso, al dettaglio, riparazioni: 62,
		/// Alberghi, ristoranti e pubblici esercizi: 63,
		/// Attività immobiliari, noleggio: 65,
		/// Pubblica Amministrazione e difesa; assicurazione soc. obbl.: 66,
		/// Istruzione: 67,
		/// Sanità e altri servizi sociali: 68,
		/// Altri servizi pubblici, sociali e personali: 69,
		/// Organizzazioni ed organismi extraterritoriali: 71,
		/// Informatica e attività connesse: 74,
		/// Industrie alimentari, delle bevande e del tabacco: 76,
		/// Industrie tessili e dell'abbigliamento: 93,
		/// Fabbricazione di articoli in gomma e plastica: 119,
		/// Fabbricazione di prodotti farmaceutici e cosmetici: 123,
		/// Poste e telecomunicazioni: 124,
		/// Attività delle banche centrali: 125,
		/// Attività legali e contabili: 126,
		/// Consulenza amministrativo-gestionale: 127,
		/// Pubblicità: 128,
		/// Fabbricazione macchine e apparecchi meccanici: 129,
		/// Fabbricazione macchine per ufficio e sistemi informatici: 130,
		/// Fabbricazione macchine e apparecchi elettrici: 131,
		/// Fabbricazione apparecchi di precisione: 132,
		/// Fabbricazione di mezzi di trasporto: 133,
		/// Trasporti, magazzinaggio: 134,
		/// Description: Selezionare il settore primario dell'account da usare nella segmentazione di marketing e nell'analisi demografica.
		/// </summary>
		public static string industrycode => "industrycode";

		/// <summary>
		/// Display Name: Ultimo periodo sospensione,
		/// Type: DateTime,
		/// Description: Contiene l'indicatore di data e ora dell'ultimo periodo di sospensione.
		/// </summary>
		public static string lastonholdtime => "lastonholdtime";

		/// <summary>
		/// Display Name: Data ultima inclusione in campagna,
		/// Type: DateTime,
		/// Description: Mostra la data dell'ultima volta in cui l'account è stato incluso in una campagna di marketing o in una mini-campagna.
		/// </summary>
		public static string lastusedincampaign => "lastusedincampaign";

		/// <summary>
		/// Display Name: Capitalizzazione di mercato,
		/// Type: Money,
		/// Description: Digitare la capitalizzazione di mercato dell'account per identificare il capitale netto della società, usato come indicatore nell'analisi delle prestazioni finanziarie.
		/// </summary>
		public static string marketcap => "marketcap";

		/// <summary>
		/// Display Name: Capitalizzazione di mercato (base),
		/// Type: Money,
		/// Description: Mostra la capitalizzazione di mercato convertita nella valuta di base predefinita del sistema.
		/// </summary>
		public static string marketcap_base => "marketcap_base";

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
		/// Related entities: account,
		/// Description: Mostra l'account master a cui l'account è stato unito.
		/// </summary>
		public static string masterid => "masterid";

		/// <summary>
		/// Display Name: Unito,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Mostra se l'account è stato unito a un altro account.
		/// </summary>
		public static string merged => "merged";

		/// <summary>
		/// Display Name: Autore modifica (parte esterna),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Mostra la parte esterna che ha modificato il record.
		/// </summary>
		public static string modifiedbyexternalparty => "modifiedbyexternalparty";

		/// <summary>
		/// Display Name: Managing Partner,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Unique identifier for Account associated with Account.
		/// </summary>
		public static string msa_managingpartnerid => "msa_managingpartnerid";

		/// <summary>
		/// Display Name: KPI,
		/// Type: Lookup,
		/// Related entities: msdyn_accountkpiitem,
		/// Description: 
		/// </summary>
		public static string msdyn_accountkpiid => "msdyn_accountkpiid";

		/// <summary>
		/// Display Name: Rifiuto esplicito RGPD,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se l'account è stato rifiutato esplicitamente o meno
		/// </summary>
		public static string msdyn_gdproptout => "msdyn_gdproptout";

		/// <summary>
		/// Display Name: Fuso orario principale,
		/// Type: Integer,
		/// Description: Indica il fuso orario primario in cui lavora il client.
		/// </summary>
		public static string msdyn_primarytimezone => "msdyn_primarytimezone";

		/// <summary>
		/// Display Name: ID informazioni dettagliate Accelerazione delle vendite,
		/// Type: Lookup,
		/// Related entities: msdyn_salesaccelerationinsight,
		/// Description: ID informazioni dettagliate Accelerazione delle vendite
		/// </summary>
		public static string msdyn_salesaccelerationinsightid => "msdyn_salesaccelerationinsightid";

		/// <summary>
		/// Display Name: Denominazione,
		/// Type: String,
		/// Description: Digitare il nome della società o dell'azienda.
		/// </summary>
		public static string name => "name";

		/// <summary>
		/// Display Name: Numero di dipendenti,
		/// Type: Integer,
		/// Description: Digitare il numero di dipendenti che lavorano presso l'account da usare nella segmentazione di marketing e nell'analisi demografica.
		/// </summary>
		public static string numberofemployees => "numberofemployees";

		/// <summary>
		/// Display Name: Periodo di sospensione (minuti),
		/// Type: Integer,
		/// Description: Mostra la durata della sospensione del record in minuti.
		/// </summary>
		public static string onholdtime => "onholdtime";

		/// <summary>
		/// Display Name: Transazioni aperte,
		/// Type: Integer,
		/// Description: Numero di opportunità aperte in relazione a un account e ai relativi account figlio.
		/// </summary>
		public static string opendeals => "opendeals";

		/// <summary>
		/// Display Name: Transazioni aperte (Last Updated On),
		/// Type: DateTime,
		/// Description: Last Updated time of rollup field Transazioni aperte.
		/// </summary>
		public static string opendeals_date => "opendeals_date";

		/// <summary>
		/// Display Name: Transazioni aperte (State),
		/// Type: Integer,
		/// Description: State of rollup field Transazioni aperte.
		/// </summary>
		public static string opendeals_state => "opendeals_state";

		/// <summary>
		/// Display Name: Ricavi aperti,
		/// Type: Money,
		/// Description: Somma dei ricavi aperti in relazione a un account e ai relativi account figlio.
		/// </summary>
		public static string openrevenue => "openrevenue";

		/// <summary>
		/// Display Name: Ricavi aperti (Base),
		/// Type: Money,
		/// Description: Value of the Ricavi aperti in base currency.
		/// </summary>
		public static string openrevenue_base => "openrevenue_base";

		/// <summary>
		/// Display Name: Ricavi aperti (Last Updated On),
		/// Type: DateTime,
		/// Description: Last Updated time of rollup field Ricavi aperti.
		/// </summary>
		public static string openrevenue_date => "openrevenue_date";

		/// <summary>
		/// Display Name: Ricavi aperti (State),
		/// Type: Integer,
		/// Description: State of rollup field Ricavi aperti.
		/// </summary>
		public static string openrevenue_state => "openrevenue_state";

		/// <summary>
		/// Display Name: Lead di origine,
		/// Type: Lookup,
		/// Related entities: lead,
		/// Description: Mostra il lead da cui è stato creato l'account se l'account è stato creato convertendo un lead in Microsoft Dynamics 365. Usato per associare l'account ai dati sul lead di origine a scopo di report e analisi.
		/// </summary>
		public static string originatingleadid => "originatingleadid";

		/// <summary>
		/// Display Name: Proprietà,
		/// Type: Picklist,
		/// Values:
		/// Pubblica: 1,
		/// Privata: 2,
		/// Affiliata: 3,
		/// Altro: 4,
		/// Description: Selezionare la struttura di proprietà dell'account, cioè pubblica o privata.
		/// </summary>
		public static string ownershipcode => "ownershipcode";

		/// <summary>
		/// Display Name: Account padre,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Scegli l'account padre associato a questo account per mostrare le aziende padre e figlio a scopo di report e analisi.
		/// </summary>
		public static string parentaccountid => "parentaccountid";

		/// <summary>
		/// Display Name: Partecipa al flusso di lavoro,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Solo per uso di sistema. Dati di flusso di lavoro di Microsoft Dynamics CRM 3.0 legacy.
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
		/// Description: Scegli le attrezzature o la struttura di servizio preferita dell'account per assicurarti che i servizi siano pianificati correttamente per il cliente.
		/// </summary>
		public static string preferredequipmentid => "preferredequipmentid";

		/// <summary>
		/// Display Name: Servizio preferito,
		/// Type: Lookup,
		/// Related entities: service,
		/// Description: Scegli il servizio preferito dell'account da usare come riferimento quando si pianificano gli impegni di tipo servizio.
		/// </summary>
		public static string preferredserviceid => "preferredserviceid";

		/// <summary>
		/// Display Name: Utente preferito,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Scegliere il rappresentante del servizio preferito da usare come riferimento quando si pianificano gli impegni di tipo servizio per l'account.
		/// </summary>
		public static string preferredsystemuserid => "preferredsystemuserid";

		/// <summary>
		/// Display Name: Contatto primario,
		/// Type: Lookup,
		/// Related entities: contact,
		/// Description: Scegliere il contatto primario per l'account per fornire accesso rapido ai dettagli di contatto.
		/// </summary>
		public static string primarycontactid => "primarycontactid";

		/// <summary>
		/// Display Name: ID Satori principale,
		/// Type: String,
		/// Description: ID Satori principale per account
		/// </summary>
		public static string primarysatoriid => "primarysatoriid";

		/// <summary>
		/// Display Name: ID Twitter principale,
		/// Type: String,
		/// Description: ID Twitter principale per account
		/// </summary>
		public static string primarytwitterid => "primarytwitterid";

		/// <summary>
		/// Display Name: Processo,
		/// Type: Uniqueidentifier,
		/// Description: Mostra l'ID del processo.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Natura giuridica,
		/// Type: Picklist,
		/// Values:
		/// Persona fisica: 100000000,
		/// Persona giuridica: 100000001,
		/// Description: 
		/// </summary>
		public static string res_accountnaturecode => "res_accountnaturecode";

		/// <summary>
		/// Display Name: Tipologia,
		/// Type: Virtual,
		/// Values:
		/// Cliente: 100000000,
		/// Fornitore: 100000001,
		/// Description: 
		/// </summary>
		public static string res_accounttypecodes => "res_accounttypecodes";

		/// <summary>
		/// Display Name: Ns. Banca,
		/// Type: Lookup,
		/// Related entities: res_bankdetails,
		/// Description: 
		/// </summary>
		public static string res_bankdetailsid => "res_bankdetailsid";

		/// <summary>
		/// Display Name: Nazione,
		/// Type: Lookup,
		/// Related entities: res_country,
		/// Description: 
		/// </summary>
		public static string res_countryid => "res_countryid";

		/// <summary>
		/// Display Name: Codice medico,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_doctorcode => "res_doctorcode";

		/// <summary>
		/// Display Name: Località,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_location => "res_location";

		/// <summary>
		/// Display Name: Cellulare,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_mobilenumber => "res_mobilenumber";

		/// <summary>
		/// Display Name: Condizione di pagamento,
		/// Type: Lookup,
		/// Related entities: res_paymentterm,
		/// Description: 
		/// </summary>
		public static string res_paymenttermid => "res_paymenttermid";

		/// <summary>
		/// Display Name: SDI,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_sdi => "res_sdi";

		/// <summary>
		/// Display Name: Codice fiscale,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_taxcode => "res_taxcode";

		/// <summary>
		/// Display Name: Partita IVA,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_vatnumber => "res_vatnumber";

		/// <summary>
		/// Display Name: Ricavi annuali,
		/// Type: Money,
		/// Description: Digitare i ricavi annuali dell'account, usati come indicatore nell'analisi delle prestazioni finanziarie.
		/// </summary>
		public static string revenue => "revenue";

		/// <summary>
		/// Display Name: Ricavi annuali (base),
		/// Type: Money,
		/// Description: Mostra i ricavi annuali convertiti nella valuta di base predefinita del sistema. I calcoli usano il tasso di cambio specificato nell'area Valute.
		/// </summary>
		public static string revenue_base => "revenue_base";

		/// <summary>
		/// Display Name: Azioni in circolazione,
		/// Type: Integer,
		/// Description: Digitare il numero di azioni disponibili al pubblico per l'account. Questo numero è usato come indicatore nell'analisi delle prestazioni finanziarie.
		/// </summary>
		public static string sharesoutstanding => "sharesoutstanding";

		/// <summary>
		/// Display Name: Metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un metodo di spedizione per le consegne inviate all'indirizzo dell'account per definire il vettore preferito o un'altra opzione di consegna.
		/// </summary>
		public static string shippingmethodcode => "shippingmethodcode";

		/// <summary>
		/// Display Name: Codice NACE,
		/// Type: String,
		/// Description: Digitare il codice NACE che indica il settore primario dell'account, da usare nella segmentazione di marketing e nell'analisi demografica.
		/// </summary>
		public static string sic => "sic";

		/// <summary>
		/// Display Name: CONTRATTO DI SERVIZIO,
		/// Type: Lookup,
		/// Related entities: sla,
		/// Description: Scegli il contratto di servizio da applicare al record di account.
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
		/// Display Name: (Deprecata) Fase del processo,
		/// Type: Uniqueidentifier,
		/// Description: Mostra l'ID della fase.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Listino di Borsa,
		/// Type: String,
		/// Description: Digitare il listino di Borsa in cui l'account è quotato per tenere traccia del suo capitale e delle prestazioni finanziarie della società.
		/// </summary>
		public static string stockexchange => "stockexchange";

		/// <summary>
		/// Display Name: TeamsFollowed,
		/// Type: Integer,
		/// Description: Numero di utenti o conversazioni che hanno seguito il record
		/// </summary>
		public static string teamsfollowed => "teamsfollowed";

		/// <summary>
		/// Display Name: Telefono,
		/// Type: String,
		/// Description: Digitare il numero di telefono principale per questo account.
		/// </summary>
		public static string telephone1 => "telephone1";

		/// <summary>
		/// Display Name: Altro telefono,
		/// Type: String,
		/// Description: Digitare un secondo numero di telefono per questo account.
		/// </summary>
		public static string telephone2 => "telephone2";

		/// <summary>
		/// Display Name: Telefono 3,
		/// Type: String,
		/// Description: Digitare un terzo numero di telefono per questo account.
		/// </summary>
		public static string telephone3 => "telephone3";

		/// <summary>
		/// Display Name: Codice area,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare un'area per l'account da usare nella segmentazione e nell'analisi.
		/// </summary>
		public static string territorycode => "territorycode";

		/// <summary>
		/// Display Name: Area,
		/// Type: Lookup,
		/// Related entities: territory,
		/// Description: Scegli un'area di vendita per l'account per assicurarti che venga assegnato al rappresentante corretto e da usare nella segmentazione e nell'analisi.
		/// </summary>
		public static string territoryid => "territoryid";

		/// <summary>
		/// Display Name: Simbolo del titolo,
		/// Type: String,
		/// Description: Digitare il simbolo del listino di Borsa dell'account per tenere traccia delle prestazioni finanziarie della società. È possibile fare clic sul codice immesso in questo campo per accedere alle informazioni commerciali più aggiornate da MSN Money.
		/// </summary>
		public static string tickersymbol => "tickersymbol";

		/// <summary>
		/// Display Name: Tempo dedicato personalmente,
		/// Type: String,
		/// Description: Tempo totale dedicato personalmente ai messaggi e-mail (lettura e scrittura) e alle riunioni relativamente al record dell'account.
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
		/// Description: Digitare l'URL del sito Web dell'account per ottenere dettagli rapidi sul profilo della società.
		/// </summary>
		public static string websiteurl => "websiteurl";

		/// <summary>
		/// Display Name: Nome Yomi account,
		/// Type: String,
		/// Description: Digitare la forma fonetica del nome della società, se specificato in giapponese, per assicurare che il nome venga pronunciato correttamente nelle telefonate e in altre comunicazioni.
		/// </summary>
		public static string yominame => "yominame";


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
		/// Values for field Categoria
		/// <summary>
		public enum accountcategorycodeValues
		{
			Clientepreferito = 1,
			Standard = 2
		}

		/// <summary>
		/// Values for field Classificazione
		/// <summary>
		public enum accountclassificationcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Livello di interesse account
		/// <summary>
		public enum accountratingcodeValues
		{
			Valorepredefinito = 1
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
		/// Values for field Tipo di azienda
		/// <summary>
		public enum businesstypecodeValues
		{
			Valorepredefinito = 1
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
			Altro = 12,
			Cliente = 3,
			Concorrente = 1,
			Consulente = 2,
			Fornitore = 11,
			Influenzatore = 6,
			Investitore = 4,
			Partner = 5,
			Potenzialecliente = 8,
			Produttore = 10,
			Rivenditore = 9,
			Stampa = 7
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
		/// Values for field Segui impegno e-mail
		/// <summary>
		public enum followemailValues
		{
			Consenti = 1,
			Nonconsentire = 0
		}

		/// <summary>
		/// Values for field Settore
		/// <summary>
		public enum industrycodeValues
		{
			Agricolturacacciapescaesilvicoltura = 34,
			Alberghiristorantiepubbliciesercizi = 63,
			Altreattivitadiservizi = 43,
			Altreindustriemanifatturiere = 38,
			Altriservizipubblicisocialiepersonali = 69,
			Attivitadellebanchecentrali = 125,
			Attivitaimmobiliarinoleggio = 65,
			Attivitalegaliecontabili = 126,
			Commercioallingrossoaldettaglioriparazioni1 = 16,
			Commercioallingrossoaldettaglioriparazioni2 = 62,
			Consulenzaamministrativogestionale = 127,
			Edilizia = 53,
			Editoriaestampa = 20,
			Estrazionedimineralienergeticienonenergetici = 59,
			Fabbricazioneapparecchidiprecisione = 132,
			Fabbricazionediarticoliingommaeplastica = 119,
			Fabbricazionedimezziditrasporto = 133,
			Fabbricazionediprodottichimici = 44,
			Fabbricazionediprodottifarmaceuticiecosmetici = 123,
			Fabbricazionemacchineeapparecchielettrici = 131,
			Fabbricazionemacchineeapparecchimeccanici = 129,
			Fabbricazionemacchineperufficioesistemiinformatici = 130,
			Industriealimentaridellebevandeedeltabacco1 = 3,
			Industriealimentaridellebevandeedeltabacco2 = 76,
			Industrietessiliedellabbigliamento = 93,
			Informaticaeattivitaconnesse = 74,
			Istruzione = 67,
			Organizzazioniedorganismiextraterritoriali = 71,
			Posteetelecomunicazioni = 124,
			Prodedistribdienergiaelettricagaseacqua = 61,
			PubblicaAmministrazioneedifesaassicurazionesocobbl = 66,
			Pubblicita = 128,
			Sanitaealtriservizisociali = 68,
			Trasportimagazzinaggio = 134
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
		/// Values for field Rifiuto esplicito RGPD
		/// <summary>
		public enum msdyn_gdproptoutValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Proprietà
		/// <summary>
		public enum ownershipcodeValues
		{
			Affiliata = 3,
			Altro = 4,
			Privata = 2,
			Pubblica = 1
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
		/// Values for field Natura giuridica
		/// <summary>
		public enum res_accountnaturecodeValues
		{
			Personafisica = 100000000,
			Personagiuridica = 100000001
		}

		/// <summary>
		/// Values for field Tipologia
		/// <summary>
		public enum res_accounttypecodesValues
		{
			Cliente = 100000000,
			Fornitore = 100000001
		}

		/// <summary>
		/// Values for field Metodo di spedizione
		/// <summary>
		public enum shippingmethodcodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Codice area
		/// <summary>
		public enum territorycodeValues
		{
			Valorepredefinito = 1
		}
	};
}
