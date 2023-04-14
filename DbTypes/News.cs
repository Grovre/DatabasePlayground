using System.Net.NetworkInformation;
using DbTypes.Interfaces;

namespace DbTypes;

/// <summary>
/// Simple events disguised as (dev-sponsored) journalism for affecting stock values.
/// Includes a headline, public opinion weight and a random offset to the weight for variation.
/// </summary>
public class News : IIdentification
{
    /// <summary>
    /// Fires after every enumerated stock in the ApplyToMarketValues method. The sender will always be the
    /// invoking News instance.
    /// </summary>
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

    /// <summary>
    /// Enumerates all stocks and applies the news to the value of every stock given.
    /// </summary>
    /// <param name="stocks">Stocks to enumerate</param>
    /// <param name="isPositiveToStockValues">If the news has a bad outcome for the given stocks (false), negates the difference in value</param>
    public void ApplyToMarketValues(IEnumerable<Stock> stocks, bool isPositiveToStockValues = true)
    {
        foreach (var stock in stocks)
        {
            var oldValue = stock.Value;
            // Adding a random bounded offset into the public opinion weight percentage
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