using Amazon;
using Amazon.Runtime;
using JustSaying;
using JustSaying.AwsTools;

namespace MicroFx.Bus.Aws.Impl
{
    public class ServiceBus : IServiceBus
    {
        private readonly IBusPublisher publisher;
        private readonly IBusSubscriber subscriber;

        public ServiceBus(IBusPublisher publisher, IBusSubscriber subscriber)
        {
            this.publisher = publisher;
            this.subscriber = subscriber;
        }

        public void Initialise()
        {
            CreateMeABus.DefaultClientFactory = () => new DefaultAwsClientFactory(new BasicAWSCredentials("accessKey", "secretKey"));

            var bus = CreateMeABus
                .InRegion(RegionEndpoint.EUWest1.SystemName)
                .WithNamingStrategy(() => new NameStrategy());
            
            publisher.WithBus(bus);
            subscriber.WithBus(bus);
        }

        public void ConfigurePointToPoint()
        {
            publisher.RegisterCommands();
            subscriber.RegisterCommandHandlers();
        }
        public void ConfigurePubSub()
        {
            publisher.RegisterEvents();
            subscriber.RegisterEventHandlers();
        }

        public void Start()
        {
            subscriber.Listen();
        }

        public void Stop()
        {
            subscriber.Stop();
        }
    }
}