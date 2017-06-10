using DbUp.Engine.Output;
using log4net;

namespace MicroFx.Data.Migration
{
    public class Log4NetProvider : IUpgradeLog
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Log4NetProvider));

        public void WriteInformation(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            logger.ErrorFormat(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }
    }
}