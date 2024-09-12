

RSMNG.TAUMEDIKA.DataModel.pricelevel = {
	///Listino prezzi constants.
	logicalName: "pricelevel",
	displayName: "Listino prezzi",
	///Data di inizio
	begindate: "begindate",
	///Descrizione
	description: "description",
	///Data di fine
	enddate: "enddate",
	///Tasso di cambio
	exchangerate: "exchangerate",
	///Condizioni di spedizione
	freighttermscode: "freighttermscode",
	///Nome
	name: "name",
	///Organization Id
	organizationid: "organizationid",
	///Modalità di pagamento 
	paymentmethodcode: "paymentmethodcode",
	///Listino prezzi
	pricelevelid: "pricelevelid",
	///Default per agenti
	res_isdefaultforagents: "res_isdefaultforagents",
	///Default per sito web
	res_isdefaultforwebsite: "res_isdefaultforwebsite",
	///Import ERP
	res_iserpimport: "res_iserpimport",
	///Ambito
	res_scopetypecodes: "res_scopetypecodes",
	///Metodo di spedizione
	shippingmethodcode: "shippingmethodcode",
	///Valuta
	transactioncurrencyid: "transactioncurrencyid",

	/// Values for field Stato 
	statecodeValues: {
		Attivo: 0,
		Inattivo: 1
	},

	/// Values for field Motivo stato
	statuscodeValues: {
		Attivo_StateAttivo: 100001,
		Inattivo_StateInattivo: 100002
	},

	/// Values for field Condizioni di spedizione
	freighttermscodeValues: {
		Valorepredefinito: 1
	},

	/// Values for field Modalità di pagamento 
	paymentmethodcodeValues: {
		Valorepredefinito: 1
	},

	/// Values for field Default per agenti
	res_isdefaultforagentsValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Default per sito web
	res_isdefaultforwebsiteValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Import ERP
	res_iserpimportValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Ambito
	res_scopetypecodesValues: {
		Agenti: 100000000,
		SitoWeb: 100000001
	},

	/// Values for field Metodo di spedizione
	shippingmethodcodeValues: {
		Valorepredefinito: 1
	}
};
