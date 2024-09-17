

RSMNG.TAUMEDIKA.DataModel.res_document = {
	///Documento constants.
	logicalName: "res_document",
	displayName: "Documento",
	///Tasso di cambio
	exchangerate: "exchangerate",
	///Agente
	res_agent: "res_agent",
	///Provvigione Agente
	res_agentcommissionid: "res_agentcommissionid",
	///Coordinata Bancaria
	res_bankdetailsid: "res_bankdetailsid",
	///Provvigione  Calcolata
	res_calculatedcommission: "res_calculatedcommission",
	///Provvigione  Calcolata (base)
	res_calculatedcommission_base: "res_calculatedcommission_base",
	///Cliente
	res_customerid: "res_customerid",
	///Cod. Cliente
	res_customernumber: "res_customernumber",
	///Data
	res_date: "res_date",
	///Documento
	res_documentid: "res_documentid",
	///N. Documento
	res_documentnumber: "res_documentnumber",
	///Totale documento
	res_documenttotal: "res_documenttotal",
	///Totale documento (base)
	res_documenttotal_base: "res_documenttotal_base",
	///Tipo Documento
	res_documenttypecode: "res_documenttypecode",
	///Escluso da calcolo provvigioni
	res_isexcludedfromcalculation: "res_isexcludedfromcalculation",
	///Ancora da saldare
	res_ispendingpayment: "res_ispendingpayment",
	///Data ultimo pagamento
	res_lastpaymentdate: "res_lastpaymentdate",
	///Tot. Netto IVA
	res_nettotalexcludingvat: "res_nettotalexcludingvat",
	///Tot. Netto IVA (base)
	res_nettotalexcludingvat_base: "res_nettotalexcludingvat_base",
	///Nome
	res_nome: "res_nome",
	///Note
	res_note: "res_note",
	///Condizione di pagamento
	res_paymenttermid: "res_paymenttermid",
	///IVA
	res_vat: "res_vat",
	///IVA (base)
	res_vat_base: "res_vat_base",
	///Valuta
	transactioncurrencyid: "transactioncurrencyid",

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

	/// Values for field Tipo Documento
	res_documenttypecodeValues: {
		Fattura: 100000000,
		RicevutaFiscale: 100000001
	},

	/// Values for field Escluso da calcolo provvigioni
	res_isexcludedfromcalculationValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Ancora da saldare
	res_ispendingpaymentValues: {
		No: 0,
		Si: 1
	}
};
