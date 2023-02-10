

using Azure.Messaging.ServiceBus;

namespace ToDoList.Services
{
    public class ServiceBusSenderService
    {

        public async void Sender(string message)
        {
            ServiceBusClient client;

            ServiceBusSender sender;


            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient("Endpoint=sb://todolist-dotnet.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=9UOAPNPf83v6qC5IpmKxpNboyhkAYMvebPrAdX5pKZk=", clientOptions);
            sender = client.CreateSender("notification");

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
            {
                throw new Exception($"The message is too large to fit in the batch.");
            }

            try
            {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A message {message} has been published to the queue.");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }

    }
}

