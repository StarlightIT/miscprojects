﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
