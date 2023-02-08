using Account.WEBAPI.App.GlobalConfig;

namespace Account.WebAPI.App.GlobalConfig
{
    public static class ServiceGlobalExtensions
    {
        public static void AddGlobalSettings(this IServiceCollection services)
        {
            services.AddSingleton<IGlobalSettings, GlobalSettings>();
        }
    }
}