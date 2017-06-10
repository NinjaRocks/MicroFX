using System;
using System.Configuration;
using System.IO;
using DbUp;
using log4net;

namespace MicroFx.Data.Migration
{
    public class DbMigrator : IStartupTask
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DbMigrator));

        public void Start()
        {
            logger.Info("Db migration started..");

            var connectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;

            var path = AppDomain.CurrentDomain.BaseDirectory + @"\DbScripts";

            var upgrader =
                 DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithTransaction()
                    .WithScriptsFromFileSystem(Path.Combine(path, "Schema"))
                    .WithScriptsFromFileSystem(Path.Combine(path, "Data"))
                  //  .WithScripts(new ScriptProvider("Schema"))
                  //  .WithScripts(new ScriptProvider("Data"))
                    //.JournalTo(new CustomJournal())
                    .LogTo(new Log4NetProvider())
                    .Build();
            try
            {
                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    logger.Error(result.Error);
                    Environment.ExitCode = -1;
                    return;
                }

                logger.Info("Db Update Successful!");
                Environment.ExitCode = 0;
            }
            catch (Exception e)
            {
                logger.Error(e);
                Environment.ExitCode = -1;
            }
        }

        public void Stop()
        {
            
        }
    }
}
