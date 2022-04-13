namespace EnergyMonitorLibrary;
using System.Collections;

/// <summary>
/// Třída je databází cen. Do třídy jdou uložit pouze validní intervaly,
/// které se navzájem nepřekrývají.
/// Implementuje rozhraní IEnumerable.
/// </summary>
public class PricesDatabase : IEnumerable
{
    public List<Price> Data { get; } = new();
    public int Count => Data.Count;


    /// <summary>
    /// Metoda sloužící k zadání nové validní ceny do databáze cen.
    /// </summary>
    /// <param name="dateStart">Počáteční datum cenovky.</param>
    /// <param name="dateEnd">Expirační datum cenovky.</param>
    /// <param name="price">Cena za jednotku.</param>
    public void Add(Price newPrice)
    {
        if (newPrice.StartDate > newPrice.EndDate)
        {
            throw new ArgumentException("Počáteční datum musí být před datumem konce.");
        }

        List<Price> tmpData = new(Data);
        tmpData.Add(newPrice);
        tmpData.Sort(new Price.Comparer());

        if (EMValidator.ContainsOverlapingPrices(tmpData))
        {
            throw new InvalidOperationException("Zadané období se nesmí překrývat s již existujícím.");
        }
        else
        {
            Data.Add(newPrice);
            Data.Sort(new Price.Comparer());
        }

    }

    /// <summary>
    /// Metoda sloužící k editaci existující ceny.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="editedPrice"></param>
    /// <exception cref="ArgumentException"></exception>
    internal void Edit(int index, Price editedPrice)
    {
        List<Price> tmpData = new(Data);
        tmpData.RemoveAt(index);
        tmpData.Add(editedPrice);
        tmpData.Sort(new Price.Comparer());

        if (EMValidator.ContainsOverlapingPrices(tmpData))
        {
            Data.RemoveAt(index);
            Data.Add(editedPrice);
            Data.Sort(new Price.Comparer());
        }
        else
        {
            throw new ArgumentException("Zadávaná cena se překrývá s jinou, již existující cenou.");
        }
    }

    /// <summary>
    /// Metoda sloužící k odebrání prvku z určitého indexu.
    /// </summary>
    /// <param name="index"></param>
    internal void Remove(int index)
    {
        if(index < 0 || index >= Data.Count)
        {
            return;
        }
        Data.RemoveAt(index);
    }


    public IEnumerator GetEnumerator() => Data.GetEnumerator();

    public Price this[int index]
    {
        get => Data[index];
    }
}
