using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Shop.API.Utils;
using Shop.Domain.Interfaces;
using Shop.Domain.Services;
using Shop.Infrastructure.DAL;
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
        private readonly int _limit = 1024 * 1024;

        public DataController(ShopEFContext dbContext)
        {
            _parser = new ModelParserService();
            _efWriter = new DatasourceService(new EFRepository(dbContext));
            _jsonWriter = new DatasourceService(new JsonRepository());
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadShopData()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File", $"The request couldn't be processed.");

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _limit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            string[] keys = null;
            int efCounter = 0, jsonCounter = 0;
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                using (var streamReader = new StreamReader(section.Body, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
                {
                    var value = (await streamReader.ReadToEndAsync()).Replace("\r", "");

                    if (string.IsNullOrEmpty(value) || string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                    { 
                        section = await reader.ReadNextSectionAsync();
                        continue;
                    }

                    if (keys == null)
                    {
                        try
                        {
                            keys = value.Split('\n')[0].Split(',');
                            _parser.SetKeysPositions(keys);
                            value = value.Substring(value.IndexOf('\n') + 1);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            section = await reader.ReadNextSectionAsync();
                            continue;
                        }
                    }

                    var data = _parser.ParseBatch(value);
                    //efCounter += await _efWriter.SaveModelDataAsync(data);
                    jsonCounter += await _jsonWriter.SaveModelDataAsync(data);
                }

                section = await reader.ReadNextSectionAsync();
            }
            
            return Ok(string.Format("Done! Rows inserted EF = {0}, JSON = {1}", efCounter, jsonCounter));
        }
    }
}