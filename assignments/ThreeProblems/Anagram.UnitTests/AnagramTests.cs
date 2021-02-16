using Anagram.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Anagram.UnitTests
{
    public class AnagramTests
    {
        private AnagramService Service = new AnagramService();

        [Fact]
        public void AnagramTest1()
        {
            bool result = Service.CheckAnagram("army", "mary");
            Assert.True(result);
        }
    }
}
