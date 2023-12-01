using Microsoft.Azure.Storage.Queue;
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var functionUrl = "http://localhost:7071/api/MyHttpTrigger";

        Console.WriteLine("Enter your name:");
        var name = Console.ReadLine();

        var response = await httpClient.GetAsync($"{functionUrl}?name={name}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
            string queueName = "myqueue-items";

            Microsoft.Azure.Storage.CloudStorageAccount storageAccount = Microsoft.Azure.Storage.CloudStorageAccount.Parse(connectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);
            queue.CreateIfNotExists();

            Console.WriteLine("Enter a message to enqueue:");
            string message = Console.ReadLine();

            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            queue.AddMessage(queueMessage);

            Console.WriteLine($"Message '{message}' added to the queue '{queueName}'");
        }
        else
        {
            Console.WriteLine($"Failed to call the function. Status code: {response.StatusCode}");
        }

        httpClient.Dispose();
        Console.ReadLine();
    }
}
