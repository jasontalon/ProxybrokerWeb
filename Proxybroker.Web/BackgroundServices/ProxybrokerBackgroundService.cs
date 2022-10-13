using Proxybroker.Domain.Dtos;
using Proxybroker.Infrastructure.Services;

namespace Proxybroker.Web.BackgroundServices;

internal sealed class ProxybrokerBackgroundService : BackgroundService
{
    private readonly ILogger<ProxybrokerBackgroundService> _logger;
    private readonly IProxybrokerService _proxybrokerService;

    public ProxybrokerBackgroundService(ILogger<ProxybrokerBackgroundService> logger,
        IProxybrokerService proxybrokerService)
    {
        _logger = logger;
        _proxybrokerService = proxybrokerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromMinutes(5));

        while (!stoppingToken.IsCancellationRequested)
        {
            await _proxybrokerService.FindAsync(OnProxyReceived, error => { }, stoppingToken);
            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

    private void OnProxyReceived(Proxy proxy)
    {
    }
}