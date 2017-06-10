using MicroFx;
using MicroFx.Data.Migration;
using MicroService.Library.Properties;
using Topshelf;

namespace MicroService.Library
{
    class Program
    {
        private static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(c =>
            {
                var settings = new ServiceSettings(Settings.Default);

                c.Service<MicroFx.MicroService>(service =>
                {
                    var s = service;

                    s.ConstructUsing
                        (() => new MicroFx.MicroService(settings)
                                          .WithStartupTask(() => new DbMigrator())
                        );

                    s.WhenStarted(a => a.Start());
                    s.WhenStopped(a => a.Stop());
                });

                c.SetServiceName(settings.ServiceName);
                c.SetDisplayName(settings.ServiceName);
                c.SetDescription(settings.Description);

                c.StartAutomatically();
                c.RunAsLocalSystem();

               // c.DependsOnEventLog();
               // c.DependsOnMsSql();
            });

            return (int) exitCode;
        }

       
    }
}
