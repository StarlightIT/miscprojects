using System;
using System.Collections.Generic;
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
            var blobs = new List<string>();

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await _cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    var uri = item.Uri.ToString();
                    var parts = uri.Split("/");

                    blobs.Add(parts[parts.Length-2]);
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return blobs;   
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


        public IEnumerable<string> GetSensors()
        {
            throw new NotImplementedException();
        }

        public async Task<DateRange> GetAvailableDataRanges(string deviceId, string sensorId)
        {
            var cloudBlobDirectory = _cloudBlobContainer.GetDirectoryReference(deviceId).GetDirectoryReference(sensorId);
            var blobUris = await GetContentsOfBlobDirectory(cloudBlobDirectory);

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

            dates.Sort();
            return new DateRange
            {
                StartDate = dates.First(),
                EndDate = dates.Last(),
            };
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

        public async Task<DateRange> GetAvailableArchiveDateRanges(string deviceId, string sensorId)
        {
            var cloudBlobDirectory = _cloudBlobContainer.GetDirectoryReference(deviceId).GetDirectoryReference(sensorId);
            var cloudBlockBlob = cloudBlobDirectory.GetBlockBlobReference("historical.zip");

            return new DateRange
            {
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MinValue,
            };
        }
    }
}
