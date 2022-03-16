using R2RTechnicalTests.Models;
using System.Text.Json.Serialization;


namespace R2RTechnicalTests.Config
{
    [JsonSerializable(typeof(ValidOrder))]
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default)]
    public partial class ValidOrdersJsonContext : JsonSerializerContext
    {
    }
}
