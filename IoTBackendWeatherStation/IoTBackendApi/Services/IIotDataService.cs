﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTBackendApi.Services
{
    public interface IIotDataService
    {
        IEnumerable<string> GetDevices();
    }
}