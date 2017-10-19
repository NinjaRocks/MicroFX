using System;

namespace MicroFx.Bus
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageQueueAttribute : Attribute
    {
        public MessageQueueAttribute(string queue)
        {
            Queue = queue;
        }

        public string Queue { get; }
    }
}