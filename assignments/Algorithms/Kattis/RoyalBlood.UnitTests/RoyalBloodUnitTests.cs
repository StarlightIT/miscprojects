using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using RoyalBlood;

namespace RoyalBlood.UnitTests
{
    public class RoyalBloodUnitTests
    {
        public List<string> Input1 = new List<string>();
        public List<string> Output1 = new List<string>();

        public List<string> Input2 = new List<string>();
        public List<string> Output2 = new List<string>();

        public RoyalBloodUnitTests()
        {
            Input1 = File.ReadAllLines("1.in").ToList();
            Output1 = File.ReadAllLines("1.ans").ToList();

            Input2 = File.ReadAllLines("2.in").ToList();
            Output2 = File.ReadAllLines("2.ans").ToList();
        }

        [Fact]
        public void DataSet1Test()
        {
            var result = Program.FindHeir(Input1);
            Assert.Equal("elena", result);
        }

        [Fact]
        public void DataSet2Test()
        {
            var result = Program.FindHeir(Input2);
            Assert.Equal("matthew", result);
        }

    }
}
