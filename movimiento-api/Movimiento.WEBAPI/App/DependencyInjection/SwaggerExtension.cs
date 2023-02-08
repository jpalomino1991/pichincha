using Microsoft.OpenApi.Models;

namespace Movimiento.WebAPI.App.DependencyInjection
{
    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
            });
        }

        public static IApplicationBuilder UseSwaggerApi(this IApplicationBuilder app)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI();
        }
    }
}