namespace DatabasePlayground.Extensions;

public static class RandomExtensions
{
    public static T SelectFrom<T>(this Random random, IList<T> list) => list[random.Next(list.Count)];
}