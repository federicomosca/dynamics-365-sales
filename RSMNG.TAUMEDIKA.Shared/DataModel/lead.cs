namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Lead constants.
	/// </summary>
	public sealed class lead : EntityGenericConstants
	{
		/// <summary>
		/// lead
		/// </summary>
		public static string logicalName => "lead";

		/// <summary>
		/// Lead
		/// </summary>
		public static string displayName => "Lead";

		/// <summary>
		/// Display Name: Account,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Identificatore univoco dell'account a cui è associato il lead.
		/// </summary>
		public static string accountid => "accountid";

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
		/// Display Name: Paese/area geografica,
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
		/// Display Name: Indirizzo 1: latitudine,
		/// Type: Double,
		/// Description: Digitare la latitudine per l'indirizzo primario da usare nella creazione di mappe e in altre applicazioni.
		/// </summary>
		public static string address1_latitude => "address1_latitude";

		/// <summary>
		/// Display Name: Via 1,
		/// Type: String,
		/// Description: Digitare la prima riga dell'indirizzo primario.
		/// </summary>
		public static string address1_line1 => "address1_line1";

		/// <summary>
		/// Display Name: Via 2,
		/// Type: String,
		/// Description: Digitare la seconda riga dell'indirizzo primario.
		/// </summary>
		public static string address1_line2 => "address1_line2";

		/// <summary>
		/// Display Name: Via 3,
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
		/// Display Name: Indirizzo 1: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Seleziona un metodo di spedizione per le consegne inviate a questo indirizzo.
		/// </summary>
		public static string address1_shippingmethodcode => "address1_shippingmethodcode";

		/// <summary>
		/// Display Name: Provincia,
		/// Type: String,
		/// Description: Digitare la provincia dell'indirizzo primario.
		/// </summary>
		public static string address1_stateorprovince => "address1_stateorprovince";

		/// <summary>
		/// Display Name: Indirizzo 1: telefono 1,
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
		/// Description: Digita un terzo numero di telefono associato all'indirizzo primario.
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
		/// Description: Seleziona il fuso orario o la differenza UTC per questo indirizzo in modo che altre persone possano farvi riferimento quando contattano qualcuno presso questo indirizzo.
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
		/// Display Name: Indirizzo 2: metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Seleziona un metodo di spedizione per le consegne inviate a questo indirizzo.
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
		/// Description: Seleziona il fuso orario o la differenza UTC per questo indirizzo in modo che altre persone possano farvi riferimento quando contattano qualcuno presso questo indirizzo.
		/// </summary>
		public static string address2_utcoffset => "address2_utcoffset";

		/// <summary>
		/// Display Name: Importo budget,
		/// Type: Money,
		/// Description: Informazioni sull'importo dei costi preventivati della società o dell'organizzazione del lead.
		/// </summary>
		public static string budgetamount => "budgetamount";

		/// <summary>
		/// Display Name: Importo budget (Base),
		/// Type: Money,
		/// Description: Value of the Importo budget in base currency.
		/// </summary>
		public static string budgetamount_base => "budgetamount_base";

		/// <summary>
		/// Display Name: Costi preventivati,
		/// Type: Picklist,
		/// Values:
		/// Nessun budget impegnato: 0,
		/// Acquisto possibile: 1,
		/// Acquisto probabile: 2,
		/// Acquisto sicuro: 3,
		/// Description: Informazioni sullo stato dei costi preventivati della società o dell'organizzazione del lead.
		/// </summary>
		public static string budgetstatus => "budgetstatus";

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
		/// Display Name: Campagna di origine,
		/// Type: Lookup,
		/// Related entities: campaign,
		/// Description: Scegli la campagna da cui è stato generato il lead per registrare l'efficacia delle campagne di marketing e identificare le comunicazioni ricevute dal lead.
		/// </summary>
		public static string campaignid => "campaignid";

		/// <summary>
		/// Display Name: Nome società,
		/// Type: String,
		/// Description: Digitare il nome della società associata al lead. Questa impostazione diventa il nome dell'account quando il lead è qualificato e convertito in un account cliente.
		/// </summary>
		public static string companyname => "companyname";

		/// <summary>
		/// Display Name: Conferma interesse,
		/// Type: Boolean,
		/// Values:
		/// No: 1,
		/// Sì: 0,
		/// Description: Specificare se il lead ha confermato un interesse nei confronti delle offerte ricevute. In questo modo è possibile determinare il livello di interesse del lead.
		/// </summary>
		public static string confirminterest => "confirminterest";

		/// <summary>
		/// Display Name: Contatto,
		/// Type: Lookup,
		/// Related entities: contact,
		/// Description: Identificatore univoco del contatto a cui è associato il lead.
		/// </summary>
		public static string contactid => "contactid";

		/// <summary>
		/// Display Name: Cliente,
		/// Type: Customer,
		/// Description: Seleziona l'account cliente o il contatto per fornire un link rapido a dettagli aggiuntivi sul cliente, ad esempio informazioni sull'account, impegni e opportunità.
		/// </summary>
		public static string customerid => "customerid";

		/// <summary>
		/// Display Name: Tipo di cliente,
		/// Type: EntityName,
		/// Description: 
		/// </summary>
		public static string customeridtype => "customeridtype";

		/// <summary>
		/// Display Name: Decisore?,
		/// Type: Boolean,
		/// Values:
		/// completato: 1,
		/// segna come completato: 0,
		/// Description: Specificare se le note includono informazioni sull'utente che prende le decisioni di acquisto nella società del lead.
		/// </summary>
		public static string decisionmaker => "decisionmaker";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Digitare informazioni aggiuntive per descrivere il lead, ad esempio un estratto del sito Web della società.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Non consentire invio di messaggi e-mail in blocco,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Specificare se il lead accetta l'invio di messaggi e-mail in blocco tramite campagne di marketing o mini-campagne. Se si seleziona Non consentire, il lead potrà essere aggiunto agli elenchi marketing ma sarà escluso dai messaggi e-mail.
		/// </summary>
		public static string donotbulkemail => "donotbulkemail";

		/// <summary>
		/// Display Name: Non consentire invio di messaggi e-mail,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Seleziona se il lead consente l'invio di mailing diretto da Microsoft Dynamics 365.
		/// </summary>
		public static string donotemail => "donotemail";

		/// <summary>
		/// Display Name: Non consentire fax,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Specificare se il lead consente i fax.
		/// </summary>
		public static string donotfax => "donotfax";

		/// <summary>
		/// Display Name: Non consentire telefonate,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Specificare se il lead consente le telefonate.
		/// </summary>
		public static string donotphone => "donotphone";

		/// <summary>
		/// Display Name: Non consentire posta,
		/// Type: Boolean,
		/// Values:
		/// Non consentire: 1,
		/// Consenti: 0,
		/// Description: Specificare se il lead consente la posta diretta.
		/// </summary>
		public static string donotpostalmail => "donotpostalmail";

		/// <summary>
		/// Display Name: Materiale marketing,
		/// Type: Boolean,
		/// Values:
		/// Non inviare: 1,
		/// Invia: 0,
		/// Description: Specificare se il lead accetta materiale di marketing, ad esempio brochure o cataloghi. I lead che rifiutano esplicitamente possono essere esclusi dalle iniziative di marketing.
		/// </summary>
		public static string donotsendmm => "donotsendmm";

		/// <summary>
		/// Display Name: E-mail,
		/// Type: String,
		/// Description: Digitare l'indirizzo e-mail primario per il lead.
		/// </summary>
		public static string emailaddress1 => "emailaddress1";

		/// <summary>
		/// Display Name: Indirizzo e-mail 2,
		/// Type: String,
		/// Description: Digitare l'indirizzo e-mail secondario per il lead.
		/// </summary>
		public static string emailaddress2 => "emailaddress2";

		/// <summary>
		/// Display Name: Indirizzo e-mail 3,
		/// Type: String,
		/// Description: Digitare un terzo indirizzo e-mail per il lead.
		/// </summary>
		public static string emailaddress3 => "emailaddress3";

		/// <summary>
		/// Display Name: Immagine entità,
		/// Type: Virtual,
		/// Description: Mostra l'immagine predefinita del record.
		/// </summary>
		public static string entityimage => "entityimage";

		/// <summary>
		/// Display Name: Valore prev.,
		/// Type: Money,
		/// Description: Digitare il valore dei ricavi previsti generati dal lead per supportare la previsione e la pianificazione delle vendite.
		/// </summary>
		public static string estimatedamount => "estimatedamount";

		/// <summary>
		/// Display Name: Valore prev. (Base),
		/// Type: Money,
		/// Description: Value of the Valore prev. in base currency.
		/// </summary>
		public static string estimatedamount_base => "estimatedamount_base";

		/// <summary>
		/// Display Name: Data chiusura prevista,
		/// Type: DateTime,
		/// Description: Immettere la data di chiusura prevista per il lead in modo che il team di vendita possa pianificare tempestivamente riunioni di completamento per spostare il prospect nella fase di vendita successiva.
		/// </summary>
		public static string estimatedclosedate => "estimatedclosedate";

		/// <summary>
		/// Display Name: Valore prev. (deprecato),
		/// Type: Double,
		/// Description: Digitare un valore numerico del valore previsto per il lead, ad esempio una quantità di prodotti, se non è possibile specificare alcun importo di ricavi nel campo Valore prev. Questo valore può essere utilizzato per la previsione e la pianificazione delle vendite.
		/// </summary>
		public static string estimatedvalue => "estimatedvalue";

		/// <summary>
		/// Display Name: Valuta corrispondenza,
		/// Type: Boolean,
		/// Values:
		/// No: 1,
		/// Sì: 0,
		/// Description: Specificare se è stata valutata la corrispondenza tra le richieste del lead e le offerte effettuate.
		/// </summary>
		public static string evaluatefit => "evaluatefit";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Mostra il tasso di conversione della valuta del record. Il tasso di cambio è usato per convertire tutti i campi di tipo money nel record dalla valuta locale alla valuta predefinita del sistema.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Fax,
		/// Type: String,
		/// Description: Digitare il numero di fax per il contatto primario per il lead.
		/// </summary>
		public static string fax => "fax";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Digitare il nome del contatto primario per il lead per garantire che il prospect sia indicato correttamente nelle chiamate di vendita, nei messaggi e-mail e nelle campagne di marketing.
		/// </summary>
		public static string firstname => "firstname";

		/// <summary>
		/// Display Name: Segui impegno e-mail,
		/// Type: Boolean,
		/// Values:
		/// Consenti: 1,
		/// Non consentire: 0,
		/// Description: Specifica se consentire o meno che gli impegni dei messaggi e-mail inviati al lead vengano seguiti fornendo informazioni quali: numero di volte in cui un messaggio viene aperto, numero di visualizzazioni degli allegati e numero di clic sul collegamento.
		/// </summary>
		public static string followemail => "followemail";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Combina e mostra nome e cognome del lead per consentire di mostrare il nome completo in visualizzazioni e report.
		/// </summary>
		public static string fullname => "fullname";

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
		/// Description: Selezionare il settore primario dell'azienda del lead da usare nella segmentazione di marketing e nell'analisi demografica.
		/// </summary>
		public static string industrycode => "industrycode";

		/// <summary>
		/// Display Name: Comunicazione iniziale,
		/// Type: Picklist,
		/// Values:
		/// Contattato: 0,
		/// Non contattato: 1,
		/// Description: Scegliere se un membro del team di vendita ha contattato il lead in precedenza.
		/// </summary>
		public static string initialcommunication => "initialcommunication";

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
		/// Display Name: Privato,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il lead è privato o è visibile all'intera organizzazione.
		/// </summary>
		public static string isprivate => "isprivate";

		/// <summary>
		/// Display Name: Posizione,
		/// Type: String,
		/// Description: Digitare la posizione del contatto primario per il lead per garantire che il prospect sia indicato correttamente nelle chiamate di vendita, nei messaggi e-mail e nelle campagne di marketing.
		/// </summary>
		public static string jobtitle => "jobtitle";

		/// <summary>
		/// Display Name: Cognome,
		/// Type: String,
		/// Description: Digitare il cognome del contatto primario per il lead per garantire che il prospect sia indicato correttamente nelle chiamate di vendita, nei messaggi e-mail e nelle campagne di marketing.
		/// </summary>
		public static string lastname => "lastname";

		/// <summary>
		/// Display Name: Ultimo periodo sospensione,
		/// Type: DateTime,
		/// Description: Contiene l'indicatore di data e ora dell'ultimo periodo di sospensione.
		/// </summary>
		public static string lastonholdtime => "lastonholdtime";

		/// <summary>
		/// Display Name: Data ultima campagna,
		/// Type: DateTime,
		/// Description: Mostra la data dell'ultima volta in cui il lead è stato incluso in una campagna di marketing o in una mini-campagna.
		/// </summary>
		public static string lastusedincampaign => "lastusedincampaign";

		/// <summary>
		/// Display Name: Lead,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco del lead.
		/// </summary>
		public static string leadid => "leadid";

		/// <summary>
		/// Display Name: Valutazione,
		/// Type: Picklist,
		/// Values:
		/// Alto: 1,
		/// Medio: 2,
		/// Basso: 3,
		/// Description: Selezionare un valore di classificazione per indicare la probabilità che il lead diventi un cliente.
		/// </summary>
		public static string leadqualitycode => "leadqualitycode";

		/// <summary>
		/// Display Name: Fonte,
		/// Type: Picklist,
		/// Values:
		/// Annuncio pubblicitario: 1,
		/// Segnalazione dipendente: 2,
		/// Segnalazione esterna: 3,
		/// Partner: 4,
		/// Relazioni pubbliche: 5,
		/// Seminario: 6,
		/// Fiera: 7,
		/// Web: 8,
		/// Passaparola: 9,
		/// Altro: 10,
		/// Description: Selezionare l'origine di marketing primaria che ha spinto il lead al contatto.
		/// </summary>
		public static string leadsourcecode => "leadsourcecode";

		/// <summary>
		/// Display Name: ID master,
		/// Type: Lookup,
		/// Related entities: lead,
		/// Description: Identificatore univoco del lead master per l'unione.
		/// </summary>
		public static string masterid => "masterid";

		/// <summary>
		/// Display Name: Unito,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il lead è stato unito a un altro lead.
		/// </summary>
		public static string merged => "merged";

		/// <summary>
		/// Display Name: Secondo nome,
		/// Type: String,
		/// Description: Digitare il secondo nome o l'iniziale del contatto primario per il lead per garantire che il prospect sia indicato correttamente.
		/// </summary>
		public static string middlename => "middlename";

		/// <summary>
		/// Display Name: Cellulare,
		/// Type: String,
		/// Description: Digitare il numero di cellulare per il contatto primario per il lead.
		/// </summary>
		public static string mobilephone => "mobilephone";

		/// <summary>
		/// Display Name: Rifiuto esplicito GDPR,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il lead è stato rifiutato esplicitamente o meno
		/// </summary>
		public static string msdyn_gdproptout => "msdyn_gdproptout";

		/// <summary>
		/// Display Name: (Deprecato) Grado lead,
		/// Type: Picklist,
		/// Values:
		/// Grado A: 0,
		/// Grado B: 1,
		/// Grado C: 2,
		/// Grado D: 3,
		/// Description: 
		/// </summary>
		public static string msdyn_leadgrade => "msdyn_leadgrade";

		/// <summary>
		/// Display Name: KPI,
		/// Type: Lookup,
		/// Related entities: msdyn_leadkpiitem,
		/// Description: LeadKPIId
		/// </summary>
		public static string msdyn_leadkpiid => "msdyn_leadkpiid";

		/// <summary>
		/// Display Name: (Deprecato) Punteggio lead,
		/// Type: Integer,
		/// Description: 
		/// </summary>
		public static string msdyn_leadscore => "msdyn_leadscore";

		/// <summary>
		/// Display Name: (Deprecato) Tendenza punteggio lead,
		/// Type: Picklist,
		/// Values:
		/// In crescita: 0,
		/// Stabile: 1,
		/// In calo: 2,
		/// Informazioni insufficienti: 3,
		/// Description: 
		/// </summary>
		public static string msdyn_leadscoretrend => "msdyn_leadscoretrend";

		/// <summary>
		/// Display Name: Punteggio predittivo,
		/// Type: Lookup,
		/// Related entities: msdyn_predictivescore,
		/// Description: Punteggio predittivo
		/// </summary>
		public static string msdyn_predictivescoreid => "msdyn_predictivescoreid";

		/// <summary>
		/// Display Name: Risultato regola di assegnazione,
		/// Type: Picklist,
		/// Values:
		/// Completata: 0,
		/// Non riuscito: 1,
		/// Description: Risultato del processo della regola di assegnazione
		/// </summary>
		public static string msdyn_salesassignmentresult => "msdyn_salesassignmentresult";

		/// <summary>
		/// Display Name: (Deprecato) Cronologia punteggio,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string msdyn_scorehistory => "msdyn_scorehistory";

		/// <summary>
		/// Display Name: (Deprecato) Motivi punteggio,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string msdyn_scorereasons => "msdyn_scorereasons";

		/// <summary>
		/// Display Name: ID segmento,
		/// Type: Lookup,
		/// Related entities: msdyn_segment,
		/// Description: Identificatore univoco del segmento associato al lead.
		/// </summary>
		public static string msdyn_segmentid => "msdyn_segmentid";

		/// <summary>
		/// Display Name: Esigenza,
		/// Type: Picklist,
		/// Values:
		/// Irrinunciabile: 0,
		/// Necessaria: 1,
		/// Accessoria: 2,
		/// Nessuna esigenza: 3,
		/// Description: Scegliere il livello di esigenza per la società del lead.
		/// </summary>
		public static string need => "need";

		/// <summary>
		/// Display Name: N. dipendenti,
		/// Type: Integer,
		/// Description: Digitare il numero di dipendenti che lavorano presso la società associata al lead da usare nella segmentazione di marketing e nell'analisi demografica.
		/// </summary>
		public static string numberofemployees => "numberofemployees";

		/// <summary>
		/// Display Name: Periodo di sospensione (minuti),
		/// Type: Integer,
		/// Description: Mostra la durata della sospensione del record in minuti.
		/// </summary>
		public static string onholdtime => "onholdtime";

		/// <summary>
		/// Display Name: Caso di origine,
		/// Type: Lookup,
		/// Related entities: incident,
		/// Description: Questo attributo è usato per i processi aziendali servizio di esempio.
		/// </summary>
		public static string originatingcaseid => "originatingcaseid";

		/// <summary>
		/// Display Name: Cercapersone,
		/// Type: String,
		/// Description: Digitare il numero del secondo cellulare per il contatto primario per il lead.
		/// </summary>
		public static string pager => "pager";

		/// <summary>
		/// Display Name: Account padre per il lead,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Scegli un account a cui connettere il lead in modo che la relazione sia visibile nei report e nelle analisi.
		/// </summary>
		public static string parentaccountid => "parentaccountid";

		/// <summary>
		/// Display Name: Contatto padre per il lead,
		/// Type: Lookup,
		/// Related entities: contact,
		/// Description: Scegli un contatto a cui connettere il lead in modo che la relazione sia visibile nei report e nelle analisi.
		/// </summary>
		public static string parentcontactid => "parentcontactid";

		/// <summary>
		/// Display Name: Partecipa al flusso di lavoro,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Mostra se il lead partecipa alle regole del flusso di lavoro.
		/// </summary>
		public static string participatesinworkflow => "participatesinworkflow";

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
		/// Display Name: Priorità,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare la priorità in modo che i clienti preferiti o i problemi critici vengano gestiti rapidamente.
		/// </summary>
		public static string prioritycode => "prioritycode";

		/// <summary>
		/// Display Name: ID processo,
		/// Type: Uniqueidentifier,
		/// Description: Contiene l'ID del processo associato all'entità.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Processo di acquisto,
		/// Type: Picklist,
		/// Values:
		/// Singolo utente: 0,
		/// Comitato: 1,
		/// Sconosciuto: 2,
		/// Description: Scegli se nel processo di acquisto per il lead verrà coinvolto un singolo utente o un insieme di utenti.
		/// </summary>
		public static string purchaseprocess => "purchaseprocess";

		/// <summary>
		/// Display Name: Intervallo di tempo acquisti,
		/// Type: Picklist,
		/// Values:
		/// Immediatamente: 0,
		/// Trimestre corrente: 1,
		/// Trimestre successivo: 2,
		/// Quest'anno: 3,
		/// Sconosciuto: 4,
		/// Description: Scegliere il tempo previsto di acquisto da parte del lead in modo che il team di vendita ne sia consapevole.
		/// </summary>
		public static string purchasetimeframe => "purchasetimeframe";

		/// <summary>
		/// Display Name: Commenti qualifica,
		/// Type: Memo,
		/// Description: Digitare i commenti sulla qualifica o sul punteggio del lead.
		/// </summary>
		public static string qualificationcomments => "qualificationcomments";

		/// <summary>
		/// Display Name: Opportunità qualificante,
		/// Type: Lookup,
		/// Related entities: opportunity,
		/// Description: Scegli l'opportunità in base alla quale il lead è stato qualificato e convertito.
		/// </summary>
		public static string qualifyingopportunityid => "qualifyingopportunityid";

		/// <summary>
		/// Display Name: Risposta campagna correlata,
		/// Type: Lookup,
		/// Related entities: campaignresponse,
		/// Description: Risposta campagna correlata.
		/// </summary>
		public static string relatedobjectid => "relatedobjectid";

		/// <summary>
		/// Display Name: Ricavi annuali,
		/// Type: Money,
		/// Description: Digitare i ricavi annuali della società associati al lead per indicare informazioni sull'azienda del prospect.
		/// </summary>
		public static string revenue => "revenue";

		/// <summary>
		/// Display Name: Ricavi annuali (Base),
		/// Type: Money,
		/// Description: Value of the Ricavi annuali in base currency.
		/// </summary>
		public static string revenue_base => "revenue_base";

		/// <summary>
		/// Display Name: Fase di vendita,
		/// Type: Picklist,
		/// Values:
		/// Qualifica: 0,
		/// Description: Selezionare la fase di vendita del lead per supportare il team di vendita nella conversione del lead in un'opportunità.
		/// </summary>
		public static string salesstage => "salesstage";

		/// <summary>
		/// Display Name: Codice fase di vendita,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Selezionare la fase del processo di vendita per il lead per determinare la probabilità che il lead venga convertito in un'opportunità.
		/// </summary>
		public static string salesstagecode => "salesstagecode";

		/// <summary>
		/// Display Name: Formula d'apertura,
		/// Type: String,
		/// Description: Digitare il titolo del contatto primario per il lead per garantire che il prospect sia indicato correttamente nelle chiamate di vendita, nei messaggi e-mail e nelle campagne di marketing.
		/// </summary>
		public static string salutation => "salutation";

		/// <summary>
		/// Display Name: Pianifica completamento (cliente potenziale),
		/// Type: DateTime,
		/// Description: Immettere la data e l'ora della riunione di completamento esplorativa con il lead.
		/// </summary>
		public static string schedulefollowup_prospect => "schedulefollowup_prospect";

		/// <summary>
		/// Display Name: Pianifica completamento (qualificato),
		/// Type: DateTime,
		/// Description: Immettere la data e l'ora della riunione di completamento dell'impostazione del lead come qualificato.
		/// </summary>
		public static string schedulefollowup_qualify => "schedulefollowup_qualify";

		/// <summary>
		/// Display Name: Codice NACE,
		/// Type: String,
		/// Description: Digitare il codice NACE che indica il settore primario del lead da usare nella segmentazione di marketing e nell'analisi demografica.
		/// </summary>
		public static string sic => "sic";

		/// <summary>
		/// Display Name: CONTRATTO DI SERVIZIO,
		/// Type: Lookup,
		/// Related entities: sla,
		/// Description: Scegli il contratto di servizio da applicare al record del lead.
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
		/// Display Name: (Deprecated) Stage Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the stage where the entity is located.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Argomento,
		/// Type: String,
		/// Description: Digitare un argomento o un nome descrittivo, ad esempio l'ordine previsto, il nome della società o l'elenco marketing di origine, per identificare il lead.
		/// </summary>
		public static string subject => "subject";

		/// <summary>
		/// Display Name: TeamsFollowed,
		/// Type: Integer,
		/// Description: Numero di utenti o conversazioni che hanno seguito il record
		/// </summary>
		public static string teamsfollowed => "teamsfollowed";

		/// <summary>
		/// Display Name: Telefono ufficio,
		/// Type: String,
		/// Description: Digitare il numero di cellulare dell'ufficio per il contatto primario per il lead.
		/// </summary>
		public static string telephone1 => "telephone1";

		/// <summary>
		/// Display Name: Telefono abitazione,
		/// Type: String,
		/// Description: Digitare il numero di telefono dell'abitazione per il contatto primario per il lead.
		/// </summary>
		public static string telephone2 => "telephone2";

		/// <summary>
		/// Display Name: Altro telefono,
		/// Type: String,
		/// Description: Digitare un numero di telefono alternativo per il contatto primario per il lead.
		/// </summary>
		public static string telephone3 => "telephone3";

		/// <summary>
		/// Display Name: Tempo dedicato personalmente,
		/// Type: String,
		/// Description: Tempo totale dedicato personalmente ai messaggi e-mail (lettura e scrittura) e alle riunioni relativamente al record del lead.
		/// </summary>
		public static string timespentbymeonemailandmeetings => "timespentbymeonemailandmeetings";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Scegli la valuta locale per il record per assicurarti che i budget vengano espressi nella valuta corretta.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";

		/// <summary>
		/// Display Name: (Deprecated) Traversed Path,
		/// Type: String,
		/// Description: A comma separated list of string values representing the unique identifiers of stages in a Business Process Flow Instance in the order that they occur.
		/// </summary>
		public static string traversedpath => "traversedpath";

		/// <summary>
		/// Display Name: Sito Web,
		/// Type: String,
		/// Description: Digitare l'URL del sito Web della società associata al lead.
		/// </summary>
		public static string websiteurl => "websiteurl";

		/// <summary>
		/// Display Name: Nome società Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del nome della società del lead, se specificato in giapponese, per garantire che venga pronunciato correttamente nelle telefonate con il prospect.
		/// </summary>
		public static string yomicompanyname => "yomicompanyname";

		/// <summary>
		/// Display Name: Nome Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del nome del lead, se specificato in giapponese, per garantire che venga pronunciato correttamente nelle telefonate con il prospect.
		/// </summary>
		public static string yomifirstname => "yomifirstname";

		/// <summary>
		/// Display Name: Nome completo Yomi,
		/// Type: String,
		/// Description: Combina e mostra il nome e cognome Yomi del lead per consentire di mostrare il nome fonetico completo in visualizzazioni e report.
		/// </summary>
		public static string yomifullname => "yomifullname";

		/// <summary>
		/// Display Name: Cognome Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del cognome del lead, se specificato in giapponese, per garantire che venga pronunciato correttamente nelle telefonate con il prospect.
		/// </summary>
		public static string yomilastname => "yomilastname";

		/// <summary>
		/// Display Name: Secondo nome Yomi,
		/// Type: String,
		/// Description: Digitare la forma fonetica del secondo nome del lead, se specificato in giapponese, per garantire che venga pronunciato correttamente nelle telefonate con il prospect.
		/// </summary>
		public static string yomimiddlename => "yomimiddlename";


		/// <summary>
		/// Values for field Stato
		/// <summary>
		public new enum statecodeValues
		{
			Apri = 0,
			Nonqualificato = 2,
			Qualificato = 1
		}

		/// <summary>
		/// Values for field Motivo stato
		/// <summary>
		public new enum statuscodeValues
		{
			Annullato_StateNonqualificato = 7,
			Contattato_StateApri = 2,
			Noncontattabile_StateNonqualificato = 5,
			Nonpiuinteressato_StateNonqualificato = 6,
			Nuovo_StateApri = 1,
			Perso_StateNonqualificato = 4,
			Qualificato_StateQualificato = 3
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
		/// Values for field Costi preventivati
		/// <summary>
		public enum budgetstatusValues
		{
			Acquistopossibile = 1,
			Acquistoprobabile = 2,
			Acquistosicuro = 3,
			Nessunbudgetimpegnato = 0
		}

		/// <summary>
		/// Values for field Conferma interesse
		/// <summary>
		public enum confirminterestValues
		{
			No = 1,
			Si = 0
		}

		/// <summary>
		/// Values for field Decisore?
		/// <summary>
		public enum decisionmakerValues
		{
			completato = 1,
			segnacomecompletato = 0
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
		/// Values for field Materiale marketing
		/// <summary>
		public enum donotsendmmValues
		{
			Invia = 0,
			Noninviare = 1
		}

		/// <summary>
		/// Values for field Valuta corrispondenza
		/// <summary>
		public enum evaluatefitValues
		{
			No = 1,
			Si = 0
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
		/// Values for field Comunicazione iniziale
		/// <summary>
		public enum initialcommunicationValues
		{
			Contattato = 0,
			Noncontattato = 1
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
		/// Values for field Privato
		/// <summary>
		public enum isprivateValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Valutazione
		/// <summary>
		public enum leadqualitycodeValues
		{
			Alto = 1,
			Basso = 3,
			Medio = 2
		}

		/// <summary>
		/// Values for field Fonte
		/// <summary>
		public enum leadsourcecodeValues
		{
			Altro = 10,
			Annunciopubblicitario = 1,
			Fiera = 7,
			Partner = 4,
			Passaparola = 9,
			Relazionipubbliche = 5,
			Segnalazionedipendente = 2,
			Segnalazioneesterna = 3,
			Seminario = 6,
			Web = 8
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
		/// Values for field Rifiuto esplicito GDPR
		/// <summary>
		public enum msdyn_gdproptoutValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field (Deprecato) Grado lead
		/// <summary>
		public enum msdyn_leadgradeValues
		{
			GradoA = 0,
			GradoB = 1,
			GradoC = 2,
			GradoD = 3
		}

		/// <summary>
		/// Values for field (Deprecato) Tendenza punteggio lead
		/// <summary>
		public enum msdyn_leadscoretrendValues
		{
			Incalo = 2,
			Increscita = 0,
			Informazioniinsufficienti = 3,
			Stabile = 1
		}

		/// <summary>
		/// Values for field Risultato regola di assegnazione
		/// <summary>
		public enum msdyn_salesassignmentresultValues
		{
			Completata = 0,
			Nonriuscito = 1
		}

		/// <summary>
		/// Values for field Esigenza
		/// <summary>
		public enum needValues
		{
			Accessoria = 2,
			Irrinunciabile = 0,
			Necessaria = 1,
			Nessunaesigenza = 3
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
		/// Values for field Priorità
		/// <summary>
		public enum prioritycodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Processo di acquisto
		/// <summary>
		public enum purchaseprocessValues
		{
			Comitato = 1,
			Sconosciuto = 2,
			Singoloutente = 0
		}

		/// <summary>
		/// Values for field Intervallo di tempo acquisti
		/// <summary>
		public enum purchasetimeframeValues
		{
			Immediatamente = 0,
			Questanno = 3,
			Sconosciuto = 4,
			Trimestrecorrente = 1,
			Trimestresuccessivo = 2
		}

		/// <summary>
		/// Values for field Fase di vendita
		/// <summary>
		public enum salesstageValues
		{
			Qualifica = 0
		}

		/// <summary>
		/// Values for field Codice fase di vendita
		/// <summary>
		public enum salesstagecodeValues
		{
			Valorepredefinito = 1
		}
	};
}
