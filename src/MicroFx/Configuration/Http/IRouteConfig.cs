using System.Web.Http;

namespace MicroFx.Configuration.Http
{
    public interface IRouteConfig
    {
        void Configure(HttpConfiguration config);
    }
}