using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Domain;

namespace IoTBackendApi.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<string>> GetDevices();

        IEnumerable<string> GetSensors();

        Task<DateRange> GetAvailableDataRanges(string deviceId, string sensorId);

        Task<DateRange> GetAvailableArchiveDateRanges(string deviceId, string sensorId);

        
    }
}
