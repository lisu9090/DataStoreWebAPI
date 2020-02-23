using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace Shop.Infrastructure.DAL
{
    class ShopContext: DbContext
    {
        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<VariantModel> Variants { get; set; }

        public ShopContext(string connectionString = "ShopContext") : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
