﻿using Infrastructure.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IDataRepository: IUnitOfWork
    {
        void WriteData(ArticleModel data);
        Task WriteDataAsync(ArticleModel data);
    }
}
