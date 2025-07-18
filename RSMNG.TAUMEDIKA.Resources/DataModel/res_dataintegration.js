

RSMNG.TAUMEDIKA.DataModel.res_dataintegration = {
	///Integrazione Dati constants.
	logicalName: "res_dataintegration",
	displayName: "Integrazione Dati",
	///DataIntegration
	res_dataintegrationid: "res_dataintegrationid",
	///File di Distribuzione
	res_distributionfile: "res_distributionfile",
	///Azione dell'Integrazione
	res_integrationaction: "res_integrationaction",
	///Dati dell'Integrazione
	res_integrationdata: "res_integrationdata",
	///File di Integrazione
	res_integrationfile: "res_integrationfile",
	///Risultato dell'Integrazione
	res_integrationresult: "res_integrationresult",
	///Numero di integrazioni
	res_integrationsnumber: "res_integrationsnumber",
	///Tipo di Integrazione
	res_integrationtype: "res_integrationtype",
	///Name
	res_name: "res_name",

	/// Values for field Stato
	statecodeValues: {
		Attivo: 0,
		Inattivo: 1
	},

	/// Values for field Motivo stato
	statuscodeValues: {
		Creato_StateAttivo: 1,
		Distribuito_StateInattivo: 2,
		Distribuitoparzialmente_StateInattivo: 100000004,
		InElaborazione_StateAttivo: 100000002,
		InValidazione_StateAttivo: 100000001,
		NonDistribuito_StateInattivo: 100000003
	},

	/// Values for field Azione dell'Integrazione
	res_integrationactionValues: {
		Articoli: 100000002,
		Clienti: 100000000,
		DocumentiFatture: 100000004,
		DocumentiRicevute: 100000003,
		Ordini: 100000001,
		Pagamenti: 100000005
	},

	/// Values for field Tipo di Integrazione
	res_integrationtypeValues: {
		Export: 100000001,
		Import: 100000000
	}
};
