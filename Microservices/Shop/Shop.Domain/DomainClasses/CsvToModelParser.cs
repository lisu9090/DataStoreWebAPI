using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.DomainClasses
{
    public class CsvToModelParser : ICsvToModelParser
    {
        public Dictionary<string, int> KeysPositions { get; private set; }

        public CsvToModelParser() { }

        public CsvToModelParser(string[] keys)
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

        public VariantModel Parse(string data)
        {
            var model = new VariantModel();
            var dataArray = data.Split(',');

            model.Article = new ArticleModel();
            model.Size = new SizeModel();
            model.Size.Q1 = new Q1Model();
            model.Color = new ColorModel();
            model.Color.ColorCode = new ColorCodeModel();

            if (KeysPositions != null && KeysPositions.Count > 0)
            {
                int tmpInt;
                double tmpDouble;

                //Varaint
                model.Key = dataArray[KeysPositions["Key"]];
                if(double.TryParse(dataArray[KeysPositions["Price"]], out tmpDouble))
                {
                    model.Price = tmpDouble;
                }
                if (double.TryParse(dataArray[KeysPositions["DiscountPrice"]], out tmpDouble))
                {
                    model.DiscountPrice = tmpDouble;
                }
                model.DeliveredIn = dataArray[KeysPositions["DeliveredIn"]];
                
                //Article
                model.ArticleCode = dataArray[KeysPositions["ArtikelCode"]];
                model.Article.ArticleCode = model.ArticleCode;
                model.Article.Description = dataArray[KeysPositions["Description"]];
                
                //Size
                if (int.TryParse(dataArray[KeysPositions["Size"]], out tmpInt))
                {
                    model.Size.Size = tmpInt;
                }

                //Size.Q1
                model.Size.Q1.Q1 = dataArray[KeysPositions["Q1"]];

                //Color
                model.Color.Color = dataArray[KeysPositions["Color"]];

                //Color.ColorCode
                model.Color.ColorCode.Code = dataArray[KeysPositions["ColorCode"]];
            }

            return model;
        }

        public Task<VariantModel> ParseAsync(string data)
        {
            return new TaskFactory().StartNew(() => {
                return Parse(data);
            });
        }

        public IEnumerable<VariantModel> ParseBatch(string data)
        {
            return from item in data.Split('\n') select Parse(item);
        }

        public Task<IEnumerable<VariantModel>> ParseBatchAsync(string data)
        {
            return new TaskFactory().StartNew(()=> {
                return ParseBatch(data);
            });
        }
    }
}
