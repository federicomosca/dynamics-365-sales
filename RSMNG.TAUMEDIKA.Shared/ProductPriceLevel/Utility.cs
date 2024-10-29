using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using RSMNG.TAUMEDIKA.DataModel;

namespace RSMNG.TAUMEDIKA.Shared.ProductPriceLevel
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
        public static Entity GetProductPriceLevel(IOrganizationService service, Guid productId, Guid priceLevelId, int origin)
        {
            Entity productPriceLevel = null;
            var fetchData = new
            {
                productid = productId,
                pricelevelid= priceLevelId,
                res_origine = origin

            };
            var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
            <fetch>
              <entity name=""{productpricelevel.logicalName}"">
                <attribute name=""{productpricelevel.productpricelevelid}"" />
                <filter>
                  <condition attribute=""{productpricelevel.productid}"" operator=""eq"" value=""{fetchData.productid}"" />
                  <condition attribute=""{productpricelevel.pricelevelid}"" operator=""eq"" value=""{fetchData.pricelevelid}"" />
                  <condition attribute=""{productpricelevel.res_origine}"" operator=""eq"" value=""{fetchData.res_origine}"" />
                </filter>
              </entity>
            </fetch>";
            // Esecuzione della query FetchXML
            EntityCollection result = service.RetrieveMultiple(new FetchExpression(fetchXml));
            if (result?.Entities?.Count > 0)
            {
                productPriceLevel = result.Entities[0];
            }
            return productPriceLevel;
        }
    }
}
