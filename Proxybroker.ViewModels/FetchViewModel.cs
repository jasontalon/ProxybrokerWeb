using Microsoft.Extensions.Logging;
using Proxybroker.Infrastructure.Services;

namespace Proxybroker.ViewModels;

public interface IFetchViewModel : IViewModel
{
    Task FindAsync(Action<string> onProxyReceived, CancellationToken cancellationToken = default);
}

internal sealed class FetchViewModel : ViewModelBase, IFetchViewModel
{
    private readonly IProxybrokerService _proxybrokerService;
    private bool _loading;
    private string _name;

    public FetchViewModel(ILogger logger, IProxybrokerService proxybrokerService) : base(logger)
    {
        _proxybrokerService = proxybrokerService;
    }

    public async Task FindAsync(Action<string> onProxyReceived, CancellationToken cancellationToken = default)
    {
        await _proxybrokerService.FindAsync(null, null, cancellationToken);
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public bool Loading
    {
        get => _loading;
        set => SetField(ref _loading, value);
    }
}