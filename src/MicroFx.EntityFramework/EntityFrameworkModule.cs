using Autofac;
using log4net;
using MicroFx.Extensibility;

namespace MicroFx.Data.EntityFramework
{
    public class EntityFrameworkModule: BaseExtensibleModule
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EntityFrameworkModule));

        public override bool Register(IRegisterContext context)
        {
            context.builder.RegisterModule(new AutofacDataModule());
            context.builder.RegisterModule(new AutofacEntityFrameworkModule());

            base.Next(context);

            logger.Info("EF Module initialed...");
            return true;
        }
    }
}
