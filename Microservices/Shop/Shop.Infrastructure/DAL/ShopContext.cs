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
        //public DbSet<Q1Model> Q1s { get; set; }
        //public DbSet<SizeModel> Sizes { get; set; }
        //public DbSet<ColorCodeModel> ColorCodes { get; set; }
        //public DbSet<ColorModel> Colors { get; set; }

        public ShopContext() : base("ShopContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
