using System.Diagnostics;
using Newtonsoft.Json;
using Proxybroker.Domain.Dtos;
using Proxybroker.Infrastructure.Extensions;

namespace Proxybroker.Infrastructure.Services;

public interface IProxybrokerService
{
    Task FindAsync(Action<Proxy> onProxyReceived, Action<string>? onErrorReceived,
        CancellationToken cancellationToken = default);
}

internal sealed class ProxybrokerService : IProxybrokerService
{
    public async Task FindAsync(Action<Proxy> onProxyReceived, Action<string>? onErrorReceived,
        CancellationToken cancellationToken = default)
    {
        using var process = new Process();

        void OnReceive(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            data = data.TrimEnd();

            if (!data.StartsWith("{")) return;

            var dto = JsonConvert.DeserializeObject<Proxy>(data);

            onProxyReceived(dto);
        }

        await process.RunAsync("proxybroker find --format json", OnReceive, onErrorReceived, cancellationToken);
    }
}

public sealed class ProxybrokerServiceFindQueryParameters
{
    /// <summary>
    ///  Type(s) (protocols) that need to be check on support by proxy
    /// </summary>
    public List<ProxybrokerServiceFindType>? Types { get; set; }

    /// <summary>
    /// Level(s) of anonymity (for HTTP only). By default, any level
    /// </summary>
    public List<ProxybrokerServiceFindLevel>? Levels { get; set; }

    /// <summary>
    /// Flag indicating that anonymity levels of the types (protocols) supported by a proxy must be equal to the requested types and levels of anonymity
    /// </summary>
    public bool? Strict { get; set; } = false;

    /// <summary>
    /// List of ISO 3166 Alpha-2 code Countries
    /// i.e., US, GB, CA, AU, NZ...
    /// </summary>
    public List<string> Countries { get; set; }

    /// <summary>
    /// The maximum number of working proxies
    /// Default is 100
    /// </summary>
    public int? Limit { get; set; } = 100;
}

public enum ProxybrokerServiceFindType
{
    HTTP,
    HTTPS,
    SOCKS4,
    SOCKS5,
    CONNECT80,
    CONNECT25
}

public enum ProxybrokerServiceFindLevel
{
    Transparent,
    Anonymous,
    High
}