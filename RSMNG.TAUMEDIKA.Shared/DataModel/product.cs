namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Prodotto constants.
	/// </summary>
	public sealed class product : EntityGenericConstants
	{
		/// <summary>
		/// product
		/// </summary>
		public static string logicalName => "product";

		/// <summary>
		/// Prodotto
		/// </summary>
		public static string displayName => "Prodotto";

		/// <summary>
		/// Display Name: Created By (External Party),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Shows the external party who created the record.
		/// </summary>
		public static string createdbyexternalparty => "createdbyexternalparty";

		/// <summary>
		/// Display Name: Costo corrente,
		/// Type: Money,
		/// Description: Costo corrente del prodotto utilizzato nel calcolo dei prezzi.
		/// </summary>
		public static string currentcost => "currentcost";

		/// <summary>
		/// Display Name: Costo corrente (Base),
		/// Type: Money,
		/// Description: Value of the Costo corrente in base currency.
		/// </summary>
		public static string currentcost_base => "currentcost_base";

		/// <summary>
		/// Display Name: Unità predefinita,
		/// Type: Lookup,
		/// Related entities: uom,
		/// Description: Unità predefinita per il prodotto.
		/// </summary>
		public static string defaultuomid => "defaultuomid";

		/// <summary>
		/// Display Name: Unità di vendita,
		/// Type: Lookup,
		/// Related entities: uomschedule,
		/// Description: Unità di vendita predefinita per il prodotto.
		/// </summary>
		public static string defaultuomscheduleid => "defaultuomscheduleid";

		/// <summary>
		/// Display Name: Descrizione,
		/// Type: Memo,
		/// Description: Descrizione del prodotto.
		/// </summary>
		public static string description => "description";

		/// <summary>
		/// Display Name: Solo per uso interno,
		/// Type: Integer,
		/// Description: Solo per uso interno
		/// </summary>
		public static string dmtimportstate => "dmtimportstate";

		/// <summary>
		/// Display Name: Immagine entità,
		/// Type: Virtual,
		/// Description: Mostra l'immagine predefinita del record.
		/// </summary>
		public static string entityimage => "entityimage";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Tasso di cambio per la valuta associata al prodotto rispetto alla valuta di base.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Percorso gerarchia,
		/// Type: String,
		/// Description: Percorso gerarchia del prodotto.
		/// </summary>
		public static string hierarchypath => "hierarchypath";

		/// <summary>
		/// Display Name: Kit,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Specifica se il prodotto è un kit.
		/// </summary>
		public static string iskit => "iskit";

		/// <summary>
		/// Display Name: Associato con nuovo elemento padre,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: 
		/// </summary>
		public static string isreparented => "isreparented";

		/// <summary>
		/// Display Name: Disponibile in magazzino,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Specifica se il prodotto è disponibile in magazzino.
		/// </summary>
		public static string isstockitem => "isstockitem";

		/// <summary>
		/// Display Name: Modified By (External Party),
		/// Type: Lookup,
		/// Related entities: externalparty,
		/// Description: Shows the external party who modified the record.
		/// </summary>
		public static string modifiedbyexternalparty => "modifiedbyexternalparty";

		/// <summary>
		/// Display Name: Rifiuto esplicito RGPD,
		/// Type: Boolean,
		/// Values:
		/// Sì: 1,
		/// No: 0,
		/// Description: Indica se il prodotto è stato rifiutato esplicitamente o meno
		/// </summary>
		public static string msdyn_gdproptout => "msdyn_gdproptout";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: Nome del prodotto.
		/// </summary>
		public static string name => "name";

		/// <summary>
		/// Display Name: Organization Id,
		/// Type: Lookup,
		/// Related entities: organization,
		/// Description: Unique identifier for the organization
		/// </summary>
		public static string organizationid => "organizationid";

		/// <summary>
		/// Display Name: Padre,
		/// Type: Lookup,
		/// Related entities: product,
		/// Description: Specifica la gerarchia della famiglia di prodotti padre.
		/// </summary>
		public static string parentproductid => "parentproductid";

		/// <summary>
		/// Display Name: Prezzo di listino,
		/// Type: Money,
		/// Description: Prezzo di listino della voce di prodotto. Utilizzato nel calcolo dei prezzi.
		/// </summary>
		public static string price => "price";

		/// <summary>
		/// Display Name: Prezzo di listino (Base),
		/// Type: Money,
		/// Description: Value of the Prezzo di listino in base currency.
		/// </summary>
		public static string price_base => "price_base";

		/// <summary>
		/// Display Name: Listino prezzi predefinito,
		/// Type: Lookup,
		/// Related entities: pricelevel,
		/// Description: Seleziona il listino prezzi predefinito per il prodotto.
		/// </summary>
		public static string pricelevelid => "pricelevelid";

		/// <summary>
		/// Display Name: Process Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the process associated with the entity.
		/// </summary>
		public static string processid => "processid";

		/// <summary>
		/// Display Name: Prodotto,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco del prodotto.
		/// </summary>
		public static string productid => "productid";

		/// <summary>
		/// Display Name: Codice,
		/// Type: String,
		/// Description: ID prodotto definito dall'utente.
		/// </summary>
		public static string productnumber => "productnumber";

		/// <summary>
		/// Display Name: Struttura prodotto,
		/// Type: Picklist,
		/// Values:
		/// Prodotto: 1,
		/// Famiglia di prodotti: 2,
		/// Aggregazione prodotti: 3,
		/// Description: Struttura prodotto.
		/// </summary>
		public static string productstructure => "productstructure";

		/// <summary>
		/// Display Name: Tipo di prodotto,
		/// Type: Picklist,
		/// Values:
		/// Articolo: 3,
		/// Articolo in magazzino: 7,
		/// Articolo con magazzino (lotti): 8,
		/// Articolo con magazzino (lotti) scadenze: 9,
		/// Articolo con magazzino (seriali): 100000001,
		/// Servizio: 100000002,
		/// Description: Tipo di prodotto.
		/// </summary>
		public static string producttypecode => "producttypecode";

		/// <summary>
		/// Display Name: URL,
		/// Type: String,
		/// Description: URL del sito Web associato al prodotto.
		/// </summary>
		public static string producturl => "producturl";

		/// <summary>
		/// Display Name: Decimali supportati,
		/// Type: Integer,
		/// Description: Numero di posizioni decimali che è possibile utilizzare negli importi per il prodotto.
		/// </summary>
		public static string quantitydecimal => "quantitydecimal";

		/// <summary>
		/// Display Name: Disponibilità,
		/// Type: Decimal,
		/// Description: Quantità di prodotto in magazzino.
		/// </summary>
		public static string quantityonhand => "quantityonhand";

		/// <summary>
		/// Display Name: Codice a barre,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_barcode => "res_barcode";

		/// <summary>
		/// Display Name: Peso lordo,
		/// Type: Decimal,
		/// Description: 
		/// </summary>
		public static string res_grossweight => "res_grossweight";

		/// <summary>
		/// Display Name: Produttore,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_manufacturer => "res_manufacturer";

		/// <summary>
		/// Display Name: Origine,
		/// Type: Picklist,
		/// Values:
		/// Dynamics: 100000000,
		/// ERP: 100000001,
		/// Description: 
		/// </summary>
		public static string res_origincode => "res_origincode";

		/// <summary>
		/// Display Name: Categoria Principale,
		/// Type: Lookup,
		/// Related entities: product,
		/// Description: 
		/// </summary>
		public static string res_parentcategoryid => "res_parentcategoryid";

		/// <summary>
		/// Display Name: Unità di misura (peso),
		/// Type: Lookup,
		/// Related entities: uom,
		/// Description: 
		/// </summary>
		public static string res_uomweightid => "res_uomweightid";

		/// <summary>
		/// Display Name: Codice IVA,
		/// Type: Lookup,
		/// Related entities: res_vatnumber,
		/// Description: 
		/// </summary>
		public static string res_vatnumberid => "res_vatnumberid";

		/// <summary>
		/// Display Name: Dimensioni,
		/// Type: String,
		/// Description: Dimensioni del prodotto.
		/// </summary>
		public static string size => "size";

		/// <summary>
		/// Display Name: (Deprecated) Stage Id,
		/// Type: Uniqueidentifier,
		/// Description: Contains the id of the stage where the entity is located.
		/// </summary>
		public static string stageid => "stageid";

		/// <summary>
		/// Display Name: Costo medio,
		/// Type: Money,
		/// Description: Costo standard della voce di prodotto. Utilizzato nel calcolo dei prezzi.
		/// </summary>
		public static string standardcost => "standardcost";

		/// <summary>
		/// Display Name: Costo medio (Base),
		/// Type: Money,
		/// Description: Value of the Costo medio in base currency.
		/// </summary>
		public static string standardcost_base => "standardcost_base";

		/// <summary>
		/// Display Name: Volume (cm3),
		/// Type: Decimal,
		/// Description: Volume del prodotto.
		/// </summary>
		public static string stockvolume => "stockvolume";

		/// <summary>
		/// Display Name: Peso netto,
		/// Type: Decimal,
		/// Description: Peso del prodotto.
		/// </summary>
		public static string stockweight => "stockweight";

		/// <summary>
		/// Display Name: Argomento,
		/// Type: Lookup,
		/// Related entities: subject,
		/// Description: Seleziona una categoria per il prodotto.
		/// </summary>
		public static string subjectid => "subjectid";

		/// <summary>
		/// Display Name: Fornitore,
		/// Type: String,
		/// Description: Nome del fornitore del prodotto.
		/// </summary>
		public static string suppliername => "suppliername";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Identificatore univoco della valuta associata al prodotto.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";

		/// <summary>
		/// Display Name: (Deprecated) Traversed Path,
		/// Type: String,
		/// Description: A comma separated list of string values representing the unique identifiers of stages in a Business Process Flow Instance in the order that they occur.
		/// </summary>
		public static string traversedpath => "traversedpath";

		/// <summary>
		/// Display Name: Valido da,
		/// Type: DateTime,
		/// Description: Data di inizio validità del prodotto.
		/// </summary>
		public static string validfromdate => "validfromdate";

		/// <summary>
		/// Display Name: Valido fino a,
		/// Type: DateTime,
		/// Description: Data di fine validità del prodotto.
		/// </summary>
		public static string validtodate => "validtodate";

		/// <summary>
		/// Display Name: ID fornitore,
		/// Type: String,
		/// Description: Identificatore univoco del fornitore del prodotto.
		/// </summary>
		public static string vendorid => "vendorid";

		/// <summary>
		/// Display Name: Fornitore,
		/// Type: String,
		/// Description: Nome del fornitore del prodotto.
		/// </summary>
		public static string vendorname => "vendorname";

		/// <summary>
		/// Display Name: Nome fornitore,
		/// Type: String,
		/// Description: Identificatore parte univoco del catalogo fornitore del prodotto.
		/// </summary>
		public static string vendorpartnumber => "vendorpartnumber";


		/// <summary>
		/// Values for field Stato
		/// <summary>
		public new enum statecodeValues
		{
			Attivo = 0,
			Bozza = 2,
			Inaggiornamento = 3,
			Ritirato = 1
		}

		/// <summary>
		/// Values for field Motivo stato
		/// <summary>
		public new enum statuscodeValues
		{
			Attivo_StateAttivo = 1,
			Bozza_StateBozza = 0,
			Inaggiornamento_StateInaggiornamento = 3,
			Ritirato_StateRitirato = 2
		}

		/// <summary>
		/// Values for field Kit
		/// <summary>
		public enum iskitValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Associato con nuovo elemento padre
		/// <summary>
		public enum isreparentedValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Disponibile in magazzino
		/// <summary>
		public enum isstockitemValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Rifiuto esplicito RGPD
		/// <summary>
		public enum msdyn_gdproptoutValues
		{
			No = 0,
			Si = 1
		}

		/// <summary>
		/// Values for field Struttura prodotto
		/// <summary>
		public enum productstructureValues
		{
			Aggregazioneprodotti = 3,
			Famigliadiprodotti = 2,
			Prodotto = 1
		}

		/// <summary>
		/// Values for field Tipo di prodotto
		/// <summary>
		public enum producttypecodeValues
		{
			Articolo = 3,
			Articoloconmagazzinolotti = 8,
			Articoloconmagazzinolottiscadenze = 9,
			Articoloconmagazzinoseriali = 100000001,
			Articoloinmagazzino = 7,
			Servizio = 100000002
		}

		/// <summary>
		/// Values for field Origine
		/// <summary>
		public enum res_origincodeValues
		{
			Dynamics = 100000000,
			ERP = 100000001
		}
	};
}
