using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
enum Status
{
    Success,
    PartialSuccess,
    AllRowsInvalid
}



