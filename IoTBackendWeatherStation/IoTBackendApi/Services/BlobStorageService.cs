using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Configuration;
using IoTBackendApi.Models.Domain;
using Microsoft.Extensions.Options;

namespace IoTBackendApi.Services
{
    public class BlobStorageService : IStorageService
    {
        private StorageOptions _options;

        public BlobStorageService(IOptions<StorageOptions> options)
        {
            _options = options.Value;
        }

        public IEnumerable<string> GetDevices()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSensors()
        {
            throw new NotImplementedException();
        }

        public DateRange GetAvailableDataRanges()
        {
            throw new NotImplementedException();
        }

        public DateRange GetAvailableArchiveDateRanges()
        {
            throw new NotImplementedException();
        }
    }
}
