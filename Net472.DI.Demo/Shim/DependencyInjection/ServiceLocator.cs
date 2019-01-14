using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shim
{
    /// <summary>
    /// Service locator anti-pattren. Using generics to allow the consumer to 
    /// provide the factory. 
    /// </summary>
    /// <typeparam name="TServiceProviderFactory"></typeparam>
    public static class ServiceLocator<TServiceProviderFactory>
        where TServiceProviderFactory : IServiceProviderFactory, new()
    {
#pragma warning disable S2743 // Static fields should not be used in generic types
        // gotcha thats the intent
        private static IServiceProvider ServiceProvider;
        private static object LockObject = new object();

        /// <summary>
        /// Gets or creates the current service provider. 
        /// </summary>
        public static IServiceProvider Current { get => ServiceProvider ?? CreateServiceProvider(); }
#pragma warning restore S2743 // Static fields should not be used in generic types

        /// <summary>
        /// Creates the current service provider. 
        /// </summary>
        /// <returns>The created and configured service provider from the factory provided
        /// to the generic type.</returns>
        private static IServiceProvider CreateServiceProvider()
        {
            lock(LockObject)
            {
                if (ServiceProvider == null)
                {
                    var serviceProviderFactory = new TServiceProviderFactory();
                    var containerBuilder = serviceProviderFactory.CreateBuilder(new ServiceCollection());
                    ServiceProvider = serviceProviderFactory.CreateServiceProvider(containerBuilder);
                }
            }
            return ServiceProvider;
        }
    }
}
