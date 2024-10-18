

RSMNG.TAUMEDIKA.DataModel.res_agentcommission = {
	///Provvigione Agente constants.
	logicalName: "res_agentcommission",
	displayName: "Provvigione Agente",
	///Tasso di cambio
	exchangerate: "exchangerate",
	///Rettifica
	res_adjustment: "res_adjustment",
	///Rettifica (base)
	res_adjustment_base: "res_adjustment_base",
	///Provvigione Agente
	res_agentcommissionid: "res_agentcommissionid",
	///Agente
	res_agentid: "res_agentid",
	///Provvigione calcolata
	res_calculatedcommission: "res_calculatedcommission",
	///Provvigione calcolata (base)
	res_calculatedcommission_base: "res_calculatedcommission_base",
	///Provvigione
	res_commissionid: "res_commissionid",
	///Totale Provvigione
	res_commissiontotalamount: "res_commissiontotalamount",
	///Totale Provvigione (base)
	res_commissiontotalamount_base: "res_commissiontotalamount_base",
	///Nome
	res_name: "res_name",
	///Nota
	res_note: "res_note",
	///Totale venduto
	res_soldtotalamount: "res_soldtotalamount",
	///Totale venduto (base)
	res_soldtotalamount_base: "res_soldtotalamount_base",
	///Valuta
	transactioncurrencyid: "transactioncurrencyid",

	/// Values for field Stato
	statecodeValues: {
		Attivo: 0,
		Inattivo: 1
	},

	/// Values for field Motivo stato
	statuscodeValues: {
		Bozza_StateAttivo: 1,
		Calcolato_StateAttivo: 100000002,
		Calcolatoerrori_StateAttivo: 100000003,
		Calcoloincorso_StateAttivo: 100000001,
		Inattivo_StateInattivo: 2
	}
};
