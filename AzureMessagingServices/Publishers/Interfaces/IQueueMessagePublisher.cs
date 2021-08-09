using AzureMessagingServices.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureMessagingServices.Publishers.Interfaces
{
    public interface IQueueMessagePublisher
    {
        Task PublishMessageAsync<TBody>(QueueMessage<TBody> message) where TBody : class;
    }
}
