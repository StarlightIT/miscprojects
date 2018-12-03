using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Domain;

namespace IoTBackendApi.Services
{
    public class IotDataService : IIotDataService
    {
        private IStorageService _storageService;

        public IotDataService(IStorageService storageService) => _storageService = storageService;

        public async Task<IEnumerable<string>> GetDevices()
        {
            return await _storageService.GetDevices();
        }

        public async Task<IEnumerable<string>> GetSensors(string deviceId)
        {
            return await _storageService.GetSensors(deviceId);
        }

        public async Task<IEnumerable<SensorResult>> GetSensorDataForDate(string deviceId, DateTime date)
        {
            var sensorResults = new List<SensorResult>();
            var sensors = await GetSensors(deviceId);

            foreach (var sensorId in sensors)
            {
                var sensorResult = await GetSensorDataForDateAndSensor(deviceId, date, sensorId);
                sensorResults.Add(sensorResult);
            }

            return sensorResults;
        }

        public async Task<SensorResult> GetSensorDataForDateAndSensor(string deviceId, DateTime date, string sensorId)
        {
            var availableDateRanges = await _storageService.GetAvailableSensorDates(deviceId, sensorId);
            if (!availableDateRanges.Contains(date))
            {
                return null;
            }

            var sensorData = await _storageService.GetSensorDataForDate(deviceId, date, sensorId);
            return new SensorResult
            {
                DeviceId = deviceId,
                SensorId =  sensorId,
                Date = date,
                SensorData = sensorData.ToList(),
            };
        }

        
    }
}
