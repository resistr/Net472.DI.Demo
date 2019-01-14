using Microsoft.Extensions.DependencyInjection;

namespace Shim
{
    /// <summary>
    /// Interface defining a service provider factory. 
    /// </summary>
    public interface IServiceProviderFactory : IServiceProviderFactory<IServiceCollection> { }
}
