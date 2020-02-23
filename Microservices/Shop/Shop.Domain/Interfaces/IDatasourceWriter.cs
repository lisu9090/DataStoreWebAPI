using Infrastructure.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IDatasourceWriter
    {
        int SaveModelData(IEnumerable<VariantModel> data);
        Task<int> SaveModelDataAsync(IEnumerable<VariantModel> data);
    }
}
