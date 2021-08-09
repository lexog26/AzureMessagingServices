using AzureMessagingServices.Configurations;
using AzureMessagingServices.Core.Messages;
using AzureMessagingServices.Publishers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureMessagingServices.Publishers
{
    public abstract class QueueMessagePublisher : IQueueMessagePublisher
    {
        public QueueMessagePublisher(PublisherSettings queueSettings)
        {
            Settings = queueSettings;
        }

        public PublisherSettings Settings { get; set; }

        public abstract Task PublishMessageAsync<TBody>(QueueMessage<TBody> queueMessage) where TBody : class;
    }
}
