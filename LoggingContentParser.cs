// Dekorator dodajacy logowanie do DOWOLNEGO IContentParser
class LoggingContentParser : IContentParser   // TEN SAM interfejs co opakowywany
{
    private readonly IContentParser _inner;   // opakowany parser (typ interfejsu!)

    public LoggingContentParser(IContentParser inner)   // dostaje go z zewnatrz (jak DI)
    {
        _inner = inner;
    }

    public ParseResult Parse(string decodedContent)
    {
        Console.WriteLine("LOG wejściowy");

        ParseResult result = _inner.Parse(decodedContent);   // DELEGACJA do opakowanego

        Console.WriteLine("LOG wyjściowy");
        return result;
    }
}
