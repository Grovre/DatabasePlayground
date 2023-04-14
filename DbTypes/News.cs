using DbTypes.Interfaces;

namespace DbTypes;

public class News : IIdentification
{
    public Guid Id { get; }
    public string Headline { get; }
    public double PublicOpinionWeight { get; }
    public double PublicOpinionWeightRandomOffset { get; }

    public News(string headline, double publicOpinionWeight, double publicOpinionWeightRandomOffset)
    {
        Id = Guid.NewGuid();
        Headline = headline;
        PublicOpinionWeight = publicOpinionWeight;
        PublicOpinionWeightRandomOffset = publicOpinionWeightRandomOffset;
    }

    public void ApplyToMarketValues(IEnumerable<Stock> stocks, bool isPositiveToStockValues = true)
    {
        foreach (var stock in stocks)
        {
            var addedValue = PublicOpinionWeight * stock.Value +
                PublicOpinionWeightRandomOffset * 2 * Random.Shared.NextDouble() - PublicOpinionWeightRandomOffset;
            if (isPositiveToStockValues)
                addedValue = -addedValue;
            stock.Value += addedValue;
        }
    }
}