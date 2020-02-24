using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace Shop.Infrastructure.DAL
{
    class ShopEFContext: DbContext
    {
        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<VariantModel> Variants { get; set; }

        public ShopEFContext(string connectionString = "ShopContext") : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
