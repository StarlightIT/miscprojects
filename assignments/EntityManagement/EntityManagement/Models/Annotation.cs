using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Models
{
    public class Annotation
    {
        public int From { get; set; }
        public List<int> To { get; set; }
        public string Percentage { get; set; }
    }
}
