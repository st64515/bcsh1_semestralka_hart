namespace EnergyMonitorLibrary;
using System.Collections;

/// <summary>
/// Třída je databází odečtů. Do třídy jdou uložit pouze takové odečty,
/// které dávají smysl. Musí být dodržena rostoucí tendence dnů a stavu měřiče.
/// Záznamy jsou uloženy chronologicky.
/// </summary>
public class ReadingsDatabase : IEnumerable
{
    public List<Reading> Data { get; } = new();
    public int Count => Data.Count;
    public int LastStateOfGauge => Data[^1].StateOfGauge;
    public DateTime LastReading => Data[^1].Date;
    
    /*
    public ReadingsDatabase(Reading firstReading)
    {
        this.Add(firstReading);
    }
    */

    public ReadingsDatabase()
    {

    }


    /// <summary>
    /// Metoda zajistí přidání validního odečtu vzhledem k odečtům minulým.
    /// </summary>
    /// <param name="dateOfReading">Datum odečtu.</param>
    /// <param name="actualStateOfGauge">Stav hodin při odečtu</param>
    public void Add(Reading newReading)
    {
        if(newReading.StateOfGauge < 0)
        {
            throw new ArgumentException("Odečet nesmí být záporná hodnota.");
        }

        List<Reading> tmpData = new(Data);
        tmpData.Add(newReading);
        tmpData.Sort(new Reading.Comparer());

        if (ReadingsAreAscending(tmpData))
        {
            Data.Add(newReading);
            Data.Sort(new Reading.Comparer());
        }
        else
        {
            throw new ArgumentException("Zadaná hodnota narušuje stoupající posloupnost stavu měřiče vzhledem k datumu." +
                "Odečet nemůže být nižší než jeho následovník.");
        }
    }

    /// <summary>
    /// Metoda sloužící k editaci existujícího odečtu.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="editedReading"></param>
    public void Edit(int index, Reading editedReading)
    {
        List<Reading> tmpData = new(Data);
        tmpData.RemoveAt(index);
        tmpData.Add(editedReading);
        tmpData.Sort(new Reading.Comparer());

        if (ReadingsAreAscending(tmpData))
        {
            Data.RemoveAt(index);
            Data.Add(editedReading);
            Data.Sort(new Reading.Comparer());
        }
        else
        {
            throw new ArgumentException("Zadaná hodnota narušuje stoupající posloupnost stavu měřiče vzhledem k datumu. " +
                "Odečet nemůže být nižší než jeho následovník.");
        }
    }

    /// <summary>
    /// Metoda sloužící k odebrání odečtu z určitého indexu.
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

    /// <summary>
    /// Enumerátor odečtů
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }
    
    public Reading this[int index]
    {
        get => Data[index];
    }
    
    private static bool ReadingsAreAscending(List<Reading> tmpData)
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
