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
        static void AddParentWithChildren(ref Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>> parentChildDict, KeyValuePair<string, Guid> parent, List<KeyValuePair<string, Guid>> children)
        {
            // Se il padre non esiste nel dizionario, lo aggiungiamo con la lista dei figli
            if (!parentChildDict.ContainsKey(parent))
            {
                parentChildDict[parent] = new List<KeyValuePair<string, Guid>>();
            }

            // Aggiungiamo i figli, ma solo se non sono già presenti nella lista del padre
            foreach (var child in children)
            {
                // Controlla se il figlio è già nella lista
                if (!parentChildDict[parent].Contains(child))
                {
                    parentChildDict[parent].Add(child);
                }
            }
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
              <entity name=""{product.logicalName}"">
                <attribute name=""{product.productnumber}"" />
                <attribute name=""{product.productid}"" />
                <filter>
                  <condition attribute=""{product.productstructure}"" operator=""eq"" value=""{fetchData.productstructure}"" />
                  <condition attribute=""{product.statecode}"" operator=""in"">{fetchData.statecode}</condition>
                </filter>
                <order attribute=""{product.name}"" descending=""false"" />
                <link-entity name=""{product.logicalName}"" from=""{product.parentproductid}"" to=""{product.productid}"" link-type=""outer"" alias=""childproduct"">
                  <attribute name=""{product.productnumber}"" />
                  <attribute name=""{product.productid}"" />
                  <filter>
                    <condition attribute=""{product.productstructure}"" operator=""eq"" value=""{fetchData.productstructure}"" />
                    <condition attribute=""{product.statecode}"" operator=""in"">{fetchData.statecode}</condition>
                  </filter>
                  <order attribute=""{product.name}"" descending=""false"" />
                </link-entity>
              </entity>
            </fetch>";

            //                  <condition entityname=""childproduct"" attribute=""{product.parentproductid}"" operator=""not-null"" />

            // Esecuzione della query FetchXML
            EntityCollection result = service.RetrieveMultiple(new FetchExpression(fetchXml));

            // Dizionario per memorizzare la gerarchia prodotto-genitore-figli
            Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>> productHierarchy = new Dictionary<KeyValuePair<string, Guid>, List<KeyValuePair<string, Guid>>>();

            // Elaborazione dei risultati
            foreach (Entity entity in result.Entities)
            {
                string parentProductNumber = string.Empty;
                Guid parentProductId = Guid.Empty;
                bool isChildren = true;
                if (entity.ContainsAttributeNotNull(product.productnumber))
                {
                    // Ottieni il numero e l'ID del prodotto genitore
                    parentProductNumber = entity.GetAttributeValue<string>(product.productnumber);
                    parentProductId = entity.Id;
                }
                else if (entity.Contains($"childproduct.{product.productid}"))
                {
                    parentProductNumber = entity.GetAttributeValue<AliasedValue>($"childproduct.{product.productnumber}").Value.ToString();
                    parentProductId = (Guid)entity.GetAttributeValue<AliasedValue>($"childproduct.{product.productid}").Value;
                    isChildren = false;
                }

                // Chiave del prodotto genitore (numero + ID)
                var parentKey = new KeyValuePair<string, Guid>(parentProductNumber, parentProductId);

                // Verifica se ci sono figli (dalla link-entity alias 'childproduct')
                var children = new List<KeyValuePair<string, Guid>>();
                
                if (isChildren && entity.Contains($"childproduct.{product.productid}"))
                {
                    var childProductNumber = entity.GetAttributeValue<AliasedValue>($"childproduct.{product.productnumber}").Value.ToString();
                    var childProductId = (Guid)entity.GetAttributeValue<AliasedValue>($"childproduct.{product.productid}").Value;

                    children.Add(new KeyValuePair<string, Guid>(childProductNumber, childProductId));
                }
                AddParentWithChildren(ref productHierarchy, parentKey, children);
                // Aggiungi il genitore e i figli al dizionario
                //productHierarchy[parentKey] = children;
            }

            return productHierarchy;
        }
        public static Entity GetProduct(IOrganizationService service, string productNumber)
        {
            Entity product = null;
            var fetchData = new
            {
                productnumber = productNumber
            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch>
              <entity name=""product"">
                <attribute name=""productid"" />
                <attribute name=""name"" />
                <filter>
                  <condition attribute=""productnumber"" operator=""eq"" value=""{fetchData.productnumber/*aaa*/}"" />
                </filter>
              </entity>
            </fetch>";
            // Esecuzione della query FetchXML
            EntityCollection result = service.RetrieveMultiple(new FetchExpression(fetchXml));
            if (result?.Entities?.Count > 0)
            {
                product = result.Entities[0];
            }
            return product;
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
        [DataMember] public int? Value { get; set; }
        [DataMember] public string ExternalValue { get; set; }
        [DataMember] public string Text { get; set; }
    }

    [DataContract]
    public class LookUp
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Text { get; set; }
        [DataMember] public string Entity { get; set; }
    }
}
