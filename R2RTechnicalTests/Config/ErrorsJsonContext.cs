using R2RTechnicalTests.Models;
using System.Text.Json.Serialization;

namespace R2RTechnicalTests.Config
{   
    [JsonSerializable(typeof(Errors))]
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    public partial class ErrorsJsonContext : JsonSerializerContext
    {
    }
}
