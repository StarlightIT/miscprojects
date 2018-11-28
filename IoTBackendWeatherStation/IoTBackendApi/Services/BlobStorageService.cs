using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTBackendApi.Models.Configuration;
using IoTBackendApi.Models.Domain;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace IoTBackendApi.Services
{
    public class BlobStorageService : IStorageService
    {
        private StorageOptions _options;
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _cloudBlobClient;
        private CloudBlobContainer _cloudBlobContainer;

        public BlobStorageService(IOptions<StorageOptions> options)
        {
            _options = options.Value;
            _storageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
            _cloudBlobClient = _storageAccount.CreateCloudBlobClient();
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

        public IEnumerable<string> GetSensors()
        {
            throw new NotImplementedException();
        }

        public DateRange GetAvailableDataRanges()
        {
            throw new NotImplementedException();
        }

        public DateRange GetAvailableArchiveDateRanges()
        {
            throw new NotImplementedException();
        }
    }
}
