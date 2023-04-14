using DatabasePlayground.Extensions;
using DbTypes;

namespace DatabasePlayground;

/// <summary>
/// This will likely undergo changes to make bank accounts/persons have a weighted decision on purchasing shares
/// of a stock based on news and randomization instead of relying on "dev-sponsored" journalism (News instances)
/// </summary>
public class PeopleSim
{
    public event EventHandler<(BankAccount[] accounts, Stock[] stocks)>? SimulationTick;
    
    public BankAccount[] BankAccounts { get; }
    public Stock[] Stocks { get; }
    public bool IsRunning { get; private set; } = false;
    public double MaximumRandomizedStockValueChangePerTick { get; }
    public TimeSpan TickInterval { get; set; }

    public PeopleSim(double maximumRandomizedStockValueChange, int amountOfBankAccounts, int amountOfStocks, TimeSpan tickInterval)
    {
        TickInterval = tickInterval;
        MaximumRandomizedStockValueChangePerTick = maximumRandomizedStockValueChange;
        BankAccounts = new BankAccount[amountOfBankAccounts];
        Stocks = new Stock[amountOfStocks];

        foreach (ref var acc in BankAccounts.AsSpan())
        {
            var p = new Person("Name", DateTime.Now - TimeSpan.FromDays(365 * Random.Shared.Next(18, 67)));
            acc = new BankAccount(p, Random.Shared.NextDouble() * 3_000_000d, Random.Shared.NextSingle() >= 0.965f);
        }

        foreach (ref var stock in Stocks.AsSpan())
        {
            stock = new Stock(Random.Shared.NextString(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"), 100d);
        }
    }

    public void StartSimThread(CancellationToken cancelToken)
    {
        IsRunning = true;
        new TaskFactory().StartNew(async () =>
        {
            while (!cancelToken.IsCancellationRequested)
            {
                foreach (var t in Stocks)
                {
                    t.Value += t.Value * (Random.Shared.NextDouble() * MaximumRandomizedStockValueChangePerTick * 2 - MaximumRandomizedStockValueChangePerTick);
                }

                SimulationTick?.Invoke(this, (BankAccounts, Stocks));
                await Task.Delay(TickInterval, cancelToken);
            }
        }, TaskCreationOptions.LongRunning);
    }
}