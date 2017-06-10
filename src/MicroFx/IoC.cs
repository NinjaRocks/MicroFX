using System;
using System.Collections.Generic;
using Autofac;

namespace MicroFx
{
    public static class IoC
    {
        private static IContainer container;
        private static readonly object localContainerKey = new object();


        public static void Initialize(IContainer globalContainer)
        {
            GlobalContainer = globalContainer;
        }

        public static object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }
        
       
        public static T TryResolve<T>()
        {
            return TryResolve(default(T));
        }

        public static T TryResolve<T>(T defaultValue)
        {
            if (Container.Resolve(typeof(T)) == null)
                return defaultValue;

            return Container.Resolve<T>();
        }

        
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        
       
        public static IContainer Container
        {
            get
            {
                var result = LocalContainer ?? GlobalContainer;
                if (result == null)

                    throw new InvalidOperationException("The container has not been initialized!");

                return result;
            }
        }

        private static IContainer LocalContainer
        {
            get
            {
                if (LocalContainerStack.Count == 0)
                    return null;

                return LocalContainerStack.Peek();
            }
        }

        private static Stack<IContainer> LocalContainerStack
        {
            get
            {
                var stack = Local.Data[localContainerKey] as Stack<IContainer>;

                if (stack == null)
                    Local.Data[localContainerKey] = stack = new Stack<IContainer>();

                return stack;
            }
        }
        
        public static bool IsInitialized
        {
            get { return GlobalContainer != null; }
        }

        internal static IContainer GlobalContainer
        {
            get
            {
                return container;
            }
            set
            {
                container = value;
            }
        }

        public static IDisposable UseLocalContainer(IContainer localContainer)
        {
            LocalContainerStack.Push(localContainer);
            return new DisposableAction(() => Reset(localContainer));

        }

        public static void Reset(IContainer containerToReset)
        {
            if (containerToReset == null)
                return;
            if (ReferenceEquals(LocalContainer, containerToReset))
            {
                LocalContainerStack.Pop();
                if (LocalContainerStack.Count == 0)
                    Local.Data[localContainerKey] = null;

                return;
            }
            if (ReferenceEquals(GlobalContainer, containerToReset))
            {
                GlobalContainer = null;

            }
        }

        public static void Reset()
        {
            var thisContainer = LocalContainer ?? GlobalContainer;
            Reset(thisContainer);
        }
    }

    public class DisposableAction : IDisposable
    {
        private Action action;

        public DisposableAction(Action action)
        {
            this.action = action;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (action != null)
                action = () => Dispose();
        }

        #endregion
    }
}
