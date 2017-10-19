using System.Collections.Generic;
using System.Linq;
using Autofac;
using JustSaying;
using JustSaying.Models;
using MicroFx.Bus.Publisher;

namespace MicroFx.Bus.Aws.Impl
{
    public class Subscriber : IBusSubscriber
    {
        private readonly ILifetimeScope scope;
        private IMayWantOptionalSettings bus;

        private IHaveFulfilledPublishRequirements settings;

        public Subscriber(ILifetimeScope scope)
        {
            this.scope = scope;
        }
        public void WithBus(IMayWantOptionalSettings bus1)
        {
            this.bus = bus1;
        }
        public void Listen()
        {
            settings.StartListening();
        }

        public void Stop()
        {
            settings.StopListening();
        }

        public void RegisterCommandHandlers()
        {
            var messages = scope.Resolve<IEnumerable<ICommand>>()?.ToList();
            if (messages != null && messages.Any())
            {
                var sqsSetting = bus.WithSqsPointToPointSubscriber()
                    .IntoQueue(Service.GetName())
                    .ConfigureSubscriptionWith(c => c.QueueName = Service.GetName());

                messages.ForEach(m => RegisterHandler(m as Message, sqsSetting));
            }
        }

        private void RegisterHandler<T>(T obj, IFluentSubscription subscription) where T : Message
        {
            subscription.WithMessageHandler<T>(new HandlerResolver(scope));
        }

        public void RegisterEventHandlers()
        {
            var messages = scope.Resolve<IEnumerable<IEvent>>()?.ToList();
            if (messages != null && messages.Any())
            {
                var sqsSetting = bus.WithSqsTopicSubscriber()
                    .IntoQueue(Service.GetName());
                
                messages.ForEach(m => RegisterHandler(m as Message, sqsSetting));
            }
        }
    }
}