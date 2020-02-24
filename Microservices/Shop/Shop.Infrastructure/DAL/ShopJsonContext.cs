using Infrastructure.Interfaces;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.DAL
{
    class ShopJsonContext: IUnitOfWork
    {
        private string _path;
        private List<ArticleModel> _models;

        public List<ArticleModel> Models { get; set; }

        public ShopJsonContext(string path = @"C:\Shop\Shop.json")
        {
            _path = path;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('\\') + 1));
                File.Create(path);
            }

            Clean();
        }

        public void BeginTransaction()
        {
            Clean();
        }

        public void RollbackTransaction()
        {
            Models = new List<ArticleModel>();
        }

        public void CommitTransaction()
        {
            _models.AddRange(Models);
            Models = new List<ArticleModel>();
        }

        public Task<int> SaveChangesAsync()
        {
            return new TaskFactory().StartNew(() => {
                CommitTransaction();

                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(_path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, MergeModels());
                }

                var count = _models.Count;

                Clean();

                return count;
            });
        }

        private void Clean()
        {
            _models = new List<ArticleModel>();
            Models = new List<ArticleModel>();
        }

        private List<ArticleModel> MergeModels()
        {
            var list = new List<ArticleModel>();
            ArticleModel tmp;

            foreach(var item in _models)
            {
                tmp = list.Find(listItem => listItem.ArticleCode == item.ArticleCode);
                if (tmp != null)
                {
                    foreach(var variant in item.Variants)
                    {
                        tmp.Variants.Add(variant);
                    }
                }
                else
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
