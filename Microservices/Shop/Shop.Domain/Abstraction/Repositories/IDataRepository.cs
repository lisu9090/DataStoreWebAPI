using Infrastructure.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Abstraction.Repositories
{
    public interface IDataRepository: IUnitOfWork // to be implemented outside domain (infrastructure)
    {
        void WriteData(ArticleModel data);
        Task WriteDataAsync(ArticleModel data);
    }
}
