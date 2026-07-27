class OutputData
{
    public required Status Status { get; set; }
    public required int QtyRows { get; set; }
    public required int TotalRows { get; set; }
    public required List<Dictionary<string, object>> Data { get; set; }

}
