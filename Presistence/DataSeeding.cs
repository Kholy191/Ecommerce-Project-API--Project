using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence
{
    public class DataSeeding : IDataSeeding
    {
        readonly ApplicationDbContext _context;
        public DataSeeding(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task SeedDataAsync()
        {

            try
            {
                if ((await _context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _context.Database.MigrateAsync();
                }


                if (!await _context.ProductBrands.AnyAsync())
                {
                    var productBrands = File.OpenRead(@"..\Presistence\Data\DataSeed\brands.json");
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrands);
                    if (brands != null)
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                    }
                }

                if (!await _context.ProductTypes.AnyAsync())
                {
                    var productTypes = File.OpenRead(@"..\Presistence\Data\DataSeed\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                    if (types != null)
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                    }
                }

                if (!await _context.Products.AnyAsync())
                {
                    var productsData = File.OpenRead(@"..\Presistence\Data\DataSeed\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products != null)
                    {
                        await _context.Products.AddRangeAsync(products);
                    }
                }

                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                // Notify the error to the user or log it
            }
        }
    }
}
