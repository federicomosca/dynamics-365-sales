

RSMNG.TAUMEDIKA.DataModel.product = {
	///Prodotto constants.
	logicalName: "product",
	displayName: "Prodotto",
	///Created By (External Party)
	createdbyexternalparty: "createdbyexternalparty",
	///Costo corrente
	currentcost: "currentcost",
	///Costo corrente (Base)
	currentcost_base: "currentcost_base",
	///Unità predefinita
	defaultuomid: "defaultuomid",
	///Unità di vendita
	defaultuomscheduleid: "defaultuomscheduleid",
	///Descrizione
	description: "description",
	///Solo per uso interno
	dmtimportstate: "dmtimportstate",
	///Immagine entità
	entityimage: "entityimage",
	///Tasso di cambio
	exchangerate: "exchangerate",
	///Percorso gerarchia
	hierarchypath: "hierarchypath",
	///Kit
	iskit: "iskit",
	///Associato con nuovo elemento padre
	isreparented: "isreparented",
	///Disponibile in magazzino
	isstockitem: "isstockitem",
	///Modified By (External Party)
	modifiedbyexternalparty: "modifiedbyexternalparty",
	///Rifiuto esplicito RGPD
	msdyn_gdproptout: "msdyn_gdproptout",
	///Nome
	name: "name",
	///Organization Id
	organizationid: "organizationid",
	///Padre
	parentproductid: "parentproductid",
	///Prezzo di listino
	price: "price",
	///Prezzo di listino (Base)
	price_base: "price_base",
	///Listino prezzi predefinito
	pricelevelid: "pricelevelid",
	///Process Id
	processid: "processid",
	///Prodotto
	productid: "productid",
	///Codice
	productnumber: "productnumber",
	///Struttura prodotto
	productstructure: "productstructure",
	///Tipo di prodotto
	producttypecode: "producttypecode",
	///URL
	producturl: "producturl",
	///Decimali supportati
	quantitydecimal: "quantitydecimal",
	///Disponibilità
	quantityonhand: "quantityonhand",
	///Codice a barre
	res_barcode: "res_barcode",
	///Peso lordo
	res_grossweight: "res_grossweight",
	///Produttore
	res_manufacturer: "res_manufacturer",
	///Origine
	res_origincode: "res_origincode",
	///Categoria Principale
	res_parentcategoryid: "res_parentcategoryid",
	///Unità di misura (peso)
	res_uomweightid: "res_uomweightid",
	///Codice IVA
	res_vatnumberid: "res_vatnumberid",
	///Dimensioni
	size: "size",
	///(Deprecated) Stage Id
	stageid: "stageid",
	///Costo medio
	standardcost: "standardcost",
	///Costo medio (Base)
	standardcost_base: "standardcost_base",
	///Volume (cm3)
	stockvolume: "stockvolume",
	///Peso netto
	stockweight: "stockweight",
	///Argomento
	subjectid: "subjectid",
	///Fornitore
	suppliername: "suppliername",
	///Valuta
	transactioncurrencyid: "transactioncurrencyid",
	///(Deprecated) Traversed Path
	traversedpath: "traversedpath",
	///Valido da
	validfromdate: "validfromdate",
	///Valido fino a
	validtodate: "validtodate",
	///ID fornitore
	vendorid: "vendorid",
	///Fornitore
	vendorname: "vendorname",
	///Nome fornitore
	vendorpartnumber: "vendorpartnumber",

	/// Values for field Stato
	statecodeValues: {
		Attivo: 0,
		Bozza: 2,
		Inaggiornamento: 3,
		Ritirato: 1
	},

	/// Values for field Motivo stato
	statuscodeValues: {
		Attivo_StateAttivo: 1,
		Bozza_StateBozza: 0,
		Inaggiornamento_StateInaggiornamento: 3,
		Ritirato_StateRitirato: 2
	},

	/// Values for field Kit
	iskitValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Associato con nuovo elemento padre
	isreparentedValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Disponibile in magazzino
	isstockitemValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Rifiuto esplicito RGPD
	msdyn_gdproptoutValues: {
		No: 0,
		Si: 1
	},

	/// Values for field Struttura prodotto
	productstructureValues: {
		Aggregazioneprodotti: 3,
		Famigliadiprodotti: 2,
		Prodotto: 1
	},

	/// Values for field Tipo di prodotto
	producttypecodeValues: {
		Articolo: 3,
		Articoloconmagazzinolotti: 8,
		Articoloconmagazzinolottiscadenze: 9,
		Articoloconmagazzinoseriali: 100000001,
		Articoloinmagazzino: 7,
		Servizio: 100000002
	},

	/// Values for field Origine
	res_origincodeValues: {
		Dynamics: 100000000,
		ERP: 100000001
	}
};
