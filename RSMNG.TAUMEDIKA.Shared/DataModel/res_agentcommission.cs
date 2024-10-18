namespace RSMNG.TAUMEDIKA.DataModel
{

	/// <summary>
	/// Provvigione Agente constants.
	/// </summary>
	public sealed class res_agentcommission : EntityGenericConstants
	{
		/// <summary>
		/// res_agentcommission
		/// </summary>
		public static string logicalName => "res_agentcommission";

		/// <summary>
		/// Provvigione Agente
		/// </summary>
		public static string displayName => "Provvigione Agente";

		/// <summary>
		/// Display Name: Tasso di cambio,
		/// Type: Decimal,
		/// Description: Tasso di cambio per la valuta associata all'entità rispetto alla valuta di base.
		/// </summary>
		public static string exchangerate => "exchangerate";

		/// <summary>
		/// Display Name: Rettifica,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_adjustment => "res_adjustment";

		/// <summary>
		/// Display Name: Rettifica (base),
		/// Type: Money,
		/// Description: Valore di Rettifica nella valuta di base.
		/// </summary>
		public static string res_adjustment_base => "res_adjustment_base";

		/// <summary>
		/// Display Name: Provvigione Agente,
		/// Type: Uniqueidentifier,
		/// Description: Identificatore univoco delle istanze di entità
		/// </summary>
		public static string res_agentcommissionid => "res_agentcommissionid";

		/// <summary>
		/// Display Name: Agente,
		/// Type: Lookup,
		/// Related entities: systemuser,
		/// Description: 
		/// </summary>
		public static string res_agentid => "res_agentid";

		/// <summary>
		/// Display Name: Provvigione calcolata,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_calculatedcommission => "res_calculatedcommission";

		/// <summary>
		/// Display Name: Provvigione calcolata (base),
		/// Type: Money,
		/// Description: Valore di Provvigione calcolata nella valuta di base.
		/// </summary>
		public static string res_calculatedcommission_base => "res_calculatedcommission_base";

		/// <summary>
		/// Display Name: Provvigione,
		/// Type: Lookup,
		/// Related entities: res_commission,
		/// Description: 
		/// </summary>
		public static string res_commissionid => "res_commissionid";

		/// <summary>
		/// Display Name: Totale Provvigione,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_commissiontotalamount => "res_commissiontotalamount";

		/// <summary>
		/// Display Name: Totale Provvigione (base),
		/// Type: Money,
		/// Description: Valore di Totale Provvigione nella valuta di base.
		/// </summary>
		public static string res_commissiontotalamount_base => "res_commissiontotalamount_base";

		/// <summary>
		/// Display Name: Nome,
		/// Type: String,
		/// Description: 
		/// </summary>
		public static string res_name => "res_name";

		/// <summary>
		/// Display Name: Nota,
		/// Type: Memo,
		/// Description: 
		/// </summary>
		public static string res_note => "res_note";

		/// <summary>
		/// Display Name: Totale venduto,
		/// Type: Money,
		/// Description: 
		/// </summary>
		public static string res_soldtotalamount => "res_soldtotalamount";

		/// <summary>
		/// Display Name: Totale venduto (base),
		/// Type: Money,
		/// Description: Valore di Totale venduto nella valuta di base.
		/// </summary>
		public static string res_soldtotalamount_base => "res_soldtotalamount_base";

		/// <summary>
		/// Display Name: Valuta,
		/// Type: Lookup,
		/// Related entities: transactioncurrency,
		/// Description: Identificatore univoco della valuta associata all'entità.
		/// </summary>
		public static string transactioncurrencyid => "transactioncurrencyid";


		/// <summary>
		/// Values for field Stato
		/// <summary>
		public new enum statecodeValues
		{
			Attivo = 0,
			Inattivo = 1
		}

		/// <summary>
		/// Values for field Motivo stato
		/// <summary>
		public new enum statuscodeValues
		{
			Bozza_StateAttivo = 1,
			Calcolato_StateAttivo = 100000002,
			Calcolatoerrori_StateAttivo = 100000003,
			Calcoloincorso_StateAttivo = 100000001,
			Inattivo_StateInattivo = 2
		}
	};
}
