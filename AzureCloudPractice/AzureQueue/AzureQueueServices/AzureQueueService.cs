using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AzureQueue.AzureQueueServices
{
    public class AzureQueueService
    {
        private QueueClient _queueClient;

        public AzureQueueService(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
        }

        public async Task CreateQueueAsync()
        {
            await _queueClient.CreateIfNotExistsAsync();
        }

        public async Task AddMessageAsync(string messageContent)
        {
            await _queueClient.SendMessageAsync(messageContent);
        }

        public async Task<IEnumerable<PeekedMessage>> PeekMessagesAsync(int numberOfMessages)
        {
            var response = await _queueClient.PeekMessagesAsync(numberOfMessages);
            return response.Value;
        }

        public async Task<int> GetQueueLengthAsync()
        {
            var queueProperties = await _queueClient.GetPropertiesAsync();
            return queueProperties.Value.ApproximateMessagesCount;
        }

        public async Task<IEnumerable<QueueMessage>> ReceiveMessagesAsync(int numberOfMessages)
        {
            var response = await _queueClient.ReceiveMessagesAsync(numberOfMessages);
            return response.Value;
        }

        public async Task UpdateMessageAsync(string messageId, string popReceipt, string newMessageContent)
        {
            await _queueClient.UpdateMessageAsync(messageId, popReceipt, newMessageContent);
        }

        public async Task DeleteMessageAsync(string messageId, string popReceipt)
        {
            await _queueClient.DeleteMessageAsync(messageId, popReceipt);
        }

        public async Task DeleteQueueAsync()
        {
            await _queueClient.DeleteIfExistsAsync();
        }
    }
}
