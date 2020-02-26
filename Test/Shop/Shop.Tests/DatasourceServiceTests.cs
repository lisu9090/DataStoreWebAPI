using Shop.Domain.Interfaces;
using Shop.Domain.Models;
using Shop.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests
{
    public class DatasourceServiceTests
    {
        [Fact]
        public void SaveModelData_CreatesTransaction_ReturnsNumberOfRows()
        {
            var repoMock = new DataRepositoryMock();
            var datasourceRepository = new DatasourceService(repoMock);
            var inputData = new List<ArticleModel>() { new ArticleModel() };
            var expectedData = 1;

            var actualData = datasourceRepository.SaveModelData(inputData);

            Assert.Equal(expectedData, actualData);
            Assert.True(repoMock.Begined);
            Assert.True(repoMock.Commited);
            Assert.True(repoMock.Saved);
            Assert.False(repoMock.Rollbacked);
        }

        [Fact]
        public void SaveModelData_CreatesTransaction_RollbacksTransaction()
        {
            var repoMock = new DataRepositoryMock();
            var datasourceRepository = new DatasourceService(repoMock);
            var inputData = new List<ArticleModel>() { new ArticleModel(), null };
            var expectedData = 1;

            var actualData = datasourceRepository.SaveModelData(inputData);

            Assert.Equal(expectedData, actualData);
            Assert.True(repoMock.Begined);
            Assert.False(repoMock.Commited);
            Assert.True(repoMock.Saved);
            Assert.True(repoMock.Rollbacked);
        }

        [Fact]
        public void SaveModelDataAsync_CreatesTransaction_ReturnsNumberOfRows()
        {
            var repoMock = new DataRepositoryMock();
            var datasourceRepository = new DatasourceService(repoMock);
            var inputData = new List<ArticleModel>() { new ArticleModel() };
            var expectedData = 1;

            var actualData = datasourceRepository.SaveModelDataAsync(inputData).Result;

            Assert.Equal(expectedData, actualData);
            Assert.True(repoMock.Begined);
            Assert.True(repoMock.Commited);
            Assert.True(repoMock.Saved);
            Assert.False(repoMock.Rollbacked);
        }

        [Fact]
        public void SaveModelDataAsync_CreatesTransaction_RollbacksTransaction()
        {
            var repoMock = new DataRepositoryMock();
            var datasourceRepository = new DatasourceService(repoMock);
            var inputData = new List<ArticleModel>() { new ArticleModel(), null };
            var expectedData = 1;

            var actualData = datasourceRepository.SaveModelDataAsync(inputData).Result;

            Assert.Equal(expectedData, actualData);
            Assert.True(repoMock.Begined);
            Assert.False(repoMock.Commited);
            Assert.True(repoMock.Saved);
            Assert.True(repoMock.Rollbacked);
        }

        public class DataRepositoryMock : IDataRepository
        {
            public bool Begined { get; private set; } = false;
            public bool Commited { get; private set; } = false;
            public bool Rollbacked { get; private set; } = false;
            public bool Saved { get; private set; } = false;

            public int counter = 0;
            public void BeginTransaction()
            {
                SetValues(true, false, false, false);
            }

            public void CommitTransaction()
            {
                Commited = true;
            }

            public void RollbackTransaction()
            {
                Rollbacked = true;
            }

            public Task<int> SaveChangesAsync()
            {
                Saved = true;

                return new TaskFactory().StartNew(() => counter);
            }

            public void WriteData(ArticleModel data)
            {
                if (data == null)
                    throw new Exception("Cannot be null!");

                counter++;
            }

            public Task WriteDataAsync(ArticleModel data)
            {
                return new TaskFactory().StartNew(() => WriteData(data));
            }

            private void SetValues(bool begined, bool commited, bool rollbacked, bool saved)
            {
                Begined = begined;
                Commited = commited;
                Rollbacked = rollbacked;
                Saved = saved;
            }
        }
    }
}
