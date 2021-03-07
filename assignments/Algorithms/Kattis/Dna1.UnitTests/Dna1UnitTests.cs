using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Dna1.UnitTests
{
    public class Dna1UnitTests
    {
        public List<string> Input = new List<string>();
        public List<string> Output = new List<string>();

        public Dna1UnitTests()
        {
            Input = File.ReadAllLines("sample.in").ToList();
            Output = File.ReadAllLines("sample.ans").ToList();
        }

        [Fact]
        public void AlgorithmTest()
        {
            List<string> sampleOutput = Program.CompareStrings(Input);
            Assert.Equal(Output, sampleOutput);
        }
    }
}
