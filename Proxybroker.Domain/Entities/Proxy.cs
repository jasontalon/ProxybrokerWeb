namespace Proxybroker.Domain.Entities;

public class Proxy
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string? GeoLocationCountryCode { get; set; }
    public string? GeoLocationCountryName { get; set; }
    public string? GeoLocationRegionCode { get; set; }
    public string? GeoLocationRegionName { get; set; }
    public string? GeoCity { get; set; }

    public virtual ICollection<ProxyType> Types { get; set; }

    public decimal? AvgRespTime { get; set; }

    public decimal? ErrorRate { get; set; }
}

public class ProxyType
{
    public Proxy Proxy { get; set; }

    public string? Type { get; set; }
    public string? Level { get; set; }
}