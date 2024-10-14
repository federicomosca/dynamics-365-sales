using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using RSMNG.TAUMEDIKA.DataModel;

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
        public static Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>> GetProductFamily(IOrganizationService service)
        {
            // Definizione della query FetchXML
            var fetchData = new
            {
                productstructure = (int)product.productstructureValues.Famigliadiprodotti,
                statecode = $"<value>{(int)product.statecodeValues.Bozza}</value><value>{(int)product.statecodeValues.Attivo}</value>"
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch>
              <entity name=""product"">
                <attribute name=""productnumber"" />
                <attribute name=""productid"" />
                <filter>
                  <condition attribute=""parentproductid"" operator=""null"" />
                  <condition attribute=""productstructure"" operator=""eq"" value=""{fetchData.productstructure}"" />
                  <condition attribute=""statecode"" operator=""in"">{fetchData.statecode}</condition>
                </filter>
                <order attribute=""name"" descending=""false"" />
                <link-entity name=""product"" from=""parentproductid"" to=""productid"" link-type=""outer"" alias=""childproduct"">
                  <attribute name=""productnumber"" />
                  <attribute name=""productid"" />
                  <filter>
                    <condition attribute=""productstructure"" operator=""eq"" value=""{fetchData.productstructure}"" />
                    <condition attribute=""statecode"" operator=""in"">{fetchData.statecode}</condition>
                  </filter>
                  <order attribute=""name"" descending=""false"" />
                </link-entity>
              </entity>
            </fetch>";
            // Esecuzione della query FetchXML
            EntityCollection result = service.RetrieveMultiple(new FetchExpression(fetchXml));

            // Dizionario per memorizzare la gerarchia prodotto-genitore-figli
            Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>> productHierarchy = new Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>>();

            // Elaborazione dei risultati
            foreach (var entity in result.Entities)
            {
                // Ottieni il numero e l'ID del prodotto genitore
                string parentProductNumber = entity.GetAttributeValue<string>("productnumber");
                Guid parentProductId = entity.Id;

                // Chiave del prodotto genitore (numero + ID)
                var parentKey = new KeyValuePair<string, Guid>(parentProductNumber, parentProductId);

                // Verifica se ci sono figli (dalla link-entity alias 'childproduct')
                var children = new List<KeyValuePair<string, Guid>>();
                if (entity.Contains("childproduct.productid"))
                {
                    var childProductNumber = entity.GetAttributeValue<AliasedValue>("childproduct.productnumber").Value.ToString();
                    var childProductId = (Guid)entity.GetAttributeValue<AliasedValue>("childproduct.productid").Value;

                    children.Add(new KeyValuePair<string, Guid>(childProductNumber, childProductId));
                }

                // Aggiungi il genitore e i figli al dizionario
                productHierarchy[parentKey] = children;
            }

            return productHierarchy;
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
