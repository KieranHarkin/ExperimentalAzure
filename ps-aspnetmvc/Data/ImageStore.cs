using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ps_aspnetmvc.Data
{
    public class ImageStore
    {
        Uri _baseUri = new Uri("https://hakopscoursestore.blob.core.windows.net/");
        CloudBlobClient _client;

        public ImageStore()
        {
            _client = new CloudBlobClient(_baseUri, new StorageCredentials("hakopscoursestore", "7SmKRHklsSzNyXs3IM78/O1njwOq93Dvqd8bj2HQPnJX6f4ikI45+Ll68wo17AYmCU02tE/DzH/Y6EyGXkOysg=="));
        }

        public async Task<string> SaveImage(Stream stream)
        {
            var id = Guid.NewGuid().ToString();            
            await GetBlobReference(id).UploadFromStreamAsync(stream);
            return id;
        }

        public Uri UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.Now.AddMinutes(-60),
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30)
            };

            var sasToken = GetBlobReference(imageId).GetSharedAccessSignature(sasPolicy);

            return new Uri(_baseUri, $"/images/{imageId}{sasToken}");
        }

        private CloudBlockBlob GetBlobReference(string id)
        {
            var container = _client.GetContainerReference("images");
            return container.GetBlockBlobReference(id);
        }
    }
}