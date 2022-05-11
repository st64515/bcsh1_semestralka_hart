namespace EnergyMonitorLibrary;
using System.Collections;

/// <summary>
/// Třída je databází odečtů. Do třídy jdou uložit pouze takové odečty,
/// které dávají smysl. Musí být dodržena rostoucí tendence dnů a stavu měřiče.
/// Záznamy jsou uloženy chronologicky.
/// </summary>
public class ReadingsDatabase : IEnumerable, IReadingsSaveableLoadable
{
    public List<Reading> Data { get; } = new();
    public int Count => Data.Count;
    public int LastStateOfGauge => Data[^1].StateOfGauge;
    public DateTime LastReading => Data[^1].Date;

    public ReadingsDatabase()
    {

    }

    /// <summary>
    /// Metoda zajistí přidání validního odečtu vzhledem k odečtům minulým.
    /// </summary>
    /// <param name="dateOfReading">Datum odečtu.</param>
    /// <param name="actualStateOfGauge">Stav hodin při odečtu</param>
    public void Add(Reading? newReading)
    {
        if (newReading == null)
        {
            return;
        }
        if (newReading.StateOfGauge < 0)
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

    public void Erase()
    {
        Data.Clear();
    }

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

    public Reading[] Save()
    {
        Reading[] readingsArray = new Reading[Data.Count];
        Data.CopyTo(readingsArray, 0);

        return readingsArray;
    }

    public double GetMWhBurntIn(DateTime startDate, DateTime endDate)
    {
        startDate = startDate > Data.First().Date ? startDate : Data.First().Date;
        endDate = endDate < Data.Last().Date ? endDate : Data.Last().Date;

        int iStart = 1;
        int iEnd = -1;
        double sum = 0;

        //najdi první pozdější odečet než je začátek
        for (int i = 0; i < Data.Count; i++)
        {
            if (Data[i].Date >= startDate)
            {
                iStart = i;
                break;
            }
        }

        //najdi první pozdější odečet než je konec
        for (int i = 0; i < Data.Count; i++)
        {
            if (Data[i].Date >= endDate)
            {
                iEnd = i;
                break;
            }
        }

        if (iStart == 0)
        {
            iStart = 1; //jedeme od začátku
        }

        //pokud jsou ve stejném intervalu
        if (iStart == iEnd)
        {
            double daysInThisInterval = (endDate - startDate).Days + 1;
            double mwhInThisInterval = (Data[iStart].StateOfGauge - Data[iStart - 1].StateOfGauge) * 0.001;
            double totalDaysInThisInterval = (Data[iStart].Date - Data[iStart - 1].Date).Days + 1;
            sum += (daysInThisInterval / totalDaysInThisInterval) * mwhInThisInterval;
            //FIXME
        }
        else
        {
            //dej cenu z více intervalů
            for (int i = iStart; i <= iEnd; i++)
            {
                //započítej z prvního intervalu (pouze některé dny z konce)
                if (i == iStart)
                {
                    double daysInThisInterval = (Data[i].Date - startDate).Days + 1;
                    double totalDaysInThisInterval = (Data[i].Date - Data[i - 1].Date).Days + 1;
                    double mwhInThisInterval = (Data[i].StateOfGauge - Data[i - 1].StateOfGauge) * 0.001;
                    sum += (daysInThisInterval / totalDaysInThisInterval) * mwhInThisInterval;
                }
                //započítej z prostředního intervalu (všechny dny)
                else if (i > iStart && i < iEnd)
                {
                    double mwhInThisInterval = (Data[i].StateOfGauge - Data[i - 1].StateOfGauge) * 0.001;
                    sum += 1 * mwhInThisInterval;
                }
                //započítej z koncového intervalu (pouze některé dny z počátku)
                else if (i == iEnd)
                {
                    double daysInThisInterval = (endDate - Data[i - 1].Date).Days + 1;
                    double totalDaysInThisInterval = (Data[i].Date - Data[i - 1].Date).Days + 1;
                    double mwhInThisInterval = (Data[i].StateOfGauge - Data[i - 1].StateOfGauge) * 0.001;
                    sum += (daysInThisInterval / totalDaysInThisInterval) * mwhInThisInterval;
                }
            }
        }

        return sum;
    }

    public void Load(Reading?[] loadedReadings)
    {
        Erase();

        for (int i = 0; i < loadedReadings.Length; i++)
        {
            Add(loadedReadings[i]);
        }
    }
}
