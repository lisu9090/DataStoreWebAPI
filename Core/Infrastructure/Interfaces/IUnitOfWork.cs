using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        void BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        Task<int> SaveChangesAsync();
    }
}
