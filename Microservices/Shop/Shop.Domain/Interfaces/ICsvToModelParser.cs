using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICsvToModelParser
    {
        VariantModel Parse(string data);
        Task<VariantModel> ParseAsync(string data);
        IEnumerable<VariantModel> ParseBatch(string data);
        Task<IEnumerable<VariantModel>> ParseBatchAsync(string data);
    }
}
