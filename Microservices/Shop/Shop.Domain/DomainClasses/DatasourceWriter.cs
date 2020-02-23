using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.DomainClasses
{
    public class DatasourceWriter: IDatasourceWriter
    {
        private IDataRepository _repository;

        public DatasourceWriter(IDataRepository repository)
        {
            _repository = repository;
        }

        public int SaveModelData(IEnumerable<VariantModel> data)
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

        public Task<int> SaveModelDataAsync(IEnumerable<VariantModel> data)
        {
            return new TaskFactory().StartNew(() => {
                return SaveModelData(data);
            });
        }
    }
}
