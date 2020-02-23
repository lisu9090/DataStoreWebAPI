using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositiories
{
    public class EFRepository : IDataRepository
    {
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public int WriteData(ArticleModel data)
        {
            throw new NotImplementedException();
        }

        public Task<int> WriteDataAsync(ArticleModel data)
        {
            throw new NotImplementedException();
        }
    }
}
