// See https://aka.ms/new-console-template for more information

using DbTypes;

var tlr = new ThreadLocal<Random>(() => new Random());
File.ReadLines(@"C:\Users\lando\RiderProjects\DatabasePlayground\DatabasePlayground\names.txt")
    .AsParallel()
    .Select(s =>
    {
        var p = new Person(s, DateTime.Now);
        new BankAccount(p.Id, tlr.Value!.NextDouble() * 1_000_000, false);
        return p;
    })
    .ForAll(p =>
    {
        var s = BankAccount.BankAccountOwnerMap[p.Id].Balance.ToString("N2");
        Console.WriteLine(s);
    });