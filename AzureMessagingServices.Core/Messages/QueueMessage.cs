namespace AzureMessagingServices.Core.Messages
{
    public class QueueMessage<T> where T : class
    {
        public T Body { get; set; }

        public string QueueName { get; set; }
    }
}
