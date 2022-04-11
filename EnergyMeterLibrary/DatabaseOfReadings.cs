namespace EnergyMonitorLibrary;
using System.Collections;
/// <summary>
/// Třída je databází odečtů. Do třídy jdou uložit pouze takové odečty,
/// které dávají smysl. Musí být dodržena rostoucí tendence dnů a stavu měřiče.
/// Záznamy jsou uloženy chronologicky.
/// </summary>
public class DatabaseOfReadings : IEnumerable
{
    public List<Reading> Data { get; } = new();
    public int Count => Data.Count;
    public int LastStateOfGauge => Data[^1].StateOfGauge;
    public DateTime LastReading => Data[^1].Date;

    public DatabaseOfReadings(Reading firstReading)
    {
        this.Add(firstReading);
    }

    public Reading this[int index]
    {
        get => Data[index];
    }

    /// <summary>
    /// Metoda zajistí přidání validního čtení vzhledem ke čtením minulým.
    /// </summary>
    /// <param name="dateOfReading">Datum odečtu.</param>
    /// <param name="actualStateOfGauge">Stav hodin při odečtu</param>
    internal void Add(Reading r)
    {
        List<Reading> tmpData = new(Data);
        tmpData.Add(r);
        tmpData.Sort(new Reading.Comparer());

        if (CheckReadingsAsc(tmpData))
        {
            Data.Add(r);
            Data.Sort(new Reading.Comparer());
        }
        else
        {
            throw new ArgumentException("Zadaná hodnota narušuje stoupající posloupnost stavu měřiče vzhledem k datumu." +
                "Odečet nemůže být nižší než jeho následovník.");
        }
    }


    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    public void Remove(int i)
    {
        Data.RemoveAt(i);
    }
    public void Edit(int index, Reading r)
    {
        List<Reading> tmpData = new(Data);
        tmpData.RemoveAt(index);
        tmpData.Add(r);
        tmpData.Sort(new Reading.Comparer());

        if (CheckReadingsAsc(tmpData))
        {
            Data.Add(r);
            Data.Sort(new Reading.Comparer());
        }
        else
        {
            throw new ArgumentException("Zadaná hodnota narušuje stoupající posloupnost stavu měřiče vzhledem k datumu." +
                "Odečet nemůže být nižší než jeho následovník.");
        }
    }
    private static bool CheckReadingsAsc(List<Reading> tmpData)
    {
        if (tmpData.Count == 0)
        {
            return true;
        }
        for (int i = 0; i < tmpData.Count - 1; i++)
        {
            if ((tmpData[i].Date - tmpData[i + 1].Date).TotalDays >= 0 ||
                (tmpData[i].StateOfGauge - tmpData[i + 1].StateOfGauge) >= 0)
            {
                return false;
            }
        }
        return true;
    }

}
