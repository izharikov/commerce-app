using System;
using Commerce.Feature.Db.Catalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Commerce.Feature.Db.Catalog.Context
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public CatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}