using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Utils
{
    public class CsvModelParser : ICsvToModelParser
    {
        public Dictionary<string, int> KeysPositions { get; private set; }

        public CsvModelParser() { }

        public CsvModelParser(string[] keys)
        {
            SetKeysPositions(keys);
        }

        public void SetKeysPositions(string[] keys) // create dictionary with columns names and their order indexes
        {
            KeysPositions = new Dictionary<string, int>();

            for (int i = 0; i < keys.Length; i++)
            {
                KeysPositions.Add(keys[i], i);
            }
        }

        public ArticleModel Parse(string data)
        {
            var articleModel = new ArticleModel(); //create empty model
            var variantModel = new VariantModel();
            articleModel.Variants = new List<VariantModel>() { variantModel }; 

            if (string.IsNullOrEmpty(data)) //if data is empty return empty model
                return articleModel; 

            var dataArray = data.Split(',');

            if (KeysPositions != null && KeysPositions.Count > 0) //if keys are no set return empty model
            {
                //parse data to proper type and fill model
                int tmpInt;
                double tmpDouble; 

                //Varaint
                variantModel.Key = dataArray[KeysPositions["Key"]]; 
                variantModel.ArticleCode = dataArray[KeysPositions["ArtikelCode"]];
                variantModel.Color = dataArray[KeysPositions["Color"]];
                variantModel.DeliveredIn = dataArray[KeysPositions["DeliveredIn"]];
                if (int.TryParse(dataArray[KeysPositions["Size"]], out tmpInt))
                {
                    variantModel.Size = tmpInt;
                }
                if (double.TryParse(dataArray[KeysPositions["Price"]], out tmpDouble))
                {
                    variantModel.Price = tmpDouble;
                }
                if (double.TryParse(dataArray[KeysPositions["DiscountPrice"]], out tmpDouble))
                {
                    variantModel.DiscountPrice = tmpDouble;
                }

                //Article
                articleModel.ArticleCode = dataArray[KeysPositions["ArtikelCode"]];
                articleModel.Description = dataArray[KeysPositions["Description"]];
                articleModel.Q1 = dataArray[KeysPositions["Q1"]];
                articleModel.ColorCode = dataArray[KeysPositions["ColorCode"]];
            }

            return articleModel;
        }

        public Task<ArticleModel> ParseAsync(string data)
        {
            return new TaskFactory().StartNew(() => {
                return Parse(data);
            });
        }

        public IEnumerable<ArticleModel> ParseBatch(string data)
        {
            return MergeModels(from item in data.Split('\n') select Parse(item)); //parse batch of data and return model collection
        }

        public Task<IEnumerable<ArticleModel>> ParseBatchAsync(string data)
        {
            return new TaskFactory().StartNew(()=> {
                return ParseBatch(data);
            });
        }

        private List<ArticleModel> MergeModels(IEnumerable<ArticleModel> models) //conver data from (1 article : 1 variant) to (1 article : many variants) models
        {
            var list = new List<ArticleModel>();
            ArticleModel tmp;

            foreach (var item in models)
            {
                if (string.IsNullOrEmpty(item.ArticleCode))
                    continue;

                tmp = list.Find(listItem => listItem.ArticleCode == item.ArticleCode); //Use ArticleCode to fill aticle's variants collecion
                if (tmp != null)
                {
                    foreach (var variant in item.Variants)
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
