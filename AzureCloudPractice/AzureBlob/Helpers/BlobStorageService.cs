using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlob.Helpers
{
    public class BlobStorageService
    {
        private BlobServiceClient _blobServiceClient;

        public BlobStorageService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadFileToBlobAsync(string containerName, string blobName, string localFilePath)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            Console.WriteLine("Uploading to Blob storage...");
            using FileStream uploadFileStream = File.OpenRead(localFilePath);
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();

            Console.WriteLine("File uploaded to Blob storage.");
        }

        public async Task DownloadBlobToFileAsync(string containerName, string blobName, string downloadFilePath)
        {
            try
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                Console.WriteLine("Downloading from Blob storage...");
                BlobDownloadInfo download = await blobClient.DownloadAsync();

                using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
                {
                    await download.Content.CopyToAsync(downloadFileStream);
                }

                Console.WriteLine("File downloaded from Blob storage.");
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error downloading blob: {ex.Message}");
                // Handle exception as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Handle exception as needed
            }
        }

        public async Task ListBlobsInContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine($"Blob: {blobItem.Name}");
            }
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
            Console.WriteLine("Blob deleted from Blob storage.");
        }
    }
}
