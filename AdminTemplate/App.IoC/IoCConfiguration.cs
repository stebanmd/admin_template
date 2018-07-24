using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace App.IoC
{
    public class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            Configure(services, App.Data.IoC.Module.GetTypes());
            Configure(services, App.Services.IoC.Module.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}