// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices.ComTypes;
using System.Text;
using DatabasePlayground;
using DbTypes;

var sim = new PeopleSim(0.15, 0, 10, TimeSpan.FromSeconds(0.1));
sim.SimulationTick += (_, info) =>
{
    Console.WriteLine(string.Join(", ", info.stocks.Select(s => $"{s.Symbol}: {s.Value:C3}")));
};

var cancelTokenSrc = new CancellationTokenSource(TimeSpan.FromSeconds(60));
sim.StartSimThread(cancelTokenSrc.Token);

cancelTokenSrc.Token.WaitHandle.WaitOne();