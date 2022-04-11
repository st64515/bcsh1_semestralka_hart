namespace EnergyMonitorLibrary;

using System.Collections;

/// <summary>
/// Třída je databází cen. Do třídy jdou uložit pouze validní intervaly,
/// které se navzájem nepřekrývají.
/// Implementuje rozhraní IEnumerable.
/// </summary>
public class DatabaseOfPrices : IEnumerable
{
    public List<Price> Data { get; } = new();
    public int Count => Data.Count;


    //private EMValidator validator = new();


    public Price this[int index]
    {
        get => Data[index];
    }

    public void Add(DateTime dateStart, DateTime dateEnd, double price)
    {
        if (dateStart > dateEnd)
        {
            throw new ArgumentException("Start date must be before date of end.");
        }
        if (EMValidator.Overlaps(Data, dateStart, dateEnd))
        {
            throw new InvalidOperationException("Inserted period must not overlap existing ones.");
        }
        Data.Add(new Price(dateStart, dateEnd, price));
        Data.Sort(new Price.PriceTagIntervalComparer());
    }

    public IEnumerator GetEnumerator() => Data.GetEnumerator();

    

}
