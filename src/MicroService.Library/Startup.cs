using System.Web.Http;
using log4net;
using MicroFx;
using MicroFx.Data.EntityFramework;

namespace MicroService.Library
{
    public class Startup : BaseStartup
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Startup));

        public Startup() : base(new HttpConfiguration())
        {
            logger.Info("Startup started....");

            Use(new EntityFrameworkModule());
        }
    }   
}

