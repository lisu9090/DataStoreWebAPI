using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using Shop.Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositiories
{
    public class JsonRepository : IDataRepository
    {
        ShopJsonContext _jsonContext;

        public JsonRepository()
        {
            _jsonContext = new ShopJsonContext();
        }
        public JsonRepository(string path)
        {
            _jsonContext = new ShopJsonContext(path);
        }
        public void BeginTransaction()
        {
            _jsonContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _jsonContext.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _jsonContext.RollbackTransaction();
        }

        public Task<int> SaveChangesAsync()
        {
            return _jsonContext.SaveChangesAsync();
        }

        public void WriteData(ArticleModel data)
        {
            _jsonContext.Models.Add(data);
        }

        public Task WriteDataAsync(ArticleModel data)
        {
            return new TaskFactory().StartNew(() => WriteData(data));
        }
    }
}
