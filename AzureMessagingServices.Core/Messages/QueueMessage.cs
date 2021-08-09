namespace AzureMessagingServices.Core.Messages
{
    public class QueueMessage<T> : Message<T> where T : class
    {
        public string QueueName { get; set; }
    }
}
