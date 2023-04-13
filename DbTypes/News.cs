using DbTypes.Interfaces;

namespace DbTypes;

public class News : IIdentification
{
    public Guid Id { get; }
    public string Headline { get; }
    public double PublicOpinionWeight { get; }
    public double PublicOpinionWeightRandomOffset { get; }
    public IEnumerable<Stock> RelevantStocks { get; }

    public News(string headline, double publicOpinionWeight, double publicOpinionWeightRandomOffset, IEnumerable<Stock> relevantStocks)
    {
        Id = Guid.NewGuid();
        Headline = headline;
        PublicOpinionWeight = publicOpinionWeight;
        PublicOpinionWeightRandomOffset = publicOpinionWeightRandomOffset;
        RelevantStocks = relevantStocks;
    }

    public void ApplyToMarketValues()
    {
        foreach (var stock in RelevantStocks)
        {
            stock.Value += PublicOpinionWeight * stock.Value +
                PublicOpinionWeightRandomOffset * 2 * Random.Shared.NextDouble() - PublicOpinionWeightRandomOffset;
        }
    }
}