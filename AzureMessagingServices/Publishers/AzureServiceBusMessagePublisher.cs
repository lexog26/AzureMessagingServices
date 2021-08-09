using Azure.Messaging.ServiceBus;
using AzureMessagingServices.Configurations;
using AzureMessagingServices.Core.Messages;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureMessagingServices.Publishers
{
    public class AzureServiceBusMessagePublisher : QueueMessagePublisher, IAsyncDisposable
    {
        private ServiceBusClient _serviceBusClient;

        public AzureServiceBusMessagePublisher(PublisherSettings queueSettings) : base(queueSettings)
        {
            _serviceBusClient = new ServiceBusClient(queueSettings.ConnectionString);
        }

        public ValueTask DisposeAsync()
        {
            return _serviceBusClient.DisposeAsync();
        }

        public override async Task PublishMessageAsync<TBody>(QueueMessage<TBody> queueMessage)
        {
            ServiceBusSender serviceBusSender = _serviceBusClient.CreateSender(queueMessage.QueueName);

            using (ServiceBusMessageBatch messageBatch = await serviceBusSender.CreateMessageBatchAsync())
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(JsonSerializer.Serialize(queueMessage.Body))))
                {
                    throw new InvalidOperationException($"The message is too large to fit in the batch.");
                }
                try
                {
                    await serviceBusSender.SendMessagesAsync(messageBatch);
                }
                finally
                {
                    await serviceBusSender.DisposeAsync();
                }
            }
        }
    }
}
