using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Models
{
    public class CompanyOwnership
    {
        public int Id { get; set; }
        public string OrganizationalNumber { get; set; }
        public double Percentage { get; set; }
        public string PercentageString { get; set; }
    }
}
