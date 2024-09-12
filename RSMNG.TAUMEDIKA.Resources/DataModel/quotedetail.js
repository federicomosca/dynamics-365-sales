

RSMNG.TAUMEDIKA.DataModel.quotedetail = {
	///Riga offerta constants.
	logicalName: "quotedetail",
	displayName: "Riga offerta",
	///Importo
	baseamount: "baseamount",
	///Importo (Base)
	baseamount_base: "baseamount_base",
	///Descrizione
	description: "description",
	///Tasso di cambio
	exchangerate: "exchangerate",
	///Importo totale
	extendedamount: "extendedamount",
	///Importo totale (Base)
	extendedamount_base: "extendedamount_base",
	///Prezzi
	ispriceoverridden: "ispriceoverridden",
	///Seleziona prodotto
	isproductoverridden: "isproductoverridden",
	///Numero voce
	lineitemnumber: "lineitemnumber",
	///Sconto totale
	manualdiscountamount: "manualdiscountamount",
	///Sconto manuale (Base)
	manualdiscountamount_base: "manualdiscountamount_base",
	///Aggregazione padre
	parentbundleid: "parentbundleid",
	///Prodotto di aggregazione prodotti
	parentbundleidref: "parentbundleidref",
	///Prezzo unitario
	priceperunit: "priceperunit",
	///Prezzo unitario (Base)
	priceperunit_base: "priceperunit_base",
	///Errore di determinazione dei prezzi 
	pricingerrorcode: "pricingerrorcode",
	///Associazione elemento aggregazione
	productassociationid: "productassociationid",
	///Prodotto fuori catalogo
	productdescription: "productdescription",
	///Prodotto esistente
	productid: "productid",
	///Nome prodotto
	productname: "productname",
	///Numero prodotto
	productnumber: "productnumber",
	///Tipo di prodotto
	producttypecode: "producttypecode",
	///Configurazione proprietà
	propertyconfigurationstatus: "propertyconfigurationstatus",
	///Quantità
	quantity: "quantity",
	///Metodo di creazione
	quotecreationmethod: "quotecreationmethod",
	///Prodotto offerta
	quotedetailid: "quotedetailid",
	///Nome
	quotedetailname: "quotedetailname",
	///Offerta
	quoteid: "quoteid",
	///Stato offerta
	quotestatecode: "quotestatecode",
	///Data consegna richiesta
	requestdeliveryby: "requestdeliveryby",
	///Sconto % 1
	res_discountpercent1: "res_discountpercent1",
	///Sconto % 2
	res_discountpercent2: "res_discountpercent2",
	///Sconto % 3
	res_discountpercent3: "res_discountpercent3",
	///Omaggio
	res_ishomage: "res_ishomage",
	///Codice Articolo
	res_itemcode: "res_itemcode",
	///Totale imponibile
	res_taxableamount: "res_taxableamount",
	///Totale imponibile (base)
	res_taxableamount_base: "res_taxableamount_base",
	///Codice IVA
	res_vatnumberid: "res_vatnumberid",
	///Aliquota IVA
	res_vatrate: "res_vatrate",
	///Venditore
	salesrepid: "salesrepid",
	///Numero sequenza
	sequencenumber: "sequencenumber",
	///ID indirizzo di spedizione
	shipto_addressid: "shipto_addressid",
	///Città spedizione
	shipto_city: "shipto_city",
	///Nome contatto spedizione
	shipto_contactname: "shipto_contactname",
	///Paese spedizione
	shipto_country: "shipto_country",
	///Fax spedizione
	shipto_fax: "shipto_fax",
	///Condizioni di spedizione
	shipto_freighttermscode: "shipto_freighttermscode",
	///Via 1 spedizione
	shipto_line1: "shipto_line1",
	///Via 2 spedizione
	shipto_line2: "shipto_line2",
	///Via 3 spedizione
	shipto_line3: "shipto_line3",
	///Nome spedizione
	shipto_name: "shipto_name",
	///CAP spedizione
	shipto_postalcode: "shipto_postalcode",
	///Provincia spedizione
	shipto_stateorprovince: "shipto_stateorprovince",
	///Telefono spedizione
	shipto_telephone: "shipto_telephone",
	///Ignora calcolo prezzo
	skippricecalculation: "skippricecalculation",
	///Totale IVA
	tax: "tax",
	///Imposte (Base)
	tax_base: "tax_base",
	///Valuta
	transactioncurrencyid: "transactioncurrencyid",
	///Unità
	uomid: "uomid",
	///Sconto per volume
	volumediscountamount: "volumediscountamount",
	///Sconto per volume (Base)
	volumediscountamount_base: "volumediscountamount_base",
	///Spedizione
	willcall: "willcall",

	/// Values for field Prezzi
	ispriceoverriddenValues: {
		Sostituisciprezzo: 1,
		Usapredefinito: 0
	},

	/// Values for field Seleziona prodotto
	isproductoverriddenValues: {
		Esistente: 0,
		Fuoricatalogo: 1
	},

	/// Values for field Errore di determinazione dei prezzi 
	pricingerrorcodeValues: {
		Attributodelprezzofuoriintervallo: 35,
		Codicedideterminazionedeiprezzimancante: 8,
		Codicedideterminazionedeiprezzinonvalido: 9,
		Costocorrentemancante: 15,
		Costocorrentenonvalido: 20,
		Costomediomancante: 16,
		Costomediononvalido: 21,
		Dettaglierrore: 1,
		Erroredicalcolodelprezzo: 25,
		Importodellistinoprezzimancante: 12,
		Importodellistinoprezzinonvalido: 17,
		Lavalutadellatransazionenoneimpostataperlavocedilistinodelprodotto: 38,
		Listinoprezziinattivo: 3,
		Listinoprezzimancante: 2,
		Nessuno: 0,
		Opzionediarrotondamentononvalida: 23,
		Overflowdellattributodellavalutadibase: 36,
		Percentualedellistinoprezzimancante: 13,
		Percentualedellistinoprezzinonvalida: 18,
		Precisionedideterminazionedeiprezzinonvalida: 30,
		Prezzomancante: 14,
		Prezzononvalido: 19,
		Prezzounitariomancante: 5,
		Prodottomancante: 6,
		Prodottononinclusonellistinoprezzi: 11,
		Prodottononvalido: 7,
		Quantitamancante: 4,
		Quantitanonvalida: 29,
		Regoladiarrotondamentononvalida: 22,
		Scontononvalido: 28,
		Statononvalidotipodisconto: 27,
		Tipodiscontoinattivo: 33,
		Tipodiscontononvalido: 26,
		Underflowdellattributodellavalutadibase: 37,
		Unitadimisuramancante: 10,
		Unitadimisurapredefinitadelprodottomancante: 31,
		Unitadivenditadelprodottomancante: 32,
		Valorediarrotondamentononvalido: 24,
		Valutadellistinoprezzinonvalida: 34
	},

	/// Values for field Tipo di prodotto
	producttypecodeValues: {
		Aggregazione: 2,
		Prodotto: 1,
		Prodottoaggregazionefacoltativo: 4,
		Prodottoaggregazioneobbligatorio: 3,
		Serviziobasatosulprogetto: 5
	},

	/// Values for field Configurazione proprietà
	propertyconfigurationstatusValues: {
		Modifica: 0,
		Nonconfigurato: 2,
		Rettifica: 1
	},

	/// Values for field Metodo di creazione
	quotecreationmethodValues: {
		Revisione: 776160001,
		Sconosciuto: 776160000
	},

	/// Values for field Omaggio
	res_ishomageValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Condizioni di spedizione
	shipto_freighttermscodeValues: {
		CFRCostoenolo: 3,
		CIFCostoassenolo: 4,
		CIPTraspeasspagatifinoa: 5,
		CPTTrasportopagatofinoa: 6,
		DAFResofrontiera: 7,
		DDPResosdoganato: 10,
		DDUResononsdoganato: 11,
		DEQResobanchina: 8,
		DESResoexship: 9,
		EXWFrancofabbrica: 12,
		FASFrancolungobordo: 13,
		FCAFrancovettore: 14,
		FOBFrancoabordo: 1,
		Gratis: 2
	},

	/// Values for field Ignora calcolo prezzo
	skippricecalculationValues: {
		DoPriceCalcAlways: 0,
		SkipPriceCalcOnCreate: 1,
		SkipPriceCalcOnUpdate: 2,
		SkipPriceCalcOnUpSert: 3
	},

	/// Values for field Spedizione
	willcallValues: {
		Indirizzo: 0,
		Ritiroacaricodelcliente: 1
	}
};
