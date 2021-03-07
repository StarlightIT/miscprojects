using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using static RoadEvents.Program;

namespace RoadEvents.UnitTests
{
    public class RoadEventsTests
    {
        public List<string> Input = new List<string>();
        public List<string> Output = new List<string>();

        public RoadEventsTests()
        {
            Input = File.ReadAllLines("test0.in").ToList();
            Output = File.ReadAllLines("test0.ans").ToList();
        }

        [Fact]
        public void RoadLoadTest()
        {
            LoadTester loadTester = new LoadTester();
            var result = loadTester.CalculateLoadSupport(Input);
            Assert.Equal(Output, result);
        }
    }
}
