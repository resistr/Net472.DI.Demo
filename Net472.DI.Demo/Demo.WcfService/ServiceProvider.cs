using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using Shim;
using Shim.DependencyInjection;
using Shim.Logging;
using System;

namespace Demo.WcfService
{
    public class ServiceProvider : ServiceProviderFactoryBase
    {
        public static IServiceProvider Current { get => ServiceLocator<ServiceProvider>.Current; }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddNLog();

            return services;
        }
    }
}