using System.Net.NetworkInformation;
using DbTypes.Interfaces;

namespace DbTypes;

public class News : IIdentification
{
    public event EventHandler<(Stock stock, double oldValue, double newValue, double percentageChange)>? 
        StockAffectedEvent;

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
            var oldValue = stock.Value;
            // Adding the random offset into the public opinion weight percentage
            var percentageChange = PublicOpinionWeight +
                PublicOpinionWeightRandomOffset * 2d * Random.Shared.NextDouble() - PublicOpinionWeightRandomOffset; 
            var addedValue = stock.Value * percentageChange;
            if (isPositiveToStockValues)
                addedValue = -addedValue;
            stock.Value += addedValue;

            var eventPayload = (stock, oldValue, stock.Value, percentageChange);
            StockAffectedEvent?.Invoke(this, eventPayload);
        }
    }
}