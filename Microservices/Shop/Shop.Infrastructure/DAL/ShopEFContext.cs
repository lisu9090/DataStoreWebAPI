using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Infrastructure.DAL
{
    public class ShopEFContext: DbContext
    {
        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<VariantModel> Variants { get; set; }

        public ShopEFContext() : base() { }
        public ShopEFContext(DbContextOptions<ShopEFContext> options) : base(options) { }
    }
}
