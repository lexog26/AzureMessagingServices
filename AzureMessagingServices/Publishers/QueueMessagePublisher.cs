using AzureMessagingServices.Configurations;
using AzureMessagingServices.Core.Messages;
using AzureMessagingServices.Publishers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureMessagingServices.Publishers
{
    public abstract class QueueMessagePublisher : IMessagePublisher
    {
        public QueueMessagePublisher(QueueSettings queueSettings)
        {
            Settings = queueSettings;
        }

        public QueueSettings Settings { get; set; }

        public async Task PublishMessageAsync<TBody>(Message<TBody> message) where TBody : class
        {
            await PublishMessageAsync(new QueueMessage<TBody> { Body = message.Body, QueueName = Settings.QueueName });
        }

        public abstract Task PublishMessageAsync<TBody>(QueueMessage<TBody> queueMessage) where TBody : class;
    }
}
