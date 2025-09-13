using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntities;
using Domain.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations.OrderModule
{
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.Cost).HasColumnType("decimal(8,2)");
            builder.Property(dm => dm.ShortName).HasMaxLength(50);
            builder.Property(dm => dm.Description).HasMaxLength(100);
            builder.Property(dm => dm.DeliveryTime).HasMaxLength(50);
        }
    }
}
