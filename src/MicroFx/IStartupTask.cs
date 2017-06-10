namespace MicroFx
{
    public interface IStartupTask
    {
        void Start();
        void Stop();
    }

    public interface IStartupMiddleware
    {
        IStartupMiddleware Next(IStartupMiddleware middleware);
    }

    public class StartupBootstrap
    {
        private IStartupMiddleware[] middlewares;

        public void Start()
        {
            var current = middlewares[0];
            foreach (var middleware in middlewares)
            {
                if(middleware == current) continue;
                middleware.Next(null);
            }
        }
    }
}