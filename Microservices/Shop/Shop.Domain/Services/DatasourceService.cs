using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services
{
    public class DatasourceService: IDatasourceWriter
    {
        private IDataRepository _repository;

        public DatasourceService(IDataRepository repository)
        {
            _repository = repository;
        }

        public int SaveModelData(IEnumerable<ArticleModel> data)
        {
            return SaveModelDataAsync(data).Result;
        }

        public async Task<int> SaveModelDataAsync(IEnumerable<ArticleModel> data)
        {
            _repository.BeginTransaction();

            try
            {
                foreach (var item in data)
                {
                    _repository.WriteData(item);
                }

                _repository.CommitTransaction();
            }
            catch (Exception e)
            {
                _repository.RollbackTransaction();
                Console.WriteLine(e);
            }

            return await _repository.SaveChangesAsync();
        }
    }
}
