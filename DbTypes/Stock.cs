using DbTypes.Interfaces;

namespace DbTypes;

public class Stock : IIdentification
{
    public Guid Id { get; }
    public string Symbol { get; }
    public double Value { get; set; }

    public Stock(string symbol, double value)
    {
        if (symbol.Any(c => !char.IsLetter(c) && !char.IsNumber(c)))
            throw new ArgumentException("A symbol can only consist of letters and numbers");
        
        Id = Guid.NewGuid();
        Symbol = symbol;
        Value = value;
    }
}