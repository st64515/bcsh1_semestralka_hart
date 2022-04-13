namespace EnergyMonitorLibrary;
using System.Collections;

/// <summary>
/// Třída je databází poplatků. Do třídy jdou vkládat jen ty poplatky,
/// jejichž datum počátku a konce platnosti dává smysl.
/// Záznamy jsou uloženy chronologicky.
/// </summary>
public class TaxesDatabase : IEnumerable
{
    public List<Tax> Data { get; } = new();
    public int Count => Data.Count;


    /// <summary>
    /// Metoda sloužící k zadání nového poplatku do databáze poplatků.
    /// </summary>
    /// <param name="name">Název poplatku.</param>
    /// <param name="dateStart">Počáteční datum účinnosti poplatku.</param>
    /// <param name="dateEnd">Expirační datum účinnosti poplatku.</param>
    /// <param name="price">Cena za jednotku.</param>
    /// <param name="interval">Interval účtování poplatku.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Add(Tax newTax)
    {
        if (newTax.StartDate > newTax.EndDate)
        {
            throw new ArgumentException("Datum počátku platnosti poplatku musí být dříve než jeho datum konce.");
        }
        Data.Add(newTax);
        Data.Sort(new Tax.Comparer());
    }

    /// <summary>
    /// Metoda sloužící k editaci existujícího poplatku.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="editedTax"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Edit(int index, Tax editedTax)
    {
        if (editedTax.StartDate > editedTax.EndDate)
        {
            throw new ArgumentException("Datum počátku platnosti poplatku musí být dříve než jeho datum konce.");
        }
        Data.RemoveAt(index);
        Data.Add(editedTax);
        Data.Sort(new Tax.Comparer());
    }

    /// <summary>
    /// Metoda sloužící k odebrání prvku z určitého indexu.
    /// </summary>
    /// <param name="index"></param>
    internal void Remove(int index)
    {
        if (index < 0 || index >= Data.Count)
        {
            return;
        }
        Data.RemoveAt(index);
    }


    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    public Tax this[int index]
    {
        get => Data[index];
    }
}