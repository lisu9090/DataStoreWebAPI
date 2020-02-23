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
            int counter = 0;

            _repository.BeginTransaction();

            try
            {
                foreach (var item in data)
                {
                    _repository.WriteData(item);
                    counter++;
                }

                _repository.CommitTransaction();
            }
            catch(Exception e)
            {
                _repository.RollbackTransaction();
                Console.WriteLine(e);
            }

            _repository.SaveChangesAsync();

            return counter;
        }

        public Task<int> SaveModelDataAsync(IEnumerable<ArticleModel> data)
        {
            return new TaskFactory().StartNew(() => {
                return SaveModelData(data);
            });
        }
    }
}
