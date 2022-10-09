using Microsoft.Extensions.Logging;
using ProxybrokerWeb.Models.Mvvm;

namespace ProxybrokerWeb.ViewModels;

internal class FetchViewModel : ViewModelBase<IFetchModel>
{
    public FetchViewModel(ILogger logger) : base(logger)
    {
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        //testing lang
        await Task.CompletedTask;
    }

    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}