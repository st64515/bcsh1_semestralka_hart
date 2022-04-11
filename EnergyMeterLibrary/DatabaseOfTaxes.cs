namespace EnergyMonitorLibrary;
using System.Collections;
public class DatabaseOfTaxes : IEnumerable
{
    public List<Tax> Data { get; } = new();
    public int Count => Data.Count;

    public Tax this[int index]
    {
        get => Data[index];
    }
    internal void Add(string name, DateTime dateStart, DateTime dateEnd, int price, Intervals interval)
    {
        if (dateStart > dateEnd)
        {
            throw new ArgumentException("Start date must be before date of end.");
        }
        Data.Add(new Tax(name, dateStart, dateEnd, price, interval));
    }

    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }
}