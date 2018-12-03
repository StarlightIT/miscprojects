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

        Task<IEnumerable<string>> GetSensors(string deviceId);

        Task<IEnumerable<DateTime>> GetAvailableSensorDates(string deviceId, string sensorId);

        Task<IEnumerable<SensorData>> GetSensorDataForDate(string deviceId, DateTime date, string sensorId);
        

        //Task<DateRange> GetAvailableArchiveDataDates(string deviceId, string sensorId);
    }
}
