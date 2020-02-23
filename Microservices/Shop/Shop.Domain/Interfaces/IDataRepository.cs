using Infrastructure.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IDataRepository: IUnitOfWork
    {
        int WriteData(ArticleModel data);
        Task<int> WriteDataAsync(ArticleModel data);
    }
}
