using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace IoTBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private IIotDataService _iotDataService;
        private IConfiguration Configuration;

        public DevicesController(IIotDataService iotDataService, IConfiguration configuration)
        {
            _iotDataService = iotDataService;
            Configuration = configuration;
        }

        // GET: api/GetDevices
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _iotDataService.GetDevices();
        }

        // GET: api/GetDevices/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetDevices
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/GetDevices/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
