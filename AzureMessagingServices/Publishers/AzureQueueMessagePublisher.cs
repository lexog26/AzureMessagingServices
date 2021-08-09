using AzureMessagingServices.Configurations;
using AzureMessagingServices.Core.Messages;
using AzureMessagingServices.Publishers.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureMessagingServices.Publishers
{
    public class AzureQueueMessagePublisher : QueueMessagePublisher
    {
        protected readonly CloudQueueClient QueueClient;
        public AzureQueueMessagePublisher(PublisherSettings queueSettings) : base(queueSettings)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(queueSettings.ConnectionString);
            QueueClient = cloudStorageAccount.CreateCloudQueueClient();
        }

        public override async Task PublishMessageAsync<TBody>(QueueMessage<TBody> queueMessage)
        {
            CloudQueue queue = QueueClient.GetQueueReference(queueMessage.QueueName);

            await queue.CreateIfNotExistsAsync();

            var cloudMessage = new CloudQueueMessage(JsonSerializer.Serialize(queueMessage.Body));

            await queue.AddMessageAsync(cloudMessage);
        }
    }
}
