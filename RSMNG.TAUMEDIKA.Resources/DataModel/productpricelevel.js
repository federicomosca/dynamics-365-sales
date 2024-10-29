

RSMNG.TAUMEDIKA.DataModel.productpricelevel = {
	///Voce di listino constants.
	logicalName: "productpricelevel",
	displayName: "Voce di listino",
	///Importo
	amount: "amount",
	///Importo (Base)
	amount_base: "amount_base",
	///Elenco sconti
	discounttypeid: "discounttypeid",
	///Tasso di cambio
	exchangerate: "exchangerate",
	///Organizzazione
	organizationid: "organizationid",
	///Percentuale
	percentage: "percentage",
	///Listino prezzi
	pricelevelid: "pricelevelid",
	///Metodo di determinazione dei prezzi
	pricingmethodcode: "pricingmethodcode",
	///Process Id
	processid: "processid",
	///Prodotto
	productid: "productid",
	///ID prodotto
	productnumber: "productnumber",
	///Listino prezzi prodotto
	productpricelevelid: "productpricelevelid",
	///Quantità minima di vendita
	quantitysellingcode: "quantitysellingcode",
	///Origine
	res_origine: "res_origine",
	///Valore di arrotondamento
	roundingoptionamount: "roundingoptionamount",
	///Valore di arrotondamento (Base)
	roundingoptionamount_base: "roundingoptionamount_base",
	///Opzione di arrotondamento
	roundingoptioncode: "roundingoptioncode",
	///Regola di arrotondamento
	roundingpolicycode: "roundingpolicycode",
	///(Deprecated) Stage Id
	stageid: "stageid",
	///Valuta
	transactioncurrencyid: "transactioncurrencyid",
	///(Deprecated) Traversed Path
	traversedpath: "traversedpath",
	///Unità
	uomid: "uomid",
	///ID unità di vendita
	uomscheduleid: "uomscheduleid",

	/// Values for field Metodo di determinazione dei prezzi
	pricingmethodcodeValues: {
		Costocorrenteconmargine: 4,
		Costocorrenteconricaricomarkup: 3,
		Costomedioconmargine: 6,
		Costomedioconricaricomarkup: 5,
		Importoforfettario: 1,
		prezzodilistino: 2
	},

	/// Values for field Quantità minima di vendita
	quantitysellingcodeValues: {
		Intera: 2,
		Interaefrazionaria: 3,
		Nondefinita: 1
	},

	/// Values for field Origine
	res_origineValues: {
		Dynamics: 100000000,
		ERP: 100000001
	},

	/// Values for field Opzione di arrotondamento
	roundingoptioncodeValues: {
		Multiplodi: 2,
		Terminacon: 1
	},

	/// Values for field Regola di arrotondamento
	roundingpolicycodeValues: {
		Alvalorepiuvicino: 4,
		Nessuno: 1,
		Perdifetto: 3,
		Pereccesso: 2
	}
};
