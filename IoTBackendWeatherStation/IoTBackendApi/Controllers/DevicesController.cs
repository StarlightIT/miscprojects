using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Domain;
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

        /// <summary>
        /// Get a list of supported devices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _iotDataService.GetDevices();
        }

        [HttpGet("{id}/data/temperature/{date}")]
        public async Task<Temperature> GetTemperature(string id, DateTime date)
        {
            var temperature = await _iotDataService.GetTemperature(id, date);

            return new Temperature();
        }
    }
}
