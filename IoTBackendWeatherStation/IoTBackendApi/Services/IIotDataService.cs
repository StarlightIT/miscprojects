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

        Task<Temperature> GetTemperature(string deviceId, DateTime date);
    }
}
