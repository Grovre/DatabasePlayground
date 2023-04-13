using System.Text;

namespace DatabasePlayground.Extensions;

public static class RandomExtensions
{
    public static T SelectFrom<T>(this Random random, IList<T> list) 
        => list[random.Next(list.Count)];
    
    public static T SelectFrom<T>(this Random random, ReadOnlySpan<T> span) 
        => span[random.Next(span.Length)];
    
    public static double NextDouble(this Random random, double minValue, double maxValue) 
        => minValue + (maxValue - minValue) * random.NextDouble();

    public static string NextString(this Random random, int length, ReadOnlySpan<char> allowedChars)
    {
        var sb = new StringBuilder(length);
        while (length-- > 0)
        {
            sb.Append(random.SelectFrom(allowedChars));
        }

        return sb.ToString();
    }
}