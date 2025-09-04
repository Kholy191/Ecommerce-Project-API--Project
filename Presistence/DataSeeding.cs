using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.IdentityEntities;
using Domain.Entities.ProductEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Identity;

namespace Presistence
{
    public class DataSeeding : IDataSeeding
    {
        readonly ApplicationDbContext _context;
        readonly IdentityStore _identityContext;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        public DataSeeding(ApplicationDbContext dbcontext, IdentityStore _identityContext, UserManager<AppUser> _usermanager, RoleManager<IdentityRole> _rolemanager)
        {
            _context = dbcontext;
            this._identityContext = _identityContext;
            _userManager = _usermanager;
            _roleManager = _rolemanager;
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

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                if (!await _identityContext.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!await _identityContext.Users.AnyAsync())
                {
                    var User1 = new AppUser()
                    {
                        UserName = "Kholy123",
                        Email = "a.elkholy2711@gmail.com",
                        DisplayName = "Ahmed Elkholy",
                    };
                    var User2 = new AppUser()
                    {
                        UserName = "Killer123",
                        Email = "Killer@gmail.com",
                        DisplayName = "Killer Admin",
                    };
                    var Result = await _userManager.CreateAsync(User1, "Kholy@1911");
                    Result = await _userManager.CreateAsync(User2, "Kholy@1911");
                    if (!Result.Succeeded)
                    {
                        throw new Exception("Failed to create users: " + string.Join(", ", Result.Errors.Select(e => e.Description)));
                    }
                    var user1 = await _userManager.FindByEmailAsync(User1.Email);
                    var user2 = await _userManager.FindByEmailAsync(User2.Email);
                    if (user1 == null || user2 == null)
                    {
                        throw new Exception("Failed to find created users by email.");
                    }
                    Result = await _userManager.AddToRoleAsync(user1, "Admin");
                    Result = await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                    await _identityContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Notify the error to the user or log it
                throw new Exception("An error occurred while seeding identity data: " + ex.Message, ex);
            }
        }
    }
}
