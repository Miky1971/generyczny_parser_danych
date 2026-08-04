using System.Text.Json;

class InternalJsonContentParser : IContentParser
{
    public ParseResult Parse(string decodedContent)
    {
        JsonDocument document = JsonDocument.Parse(decodedContent);
        JsonElement root = document.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
        {
            throw new FormatException("Oczekiwano tablicy obiektow JSON w polu content.");
        }

        int totalRows = root.GetArrayLength();
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

        foreach (JsonElement element in root.EnumerateArray())
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                data.Add(BuildingOutput(element));
            }
        }
        return new ParseResult { Data = data, TotalRows = totalRows };
    }

    private Dictionary<string, object> BuildingOutput(JsonElement element)
    {
        Dictionary<string, object> responce = new Dictionary<string, object>();

        foreach (JsonProperty record in element.EnumerateObject())
        {
            responce.Add(record.Name, record.Value.Clone()); // trzeba zrobić klon/kopię recordów, bo JsonDocument po zakonczeniu metody Parse() zwalnia swoj bufor i dane znikną
        }
        return responce;
    }
}
