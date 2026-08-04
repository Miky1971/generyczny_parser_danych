using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/v1/parse-content", (InputData input) =>
{
    IContentParser parser;

    // 1. Zweryfikować, czy przesłany type jest obsługiwany (w przypadku błędu zwrócić odpowiedni kod HTTP, np. 400 Bad Request).
    switch (input.Type)
    {
        case ContentType.CSV:
            parser = new CsvContentParser();
            break;
        case ContentType.INTERNAL_JSON:
            parser = new InternalJsonContentParser();
            break;
        default:
            // Praktycznie nieosiagalne: dopóki w ContentType.cs nie dodada się nowej pozycji, nie obsługiwanej tutaj
            return Results.BadRequest(new { error = "Nieobslugiwany typ danych." });
    }

    // 2. Zdekodować ciąg znaków z Base64 do postaci zwykłego tekstu (string).
    string decodedContent;
    try
    {
        byte[] bytes = Convert.FromBase64String(input.Content);
        decodedContent = Encoding.UTF8.GetString(bytes);
    }
    catch (FormatException)
    {
        return Results.BadRequest(new { error = "Niepoprawny format Base64 w polu content." });
    }

    // 3. W zależności od typu wykonać parsowanie danych:
    ParseResult result;
    try
    {
        result = parser.Parse(decodedContent);
    }
    catch (Exception)
    {
        return Results.BadRequest(new { error = "Niepoprawny format danych w polu content." });
    }

    // ustalenie Status na podstawie stosunku poprawnych wierszy do wszystkich.
    Status status;
    if (result.Data.Count == result.TotalRows)
        status = Status.Success;
    else if (result.Data.Count == 0)
        status = Status.AllRowsInvalid;
    else
        status = Status.PartialSuccess;

    // 4. Zwrócić odpowiedź w formacie JSON, która zawiera status operacji, liczbę przetworzonych wierszy/obiektów oraz sparsowane dane w ujednoliconej strukturze.
    OutputData output = new OutputData
    {
        Status = status,
        QtyRows = result.Data.Count,
        TotalRows = result.TotalRows,
        Data = result.Data
    };

    return Results.Ok(output);
});

app.Run();
