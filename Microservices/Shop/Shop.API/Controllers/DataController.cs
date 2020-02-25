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
using Shop.Domain.Models;
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
            var section = await reader.ReadNextSectionAsync(); //get data part

            string[] keys = null; //columns names
            int efCounter = 0, jsonCounter = 0; //Data rows counters
            var tailingData = ""; //used when received data doesn't end with \n

            while (section != null)
            {
                using (var streamReader = new StreamReader(section.Body, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
                {
                    var value = tailingData + (await streamReader.ReadToEndAsync()).Replace("\r", ""); //read data from body; remove '\r', merge with tailing data


                    if (string.IsNullOrEmpty(value) || string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase)) //if vaule is empty continue
                    {
                        section = await reader.ReadNextSectionAsync();
                        continue;
                    }

                    var incLastIdx = value.LastIndexOf('\n') + 1; //incremented last index of
                    if (incLastIdx < value.Length)
                    {
                        tailingData = value.Substring(incLastIdx);
                        value = value.Substring(0, value.Length - incLastIdx);
                    }
                    else
                        tailingData = "";

                    if (keys == null) //if columns names haven't been set yet, do it and remove first themfrom value
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

                    var data = _parser != null ? _parser.ParseBatch(value) : new List<ArticleModel>(); //use ICsvToModelParser object to convert string data to  models 
                    if (_efWriter != null)
                        efCounter += await _efWriter.SaveModelDataAsync(data); //use IDatasourceWriter object to write data to sqlserver
                    if (_jsonWriter != null)
                        jsonCounter += await _jsonWriter?.SaveModelDataAsync(data); //use IDatasourceWriter object to write data to json file
                }

                section = await reader.ReadNextSectionAsync(); //read next section
            }

            return Ok(string.Format("Done! Rows inserted EF = {0}, JSON = {1}", efCounter, jsonCounter)); //return results
        }
    }
}