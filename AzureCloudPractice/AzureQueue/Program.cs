using System;
using System.Threading.Tasks;
using Azure.Storage.Queues.Models;
using AzureQueue.AzureQueueServices;

namespace AzureQueueApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"; // Replace with your Azure Storage connection string
                string queueName = "messagequeue";

                AzureQueueService queueService = new AzureQueueService(connectionString, queueName);

                // Check if the queue exists, create if it doesn't
                await queueService.CreateQueueAsync();
                Console.WriteLine($"Queue '{queueName}' created or already exists.");

                bool exit = false;

                do
                {
                    Console.WriteLine("\nChoose an action:");
                    Console.WriteLine("1. Add message to queue");
                    Console.WriteLine("2. Peek messages in the queue");
                    Console.WriteLine("3. Get queue length");
                    Console.WriteLine("4. Receive and process messages from the queue");
                    Console.WriteLine("5. Delete the queue");
                    Console.WriteLine("6. Exit");

                    Console.Write("Enter your choice: ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter message content: ");
                            string messageContent = Console.ReadLine();
                            await queueService.AddMessageAsync(messageContent);
                            Console.WriteLine($"Added message to {queueName}");
                            break;
                        case "2":
                            var peekedMessages = await queueService.PeekMessagesAsync(5);
                            foreach (var message in peekedMessages)
                            {
                                Console.WriteLine($"Peeked Message: {message.MessageText}");
                            }
                            break;
                        case "3":
                            int queueLength = await queueService.GetQueueLengthAsync();
                            Console.WriteLine($"Queue Length: {queueLength}");
                            break;
                        case "4":
                            var receivedMessages = await queueService.ReceiveMessagesAsync(5);
                            foreach (var message in receivedMessages)
                            {
                                Console.WriteLine($"Received Message: {message.MessageText}");
                                await queueService.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                            }
                            break;
                        case "5":
                            await queueService.DeleteQueueAsync();
                            Console.WriteLine("Queue deleted.");
                            break;
                        case "6":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                } while (!exit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}