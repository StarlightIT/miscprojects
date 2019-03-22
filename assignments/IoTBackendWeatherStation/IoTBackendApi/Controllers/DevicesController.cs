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
    /// <summary>
    /// API Controller for getting information and ata from IoT devices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IIotDataService _iotDataService;

        public DevicesController(IIotDataService iotDataService) => _iotDataService = iotDataService;

        /// <summary>
        /// Gets a list of supported devices
        /// </summary>
        /// <returns>The list of supported devices</returns>
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devices = await _iotDataService.GetDevices();
            return Ok(devices);
        }

        /// <summary>
        /// Gets a list of supported sensors for a device
        /// </summary>
        /// <param name="deviceId">The device Id</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [HttpGet("{deviceId}/sensors")]
        public async Task<IActionResult> GetSensors(string deviceId)
        {
            var sensors = await _iotDataService.GetSensors(deviceId);
            return Ok(sensors);
        }

        /// <summary>
        /// Gets data for all sensors for a particular device on a particular date
        /// </summary>
        /// <param name="deviceId">The device Id</param>
        /// <param name="date">The date</param>
        /// <returns>The sensor results</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{deviceId}/data/{date}")]
        public async Task<IActionResult> GetSensorDataForDate(string deviceId, DateTime date)
        {
            var sensorResults = await _iotDataService.GetSensorDataForDate(deviceId, date);

            if (!sensorResults.Any())
            {
                return NotFound();
            }

            return Ok(sensorResults);
        }

        /// <summary>
        /// Gets the sensor data for a particular device, date, and sensor
        /// </summary>
        /// <param name="deviceId">The device Id</param>
        /// <param name="date">The Date</param>
        /// <param name="sensorId">The Sensor Id</param>
        /// <returns>The sensor data or NotFound()</returns>
        [HttpGet("{deviceId}/data/{date}/{sensorId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSensorDataForDateAndSensor(string deviceId, DateTime date, string sensorId)
        {
            var sensorResult = await _iotDataService.GetSensorDataForDateAndSensor(deviceId, date, sensorId);

            if (sensorResult == null)
            {
                return NotFound();
            }

            return Ok(sensorResult);
        }
    }
}
