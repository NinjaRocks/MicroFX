using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using MicroFx.Data.Uow;
using Module = Autofac.Module;

namespace MicroFx.Data
{
    public class AutofacDataModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TransactionInterceptor())
             .InstancePerLifetimeScope();
          
            var callingAssembly = Assembly.GetEntryAssembly();
            
            builder.RegisterAssemblyTypes(callingAssembly)
                .Where(t => typeof(IResource).IsAssignableFrom(t) && !t.IsInterface)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

            base.Load(builder);
        }
    }
}
