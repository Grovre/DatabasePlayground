using DbTypes.Interfaces;

namespace DbTypes;

/// <summary>
/// Base class for containing data relevant to a stock. Includes a symbol and value.
/// </summary>
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