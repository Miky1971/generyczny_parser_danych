class CsvContentParser : IContentParser
{
    public ParseResult Parse(string decodedContent) // 4) Ostateczny wynik parsowania -słownik (lista rekordów z nagłówkami)
    {
        List<string> lines = SplitTextToLines(decodedContent);
        int totalRows = lines.Count - 1;

        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

        List<string> header = SplitLineToFields(lines[0]);
        int qtyColumns = header.Count;
        foreach (var line in lines.Skip(1))
        {
            List<string> fields = SplitLineToFields(line);
            if (fields.Count == qtyColumns)
            {
                data.Add(BuildingOutput(header, fields));
            }
        }
        return new ParseResult{Data = data, TotalRows = totalRows};
    }

    private List<string> SplitTextToLines(string content) // 1) podział na linie po \n...
    {
        string[] lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None); // ...także z pustymi liniami (RemoveEmptyEntries bez pustych linii)
        return new List<string>(lines);
    }

    private List<string> SplitLineToFields(string line) // 2) podział linii na pola (po przecinku) -znak po znaku, z uwzględnieniem cudzysłowów i przecinków w środku cudzysłowów
    {
        bool isQuotationMark = false;
        List<string> fields = new List<string>();
        string bufor = "";


        for (int i = 0; i < line.Length; i++)
        {
            char aChar = line[i];

            switch (aChar)
            {
                case '"' when !isQuotationMark:
                    isQuotationMark = true;
                    break;

                case '"' when isQuotationMark && i + 1 < line.Length && line[i + 1] == '"':
                    bufor += '"';
                    i++;
                    break;

                case '"' when isQuotationMark:
                    isQuotationMark = false;
                    break;

                case ',' when !isQuotationMark:
                    fields.Add(bufor);
                    bufor = "";
                    break;

                default:
                    bufor += aChar;
                    break;
            }
        }
        return fields;
    }

    private Dictionary<string, object> BuildingOutput(List<string> header, List<string> fields) // 3) 
    {
        Dictionary<string, object> responce = new Dictionary<string, object>();
        for (int i = 0; i < header.Count; i++)
        {
            responce.Add(header[i], fields[i]);
        }
        return responce;
    }
}










