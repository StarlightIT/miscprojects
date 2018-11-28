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

        DateRange GetAvailableDataRanges();

        DateRange GetAvailableArchiveDateRanges();

        
    }
}
