// See https://aka.ms/new-console-template for more information

using DatabasePlayground;
using DbTypes;

var sim = new PeopleSim(0.02, 0, 3, TimeSpan.FromSeconds(2));
sim.SimulationTick += (_, info) =>
{
    var goodNews = new News("Good news!", 0.05, 0.025);
    goodNews.StockAffectedEvent += (sender, payload) =>
    {
        Console.WriteLine($"Stock {payload.stock.Symbol} had a {payload.percentageChange:P} change to its value with a difference of {payload.newValue - payload.oldValue:N3}");
    };
    
    goodNews.ApplyToMarketValues(info.stocks.Take(2), false); // Take 2 to reduce console printing for playing
    Console.WriteLine(string.Join(", ", info.stocks.Select(s => $"{s.Symbol}: {s.Value}\n")));
};

var cancelTokenSrc = new CancellationTokenSource(TimeSpan.FromSeconds(60));
sim.StartSimThread(cancelTokenSrc.Token);

cancelTokenSrc.Token.WaitHandle.WaitOne();