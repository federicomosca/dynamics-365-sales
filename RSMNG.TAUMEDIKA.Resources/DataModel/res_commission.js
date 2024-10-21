

RSMNG.TAUMEDIKA.DataModel.res_commission = {
	///Provvigione constants.
	logicalName: "res_commission",
	displayName: "Provvigione",
	///Dettaglio calcolo provvigioni
	res_commissioncalculationdetail: "res_commissioncalculationdetail",
	///Provvigione
	res_commissionid: "res_commissionid",
	///Data fine
	res_enddate: "res_enddate",
	///Nome
	res_name: "res_name",
	///Data inizio
	res_startdate: "res_startdate",

	/// Values for field Stato
	statecodeValues: {
		Attivo: 0,
		Inattivo: 1
	},

	/// Values for field Motivo stato
	statuscodeValues: {
		Bozza_StateAttivo: 1,
		Calcolata_StateAttivo: 100000002,
		Calcolataerrori_StateAttivo: 100000003,
		Calcoloincorso_StateAttivo: 100000001,
		Inattivo_StateInattivo: 2
	}
};
