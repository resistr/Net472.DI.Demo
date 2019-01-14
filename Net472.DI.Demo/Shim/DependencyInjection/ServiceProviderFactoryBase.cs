using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shim.DependencyInjection
{
    /// <summary>
    /// Base service provider factory class that takes care for creating the service 
    /// collection and building the service provider. Exposes a method for configuring 
    /// the service collection.
    /// </summary>
    public abstract class ServiceProviderFactoryBase : IServiceProviderFactory
    {
        /// <inheritdoc />
        public virtual IServiceCollection CreateBuilder(IServiceCollection services)
            => ConfigureServices(services);

        /// <summary>
        /// Configure the service collection.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <returns>The configured service collection.</returns>
        public abstract IServiceCollection ConfigureServices(IServiceCollection services);

        /// <inheritdoc />
        public virtual IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
            => containerBuilder.BuildServiceProvider();
    }
}
