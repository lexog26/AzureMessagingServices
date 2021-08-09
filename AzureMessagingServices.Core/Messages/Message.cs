using System;
using System.Collections.Generic;
using System.Text;

namespace AzureMessagingServices.Core.Messages
{
    public abstract class Message<T> where T : class
    {
        public T Body { get; set; }
    }
}
