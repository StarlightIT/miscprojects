using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTBackendApi.Models.Domain
{
    public class Rainfall
    {
        public DateTime Timestamp { get; set; }
        public int Amount { get; set; }
    }
}
