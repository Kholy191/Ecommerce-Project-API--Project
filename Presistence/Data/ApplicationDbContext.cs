using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
        DbSet<ProductBrand> ProductBrands { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefference).Assembly);
        }
    }
}
