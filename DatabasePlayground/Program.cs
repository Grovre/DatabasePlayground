// See https://aka.ms/new-console-template for more information

using DbTypes;

var tlr = new ThreadLocal<Random>(() => new Random());
var i = 0;
File.ReadLines(@"C:\Users\lando\RiderProjects\DatabasePlayground\DatabasePlayground\names.txt")
    .AsParallel()
    .Select(name =>
    {
        var p = new Person(name, DateTime.Now);
        var r = tlr.Value!;
        return new BankAccount(p, r.NextDouble() * 1_000_000, r.NextSingle() >= 0.5f);
    })
    .ForAll(ba =>
    {
        if (ba.IsFrozen)
        {
            return;
        }
        var s = ba.Balance.ToString("N2");
        Console.WriteLine(s);
        Interlocked.Increment(ref i);
    });
    
Console.WriteLine(i);