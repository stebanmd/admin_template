using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }

    public static List<T> ToList<T>(this IEnumerable<T> e)
    {
        var list = new List<T>();
        list.AddRange(e);
        return list;
    }
}