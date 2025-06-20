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

        public void SeedData()
        {

            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }


                if (!_context.ProductBrands.Any())
                {
                    var productBrands = File.ReadAllText(@"..\Presistence\Data\DataSeed\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);
                    if (brands != null)
                    {
                        _context.ProductBrands.AddRange(brands);
                    }
                }

                if (!_context.ProductTypes.Any())
                {
                    var productTypes = File.ReadAllText(@"..\Presistence\Data\DataSeed\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);
                    if (types != null)
                    {
                        _context.ProductTypes.AddRange(types);
                    }
                }

                if (!_context.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Presistence\Data\DataSeed\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products != null)
                    {
                        _context.Products.AddRange(products);
                    }
                }

                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                // Notify the error to the user or log it
            }
        }
    }
}
