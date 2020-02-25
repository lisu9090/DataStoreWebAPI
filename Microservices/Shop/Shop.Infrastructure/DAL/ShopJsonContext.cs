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
    class ShopJsonContext: IUnitOfWork //class acts as interface to write data to json file
    {
        private string _path;
        private List<ArticleModel> _models;
        private static object _lock = new object();
        public List<ArticleModel> Models { get; set; }

        public ShopJsonContext(string path = @"/home/shop/Shop.json")
        {
            _path = path;
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('/') + 1));
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

                lock (_lock)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(_path))
                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {
                            serializer.Serialize(writer, _models);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
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
    }
}
