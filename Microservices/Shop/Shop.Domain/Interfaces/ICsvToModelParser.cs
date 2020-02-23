﻿using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICsvToModelParser
    {
        ArticleModel Parse(string data);
        Task<ArticleModel> ParseAsync(string data);
        IEnumerable<ArticleModel> ParseBatch(string data);
        Task<IEnumerable<ArticleModel>> ParseBatchAsync(string data);
    }
}
