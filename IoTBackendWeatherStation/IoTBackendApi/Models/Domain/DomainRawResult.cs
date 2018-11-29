using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTBackendApi.Models.Domain
{
    public class DomainRawResult<T>
    {
        public bool HasResult { get; set; }
        public SourceType SourceType { get; set; }
        public T SensorResult { get; set; }
    }
}
