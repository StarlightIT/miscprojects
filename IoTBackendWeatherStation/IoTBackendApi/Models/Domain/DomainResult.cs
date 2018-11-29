using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTBackendApi.Models.Domain
{
    public class DomainResult<T>
    {
        public bool HasResult { get; set; }
        public T SensorResult { get; set; }
    }
}
