using Microsoft.Extensions.DependencyInjection;
using Proxybroker.Infrastructure.Services;

namespace Proxybroker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IProxybrokerService, ProxybrokerService>();

        return serviceCollection;
    }
}