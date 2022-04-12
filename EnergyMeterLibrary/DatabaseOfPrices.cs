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
    /// Metoda sloužící k zadání nové validní ceny do databáze cen.
    /// </summary>
    /// <param name="dateStart">Počáteční datum cenovky.</param>
    /// <param name="dateEnd">Expirační datum cenovky.</param>
    /// <param name="price">Cena za jednotku.</param>
    public void Add(Price p)
    {
        if (p.StartDate > p.EndDate)
        {
            throw new ArgumentException("Počáteční datum musí být před datumem konce.");
        }

        List<Price> tmpData = new(Data);
        tmpData.Add(p);
        tmpData.Sort(new Price.Comparer());

        if (EMValidator.ContainsOverlapingPrices(tmpData))
        {
            throw new InvalidOperationException("Zadané období se nesmí překrývat s již existujícím.");
        }
        else
        {
            Data.Add(p);
            Data.Sort(new Price.Comparer());
        }

    }


    internal void Edit(int index, Price editedPrice)
    {
        throw new NotImplementedException();
    }

    internal void Remove(int index)
    {
        throw new NotImplementedException();
    }
    public IEnumerator GetEnumerator() => Data.GetEnumerator();

    public Price this[int index]
    {
        get => Data[index];
    }
}
