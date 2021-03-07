using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace UniqueCities.UnitTests
{
    public class UniqueCitiesTests
    {
        public List<string> Input = new List<string>();
        public List<string> Output = new List<string>();

        public UniqueCitiesTests()
        {
            Input = File.ReadAllLines("everywhere-01.in").ToList();
            Output = File.ReadAllLines("everywhere-01.ans").ToList();
        }

        [Fact]
        public void UniqueCityTest()
        {
            var result = Program.GetUniqueCities(Input);
            Assert.Equal(Output, result);
        }
    }
}
