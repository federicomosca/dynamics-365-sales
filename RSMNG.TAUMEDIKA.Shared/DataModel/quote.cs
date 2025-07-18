namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Offerta constants.
	/// </summary>
	public sealed class quote : EntityGenericConstants
	{
		/// <summary>
		/// quote
		/// </summary>
		public static string logicalName => "quote";

		/// <summary>
		/// Offerta
		/// </summary>
		public static string displayName => "Offerta";

		/// <summary>
		/// Display Name: Account,
		/// Type: Lookup,
		/// Related entities: account,
		/// Description: Identificatore univoco dell'account a cui è associata l'offerta.
		/// </summary>
		public static string accountid => "accountid";

		/// <summary>
		/// Display Name: ID indirizzo di fatturazione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'indirizzo di fatturazione.
		/// </summary>
		public static string billto_addressid => "billto_addressid";

		/// <summary>
		/// Display Name: Città fatturazione,
		/// Type: String,
		/// Description: Digitare la città per l'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_city => "billto_city";

		/// <summary>
		/// Display Name: Indirizzo di fatturazione,
		/// Type: Memo,
		/// Description: Mostra l'indirizzo di fatturazione completo.
		/// </summary>
		public static string billto_composite => "billto_composite";

		/// <summary>
		/// Display Name: Nome contatto fatturazione,
		/// Type: String,
		/// Description: Digita il nome del contatto primario all'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_contactname => "billto_contactname";

		/// <summary>
		/// Display Name: Paese fatturazione,
		/// Type: String,
		/// Description: Digitare il paese/area geografica per l'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_country => "billto_country";

		/// <summary>
		/// Display Name: Fax fatturazione,
		/// Type: String,
		/// Description: Digitare il numero di fax per l'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_fax => "billto_fax";

		/// <summary>
		/// Display Name: Via 1 fatturazione,
		/// Type: String,
		/// Description: Digitare la prima riga dell'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_line1 => "billto_line1";

		/// <summary>
		/// Display Name: Via 2 fatturazione,
		/// Type: String,
		/// Description: Digitare la seconda riga dell'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_line2 => "billto_line2";

		/// <summary>
		/// Display Name: Via 3 fatturazione,
		/// Type: String,
		/// Description: Digita la terza riga dell'indirizzo di fatturazione.
		/// </summary>
		public static string billto_line3 => "billto_line3";

		/// <summary>
		/// Display Name: Nome fatturazione,
		/// Type: String,
		/// Description: Digitare un nome per l'indirizzo di fatturazione del cliente, ad esempio "Sede centrale" oppure "Filiale", per identificare l'indirizzo.
		/// </summary>
		public static string billto_name => "billto_name";

		/// <summary>
		/// Display Name: CAP fatturazione,
		/// Type: String,
		/// Description: Digitare il codice postale per l'indirizzo di fatturazione.
		/// </summary>
		public static string billto_postalcode => "billto_postalcode";

		/// <summary>
		/// Display Name: Provincia di fatturazione,
		/// Type: String,
		/// Description: Digitare la provincia dell'indirizzo di fatturazione.
		/// </summary>
		public static string billto_stateorprovince => "billto_stateorprovince";

		/// <summary>
		/// Display Name: Telefono fatturazione,
		/// Type: String,
		/// Description: Digitare il numero di telefono per l'indirizzo di fatturazione del cliente.
		/// </summary>
		public static string billto_telephone => "billto_telephone";

		/// <summary>
		/// Display Name: Campagna di origine,
		/// Type: Lookup,
		/// Related entities: campaign,
		/// Description: Mostra la campagna da cui è stato creato l'ordine.
		/// </summary>
		public static string campaignid => "campaignid";

		/// <summary>
		/// Display Name: Data chiusura,
		/// Type: DateTime,
		/// Description: Immettere la data di chiusura dell'offerta per indicare la data di scadenza, di aggiornamento o di annullamento.
		/// </summary>
		public static string closedon => "closedon";

		/// <summary>
		/// Display Name: Contatto,
		/// Type: Lookup,
		/// Related entities: contact,
		/// Description: Identificatore univoco del contatto associato all'offerta.
		/// </summary>
		public static string contactid => "contactid";

		/// <summary>
		/// Display Name: Potenziale cliente,
		/// Type: Customer,
		/// Description: Seleziona l'account cliente o il contatto per fornire un link rapido a dettagli aggiuntivi sul cliente, ad esempio informazioni sull'account, impegni e opportunità.
		/// </summary>
		public static string customerid => "customerid";

		/// <summary>
		/// Display Name: Tipo di potenziale cliente,
		/// Type: EntityName,
		/// Description: 
		/// </summary>
		public static string customeridtype => "customeridtype";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Digitare informazioni aggiuntive per descrivere l'offerta, ad esempio i prodotti o i servizi offerti oppure i dettagli relativi alle preferenze del cliente sui prodotti.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Importo sconto offerta,
		/// Type: Money,
		/// Description: Digitare l'importo dello sconto per l'offerta se il cliente ha diritto a sconti speciali.
		/// </summary>
		public static string discountamount => "discountamount";

		/// <summary>
		/// Display Name: Importo sconto offerta (Base),
		/// Type: Money,
		/// Description: Value of the Importo sconto offerta in base currency.
		/// </summary>
		public static string discountamount_base => "discountamount_base";

		/// <summary>
		/// Display Name: Sconto offerta (%),
		/// Type: Decimal,
		/// Description: Digitare la percentuale di sconto da applicare all'importo dello sconto per includere nell'offerta ulteriori risparmi per il cliente.
		/// </summary>
		public static string discountpercentage => "discountpercentage";

		/// <summary>
		/// Display Name: Data inizio validità,
		/// Type: DateTime,
		/// Description: Immettere la data di validità del prezzo dell'offerta o in cui il prezzo è stato comunicato per la prima volta al cliente.
		/// </summary>
		public static string effectivefrom => "effectivefrom";

		/// <summary>
		/// Display Name: Data fine validità,
		/// Type: DateTime,
		/// Description: Immettere la data di scadenza o l'ultimo giorno di validità del prezzo dell'offerta per il cliente.
		/// </summary>
		public static string effectiveto => "effectiveto";

		/// <summary>
		/// Display Name: Email Address,
		/// Type: String,
		/// Description: The primary email address for the entity.
		/// </summary>
		public static string emailaddress => "emailaddress";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Mostra il tasso di conversione della valuta del record. Il tasso di cambio è usato per convertire tutti i campi di tipo money nel record dalla valuta locale alla valuta predefinita del sistema.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Scadenza,
		/// Type: DateTime,
		/// Description: Immettere la data entro al quale il cliente deve comunicare la decisione o l'ordine per indicare la data di scadenza dell'offerta.
		/// </summary>
		public static string expireson => "expireson";

		/// <summary>
		/// Display Name: Importo spesa accessoria,
		/// Type: Money,
		/// Description: Digitare il costo di spedizione per i prodotti inclusi nell'offerta per l'uso nel calcolo del campo Totale offerta.
		/// </summary>
		public static string freightamount => "freightamount";

		/// <summary>
		/// Display Name: Spese di spedizione (Base),
		/// Type: Money,
		/// Description: Value of the Spese di spedizione in base currency.
		/// </summary>
		public static string freightamount_base => "freightamount_base";

		/// <summary>
		/// Display Name: Condizioni di spedizione,
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
		/// Description: Selezionare le condizioni di spedizione per assicurare che le spese di spedizione vengano elaborate correttamente.
		/// </summary>
		public static string freighttermscode => "freighttermscode";

		/// <summary>
		/// Display Name: Ultimo periodo sospensione,
		/// Type: DateTime,
		/// Description: Contiene la data e il timestamp dell'ultimo periodo di sospensione.
		/// </summary>
		public static string lastonholdtime => "lastonholdtime";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Digitare un nome descrittivo per l'offerta.
		/// </summary>
		public static string name => "name";

		/// <summary>
		/// Display Name: Periodo di sospensione (minuti),
		/// Type: Integer,
		/// Description: Mostra la durata in minuti della sospensione dell'offerta.
		/// </summary>
		public static string onholdtime => "onholdtime";

		/// <summary>
		/// Display Name: Opportunità,
		/// Type: Lookup,
		/// Related entities: opportunity,
		/// Description: Scegli l'opportunità a cui è correlata l'offerta a scopo di report e analisi.
		/// </summary>
		public static string opportunityid => "opportunityid";

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
		/// Display Name: Listino prezzi,
		/// Type: Lookup,
		/// Related entities: pricelevel,
		/// Description: Scegli il listino prezzi associato a questo record per garantire che i prodotti associati alla campagna vengano offerti ai prezzi corretti.
		/// </summary>
		public static string pricelevelid => "pricelevelid";

		/// <summary>
		/// Display Name: Errore di determinazione dei prezzi ,
		/// Type: Picklist,
		/// Values:
		/// Nessuno: 0,
		/// Dettagli errore: 1,
		/// Listino prezzi mancante: 2,
		/// Listino prezzi inattivo: 3,
		/// Quantità mancante: 4,
		/// Prezzo unitario mancante: 5,
		/// Prodotto mancante: 6,
		/// Prodotto non valido: 7,
		/// Codice di determinazione dei prezzi mancante: 8,
		/// Codice di determinazione dei prezzi non valido: 9,
		/// Unità di misura mancante: 10,
		/// Prodotto non incluso nel listino prezzi: 11,
		/// Importo del listino prezzi mancante: 12,
		/// Percentuale del listino prezzi mancante: 13,
		/// Prezzo mancante: 14,
		/// Costo corrente mancante: 15,
		/// Costo medio mancante: 16,
		/// Importo del listino prezzi non valido: 17,
		/// Percentuale del listino prezzi non valida: 18,
		/// Prezzo non valido: 19,
		/// Costo corrente non valido: 20,
		/// Costo medio non valido: 21,
		/// Regola di arrotondamento non valida: 22,
		/// Opzione di arrotondamento non valida: 23,
		/// Valore di arrotondamento non valido: 24,
		/// Errore di calcolo del prezzo: 25,
		/// Tipo di sconto non valido: 26,
		/// Stato non valido tipo di sconto: 27,
		/// Sconto non valido: 28,
		/// Quantità non valida: 29,
		/// Precisione di determinazione dei prezzi non valida: 30,
		/// Unità di misura predefinita del prodotto mancante: 31,
		/// Unità di vendita del prodotto mancante : 32,
		/// Tipo di sconto inattivo: 33,
		/// Valuta del listino prezzi non valida: 34,
		/// Attributo del prezzo fuori intervallo: 35,
		/// Overflow dell'attributo della valuta di base: 36,
		/// Underflow dell'attributo della valuta di base: 37,
		/// La valuta della transazione non è impostata per la voce di listino del prodotto: 38,
		/// Description: Errore di determinazione del prezzo per l'offerta.
		/// </summary>
		public static string pricingerrorcode => "pricingerrorcode";

		/// <summary>
		/// Display Name: Process Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the process associated with the entity.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Metodo di creazione,
		/// Type: Picklist,
		/// Values:
		/// Sconosciuto: 776160000,
		/// Revisione: 776160001,
		/// Description: 
		/// </summary>
		public static string quotecreationmethod => "quotecreationmethod";

		/// <summary>
		/// Display Name: Offerta,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'offerta.
		/// </summary>
		public static string quoteid => "quoteid";

		/// <summary>
		/// Display Name: Nr. Offerta,
		/// Type: String,
		/// Description: Mostra il numero di offerta come riferimento per il cliente e ai fini di ricerca. Il numero non può essere modificato.
		/// </summary>
		public static string quotenumber => "quotenumber";

		/// <summary>
		/// Display Name: Data consegna richiesta,
		/// Type: DateTime,
		/// Description: Immettere la data di consegna richiesta dal cliente per tutti i prodotti dell'offerta.
		/// </summary>
		public static string requestdeliveryby => "requestdeliveryby";

		/// <summary>
		/// Display Name: Spesa accessoria,
		/// Type: Lookup,
		/// Related entities: res_additionalexpense,
		/// Description: 
		/// </summary>
		public static string res_additionalexpenseid => "res_additionalexpenseid";

		/// <summary>
		/// Display Name: Banca,
		/// Type: Lookup,
		/// Related entities: res_bankdetails,
		/// Description: 
		/// </summary>
		public static string res_bankdetailsid => "res_bankdetailsid";

		/// <summary>
		/// Display Name: Nazione spedizione,
		/// Type: Lookup,
		/// Related entities: res_country,
		/// Description: 
		/// </summary>
		public static string res_countryid => "res_countryid";

		/// <summary>
		/// Display Name: Data,
		/// Type: DateTime,
		/// Description: 
		/// </summary>
		public static string res_date => "res_date";

		/// <summary>
		/// Display Name: Acconto,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_deposit => "res_deposit";

		/// <summary>
		/// Display Name: Acconto (base),
		/// Type: Money,
		/// Description: Valore di Acconto nella valuta di base.
		/// </summary>
		public static string res_deposit_base => "res_deposit_base";

		/// <summary>
		/// Display Name: Commento uso interno,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_internalusecomment => "res_internalusecomment";

		/// <summary>
		/// Display Name: Richiesta fattura,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isinvoicerequested => "res_isinvoicerequested";

		/// <summary>
		/// Display Name: Località,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_location => "res_location";

		/// <summary>
		/// Display Name: Motivo stato precedente,
		/// Type: Picklist,
		/// Values:
		/// Bozza: 1,
		/// In approvazione: 2,
		/// Approvata: 3,
		/// Acquisita: 4,
		/// Non approvata: 5,
		/// Persa: 6,
		/// Aggiornata: 7,
		/// Description: 
		/// </summary>
		public static string res_oldstatuscode => "res_oldstatuscode";

		/// <summary>
		/// Display Name: Condizione di pagamento,
		/// Type: Lookup,
		/// Related entities: res_paymentterm,
		/// Description: 
		/// </summary>
		public static string res_paymenttermid => "res_paymenttermid";

		/// <summary>
		/// Display Name: Destinatario,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_recipient => "res_recipient";

		/// <summary>
		/// Display Name: Riferimento spedizione,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_shippingreference => "res_shippingreference";

		/// <summary>
		/// Display Name: Codice IVA spesa accessoria,
		/// Type: Lookup,
		/// Related entities: res_vatnumber,
		/// Description: 
		/// </summary>
		public static string res_vatnumberid => "res_vatnumberid";

		/// <summary>
		/// Display Name: ID aggiornamento,
		/// Type: Integer,
		/// Description: Mostra il numero di versione dell'offerta per la registrazione della cronologia di aggiornamento.
		/// </summary>
		public static string revisionnumber => "revisionnumber";

		/// <summary>
		/// Display Name: Metodo di spedizione,
		/// Type: Picklist,
		/// Values:
		/// Trasporto aereo: 1,
		/// Spedizione Postale: 5,
		/// Trasporto stradale: 9,
		/// Trasporto ferroviario: 10,
		/// Trasporto marittimo: 11,
		/// Trasporto intermodale: 18,
		/// Corriere espresso: 28,
		/// Description: Seleziona un metodo di spedizione per le consegne inviate a questo indirizzo.
		/// </summary>
		public static string shippingmethodcode => "shippingmethodcode";

		/// <summary>
		/// Display Name: ID indirizzo di spedizione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'indirizzo di spedizione.
		/// </summary>
		public static string shipto_addressid => "shipto_addressid";

		/// <summary>
		/// Display Name: Città spedizione,
		/// Type: String,
		/// Description: Digitare la città per l'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_city => "shipto_city";

		/// <summary>
		/// Display Name: Indirizzo di spedizione,
		/// Type: Memo,
		/// Description: Mostra l'indirizzo di spedizione completo.
		/// </summary>
		public static string shipto_composite => "shipto_composite";

		/// <summary>
		/// Display Name: Nome contatto spedizione,
		/// Type: String,
		/// Description: Digitare il nome del contatto primario all'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_contactname => "shipto_contactname";

		/// <summary>
		/// Display Name: Paese spedizione,
		/// Type: String,
		/// Description: Digitare il paese/area geografica per l'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_country => "shipto_country";

		/// <summary>
		/// Display Name: Fax spedizione,
		/// Type: String,
		/// Description: Digitare il numero di fax per l'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_fax => "shipto_fax";

		/// <summary>
		/// Display Name: Condizioni di spedizione per indirizzo spedizione,
		/// Type: Picklist,
		/// Values:
		/// Valore predefinito: 1,
		/// Description: Seleziona le condizioni di spedizione per garantire che gli ordini di spedizione vengano elaborati correttamente.
		/// </summary>
		public static string shipto_freighttermscode => "shipto_freighttermscode";

		/// <summary>
		/// Display Name: Indirizzo,
		/// Type: String,
		/// Description: Digitare la prima riga dell'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_line1 => "shipto_line1";

		/// <summary>
		/// Display Name: Via 2 spedizione,
		/// Type: String,
		/// Description: Digitare la seconda riga dell'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_line2 => "shipto_line2";

		/// <summary>
		/// Display Name: Via 3 spedizione,
		/// Type: String,
		/// Description: Digita la terza riga dell'indirizzo di spedizione.
		/// </summary>
		public static string shipto_line3 => "shipto_line3";

		/// <summary>
		/// Display Name: Nome spedizione,
		/// Type: String,
		/// Description: Digita un nome per l'indirizzo di spedizione del cliente, ad esempio "Sede centrale" oppure "Filiale", per identificare l'indirizzo.
		/// </summary>
		public static string shipto_name => "shipto_name";

		/// <summary>
		/// Display Name: CAP spedizione,
		/// Type: String,
		/// Description: Digitare il codice postale per l'indirizzo di spedizione.
		/// </summary>
		public static string shipto_postalcode => "shipto_postalcode";

		/// <summary>
		/// Display Name: Provincia,
		/// Type: String,
		/// Description: Digitare la provincia dell'indirizzo di spedizione.
		/// </summary>
		public static string shipto_stateorprovince => "shipto_stateorprovince";

		/// <summary>
		/// Display Name: Telefono spedizione,
		/// Type: String,
		/// Description: Digitare il numero di telefono per l'indirizzo di spedizione del cliente.
		/// </summary>
		public static string shipto_telephone => "shipto_telephone";

		/// <summary>
		/// Display Name: Ignora calcolo prezzo,
		/// Type: Picklist,
		/// Values:
		/// DoPriceCalcAlways: 0,
		/// SkipPriceCalcOnRetrieve: 1,
		/// Description: Ignora calcolo del prezzo (per uso interno)
		/// </summary>
		public static string skippricecalculation => "skippricecalculation";

		/// <summary>
		/// Display Name: CONTRATTO DI SERVIZIO,
		/// Type: Lookup,
		/// Related entities: sla,
		/// Description: Scegli il contratto di servizio da applicare al record dell'offerta.
		/// </summary>
		public static string slaid => "slaid";

		/// <summary>
		/// Display Name: Ultimo contratto di servizio applicato,
		/// Type: Lookup,
		/// Related entities: sla,
		/// Description: Ultimo contratto di servizio applicato all'offerta. Questo campo è solo per uso interno.
		/// </summary>
		public static string slainvokedid => "slainvokedid";

		/// <summary>
		/// Display Name: (Deprecated) Stage Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the stage where the entity is located.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Importo totale,
		/// Type: Money,
		/// Description: Mostra l'importo totale dovuto, calcolato come somma dei prodotti, sconti, spese di spedizione e imposte per l'offerta.
		/// </summary>
		public static string totalamount => "totalamount";

		/// <summary>
		/// Display Name: Importo totale (Base),
		/// Type: Money,
		/// Description: Value of the Importo totale in base currency.
		/// </summary>
		public static string totalamount_base => "totalamount_base";

		/// <summary>
		/// Display Name: Totale imponibile,
		/// Type: Money,
		/// Description: Mostra l'importo totale relativo ai prodotti per l'offerta meno eventuali sconti. Questo valore viene aggiunto agli importi delle imposte e delle spese di spedizione per il calcolo dell'importo totale dovuto per l'offerta.
		/// </summary>
		public static string totalamountlessfreight => "totalamountlessfreight";

		/// <summary>
		/// Display Name: Totale senza spedizione (Base),
		/// Type: Money,
		/// Description: Value of the Totale senza spedizione in base currency.
		/// </summary>
		public static string totalamountlessfreight_base => "totalamountlessfreight_base";

		/// <summary>
		/// Display Name: Sconto totale,
		/// Type: Money,
		/// Description: Mostra l'importo totale sconto, in base al valore e alla percentuale di sconto immessi nell'offerta.
		/// </summary>
		public static string totaldiscountamount => "totaldiscountamount";

		/// <summary>
		/// Display Name: Importo totale sconto (Base),
		/// Type: Money,
		/// Description: Value of the Importo totale sconto in base currency.
		/// </summary>
		public static string totaldiscountamount_base => "totaldiscountamount_base";

		/// <summary>
		/// Display Name: Totale righe,
		/// Type: Money,
		/// Description: Mostra la somma di tutti i prodotti esistenti e fuori catalogo inclusi nell'offerta, in base al listino prezzi e alle quantità specificati.
		/// </summary>
		public static string totallineitemamount => "totallineitemamount";

		/// <summary>
		/// Display Name: Totale dettagli (Base),
		/// Type: Money,
		/// Description: Value of the Totale dettagli in base currency.
		/// </summary>
		public static string totallineitemamount_base => "totallineitemamount_base";

		/// <summary>
		/// Display Name: Importo totale sconto per voce,
		/// Type: Money,
		/// Description: Mostra il totale relativo agli sconti manuali specificati per tutti i prodotti inclusi nell'offerta. Questo valore si riflette sul campo Totale dettagli nell'offerta e viene aggiunto a qualsiasi percentuale o importo di sconto specificato nell'offerta.
		/// </summary>
		public static string totallineitemdiscountamount => "totallineitemdiscountamount";

		/// <summary>
		/// Display Name: Importo totale sconto per voce (Base),
		/// Type: Money,
		/// Description: Value of the Importo totale sconto per voce in base currency.
		/// </summary>
		public static string totallineitemdiscountamount_base => "totallineitemdiscountamount_base";

		/// <summary>
		/// Display Name: Totale IVA,
		/// Type: Money,
		/// Description: Mostra il totale relativo agli importi delle imposte specificati per tutti i prodotti inclusi nell'offerta e compresi nel calcolo dell'importo totale dovuto per l'offerta.
		/// </summary>
		public static string totaltax => "totaltax";

		/// <summary>
		/// Display Name: Totale imposte (Base),
		/// Type: Money,
		/// Description: Value of the Totale imposte in base currency.
		/// </summary>
		public static string totaltax_base => "totaltax_base";

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
		/// Display Name: ID univoco descrizione,
		/// Type: Uniqueidentifier,
		/// Description: Solo per uso interno.
		/// </summary>
		public static string uniquedscid => "uniquedscid";

		/// <summary>
		/// Display Name: Spedizione,
		/// Type: Boolean,
		/// Values:
		/// Rtiro da cliente: 1,
		/// Spedizione presso cliente: 0,
		/// Description: Specificare se i prodotti inclusi nell'offerta devono essere consegnati all'indirizzo indicato o trattenuti fino a quando il cliente non impartisce ulteriori istruzioni di ritiro o dl consegna.
		/// </summary>
		public static string willcall => "willcall";


		/// <summary>
		/// Values for field Stato
		/// <summary>
		public new enum statecodeValues
		{
			Acquisita = 2,
			Attiva = 1,
			Bozza = 0,
			Chiusa = 3
		}

		/// <summary>
		/// Values for field Motivo stato
		/// <summary>
		public new enum statuscodeValues
		{
			Acquisita_StateAcquisita = 4,
			Aggiornata_StateChiusa = 7,
			Approvata_StateAttiva = 3,
			Bozza_StateBozza = 1,
			Inapprovazione_StateAttiva = 2,
			Nonapprovata_StateChiusa = 5,
			Persa_StateChiusa = 6
		}

		/// <summary>
		/// Values for field Condizioni di spedizione
		/// <summary>
		public enum freighttermscodeValues
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
		/// Values for field Errore di determinazione dei prezzi 
		/// <summary>
		public enum pricingerrorcodeValues
		{
			Attributodelprezzofuoriintervallo = 35,
			Codicedideterminazionedeiprezzimancante = 8,
			Codicedideterminazionedeiprezzinonvalido = 9,
			Costocorrentemancante = 15,
			Costocorrentenonvalido = 20,
			Costomediomancante = 16,
			Costomediononvalido = 21,
			Dettaglierrore = 1,
			Erroredicalcolodelprezzo = 25,
			Importodellistinoprezzimancante = 12,
			Importodellistinoprezzinonvalido = 17,
			Lavalutadellatransazionenoneimpostataperlavocedilistinodelprodotto = 38,
			Listinoprezziinattivo = 3,
			Listinoprezzimancante = 2,
			Nessuno = 0,
			Opzionediarrotondamentononvalida = 23,
			Overflowdellattributodellavalutadibase = 36,
			Percentualedellistinoprezzimancante = 13,
			Percentualedellistinoprezzinonvalida = 18,
			Precisionedideterminazionedeiprezzinonvalida = 30,
			Prezzomancante = 14,
			Prezzononvalido = 19,
			Prezzounitariomancante = 5,
			Prodottomancante = 6,
			Prodottononinclusonellistinoprezzi = 11,
			Prodottononvalido = 7,
			Quantitamancante = 4,
			Quantitanonvalida = 29,
			Regoladiarrotondamentononvalida = 22,
			Scontononvalido = 28,
			Statononvalidotipodisconto = 27,
			Tipodiscontoinattivo = 33,
			Tipodiscontononvalido = 26,
			Underflowdellattributodellavalutadibase = 37,
			Unitadimisuramancante = 10,
			Unitadimisurapredefinitadelprodottomancante = 31,
			Unitadivenditadelprodottomancante = 32,
			Valorediarrotondamentononvalido = 24,
			Valutadellistinoprezzinonvalida = 34
		}

		/// <summary>
		/// Values for field Metodo di creazione
		/// <summary>
		public enum quotecreationmethodValues
		{
			Revisione = 776160001,
			Sconosciuto = 776160000
		}

		/// <summary>
		/// Values for field Richiesta fattura
		/// <summary>
		public enum res_isinvoicerequestedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Motivo stato precedente
		/// <summary>
		public enum res_oldstatuscodeValues
		{
			Acquisita = 4,
			Aggiornata = 7,
			Approvata = 3,
			Bozza = 1,
			Inapprovazione = 2,
			Nonapprovata = 5,
			Persa = 6
		}

		/// <summary>
		/// Values for field Metodo di spedizione
		/// <summary>
		public enum shippingmethodcodeValues
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
		/// Values for field Condizioni di spedizione per indirizzo spedizione
		/// <summary>
		public enum shipto_freighttermscodeValues
		{
			Valorepredefinito = 1
		}

		/// <summary>
		/// Values for field Ignora calcolo prezzo
		/// <summary>
		public enum skippricecalculationValues
		{
			DoPriceCalcAlways = 0,
			SkipPriceCalcOnRetrieve = 1
		}

		/// <summary>
		/// Values for field Spedizione
		/// <summary>
		public enum willcallValues
		{
			Rtirodacliente = 1,
			Spedizionepressocliente = 0
		}
	};
}
