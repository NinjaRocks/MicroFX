using System;
using System.Collections.Generic;
using System.Web.Http;
using log4net;
using Microsoft.Owin.Hosting;

namespace MicroFx
{
    public class MicroService
    {
        private readonly IServiceSettings serviceSettings;
        private IDisposable app;

        private readonly List<Func<IStartupTask>> initialiseTasks = new List<Func<IStartupTask>>();

        private static readonly ILog logger = LogManager.GetLogger(typeof(MicroService));

        public MicroService WithStartupTask(Func<IStartupTask> startupTask)
        {
            initialiseTasks.Add(startupTask);
            return this;
        }

        public MicroService(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public void Start()
        {
            var options = new StartOptions
            {
                Port = serviceSettings.Port
            };

            //app = WebApp.Start<Startup>(options);
            app = WebApp.Start(options);
            initialiseTasks.ForEach(task =>
            {
                var init = task();
                init.Start();
            });

            logger.Info($"Service running on port {serviceSettings.Port}");
        }

        public void Stop()
        {
            initialiseTasks.ForEach(task =>
            {
                var init = task();
                init.Stop();
            });

            app.Dispose();
            logger.Info("Service stop .....is run");
        }
    }

    //public class Startup : BaseStartup
    //{
    //    public Startup() : base(new HttpConfiguration())
    //    {
    //    }
    //}
 }