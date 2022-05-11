namespace EnergyMonitorLibrary;
using System.Collections;

/// <summary>
/// Třída je databází cen. Do třídy jdou uložit pouze validní intervaly,
/// které se navzájem nepřekrývají.
/// Implementuje rozhraní IEnumerable.
/// </summary>
public class PricesDatabase : IEnumerable, IPricesSaveableLoadable
{
    public List<Price> Data { get; } = new();
    public int Count => Data.Count;


    /// <summary>
    /// Metoda sloužící k zadání nové validní ceny do databáze cen.
    /// </summary>
    /// <param name="dateStart">Počáteční datum cenovky.</param>
    /// <param name="dateEnd">Expirační datum cenovky.</param>
    /// <param name="price">Cena za jednotku.</param>
    public void Add(Price? newPrice)
    {
        if (newPrice == null)
        {
            return;
        }
        if (newPrice.StartDate > newPrice.EndDate)
        {
            throw new ArgumentException("Počáteční datum musí být před datumem konce.");
        }
        if (Data.Count > 0 && newPrice.EndDate > Data.First().StartDate.AddYears(1))
        {
            throw new ArgumentException("Nelze spravovat více než jeden rok. " +
                $"Nová cena musí končit nejpozději dne {Data.First().StartDate.AddYears(1).ToString("dd/MM/yyyy")}");
        }
        if (newPrice.EndDate > newPrice.StartDate.AddYears(1))
        {
            throw new ArgumentException("Nelze spravovat více než jeden rok.");
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
    public void Edit(int index, Price editedPrice)
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


    public IEnumerator GetEnumerator() => Data.GetEnumerator();

    public Price[] Save()
    {
        Price[] pricesArray = new Price[Data.Count];
        Data.CopyTo(pricesArray, 0);

        return pricesArray;
    }

    public void Load(Price?[] loadedPrices)
    {
        Erase();

        for (int i = 0; i < loadedPrices.Length; i++)
        {
            Add(loadedPrices[i]);
        }
    }

    public Price this[int index]
    {
        get => Data[index];
    }

    public double GetVeightedAverageYearConsumption()
    {
        double sum = 0;
        int daysInPricelist = (Data.Last().EndDate - Data.First().StartDate).Days + 1;

        for (int i = 0; i < Data.Count; i++)
        {
            int daysBetween = (Data[i].EndDate - Data[i].StartDate).Days + 1;
            sum += (daysBetween * (Data[i].Cost));
        }

        return (sum / daysInPricelist);
    }

    public double GetAveragePriceIn(DateTime startDate, DateTime endDate)
    {
        double sumPrice = 0;

        //najdi ve kterém intervalu začíná
        int iStart = 0;
        for (int i = 0; i < Data.Count; i++)
        {
            if (startDate >= Data[i].StartDate &&
               startDate <= Data[i].EndDate)
            {
                iStart = i;
                break;
            }
        }

        //najdi ve kterém intervalu končí
        int iEnd = 0;
        for (int i = 0; i < Data.Count; i++)
        {
            if (endDate >= Data[i].StartDate &&
               endDate <= Data[i].EndDate)
            {
                iEnd = i;
                break;
            }
        }

        int countOfCountingIntervals = iEnd - iStart;

        //začíná a končí ve stejném intervalu?
        if (iStart == iEnd)
        {
            //dej cenu z tohoto intervalu
            int daysInThisInterval = (endDate - startDate).Days + 1;
            sumPrice += daysInThisInterval * Data[iStart].Cost;
        }
        else
        {
            //dej cenu z více intervalů
            for (int i = iStart; i <= iEnd; i++)
            {
                //započítej z prvního intervalu (pouze některé dny z konce)
                if (i == iStart)
                {
                    int daysInThisInterval = (Data[i].EndDate - startDate).Days + 1;
                    sumPrice += daysInThisInterval * Data[i].Cost;
                }
                //započítej z prostředního intervalu (všechny dny)
                else if (i > iStart && i < iEnd)
                {
                    int daysInThisInterval = (Data[i].EndDate - Data[i].StartDate).Days + 1;
                    sumPrice += daysInThisInterval * Data[i].Cost;
                }
                //započítej z koncového intervalu (pouze některé dny z počátku)
                else if (i == iEnd)
                {
                    int daysInThisInterval = (endDate - Data[i].StartDate).Days + 1;
                    sumPrice += daysInThisInterval * Data[i].Cost;
                }
            }
        }

        int daysTotal = (endDate - startDate).Days + 1;
        return sumPrice / daysTotal;
    }
}
