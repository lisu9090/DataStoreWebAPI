using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Abstraction.Services
{
    public interface IDataService
    {
        Task<string> ProcessDataStreamAsync(StreamReader streamReader);
        void Reset();
    }
}
