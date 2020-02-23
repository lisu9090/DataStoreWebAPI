using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public DataController()
        {
            //configure di
        }

        [HttpPost("upload")]
        public async Task UploadShopData(IAsyncEnumerable<string> stream)
        {
            await foreach(var item in stream)
            {

            }
        }

    }
}