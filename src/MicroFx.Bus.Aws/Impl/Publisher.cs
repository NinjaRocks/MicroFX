using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using JustSaying;
using JustSaying.Models;
using log4net;
using MicroFx.Bus.Publisher;

namespace MicroFx.Bus.Aws.Impl
{
    public class Publisher : IBusPublisher
    {
        private readonly ILifetimeScope scope;

        private IMayWantOptionalSettings settings;

        private readonly ILog logger = LogManager.GetLogger(typeof (Publisher));

        public Publisher(ILifetimeScope scope)
        {
            this.scope = scope;
            logger.Debug("Created new instance of publisher");
        }

        public bool Publish<T>(T message) where T : IMessage
        {
            var msg = message as Message;

            if (msg == null)
                throw new ArgumentException("Unsupported message. Not an instance of JustSaying.Models.Message type.");

            var success = true;
            try
            {
                settings.Publish(msg);
                logger.Info("Message published success");
            }
            catch (Exception e)
            {
                logger.Info("Message published failed");
                logger.Error(e);
                success = false;
            }
            logger.Debug("Published message with instance of publisher");

            return success;
        }

        public void WithBus(IMayWantOptionalSettings bus)
        {
            settings = bus;
        }


        public void RegisterCommands()
        {
            var messages = scope.Resolve<IEnumerable<ICommand>>()?.ToList();
            if (messages!=null && messages.Any())
                messages.ForEach(m => RegisterCommand(m as Message));

            logger.Debug("Registered commands with instance of publisher");
        }

        private void RegisterCommand<T>(T obj) where T : Message
        {
            settings.WithSqsMessagePublisher<T>(c => c.QueueName = Service.GetName());
        }

        public void RegisterEvents()
        {
            var messages = scope.Resolve<IEnumerable<IEvent>>()?.ToList();
            if (messages != null && messages.Any())
                if (messages.Any())
                messages.ForEach(m => RegisterEvent(m as Message));

            logger.Debug("Registered events with instance of publisher");
        }


        private void RegisterEvent<T>(T obj) where T : Message
        {
            settings.WithSnsMessagePublisher<T>();
        }

    }
}