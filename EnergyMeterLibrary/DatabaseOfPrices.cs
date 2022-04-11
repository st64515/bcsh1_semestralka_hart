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


    /// <summary>
    /// Metoda sloužící k zadání nové ceny do databáze cen.
    /// </summary>
    /// <param name="dateStart">Počáteční datum cenovky.</param>
    /// <param name="dateEnd">Expirační datum cenovky.</param>
    /// <param name="price">Cena za kWh.</param>
    public void Add(Price p)
    {
        if (p.StartDate> p.EndDate)
        {
            throw new ArgumentException("Start date must be before date of end.");
        }
        if (EMValidator.Overlaps(Data, p.StartDate, p.EndDate))
        {
            throw new InvalidOperationException("Inserted period must not overlap existing ones.");
        }
        Data.Add(new Price(p.StartDate, p.EndDate, p.Cost));

        Data.Sort(new Price.PriceTagIntervalComparer());
    }

    public IEnumerator GetEnumerator() => Data.GetEnumerator();

    public Price this[int index]
    {
        get => Data[index];
    }


}
