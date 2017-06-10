using System;
using System.Collections.Generic;
using Amazon;
using Amazon.Runtime;
using JustSaying;
using JustSaying.AwsTools;
using JustSaying.AwsTools.QueueCreation;
using JustSaying.Messaging.MessageHandling;
using JustSaying.Models;

namespace MicroFx.Aws.Bus
{
    public class Configuration
    {
        public void Configure()
        {
            CreateMeABus.DefaultClientFactory = () =>
                new DefaultAwsClientFactory(new BasicAWSCredentials("accessKey", "secretKey"));

            var settings = CreateMeABus.InRegion(RegionEndpoint.EUWest1.SystemName)
                .ConfigurePublisherWith(c =>
                {
                    c.PublishFailureReAttempts = 3;
                    c.PublishFailureBackoffMilliseconds = 50;
                })
                .WithSnsMessagePublisher<OrderAccepted>();

            settings
                .WithSqsTopicSubscriber()
                .IntoQueue(Service.GetName())
                .WithMessageHandler(new OrderNotifier())
                .StartListening();

            //settings.WithNamingStrategy(()=> new QueueNaming())

            //  var loggerFactory = new LoggerFactory();
            //var publisher = CreateMeABus.WithLogging(loggerFactory)
            //     .InRegion(RegionEndpoint.EUWest1.SystemName)
            //     .WithSnsMessagePublisher<OrderAccepted>();
        }
    }

    public class QueueNaming : INamingStrategy
    {
        public string GetTopicName(string topicName, string messageType)
        {
            throw new NotImplementedException();
        }

        public string GetQueueName(SqsReadConfiguration sqsConfig, string messageType)
        {
            throw new NotImplementedException();
        }
    }

    public class HandlerResolver : IHandlerResolver
    {
        public IEnumerable<IHandlerAsync<T>> ResolveHandlers<T>()
        {
            throw new NotImplementedException();
        }
    }
    public class BusConfiguration: IBusConfiguration
    {
        public IMayWantOptionalSettings GetConfiguration()
        {
            return null;
        }
    }

    public interface IBusConfiguration
    {
    }

    public class OrderNotifier : IHandler<OrderAccepted>
    {
        public bool Handle(OrderAccepted message)
        {
            // Some logic here ...
            // e.g. bll.NotifyRestaurantAboutOrder(message.OrderId);
            return true;
        }
    }
    public class OrderAccepted : Message
    {
        public OrderAccepted(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; private set; }
    }
}
