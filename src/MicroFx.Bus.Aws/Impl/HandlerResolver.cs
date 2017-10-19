using System.Collections.Generic;
using Autofac;
using JustSaying;
using JustSaying.Messaging.MessageHandling;

namespace MicroFx.Bus.Aws.Impl
{
    public class HandlerResolver : IHandlerResolver
    {
        private readonly ILifetimeScope scope;

        public HandlerResolver(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public IEnumerable<IHandlerAsync<T>> ResolveHandlers<T>()
        {
            yield return scope.Resolve<IHandlerAsync<T>>();
        }
    }
}