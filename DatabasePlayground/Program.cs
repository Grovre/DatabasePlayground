// See https://aka.ms/new-console-template for more information

using DatabasePlayground;
using DbTypes;

var sim = new PeopleSim(0.15, 0, 10, TimeSpan.FromSeconds(1));
sim.SimulationTick += (_, info) =>
{
    var goodNews = new News("Good news!", 0.02, 0.01, info.stocks);
    goodNews.ApplyToMarketValues();
    Console.WriteLine(string.Join(", ", info.stocks.Select(s => $"{s.Symbol}: {s.Value:C3}")));
};

var cancelTokenSrc = new CancellationTokenSource(TimeSpan.FromSeconds(60));
sim.StartSimThread(cancelTokenSrc.Token);

cancelTokenSrc.Token.WaitHandle.WaitOne();