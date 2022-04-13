namespace EnergyMonitorLibrary;
using System.Text;
public class Meter
{
    public List<Reading> Readings => readings.Data;
    public List<Price> Prices => prices.Data;
    public List<Tax> Taxes => taxes.Data;

    private readonly ReadingsDatabase readings;
    private readonly PricesDatabase prices;
    private readonly TaxesDatabase taxes;

    public Meter()
    {
        readings = new();
        prices = new();
        taxes = new();
    }

    //READINGS
    public void AddReading(Reading newReading)
    {
        readings.Add(newReading);
    }

    public void EditReading(int index, Reading editedReading)
    {
        readings.Edit(index, editedReading);
    }

    public void RemoveReading(int index)
    {
        readings.Remove(index);
    }

    //PRICES
    public void AddPrice(Price newPrice)
    {
        prices.Add(newPrice);
    }

    public void EditPrice(int index, Price editedPrice)
    {
        prices.Edit(index, editedPrice);
    }

    public void RemovePrice(int index)
    {
        prices.Remove(index);
    }

    //TAXES
    public void AddTax(Tax newTax)
    {
        taxes.Add(newTax);
    }

    public void EditTax(int index, Tax editedTax)
    {
        taxes.Edit(index, editedTax);
    }

    public void RemoveTax(int index)
    {
        taxes.Remove(index);
    }




    internal string PrintPrices()
    {
        StringBuilder sb = new();
        for (int i = 0; i < prices.Count; i++)
        {
            sb.AppendLine(prices[i].ToString());

        }
        return sb.ToString();
    }


    /// <summary>
    /// Metoda sloužící k výpisu posledních n záznamů odečtů.
    /// </summary>
    /// <param name="n">Počet záznamů k výpisu</param>
    /// <returns>String s odečty</returns>
    public string PrintLastReadings(int n)
    {
        StringBuilder sb = new();
        if (n < 0)
        {
            return string.Empty;
        }

        for (int i = readings.Count - 1; i >= 0; i--)
        {
            if (i >= n)
            {
                break;
            }
            sb.AppendLine(readings[i].ToString());

        }
        return sb.ToString();
    }

    /// <summary>
    /// Metoda sloužící k výpisu všech poplatků.
    /// </summary>
    /// <returns>String s poplatky</returns>
    public string PrintTaxes()
    {
        StringBuilder sb = new();
        for (int i = 0; i < taxes.Count; i++)
        {
            sb.AppendLine(taxes[i].ToString());

        }
        return sb.ToString();
    }

    public string PrintCalculation()
    {
        StringBuilder sb = new();
        /*
        sb.AppendLine("DATUM\t\t|STAV\t|DNI\t|SPOTR.\t|PRUM.\t|UTRAC.\t|POPLATKY");

        DateTime initDay = Readings[0].Date;
        int lastGaugeState = Readings[0].StateOfGauge;
        DateTime lastDateOfReading = Readings[0].Date;
        sb.AppendLine($"{lastDateOfReading}\t|{lastGaugeState}\t|####\t|####\t|####\t|####\t|####");


        for (int i = 1; i < Readings.Count; i++)
        {
            double AVGConsumption = AVGConsumptionBetween(Readings[i].StateOfGauge, lastGaugeState, Readings[i].Date, lastDateOfReading);
            sb.AppendLine(
                $"{Readings[i].Date}\t|" +
                $"{Readings[i].StateOfGauge}\t|" +
                $"{DaysBetween(Readings[i].Date, lastDateOfReading)}\t|" +
                $"{ConsumptionBetween(Readings[i].StateOfGauge, lastGaugeState)}\t|" +
                $"{AVGConsumption}\t|" +
                $"{GetCostBetween(lastDateOfReading, Readings[i].Date, AVGConsumption)}\t|" +
                $"####");
            lastGaugeState = Readings[i].StateOfGauge;
            lastDateOfReading = Readings[i].Date;

        }*/
        return sb.ToString();

    }



    private double GetCostBetween(DateTime lastDateOfReading, DateTime date, double AVGConsumption)
    {
        //TODO dodelat vypocet na zaklade funkce prices.GetPriceFromDate(aktualni den v danem intervalu)
        return -1;
    }

    private static int DaysBetween(DateTime date, DateTime lastDateOfReading)
    {
        return (date - lastDateOfReading).Days;
    }


    private static int ConsumptionBetween(int stateOfGauge, int lastGaugeState)
    {
        return (stateOfGauge - lastGaugeState);
    }

    private static double AVGConsumptionBetween(int stateOfGauge, int lastGaugeState, DateTime date, DateTime lastDateOfReading)
    {
        return ConsumptionBetween(stateOfGauge, lastGaugeState) / (DaysBetween(date, lastDateOfReading));
    }


    public void Save(string fileName)
    {
        {
            ReadingsFileSerializerDeserializer saver = new(readings, fileName);
            saver.Save();
        }
        {
            PricesFileSerializerDeserializer saver = new(prices, fileName);
            saver.Save();
        }
        {
            TaxesFileSerializerDeserializer saver = new(taxes, fileName);
            saver.Save();
        }
    }

    public void Load(string fileName)
    {
        {
            ReadingsFileSerializerDeserializer loader = new(readings, fileName);
            loader.Load();
        }
        {
            PricesFileSerializerDeserializer loader = new(prices, fileName);
            loader.Load();
        }
        {
            TaxesFileSerializerDeserializer loader = new(taxes, fileName);
            loader.Load();
        }

    }
}