using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManagement.Models
{
    public class FileUploadModel
    {
        public IFormFile CsvFile { get; set; }
    }
}
