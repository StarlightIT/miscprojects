using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Configuration;
using IoTBackendApi.Models.Domain;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace IoTBackendApi.Services
{
    public class BlobStorageService : IStorageService
    {
        private StorageOptions _options;
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _cloudBlobClient;
        private CloudBlobContainer _cloudBlobContainer;
        private StorageCredentials _storageCredentials;

        public BlobStorageService(IOptions<StorageOptions> options)
        {
            _options = options.Value;
            _storageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
            _cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            _storageCredentials = new StorageCredentials(_options.ConnectionString);
            _cloudBlobContainer = _cloudBlobClient.GetContainerReference("iotbackend");
        }

        public async Task<IEnumerable<string>> GetDevices()
        {
            var blobUris = await GetBlobUrisForContainers(_cloudBlobContainer);
            var devices = GetNamesFromBlobUris(blobUris);
            return devices;   
        }

        public async Task<IEnumerable<string>> GetSensors(string deviceId)
        {
            var cloudBlobDirectory = _cloudBlobContainer.GetDirectoryReference(deviceId);
            var blobUris = await GetContentsOfBlobDirectory(cloudBlobDirectory);
            var sensors = GetNamesFromBlobUris(blobUris);
            return sensors;
        }

        public async Task<IEnumerable<DateTime>> GetAvailableSensorDates(string deviceId, string sensorId)
        {
            var cloudBlobDirectory = _cloudBlobContainer.GetDirectoryReference(deviceId).GetDirectoryReference(sensorId);
            var blobUris = await GetContentsOfBlobDirectory(cloudBlobDirectory);
            var dates = ExtractDatesFromBlobUris(blobUris);
            dates.Sort();
            return dates;
        }

        private List<DateTime> ExtractDatesFromBlobUris(List<string> blobUris)
        {
            var dates = new List<DateTime>();

            foreach (var uri in blobUris)
            {
                var parts = uri.Split("/");
                var last = parts.Last();

                if (last.Contains("zip"))
                {
                    continue;
                }

                var dateRaw = last.Split(".").First();

                var date = DateTime.Parse(dateRaw);
                dates.Add(date);
            }

            return dates;
        }

        public async Task<IEnumerable<SensorData>> GetSensorDataForDate(string deviceId, DateTime date, string sensorId)
        {
            var data = await GetRawSensorData(deviceId, date, sensorId);

            var sensorDataList = new List<SensorData>();

            foreach (var dataLine in data)
            {
                var sensorData = new SensorData { Timestamp = DateTime.Parse(dataLine[0]) };

                for (var i = 1; i < dataLine.Length; i++)
                {
                    if (string.IsNullOrEmpty(dataLine[i]))
                    {
                        continue;
                    }

                    sensorData.SensorReadings.Add("Sensor" + i, double.Parse(dataLine[i]));
                }

                sensorDataList.Add(sensorData);
            }

            return sensorDataList;
        }

        private async Task<IEnumerable<string[]>> GetRawSensorData(string deviceId, DateTime date, string sensorId)
        {
            var cloudBlobDirectory = _cloudBlobContainer.GetDirectoryReference(deviceId).GetDirectoryReference(sensorId);
            var cloudBlob = cloudBlobDirectory.GetBlobReference(date.ToString("yyyy-MM-dd") + ".csv");

            var stream = new MemoryStream();
            await cloudBlob.DownloadToStreamAsync(stream);

            stream.Position = 0;

            string text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd().Trim();
            }
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var data = (from line in lines select line.Split(",")).ToList();

            return data;
        }

        private async Task<List<string>> GetBlobUrisForContainers(CloudBlobContainer container)
        {
            var blobs = new List<string>();

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    var uri = item.Uri.ToString();
                    blobs.Add(uri);
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return blobs;
        }

        private List<string> GetNamesFromBlobUris(List<string> blobUris)
        {
            var names = new List<string>();

            foreach (var uri in blobUris)
            {
                var parts = uri.Split("/");

                names.Add(parts[parts.Length - 2]);
            }

            return names;
        }

        private async Task<List<string>> GetContentsOfBlobDirectory(CloudBlobDirectory cloudBlobDirectory)
        {
            var blobs = new List<string>();

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await cloudBlobDirectory.ListBlobsSegmentedAsync(blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    var uri = item.Uri.ToString();
                    blobs.Add(uri);
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return blobs;
        }
    }
}
