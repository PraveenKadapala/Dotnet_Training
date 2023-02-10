
using Azure.Messaging.ServiceBus;
using Azure.Identity;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace ToDoList.Services
{
    public class ServiceBusReceiverService
    {
        static IQueueClient queueClient;
        public void Receiver()
        {

            queueClient = new QueueClient("Endpoint=sb://todolist-dotnet.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=9UOAPNPf83v6qC5IpmKxpNboyhkAYMvebPrAdX5pKZk=", "notification");
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
            Console.ReadLine();
            queueClient.CloseAsync();
        }
        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            Console.WriteLine("Received message:" + jsonString.ToString());
        }
        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler exception: {arg.Exception}");
            return Task.CompletedTask;
        }

    }
}

