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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);

            //Seed
            builder.HasData(
                new Category { Id = 1 , Name = "Category_one", Description = "1"},
                new Category { Id = 2 , Name = "Category_two", Description = "2"},
                new Category { Id = 3, Name = "Category_three", Description = "3" }
                );
        }
    }
}
