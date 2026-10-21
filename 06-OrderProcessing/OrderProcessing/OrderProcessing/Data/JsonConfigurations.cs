
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderProcessing.Data;

public static class JsonConfigurations
{
    public static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true, 
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };
}
