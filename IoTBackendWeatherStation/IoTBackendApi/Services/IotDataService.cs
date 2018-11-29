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

        public async Task<Temperature> GetTemperature(string deviceId, DateTime date)
        {
            var availableDateRanges = await _storageService.GetAvailableDataRanges(deviceId, "temperature");
            //var availableArchiveDateRanges = _storageService.GetAvailableArchiveDateRanges(deviceId, "temperature");

            return null;
        }
    }
}
