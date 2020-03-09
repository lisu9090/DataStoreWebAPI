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
using Shop.Domain.Abstraction.Services;
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
        //private ICsvToModelParser _parser;
        //private IDatasourceWriter _efWriter;
        //private IDatasourceWriter _jsonWriter;
        private IDataService _service;
        private readonly int _limit = 128;

        public DataController(IDataService service)
        {
            _service = service;
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
            var output = "";


            while (section != null)
            {
                output += await _service.ProcessDataStreamAsync(new StreamReader(section.Body, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true));
                section = await reader.ReadNextSectionAsync(); //read next section
            }

            return Ok(output); //return results
        }
    }
}