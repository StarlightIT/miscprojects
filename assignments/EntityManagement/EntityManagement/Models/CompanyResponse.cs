using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Models
{
    public class CompanyResponse
    {
        public List<Company> Companies { get; set; }
        public List<Annotation> Annotations { get; set; }
    }
}
