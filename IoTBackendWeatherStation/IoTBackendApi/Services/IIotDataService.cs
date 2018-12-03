using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Domain;

namespace IoTBackendApi.Services
{
    public interface IIotDataService
    {
        Task<IEnumerable<string>> GetDevices();

        Task<SensorResult> GetSensorDataForDateAndSensor(string deviceId, DateTime date, string sensorId);
        Task<IEnumerable<string>> GetSensors(string deviceId);
        Task<IEnumerable<SensorResult>> GetSensorDataForDate(string deviceId, DateTime date);
    }
}
