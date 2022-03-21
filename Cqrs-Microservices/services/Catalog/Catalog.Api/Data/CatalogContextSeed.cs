using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Products> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertMany(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Products> GetPreConfiguredProducts()
        {
            return new List<Products>() {
            new Products(){
                Id="602d2149e773f2a3990b47f5",
                Name="Hp",
                Summary="Nice Configuration",
                Description="Well deserved",
                ImageFile="product.jpeg",
                Price=4000,
                Category="Laptops"
            },
            new Products()
            {
                Id="602d2149e773f2a3990b47f6",
                Name="Dell",
                Summary="Good Battery",
                Description="Nice Look",
                ImageFile="product.jpg",
                Price=8203,
                Category="Laptops"
            }
            };
        }

        
    }
}
