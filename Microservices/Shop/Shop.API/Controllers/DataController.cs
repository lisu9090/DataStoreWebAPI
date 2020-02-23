using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Interfaces;
using Shop.Domain.Services;
using Shop.Infrastructure.Repositiories;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private ICsvToModelParser _parser;
        private IDatasourceWriter _efWriter;
        private IDatasourceWriter _jsonWriter;
        public DataController()
        {
            _parser = new ModelParserService();
            _efWriter = new DatasourceService(new EFRepository());
            _jsonWriter = new DatasourceService(new JsonRepository());
        }

        [HttpPost("upload")]
        public async Task UploadShopData(IAsyncEnumerable<string> stream)
        {
            string[] keys = null;

            await foreach(var item in stream)
            {
                if (keys == null)
                {
                    try
                    {
                        keys = item.Split('\n')[0].Split(',');
                        _parser.SetKeysPositions(keys);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                var data = _parser.ParseBatch(item);
                _efWriter.SaveModelDataAsync(data);
                _jsonWriter.SaveModelDataAsync(data);
            }
        }

    }
}