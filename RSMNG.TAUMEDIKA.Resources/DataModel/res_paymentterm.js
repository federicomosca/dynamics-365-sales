

RSMNG.TAUMEDIKA.DataModel.res_paymentterm = {
	///Condizione di pagamento constants.
	logicalName: "res_paymentterm",
	displayName: "Condizione di pagamento",
	///ID organizzazione
	organizationid: "organizationid",
	///Abilita visibilità banca
	res_isbankvisible: "res_isbankvisible",
	///Nome
	res_name: "res_name",
	///Condizione di pagamento
	res_paymenttermid: "res_paymenttermid",

	/// Values for field Stato
	statecodeValues: {
		Attivo: 0,
		Inattivo: 1
	},

	/// Values for field Motivo stato
	statuscodeValues: {
		Attivo_StateAttivo: 1,
		Inattivo_StateInattivo: 2
	},

	/// Values for field Abilita visibilità banca
	res_isbankvisibleValues: {
		No: 0,
		Si: 1
	}
};
