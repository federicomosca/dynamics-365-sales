using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace RSMNG.TAUMEDIKA.Shared.Product
{
    public class Utility
    {
        public static string GetName(IOrganizationService service, Guid entityId)
        {
            string ret = string.Empty;
            if (entityId.Equals(Guid.Empty))
            {
                return ret;
            }
            Entity enEntity = service.Retrieve(DataModel.product.logicalName, entityId, new ColumnSet(new string[] { DataModel.product.productid, DataModel.product.name }));
            if (enEntity.Attributes.Contains(DataModel.product.name) && enEntity.Attributes[DataModel.product.name] != null)
            {
                ret = enEntity.GetAttributeValue<string>(DataModel.product.name);
            }

            return ret;
        }
    }


    [DataContract]
    public class ImportProductDanea
    {
        [DataMember] public Option Origine { get; set; }
        [DataMember] public string Nome { get; set; }
        [DataMember] public string Codice { get; set; }
        [DataMember] public ProductCategoryDanea EntitaPrincipale { get; set; }
        [DataMember] public string Descrizione { get; set; }
        [DataMember] public LookUp UnitaDiVendita { get; set; }
        [DataMember] public LookUp UnitaPredefinita { get; set; }
        [DataMember] public int DecimaliSupportati { get; set; }
        [DataMember] public LookUp CodiceIVA { get; set; }
        [DataMember] public decimal? PrezzoDiListino { get; set; }
        [DataMember] public Option Tipologia { get; set; }
        [DataMember] public Option StrutturaProdotto { get; set; }
        [DataMember] public string Produttore { get; set; }
        [DataMember] public string Fornitore { get; set; }
        [DataMember] public Option Stato { get; set; }
        [DataMember] public Option MotivoStato { get; set; }
        [DataMember] public string CodiceABarre { get; set; }
        [DataMember] public decimal? PesoLordo { get; set; }
        [DataMember] public decimal? PesoNetto { get; set; }
        [DataMember] public decimal? VolumeCm3 { get; set; }
        [DataMember] public LookUp UnitaDimisuraPeso { get; set; }
        [DataMember] public ProductCategoryDanea Categoria { get; set; }
    }

    public class ProductCategoryDanea
    {
        [DataMember] public string Nome { get; set; }
        [DataMember] public string Codice { get; set; }
    }

    [DataContract]
    public class Option
    {
        [DataMember] public int? Value {  get; set; }
        [DataMember] public string ExternalValue { get; set; }
        [DataMember] public string Text { get; set; }
    }

    [DataContract]
    public class LookUp
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Text { get; set; }
        [DataMember] public string Entity {  get; set; }
    }
}
