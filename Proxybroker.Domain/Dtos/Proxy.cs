using Newtonsoft.Json;

namespace Proxybroker.Domain.Dtos;

public sealed class Proxy
{
    [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
    public string? Host { get; set; }

    [JsonProperty("port", NullValueHandling = NullValueHandling.Ignore)]
    public int? Port { get; set; }

    [JsonProperty("geo", NullValueHandling = NullValueHandling.Ignore)]
    public Geo? Geo { get; set; }

    [JsonProperty("types", NullValueHandling = NullValueHandling.Ignore)]
    public List<ProxyType>? Types { get; set; }

    [JsonProperty("avg_resp_time", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? AvgRespTime { get; set; }

    [JsonProperty("error_rate", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? ErrorRate { get; set; }
}

public sealed class Geo
{
    [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
    public Location? Country { get; set; }

    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public Location? Region { get; set; }

    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string? City { get; set; }
}

public sealed class Location
{
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public string? Code { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }
}

public sealed class ProxyType
{
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string? Type { get; set; }

    [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
    public string? Level { get; set; }
}