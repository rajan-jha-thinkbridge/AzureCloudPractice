using AzureBlob.Helpers;

internal class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        string containerName = "blobservice";
        string blobName = "Candidate-engagement.docx";
        string localFilePath = "C:\\Git Repo\\AzureCloudPractice\\AzureBlob\\filestoupload\\Candidate Engagement.docx";
        string downloadFilePath = "C:\\Git Repo\\AzureCloudPractice\\AzureBlob\\downloads\\Candidate-engagement.docx";

        BlobStorageService blobStorageService = new BlobStorageService(connectionString);

        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Upload file to Blob");
        Console.WriteLine("2. Download blob to file");
        Console.WriteLine("3. List blobs in container");
        Console.WriteLine("4. Delete a blob");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                await blobStorageService.UploadFileToBlobAsync(containerName, blobName, localFilePath);
                break;
            case 2:
                await blobStorageService.DownloadBlobToFileAsync(containerName, blobName, downloadFilePath);
                break;
            case 3:
                await blobStorageService.ListBlobsInContainerAsync(containerName);
                break;
            case 4:
                Console.WriteLine("Enter the name of the blob to delete:");
                string blobToDelete = Console.ReadLine();
                await blobStorageService.DeleteBlobAsync(containerName, blobToDelete);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}