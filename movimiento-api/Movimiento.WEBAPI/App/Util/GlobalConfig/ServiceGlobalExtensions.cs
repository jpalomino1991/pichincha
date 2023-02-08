using Movimiento.WEBAPI.App.GlobalConfig;

namespace Movimiento.WebAPI.App.GlobalConfig
{
    public static class ServiceGlobalExtensions
    {
        public static void AddGlobalSettings(this IServiceCollection services)
        {
            services.AddSingleton<IGlobalSettings, GlobalSettings>();
        }
    }
}