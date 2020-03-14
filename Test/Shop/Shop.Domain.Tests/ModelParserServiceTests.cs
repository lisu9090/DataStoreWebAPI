using Shop.Domain.Models;
using Shop.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Shop.Tests
{
    public class ModelParserServiceTests
    {
        //    [Fact]
        //    public void SetKeysPositions_GetsValidInput_AssingsPropperIndexes()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        var inputData = new string[] {"key1", "key2" };
        //        var expectedData = new Dictionary<string, int>();
        //        expectedData.Add("key1", 0);
        //        expectedData.Add("key2", 1);

        //        //act
        //        modelParserService.SetKeysPositions(inputData);
        //        var outputData = modelParserService.KeysPositions;

        //        //assert
        //        Assert.Equal(expectedData["key1"] ,outputData["key1"]);
        //        Assert.Equal(expectedData["key2"], outputData["key2"]);
        //    }

        //    [Fact]
        //    public void Parse_GetsValidString_RetunsValidModel()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "1,2,1,1,1,1,1,1,1,1";
        //        var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1"};
        //        var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.Parse(inputData);

        //        //assert
        //        Assert.Equal(expectedDataArticle.ArticleCode, outputData.ArticleCode);
        //        Assert.Equal(expectedDataArticle.ColorCode, outputData.ColorCode);
        //        Assert.Equal(expectedDataArticle.Description, outputData.Description);
        //        Assert.Equal(expectedDataArticle.Q1, outputData.Q1);
        //        foreach(var item in outputData.Variants)
        //        {
        //            Assert.Equal(expectedDataVariant.ArticleCode, item.ArticleCode);
        //            Assert.Equal(expectedDataVariant.Color, item.Color);
        //            Assert.Equal(expectedDataVariant.DeliveredIn, item.DeliveredIn);
        //            Assert.Equal(expectedDataVariant.DiscountPrice, item.DiscountPrice);
        //            Assert.Equal(expectedDataVariant.Key, item.Key);
        //            Assert.Equal(expectedDataVariant.Price, item.Price);
        //            Assert.Equal(expectedDataVariant.Size, item.Size);
        //        }
        //    }

        //    [Fact]
        //    public void Parse_GetsEmptyString_RetunsEmptyModel()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "";
        //        //var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1" };
        //        //var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.Parse(inputData);

        //        //assert
        //        Assert.NotNull(outputData);
        //        Assert.NotNull(outputData.Variants);
        //        Assert.True(outputData.Variants.Count > 0);
        //        foreach(var item in outputData.Variants)
        //            Assert.NotNull(item);
        //    }

        //    [Fact]
        //    public void ParseAsync_GetsValidString_RetunsValidModel()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "1,2,1,1,1,1,1,1,1,1";
        //        var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1" };
        //        var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.ParseAsync(inputData).Result;

        //        //assert
        //        Assert.Equal(expectedDataArticle.ArticleCode, outputData.ArticleCode);
        //        Assert.Equal(expectedDataArticle.ColorCode, outputData.ColorCode);
        //        Assert.Equal(expectedDataArticle.Description, outputData.Description);
        //        Assert.Equal(expectedDataArticle.Q1, outputData.Q1);
        //        foreach (var item in outputData.Variants)
        //        {
        //            Assert.Equal(expectedDataVariant.ArticleCode, item.ArticleCode);
        //            Assert.Equal(expectedDataVariant.Color, item.Color);
        //            Assert.Equal(expectedDataVariant.DeliveredIn, item.DeliveredIn);
        //            Assert.Equal(expectedDataVariant.DiscountPrice, item.DiscountPrice);
        //            Assert.Equal(expectedDataVariant.Key, item.Key);
        //            Assert.Equal(expectedDataVariant.Price, item.Price);
        //            Assert.Equal(expectedDataVariant.Size, item.Size);
        //        }
        //    }

        //    [Fact]
        //    public void ParseAsync_GetsEmptyString_RetunsEmptyModel()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "";

        //        //act
        //        var outputData = modelParserService.ParseAsync(inputData).Result;

        //        //assert
        //        Assert.NotNull(outputData);
        //        Assert.NotNull(outputData.Variants);
        //        Assert.True(outputData.Variants.Count > 0);
        //        foreach (var item in outputData.Variants)
        //            Assert.NotNull(item);
        //    }

        //    [Fact]
        //    public void ParseBatch_GetsValidString_RetunsValidModelCollection()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "1,2,1,1,1,1,1,1,1,1\n1,2,1,1,1,1,1,1,1,1";
        //        var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1" };
        //        var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.ParseBatch(inputData);

        //        //assert
        //        foreach(var article in outputData)
        //        {
        //            Assert.Equal(expectedDataArticle.ArticleCode, article.ArticleCode);
        //            Assert.Equal(expectedDataArticle.ColorCode, article.ColorCode);
        //            Assert.Equal(expectedDataArticle.Description, article.Description);
        //            Assert.Equal(expectedDataArticle.Q1, article.Q1);
        //            foreach (var variant in article.Variants)
        //            {
        //                Assert.Equal(expectedDataVariant.ArticleCode, variant.ArticleCode);
        //                Assert.Equal(expectedDataVariant.Color, variant.Color);
        //                Assert.Equal(expectedDataVariant.DeliveredIn, variant.DeliveredIn);
        //                Assert.Equal(expectedDataVariant.DiscountPrice, variant.DiscountPrice);
        //                Assert.Equal(expectedDataVariant.Key, variant.Key);
        //                Assert.Equal(expectedDataVariant.Price, variant.Price);
        //                Assert.Equal(expectedDataVariant.Size, variant.Size);
        //            }
        //        }
        //    }

        //    [Fact]
        //    public void ParseBatch_GetsEmptyString_SkipsEmptyModel()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "\n1,2,1,1,1,1,1,1,1,1";
        //        var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1" };
        //        var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.ParseBatch(inputData);

        //        //assert
        //        foreach (var article in outputData)
        //        {
        //            Assert.Equal(expectedDataArticle.ArticleCode, article.ArticleCode);
        //            Assert.Equal(expectedDataArticle.ColorCode, article.ColorCode);
        //            Assert.Equal(expectedDataArticle.Description, article.Description);
        //            Assert.Equal(expectedDataArticle.Q1, article.Q1);
        //            foreach (var variant in article.Variants)
        //            {
        //                Assert.Equal(expectedDataVariant.ArticleCode, variant.ArticleCode);
        //                Assert.Equal(expectedDataVariant.Color, variant.Color);
        //                Assert.Equal(expectedDataVariant.DeliveredIn, variant.DeliveredIn);
        //                Assert.Equal(expectedDataVariant.DiscountPrice, variant.DiscountPrice);
        //                Assert.Equal(expectedDataVariant.Key, variant.Key);
        //                Assert.Equal(expectedDataVariant.Price, variant.Price);
        //                Assert.Equal(expectedDataVariant.Size, variant.Size);
        //            }
        //        }
        //    }

        //    [Fact]
        //    public void ParseBatchAsync_GetsValidString_RetunsValidModelCollection()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "1,2,1,1,1,1,1,1,1,1\n1,2,1,1,1,1,1,1,1,1";
        //        var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1" };
        //        var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.ParseBatchAsync(inputData).Result;

        //        //assert
        //        foreach (var article in outputData)
        //        {
        //            Assert.Equal(expectedDataArticle.ArticleCode, article.ArticleCode);
        //            Assert.Equal(expectedDataArticle.ColorCode, article.ColorCode);
        //            Assert.Equal(expectedDataArticle.Description, article.Description);
        //            Assert.Equal(expectedDataArticle.Q1, article.Q1);
        //            foreach (var variant in article.Variants)
        //            {
        //                Assert.Equal(expectedDataVariant.ArticleCode, variant.ArticleCode);
        //                Assert.Equal(expectedDataVariant.Color, variant.Color);
        //                Assert.Equal(expectedDataVariant.DeliveredIn, variant.DeliveredIn);
        //                Assert.Equal(expectedDataVariant.DiscountPrice, variant.DiscountPrice);
        //                Assert.Equal(expectedDataVariant.Key, variant.Key);
        //                Assert.Equal(expectedDataVariant.Price, variant.Price);
        //                Assert.Equal(expectedDataVariant.Size, variant.Size);
        //            }
        //        }
        //    }

        //    [Fact]
        //    public void ParseBatchAsync_GetsEmptyString_RetunsEmptyModel()
        //    {
        //        //arrange
        //        var modelParserService = new CsvModelParser();
        //        modelParserService.SetKeysPositions("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color".Split(','));
        //        var inputData = "\n1,2,1,1,1,1,1,1,1,1";
        //        var expectedDataArticle = new ArticleModel() { ArticleCode = "2", ColorCode = "1", Description = "1", Q1 = "1" };
        //        var expectedDataVariant = new VariantModel() { ArticleCode = "2", Key = "1", Color = "1", DeliveredIn = "1", DiscountPrice = 1, Price = 1, Size = 1 };

        //        //act
        //        var outputData = modelParserService.ParseBatchAsync(inputData).Result;

        //        //assert
        //        foreach (var article in outputData)
        //        {
        //            Assert.Equal(expectedDataArticle.ArticleCode, article.ArticleCode);
        //            Assert.Equal(expectedDataArticle.ColorCode, article.ColorCode);
        //            Assert.Equal(expectedDataArticle.Description, article.Description);
        //            Assert.Equal(expectedDataArticle.Q1, article.Q1);
        //            foreach (var variant in article.Variants)
        //            {
        //                Assert.Equal(expectedDataVariant.ArticleCode, variant.ArticleCode);
        //                Assert.Equal(expectedDataVariant.Color, variant.Color);
        //                Assert.Equal(expectedDataVariant.DeliveredIn, variant.DeliveredIn);
        //                Assert.Equal(expectedDataVariant.DiscountPrice, variant.DiscountPrice);
        //                Assert.Equal(expectedDataVariant.Key, variant.Key);
        //                Assert.Equal(expectedDataVariant.Price, variant.Price);
        //                Assert.Equal(expectedDataVariant.Size, variant.Size);
        //            }
        //        }
        //    }
    }
}
