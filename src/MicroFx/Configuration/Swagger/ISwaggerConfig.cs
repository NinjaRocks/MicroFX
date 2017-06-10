using Swashbuckle.Application;

namespace MicroFx.Configuration.Swagger
{
    public interface ISwaggerConfig
    {
        void ConfigureSwagger(SwaggerDocsConfig config);
        void ConfigureSwaggerUi(SwaggerUiConfig config);
    }
}