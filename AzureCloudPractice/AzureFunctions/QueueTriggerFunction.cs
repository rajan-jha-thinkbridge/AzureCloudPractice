using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctions
{
    public class QueueTriggerFunction
    {
        [FunctionName("QueueTriggerFunction")]
        public void Run([QueueTrigger("myqueue-items", Connection = "MyStorageConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }

    }
}
