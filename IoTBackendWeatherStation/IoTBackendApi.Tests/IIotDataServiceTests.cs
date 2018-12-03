using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IoTBackendApi.Models.Domain;
using NUnit.Framework;

namespace IoTBackendApi.Tests
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class IIotDataServiceTests : IntegrationTests
    {
        private const string DOCKAN = "dockan";
        private const string HUMIDITY = "humidity";
        private const string RAINFALL = "rainfall";
        private const string TEMPERATURE = "temperature";

        [Test]
        public async Task Get_Devices_Should_Return_Correct_Value()
        {
            IEnumerable<string> devices = await _iotDataService.GetDevices();
            devices.Count().Should().Be(1);
            devices.ElementAt(0).Equals("dockan");
        }

        [Test]
        public async Task Get_Available_Sensors_For_Device()
        {
            List<string> sensors = (await _iotDataService.GetSensors(DOCKAN)).ToList();
            sensors.Capacity.Should().Be(3);
            sensors[0].Should().Be("humidity");
            sensors[1].Should().Be("rainfall");
            sensors[2].Should().Be("temperature");
        }

        [TestCase(HUMIDITY)]
        [TestCase(RAINFALL)]
        [TestCase(TEMPERATURE)]
        public async Task Get_Sensors_For_Date_Should_Return_Null_For_Nonexistant_Date(string sensorId)
        {
            var sensorData = await _iotDataService.GetSensorDataForDateAndSensor(DOCKAN, new DateTime(2010, 01, 01), sensorId);
            sensorData.Should().BeNull();
        }

        [Test]
        public async Task Get_Humidity_For_Date_Should_Return_Correct_Value()
        {
            var humidity = await _iotDataService.GetSensorDataForDateAndSensor(DOCKAN, new DateTime(2018, 09, 25), HUMIDITY);
            humidity.SensorId.Should().Be(HUMIDITY);
            humidity.SensorData.Count().Should().Be(12186);
            var fifth = humidity.SensorData.ElementAt(4);
            var fifthLast = humidity.SensorData.ElementAt(humidity.SensorData.Count() - 5);

            var sensorData1 = new Dictionary<string, double>
            {
                { "Sensor1", 49.0 },
                { "Sensor2", 11.0 },
            };
            VerifySensorReadings(fifth, new DateTime(2018, 09, 25, 00, 01, 35), sensorData1);

            var sensorData2 = new Dictionary<string, double>
            {
                { "Sensor1", 49.0 },
                { "Sensor2", 26.0 },
            };
            VerifySensorReadings(fifthLast, new DateTime(2018, 09, 25, 23, 59, 30), sensorData2);
        }

        [Test]
        public async Task Get_Rainfall_For_Date_Should_REturn_Correct_Value()
        {
            var rainfall = await _iotDataService.GetSensorDataForDateAndSensor(DOCKAN, new DateTime(2018, 11, 18), RAINFALL);
            rainfall.SensorId.Should().Be(RAINFALL);
            rainfall.SensorData.Count().Should().Be(12181);
            var eighth = rainfall.SensorData[7];
            var fifthLast = rainfall.SensorData[rainfall.SensorData.Count - 5];

            var sensorData1 = new Dictionary<string, double>
            {
                { "Sensor2", 3.0 },
            };
            VerifySensorReadings(eighth, new DateTime(2018, 11, 18, 00, 00, 35), sensorData1);

            var sensorData2 = new Dictionary<string, double>
            {
                { "Sensor2", 4.0 },
            };
            VerifySensorReadings(fifthLast, new DateTime(2018, 11, 18, 23, 59, 30), sensorData2);
        }

        [Test]
        public async Task Get_Temperature_For_Date_Should_Return_Correct_Value()
        {
            var temperatures = await _iotDataService.GetSensorDataForDateAndSensor(DOCKAN, new DateTime(2018, 11, 22), TEMPERATURE);
            temperatures.SensorId.Should().Be(TEMPERATURE);
            temperatures.SensorData.Count().Should().Be(12052);
            var firstReading = temperatures.SensorData.First();
            var lastReading = temperatures.SensorData.Last();

            var sensorData1 = new Dictionary<string, double>
            {
                { "Sensor1", 10.0 },
                { "Sensor2", 62.0 },
            };
            VerifySensorReadings(firstReading, new DateTime(2018, 11, 22, 00, 00, 00), sensorData1);

            var sensorData2 = new Dictionary<string, double>
            {
                { "Sensor1", 10.0 },
                { "Sensor2", 63.0 },
            };
            VerifySensorReadings(lastReading, new DateTime(2018, 11, 22, 23, 59, 50), sensorData2);
        }

        private void VerifySensorReadings(SensorData sensorData, DateTime timestamp, Dictionary<string, double> sensorReadings)
        {
            sensorData.Timestamp.Should().Be(timestamp);
            sensorData.SensorReadings.Count().Should().Be(sensorReadings.Count());

            foreach (var entry in sensorData.SensorReadings)
            {
                entry.Value.Should().Be(sensorReadings[entry.Key]);
            }
        }
    }
}
