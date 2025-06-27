using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.OrderAggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext )
        {
            if (_dbContext.ProductBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands (1).json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }

                    await _dbContext.SaveChangesAsync();
                } 
            }
            if (_dbContext.ProductCategories.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/Categories(1).json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbContext.Set<ProductCategory >().Add(category);
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
            if (_dbContext.Products.Count() == 0)
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products (1).json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
            if (_dbContext.DeliveryMethods.Count() == 0)
            {
                var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");

                var Deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
                if (Deliveries?.Count() > 0)
                {
                    foreach (var Delivery in Deliveries)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(Delivery);
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }





        }
    }
}
