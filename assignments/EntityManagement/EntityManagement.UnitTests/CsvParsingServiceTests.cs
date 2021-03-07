using EntityManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace EntityManagement.UnitTests
{
    public class CsvParsingServiceTests
    {
        public CsvParsingService Service = new CsvParsingService();

        public CsvParsingServiceTests()
        {
            
        }

        [Fact]
        public void CsvParsingTest()
        {
            using var input = File.OpenRead("input.csv");
            Service.ParseCsvFile(input);
            Assert.True(true);
        }
    }
}
