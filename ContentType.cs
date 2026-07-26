using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
enum ContentType
{
    CSV,
    INTERNAL_JSON
}
