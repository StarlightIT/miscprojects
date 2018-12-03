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

        [HttpGet("{deviceId}/sensors")]
        public async Task<IEnumerable<string>> GetSensors(string deviceId)
        {
            return await _iotDataService.GetSensors(deviceId);
        }

        [HttpGet("{deviceId}/data/{date}")]
        public async Task<IEnumerable<SensorResult>> GetSensorDataForDate(string deviceId, DateTime date)
        {
            return await _iotDataService.GetSensorDataForDate(deviceId, date);
        }

        [HttpGet("{deviceId}/data/{date}/{sensorId}")]
        public async Task<SensorResult> GetSensorDataForDateAndSensor(string deviceId, DateTime date, string sensorId)
        {
            return await _iotDataService.GetSensorDataForDateAndSensor(deviceId, date, sensorId);
        }
    }
}
