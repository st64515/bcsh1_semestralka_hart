namespace EnergyMonitorLibrary;
using System.Collections;

/// <summary>
/// Třída je databází poplatků. Do třídy jdou vkládat jen ty poplatky,
/// jejichž datum počátku a konce platnosti dává smysl.
/// Záznamy jsou uloženy chronologicky.
/// </summary>
public class TaxesDatabase : IEnumerable, ITaxesSaveableLoadable
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
    public void Add(Tax? newTax)
    {
        if (newTax == null)
        {
            return;
        }
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
    public void Remove(int index)
    {
        if (index < 0 || index >= Data.Count)
        {
            return;
        }
        Data.RemoveAt(index);
    }

    public void Erase()
    {
        Data.Clear();
    }


    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    public Tax[] Save()
    {
        Tax[] taxesArray = new Tax[Data.Count];
        Data.CopyTo(taxesArray, 0);

        return taxesArray;
    }

    public void Load(Tax?[] loadedTaxes)
    {
        Erase();

        for (int i = 0; i < loadedTaxes.Length; i++)
        {
            Add(loadedTaxes[i]);
        }
    }

    public Tax this[int index]
    {
        get => Data[index];
    }

    public double GetTaxesIn(DateTime startDate, DateTime endDate, ReadingsDatabase rDatabase)
    {

        double sum = 0;


        for (int i = 0; i < Data.Count; i++)
        {
            double koeficient = 0;
            if (Data[i].Interval == Intervals.PerMonth)
            {
                //zjisti kolik měsíců zatím uběhlo
                DateTime countingStart = (startDate > Data[i].StartDate) ? startDate : Data[i].StartDate;
                DateTime countingEnd = (endDate < Data[i].EndDate) ? endDate : Data[i].EndDate;

                koeficient = ((countingEnd.Year - countingStart.Year) * 12) + countingEnd.Month - countingStart.Month;
            }
            else if (Data[i].Interval == Intervals.PerMWh)
            {
                //zjisti kolik MWh bylo v období spáleno

                koeficient = rDatabase.GetMWhBurntIn(Data[i].StartDate, Data[i].EndDate);
            }
            sum += (koeficient * Data[i].Cost);
        }
        return sum;

    }
}