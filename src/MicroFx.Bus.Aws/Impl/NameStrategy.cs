using JustSaying;
using JustSaying.AwsTools.QueueCreation;

namespace MicroFx.Bus.Aws.Impl
{
    public class NameStrategy : INamingStrategy
    {
        public string GetTopicName(string topicName, string messageType)
        {
            return messageType;
        }

        public string GetQueueName(SqsReadConfiguration sqsConfig, string messageType)
        {
            return Service.GetName();
        }
    }
}