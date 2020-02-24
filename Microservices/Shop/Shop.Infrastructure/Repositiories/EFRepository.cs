using Microsoft.EntityFrameworkCore;
using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using Shop.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositiories
{
    public class EFRepository : IDataRepository, IDisposable
    {
        private ShopEFContext _dbContext;

        public EFRepository()
        {
            _dbContext = new ShopEFContext();
        }

        public EFRepository(DbContextOptions<ShopEFContext> options)
        {
            _dbContext = new ShopEFContext(options);
        }

        public EFRepository(ShopEFContext context)
        {
            _dbContext = context;
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            _dbContext.Database.CurrentTransaction.Rollback();
        }

        public Task<int> SaveChangesAsync()
        {
            return new TaskFactory().StartNew(() => _dbContext.SaveChanges());
        }

        public void WriteData(ArticleModel data)
        {
            _dbContext.Articles.Add(data);
        }

        public Task WriteDataAsync(ArticleModel data)
        {
            return new TaskFactory().StartNew(() => WriteData(data));
        }

        public void Dispose()
        {
            _dbContext.Database.CurrentTransaction.Dispose();
            _dbContext.Dispose();
        }
    }
}
