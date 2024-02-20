using N_Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Store.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            // Seed
            builder.HasData(
                new Product { Id = 1, Name = "Product_one", Description = "1", Price=2000,CategoryId=1 },
                new Product { Id = 2, Name = "Product_two", Description = "2", Price = 2000, CategoryId = 1 },
                new Product { Id = 3, Name = "Product_three", Description = "3", Price = 2000, CategoryId = 1 }
                );

        }
    }
}
