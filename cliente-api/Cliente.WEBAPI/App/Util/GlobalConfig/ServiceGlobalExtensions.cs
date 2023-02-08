﻿using Cliente.WEBAPI.App.GlobalConfig;

namespace Cliente.WebAPI.App.GlobalConfig
{
    public static class ServiceGlobalExtensions
    {
        public static void AddGlobalSettings(this IServiceCollection services)
        {
            services.AddSingleton<IGlobalSettings, GlobalSettings>();
        }
    }
}