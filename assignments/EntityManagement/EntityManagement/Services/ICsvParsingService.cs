using EntityManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Services
{
    public interface ICsvParsingService
    {
        CompanyResponse ParseCsvFile(Stream input);
    }
}
