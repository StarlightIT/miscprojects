using System;
using System.Collections.Generic;

namespace IoTBackendApi.Models.Domain
{
    public class SensorData
    {
        public DateTime Timestamp { get; set; }
        public Dictionary<string, double> SensorReadings { get; set; } = new Dictionary<string, double>();
    }
}