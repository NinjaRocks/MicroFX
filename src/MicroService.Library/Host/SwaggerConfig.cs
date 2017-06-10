using log4net;
using MicroFx;
using MicroFx.Configuration.Swagger;
using Swashbuckle.Application;

namespace MicroService.Library.Host
{
    public class SwaggerConfig : ISwaggerConfig
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SwaggerConfig));

        public void ConfigureSwagger(SwaggerDocsConfig config)
        {
            config.SingleApiVersion("v1", Service.GetName());

            logger.Debug("Swagger doc config completed ...");
        }
        public void ConfigureSwaggerUi(SwaggerUiConfig config)
        {
            logger.Debug("Swagger ui config completed ...");
        }
    }
}