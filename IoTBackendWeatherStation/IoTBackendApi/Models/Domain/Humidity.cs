using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTBackendApi.Models.Domain
{
    public class Humidity
    {
        public DateTime Timestamp { get; set; }
        public Dictionary<string, double> SensorReadings { get; set; }
    }
}
