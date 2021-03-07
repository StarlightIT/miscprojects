using EntityManagement.Models;
using EntityManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Controllers
{
    [Route("[controller]")]
    public class FileUploadController : Controller
    {
        private readonly ICsvParsingService _service;

        public FileUploadController(ICsvParsingService service)
        {
            _service = service;
        }

        [HttpPost("CsvPost")]
        public CompanyResponse CsvPost()
        {
            var stream = Request.Form.Files[0].OpenReadStream();
            var companyResponse = _service.ParseCsvFile(stream);
            return companyResponse;
        }
    }
}
