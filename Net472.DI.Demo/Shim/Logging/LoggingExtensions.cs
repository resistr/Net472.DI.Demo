using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace Shim.Logging
{
    /// <summary>
    /// Extension methods to make registering NLog easier.  
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Add Microsoft.Extensions.Logging and NLog to service collection. 
        /// </summary>
        /// <param name="serviceCollection">The service collection to add services to.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddNLog(this IServiceCollection serviceCollection)
            => serviceCollection.AddNLog(null);

        /// <summary>
        /// Add Microsoft.Extensions.Logging and NLog to service collection. 
        /// </summary>
        /// <param name="serviceCollection">The service collection to add services to.</param>
        /// <param name="nLogConfigurationPath">The path to the NLog configuration file.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddNLog(this IServiceCollection serviceCollection, string nLogConfigurationPath)
            => serviceCollection.AddNLog(null, options =>
            {
                options.CaptureMessageTemplates = true;
                options.CaptureMessageProperties = true;
            }, _ => { });

        /// <summary>
        /// Add Microsoft.Extensions.Logging and NLog to service collection. 
        /// </summary>
        /// <param name="serviceCollection">The service collection to add services to.</param>
        /// <param name="optionsBuilder">Actions to configure NLog options.</param>
        /// <param name="loggingBuilder">Actions to configure Microsoft.Extensions.Logging.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddNLog(this IServiceCollection serviceCollection, 
            Action<NLogProviderOptions> optionsBuilder,
            Action<ILoggingBuilder> loggingBuilder)
            => serviceCollection.AddNLog(null, optionsBuilder, loggingBuilder);

        /// <summary>
        /// Add Microsoft.Extensions.Logging and NLog to service collection. 
        /// </summary>
        /// <param name="serviceCollection">The service collection to add services to.</param>
        /// <param name="nLogConfigurationPath">The path to the NLog configuration file.</param>
        /// <param name="optionsBuilder">Actions to configure NLog options.</param>
        /// <param name="loggingBuilder">Actions to configure Microsoft.Extensions.Logging.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddNLog(this IServiceCollection serviceCollection,
            string nLogConfigurationPath,
            Action<NLogProviderOptions> optionsBuilder,
            Action<ILoggingBuilder> loggingBuilder)
        {
            if (!string.IsNullOrEmpty(nLogConfigurationPath))
            {
                NLog.LogManager.LoadConfiguration(nLogConfigurationPath);
            }

            return serviceCollection.AddLogging(builder =>
            {
                loggingBuilder(builder);
                var configuredOptions = new NLogProviderOptions();
                optionsBuilder(configuredOptions);
                builder.AddNLog(configuredOptions);
            });
        }
    }
}
