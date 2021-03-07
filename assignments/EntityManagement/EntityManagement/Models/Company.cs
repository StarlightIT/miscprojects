using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string OrganizationalNumber { get; set; }
        public string InternalCompanyName { get; set; }
        public string CompanyType { get; set; }
        public bool Acquisition { get; set; }
        public DateTimeOffset AcquisitionRegistrationDate { get; set; }
        public string BoardSeat { get; set; }
        public List<string> BoardMembers { get; set; }
        public string Auditor { get; set; }
        public List<int> Parents { get; set; }
        public List<CompanyOwnership> ParentCompanies { get; set; }
        public List<CompanyOwnership> Subsidiaries { get; set; }
        public string AdministeredBy { get; set; }
    }
}
