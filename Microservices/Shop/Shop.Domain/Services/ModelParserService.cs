﻿using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services
{
    public class ModelParserService : ICsvToModelParser
    {
        public Dictionary<string, int> KeysPositions { get; private set; }

        public ModelParserService() { }

        public ModelParserService(string[] keys)
        {
            SetKeysPositions(keys);
        }

        public void SetKeysPositions(string[] keys)
        {
            KeysPositions = new Dictionary<string, int>();

            for (int i = 0; i < keys.Length; i++)
            {
                KeysPositions.Add(keys[i], i);
            }
        }

        public ArticleModel Parse(string data)
        {
            var articleModel = new ArticleModel();
            var variantModel = new VariantModel();
            var dataArray = data.Split(',');

            articleModel.Variants = new List<VariantModel>() { variantModel };

            if (KeysPositions != null && KeysPositions.Count > 0)
            {
                int tmpInt;
                double tmpDouble;

                //Varaint
                variantModel.Key = dataArray[KeysPositions["Key"]];
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
            return from item in data.Split('\n') select Parse(item);
        }

        public Task<IEnumerable<ArticleModel>> ParseBatchAsync(string data)
        {
            return new TaskFactory().StartNew(()=> {
                return ParseBatch(data);
            });
        }
    }
}
