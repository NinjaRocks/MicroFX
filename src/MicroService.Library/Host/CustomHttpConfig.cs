using System;
using System.Web.Http;
using MicroFx.Configuration.Http;

namespace MicroService.Library.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomHttpConfig: IRouteConfig
    {
        public void Configure(HttpConfiguration config)
        {
            Console.WriteLine("custom route module ...has run");
        }
    }
}
