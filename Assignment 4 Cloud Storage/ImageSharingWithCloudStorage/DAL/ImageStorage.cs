using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ImageSharingWithCloudStorage.DAL
{
    public class ImageStorage : IImageStorage
    {

        public const string ACCOUNT = "imagesharing";
        public const string CONTAINER = "images";

        protected readonly IWebHostEnvironment hostingEnvironment;

        protected ILogger<ImageStorage> logger;

        protected BlobServiceClient blobServiceClient;

        protected BlobContainerClient containerClient;


        public ImageStorage(IOptions<StorageOptions> storageOptions,
                            IWebHostEnvironment hostingEnvironment,
                            ILogger<ImageStorage> logger)
        {
            this.logger = logger;

            this.hostingEnvironment = hostingEnvironment;

            string connectionString = storageOptions.Value.ImageDb;

            logger.LogInformation("Using remote blob storage: "+connectionString);

            blobServiceClient = new BlobServiceClient(connectionString);

            containerClient = new BlobContainerClient(connectionString, CONTAINER);
        }

        /**
         * The name of a blob containing a saved image (id is key for metadata record).
         */
        protected static string BlobName (int imageId)
        {
            return "image-" + imageId + ".jpg";
        }

        protected string BlobUri (int imageId)
        {
            // TODO check this for correctness
            return containerClient.Uri + "/" + BlobName(imageId);
        }


        public async Task SaveFileAsync(IFormFile imageFile, int imageId)
        {
                logger.LogInformation("Saving image {0} to blob storage", imageId);

                BlobHttpHeaders headers = new BlobHttpHeaders();
                headers.ContentType = "image/jpeg";

                var dataDir = Path.Combine(hostingEnvironment.WebRootPath,
               "data", "images");
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                
                // TODO upload data to blob storage

                string fileName = BlobName(imageId);
                string localFilePath = Path.Combine(dataDir, fileName);

                using (var stream = System.IO.File.Create(localFilePath))
                {
                    await imageFile.CopyToAsync(stream);
                }

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                using (var stream = System.IO.File.OpenRead(localFilePath)){
                    await blobClient.UploadAsync(stream, true);
                }

        }

        public string ImageUri(IUrlHelper urlHelper, int imageId)
        {
             return BlobUri(imageId);
        }


         public async Task DeleteFileAsync(int imageId)
         {

            logger.LogInformation("Deleting image {0} from blob storage", imageId);

            var dataDir = Path.Combine(hostingEnvironment.WebRootPath,
               "data", "images");

            string fileName = BlobName(imageId);
            string localFilePath = Path.Combine(dataDir, fileName);


            File.Delete(localFilePath);

            await containerClient.DeleteBlobIfExistsAsync(fileName);


        }

    }
}