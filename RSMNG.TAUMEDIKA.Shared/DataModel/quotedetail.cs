namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Riga offerta constants.
	/// </summary>
	public sealed class quotedetail : EntityGenericConstants
	{
		/// <summary>
		/// quotedetail
		/// </summary>
		public static string logicalName => "quotedetail";

		/// <summary>
		/// Riga offerta
		/// </summary>
		public static string displayName => "Riga offerta";

		/// <summary>
		/// Display Name: Importo,
		/// Type: Money,
		/// Description: Mostra il prezzo totale del prodotto dell'offerta, in base al prezzo unitario, allo sconto per volume e alla quantità.
		/// </summary>
		public static string baseamount => "baseamount";

		/// <summary>
		/// Display Name: Importo (Base),
		/// Type: Money,
		/// Description: Value of the Importo in base currency.
		/// </summary>
		public static string baseamount_base => "baseamount_base";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Digitare informazioni aggiuntive per descrivere il prodotto dell'offerta, ad esempio dettagli di produzione o sostituzioni accettabili.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Mostra il tasso di conversione della valuta del record. Il tasso di cambio è usato per convertire tutti i campi di tipo money nel record dalla valuta locale alla valuta predefinita del sistema.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Importo totale,
		/// Type: Money,
		/// Description: Mostra l'importo totale dovuto per il prodotto dell'offerta in base alla somma del prezzo unitario, della quantità, degli sconti e delle imposte.
		/// </summary>
		public static string extendedamount => "extendedamount";

		/// <summary>
		/// Display Name: Importo totale (Base),
		/// Type: Money,
		/// Description: Value of the Importo totale in base currency.
		/// </summary>
		public static string extendedamount_base => "extendedamount_base";

		/// <summary>
		/// Display Name: Prezzi,
		/// Type: Boolean,
		/// Values:
		/// Sostituisci prezzo: 1,
		/// Usa predefinito: 0,
		/// Description: Specificare se il prezzo unitario è fissato sul valore del listino prezzi indicato o se può essere sostituito dagli utenti con diritti di modifica sui prodotti dell'offerta.
		/// </summary>
		public static string ispriceoverridden => "ispriceoverridden";

		/// <summary>
		/// Display Name: Seleziona prodotto,
		/// Type: Boolean,
		/// Values:
		/// Fuori catalogo: 1,
		/// Esistente: 0,
		/// Description: Specifica se il prodotto esiste nel catalogo prodotti di Microsoft Dynamics 365 o se è un prodotto fuori catalogo specifico dell'offerta padre.
		/// </summary>
		public static string isproductoverridden => "isproductoverridden";

		/// <summary>
		/// Display Name: Numero voce,
		/// Type: Integer,
		/// Description: Digitare il numero della voce del prodotto dell'offerta per identificare agevolmente il prodotto e garantire che sia elencato nell'ordine corretto.
		/// </summary>
		public static string lineitemnumber => "lineitemnumber";

		/// <summary>
		/// Display Name: Sconto totale,
		/// Type: Money,
		/// Description: Digitare l'importo dello sconto manuale per il prodotto dell'offerta per dedurre qualsiasi risparmio negoziato o diverso dal totale relativo ai prodotti inclusi nell'offerta.
		/// </summary>
		public static string manualdiscountamount => "manualdiscountamount";

		/// <summary>
		/// Display Name: Sconto manuale (Base),
		/// Type: Money,
		/// Description: Value of the Sconto manuale in base currency.
		/// </summary>
		public static string manualdiscountamount_base => "manualdiscountamount_base";

		/// <summary>
		/// Display Name: Aggregazione padre,
		/// Type: Uniqueidentifier,
		/// Description: Scegli l'aggregazione padre associata a questo prodotto
		/// </summary>
		public static string parentbundleid => "parentbundleid";

		/// <summary>
		/// Display Name: Prodotto di aggregazione prodotti,
		/// Type: Lookup,
		/// Related entities: quotedetail,
		/// Description: Scegli l'aggregazione padre associata a questo prodotto
		/// </summary>
		public static string parentbundleidref => "parentbundleidref";

		/// <summary>
		/// Display Name: Prezzo unitario,
		/// Type: Money,
		/// Description: Digitare il prezzo unitario del prodotto dell'offerta. Il valore predefinito è quello del listino prezzi specificato nell'offerta per i prodotti esistenti.
		/// </summary>
		public static string priceperunit => "priceperunit";

		/// <summary>
		/// Display Name: Prezzo unitario (Base),
		/// Type: Money,
		/// Description: Value of the Prezzo unitario in base currency.
		/// </summary>
		public static string priceperunit_base => "priceperunit_base";

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
		/// Description: Selezionare il tipo di errore di determinazione dei prezzi, ad esempio un prodotto non valido o mancante o una quantità mancante.
		/// </summary>
		public static string pricingerrorcode => "pricingerrorcode";

		/// <summary>
		/// Display Name: Associazione elemento aggregazione,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco dell'associazione della voce prodotto all'aggregazione nell'offerta
		/// </summary>
		public static string productassociationid => "productassociationid";

		/// <summary>
		/// Display Name: Prodotto fuori catalogo,
		/// Type: String,
		/// Description: Digita un nome o una descrizione per identificare il tipo di prodotto fuori catalogo incluso nell'offerta.
		/// </summary>
		public static string productdescription => "productdescription";

		/// <summary>
		/// Display Name: Prodotto esistente,
		/// Type: Lookup,
		/// Related entities: product,
		/// Description: Scegli il prodotto da includere nell'offerta per collegarne prezzo e altre informazioni all'offerta stessa.
		/// </summary>
		public static string productid => "productid";

		/// <summary>
		/// Display Name: Nome prodotto,
		/// Type: String,
		/// Description: Campo calcolato che verrà popolato dal nome e dalla descrizione del prodotto.
		/// </summary>
		public static string productname => "productname";

		/// <summary>
		/// Display Name: Numero prodotto,
		/// Type: String,
		/// Description: ID prodotto definito dall'utente.
		/// </summary>
		public static string productnumber => "productnumber";

		/// <summary>
		/// Display Name: Tipo di prodotto,
		/// Type: Picklist,
		/// Values:
		/// Prodotto: 1,
		/// Aggregazione: 2,
		/// Prodotto aggregazione obbligatorio: 3,
		/// Prodotto aggregazione facoltativo: 4,
		/// Servizio basato sul progetto: 5,
		/// Description: Tipo di prodotto
		/// </summary>
		public static string producttypecode => "producttypecode";

		/// <summary>
		/// Display Name: Configurazione proprietà,
		/// Type: Picklist,
		/// Values:
		/// Modifica: 0,
		/// Rettifica: 1,
		/// Non configurato: 2,
		/// Description: Stato della configurazione della proprietà.
		/// </summary>
		public static string propertyconfigurationstatus => "propertyconfigurationstatus";

		/// <summary>
		/// Display Name: Quantità,
		/// Type: Decimal,
		/// Description: Digitare l'importo o la quantità del prodotto richiesto dal cliente.
		/// </summary>
		public static string quantity => "quantity";

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
		/// Display Name: Prodotto offerta,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco della voce dell'offerta relativa al prodotto.
		/// </summary>
		public static string quotedetailid => "quotedetailid";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Nome dettagli offerta. Aggiunto per relazione referenziale 1:n (solo per scopi interni)
		/// </summary>
		public static string quotedetailname => "quotedetailname";

		/// <summary>
		/// Display Name: Offerta,
		/// Type: Lookup,
		/// Related entities: quote,
		/// Description: Identificatore univoco dell'offerta per il prodotto dell'offerta.
		/// </summary>
		public static string quoteid => "quoteid";

		/// <summary>
		/// Display Name: Stato offerta,
		/// Type: Picklist,
		/// Values:
		/// Description: Stato del prodotto dell'offerta.
		/// </summary>
		public static string quotestatecode => "quotestatecode";

		/// <summary>
		/// Display Name: Data consegna richiesta,
		/// Type: DateTime,
		/// Description: Immettere la data di consegna richiesta dal cliente per il prodotto dell'offerta.
		/// </summary>
		public static string requestdeliveryby => "requestdeliveryby";

		/// <summary>
		/// Display Name: Sconto % 1,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_discountpercent1 => "res_discountpercent1";

		/// <summary>
		/// Display Name: Sconto % 2,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_discountpercent2 => "res_discountpercent2";

		/// <summary>
		/// Display Name: Sconto % 3,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_discountpercent3 => "res_discountpercent3";

		/// <summary>
		/// Display Name: Origine Canvas App,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_isfromcanvas => "res_isfromcanvas";

		/// <summary>
		/// Display Name: Omaggio,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string res_ishomage => "res_ishomage";

		/// <summary>
		/// Display Name: Codice Articolo,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_itemcode => "res_itemcode";

		/// <summary>
		/// Display Name: Totale imponibile,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_taxableamount => "res_taxableamount";

		/// <summary>
		/// Display Name: Totale imponibile (base),
		/// Type: Money,
		/// Description: Valore di Totale imponibile nella valuta di base.
		/// </summary>
		public static string res_taxableamount_base => "res_taxableamount_base";

		/// <summary>
		/// Display Name: Codice IVA,
		/// Type: Lookup,
		/// Related entities: res_vatnumber,
		/// Description: 
		/// </summary>
		public static string res_vatnumberid => "res_vatnumberid";

		/// <summary>
		/// Display Name: Aliquota IVA,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_vatrate => "res_vatrate";

		/// <summary>
		/// Display Name: Venditore,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: Scegli l'utente responsabile della vendita del prodotto dell'offerta.
		/// </summary>
		public static string salesrepid => "salesrepid";

		/// <summary>
		/// Display Name: Numero sequenza,
		/// Type: Integer,
		/// Description: Identificatore univoco dei dati relativi alla sequenza.
		/// </summary>
		public static string sequencenumber => "sequencenumber";

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
		/// Description: Seleziona le condizioni di spedizione per garantire che gli ordini di spedizione vengano elaborati correttamente.
		/// </summary>
		public static string shipto_freighttermscode => "shipto_freighttermscode";

		/// <summary>
		/// Display Name: Via 1 spedizione,
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
		/// Display Name: Provincia spedizione,
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
		/// SkipPriceCalcOnCreate: 1,
		/// SkipPriceCalcOnUpdate: 2,
		/// SkipPriceCalcOnUpSert: 3,
		/// Description: Ignora il prezzo
		/// </summary>
		public static string skippricecalculation => "skippricecalculation";

		/// <summary>
		/// Display Name: Totale IVA,
		/// Type: Money,
		/// Description: Digitare l'importo delle imposte per il prodotto dell'offerta.
		/// </summary>
		public static string tax => "tax";

		/// <summary>
		/// Display Name: Imposte (Base),
		/// Type: Money,
		/// Description: Value of the Imposte in base currency.
		/// </summary>
		public static string tax_base => "tax_base";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Scegli la valuta locale per il record per assicurarti che i budget vengano espressi nella valuta corretta.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";

		/// <summary>
		/// Display Name: Unità,
		/// Type: Lookup,
		/// Related entities: uom,
		/// Description: Scegli l'unità di misura per la quantità dell'unità di base per questo acquisto, ad esempio singoli articoli o dozzine.
		/// </summary>
		public static string uomid => "uomid";

		/// <summary>
		/// Display Name: Sconto per volume,
		/// Type: Money,
		/// Description: Mostra l'importo di sconto unitario se si acquista un volume specificato. Configurare gli sconti per volume in Catalogo prodotti nell'area Impostazioni.
		/// </summary>
		public static string volumediscountamount => "volumediscountamount";

		/// <summary>
		/// Display Name: Sconto per volume (Base),
		/// Type: Money,
		/// Description: Value of the Sconto per volume in base currency.
		/// </summary>
		public static string volumediscountamount_base => "volumediscountamount_base";

		/// <summary>
		/// Display Name: Spedizione,
		/// Type: Boolean,
		/// Values:
		/// Ritiro a carico del cliente: 1,
		/// Indirizzo: 0,
		/// Description: Specificare se il prodotto dell'offerta deve essere consegnato all'indirizzo indicato o trattenuto fino a quando il cliente non impartisce ulteriori istruzioni di ritiro o dl consegna.
		/// </summary>
		public static string willcall => "willcall";


		/// <summary>
		/// Values for field Prezzi
		/// <summary>
		public enum ispriceoverriddenValues
		{
			Sostituisciprezzo = 1,
			Usapredefinito = 0
		}

		/// <summary>
		/// Values for field Seleziona prodotto
		/// <summary>
		public enum isproductoverriddenValues
		{
			Esistente = 0,
			Fuoricatalogo = 1
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
		/// Values for field Tipo di prodotto
		/// <summary>
		public enum producttypecodeValues
		{
			Aggregazione = 2,
			Prodotto = 1,
			Prodottoaggregazionefacoltativo = 4,
			Prodottoaggregazioneobbligatorio = 3,
			Serviziobasatosulprogetto = 5
		}

		/// <summary>
		/// Values for field Configurazione proprietà
		/// <summary>
		public enum propertyconfigurationstatusValues
		{
			Modifica = 0,
			Nonconfigurato = 2,
			Rettifica = 1
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
		/// Values for field Origine Canvas App
		/// <summary>
		public enum res_isfromcanvasValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Omaggio
		/// <summary>
		public enum res_ishomageValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Condizioni di spedizione
		/// <summary>
		public enum shipto_freighttermscodeValues
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
		/// Values for field Ignora calcolo prezzo
		/// <summary>
		public enum skippricecalculationValues
		{
			DoPriceCalcAlways = 0,
			SkipPriceCalcOnCreate = 1,
			SkipPriceCalcOnUpdate = 2,
			SkipPriceCalcOnUpSert = 3
		}

		/// <summary>
		/// Values for field Spedizione
		/// <summary>
		public enum willcallValues
		{
			Indirizzo = 0,
			Ritiroacaricodelcliente = 1
		}
	};
}
