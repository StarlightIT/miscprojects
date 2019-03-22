using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTBackendApi.Models.Domain
{
    public class SensorResult
    {
        public string DeviceId { get; set; }
        public string SensorId { get; set; }
        public DateTime Date { get; set; }
        public List<SensorData> SensorData { get; set; }
    }
}
