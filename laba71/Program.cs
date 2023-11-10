using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> Find(Func<T, bool> criteria)
    {
        return items.Where(criteria).ToList();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Repository<int> intRepository = new Repository<int>();
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);

        List<int> result = intRepository.Find(item => item > 1); // Результат: { 2, 3 }
        Console.WriteLine("Result: " + string.Join(", ", result));
    }
}
