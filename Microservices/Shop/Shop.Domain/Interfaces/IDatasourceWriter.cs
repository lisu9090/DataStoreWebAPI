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
        int SaveModelData(IEnumerable<ArticleModel> data);
        Task<int> SaveModelDataAsync(IEnumerable<ArticleModel> data);
    }
}
