using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace IoTBackendApi.Tests
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class IIotDataServiceTests : IntegrationTests
    {
        [Test]
        public async Task Get_Devices_Should_Return_Correct_Value()
        {
            IEnumerable<string> devices = await _iotDataService.GetDevices();
            devices.Count().Should().Be(1);
            devices.ElementAt(0).Equals("dockan");
        }

        [Test]
        public async Task Get_Temperature_For_Date_Should_Return_Correct_Value()
        {
            var temperature = await _iotDataService.GetTemperature("dockan", new DateTime(2018, 11, 22));
        }
    }
}
