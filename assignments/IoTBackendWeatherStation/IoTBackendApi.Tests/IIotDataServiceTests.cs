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
        public async Task Get_Rainfall_For_Date_Should_Return_Correct_Value()
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

        [Test]
        public async Task Get_Sensor_Data_For_Date_Should_Return_Empty_List()
        {
            var sensorResults = await _iotDataService.GetSensorDataForDate(DOCKAN, new DateTime(2010, 01, 01));
            sensorResults.Count().Should().Be(0);
        }

        [Test]
        public async Task Get_Sensor_Data_For_Date_Should_Return_Correct_Results()
        {
            var dateTime = new DateTime(2018, 11, 20);

            var sensorResults = await _iotDataService.GetSensorDataForDate(DOCKAN, dateTime);
            sensorResults.Count().Should().Be(3);
            SensorResult humidityResult = sensorResults.ElementAt(0);
            SensorResult rainfallResult = sensorResults.ElementAt(1);
            SensorResult temperatureResult = sensorResults.ElementAt(2);

            var humidityDataReference = new Dictionary<int, SensorData>
            {
                {
                    9, new SensorData
                    {
                        Timestamp = new DateTime(2018, 11, 20, 00, 00, 45),
                        SensorReadings = new Dictionary<string, double>
                        {
                            { "Sensor1", 28 },
                            { "Sensor2", 99 }
                        }
                    }

                }
            };
            VerifySensorResult(humidityResult, DOCKAN, HUMIDITY, dateTime, 12371, humidityDataReference);

            var rainfallDataReference = new Dictionary<int, SensorData>
            {
                {
                    5196, new SensorData
                    {
                        Timestamp = new DateTime(2018, 11, 20, 09, 54, 05),
                        SensorReadings = new Dictionary<string, double>
                        {
                            { "Sensor2", 5 }
                        }
                    }

                }
            };
            VerifySensorResult(rainfallResult, DOCKAN, RAINFALL, dateTime, 12371, rainfallDataReference);

            var temperatureDataReference = new Dictionary<int, SensorData>
            {
                {
                    12338, new SensorData
                    {
                        Timestamp = new DateTime(2018, 11, 20, 23, 55, 05),
                        SensorReadings = new Dictionary<string, double>
                        {
                            { "Sensor1", 11 },
                            { "Sensor2", 20 }
                        }
                    }

                }
            };
            VerifySensorResult(temperatureResult, DOCKAN, TEMPERATURE, dateTime, 12371, temperatureDataReference);

        }

        /// <summary>
        /// Verifies sensor results for a device on a date
        /// </summary>
        /// <param name="sensorResult">sensorResult</param>
        /// <param name="deviceId">deviceId</param>
        /// <param name="sensorId">sensorId</param>
        /// <param name="date">Date of sensor readings</param>
        /// <param name="sensorDataCount">Reading count for sensor</param>
        /// <param name="sensorDataReference">index of reading, and sensor results for that index</param>
        private void VerifySensorResult(SensorResult sensorResult, string deviceId, string sensorId, DateTime date, 
            int sensorDataCount, Dictionary<int, SensorData> sensorDataReference)
        {
            sensorResult.DeviceId.Should().Be(deviceId);
            sensorResult.SensorId.Should().Be(sensorId);
            sensorResult.Date.Should().Be(date);
            sensorResult.SensorData.Count().Should().Be(sensorDataCount);

            foreach (var data in sensorDataReference)
            {
                var sensorData = sensorResult.SensorData.ElementAt(data.Key);
                VerifySensorReadings(sensorData, data.Value.Timestamp, data.Value.SensorReadings);
            }
        }

        /// <summary>
        /// Verify SensorData
        /// </summary>
        /// <param name="sensorData">sensorData to verify</param>
        /// <param name="timestamp">Timestamp of readings</param>
        /// <param name="sensorReadings">Sensor readings for sensors at timestamp</param>
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
