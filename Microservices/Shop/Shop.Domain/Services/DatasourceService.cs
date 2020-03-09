using Shop.Domain.Abstraction.Repositories;
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
            return SaveModelDataAsync(data).Result; //Save data synchronously
        }

        public async Task<int> SaveModelDataAsync(IEnumerable<ArticleModel> data)
        {
            _repository.BeginTransaction(); //create transaction

            try
            {
                foreach (var item in data)
                {
                    _repository.WriteData(item);
                }

                _repository.CommitTransaction(); //commit transaction after whole data is written
            }
            catch (Exception e)
            {
                _repository.RollbackTransaction(); //in case of exception rollback transaction
                Console.WriteLine(e);
            }

            return await _repository.SaveChangesAsync(); //finalize transaction
        }
    }
}
