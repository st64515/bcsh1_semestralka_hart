namespace EnergyMonitorLibrary;
using System.Text;
public class Meter
{
    public List<Reading> Readings => ReadingsDatabase.Data;
    public List<Price> Prices => PricesDatabase.Data;
    public List<Tax> Taxes => TaxesDatabase.Data;

    public PricesDatabase PricesDatabase { get; }
    public ReadingsDatabase ReadingsDatabase { get; }
    public TaxesDatabase TaxesDatabase { get; }

    public Meter()
    {
        ReadingsDatabase = new();
        PricesDatabase = new();
        TaxesDatabase = new();
    }

    //READINGS
    public void AddReading(Reading newReading)
    {
        ReadingsDatabase.Add(newReading);
    }

    public void EditReading(int index, Reading editedReading)
    {
        ReadingsDatabase.Edit(index, editedReading);
    }

    public void RemoveReading(int index)
    {
        ReadingsDatabase.Remove(index);
    }

    //PRICES
    public void AddPrice(Price newPrice)
    {
        PricesDatabase.Add(newPrice);
    }

    public void EditPrice(int index, Price editedPrice)
    {
        PricesDatabase.Edit(index, editedPrice);
    }

    public void RemovePrice(int index)
    {
        PricesDatabase.Remove(index);
    }

    //TAXES
    public void AddTax(Tax newTax)
    {
        TaxesDatabase.Add(newTax);
    }

    public void EditTax(int index, Tax editedTax)
    {
        TaxesDatabase.Edit(index, editedTax);
    }

    public void RemoveTax(int index)
    {
        TaxesDatabase.Remove(index);
    }




    internal string PrintPrices()
    {
        StringBuilder sb = new();
        for (int i = 0; i < PricesDatabase.Count; i++)
        {
            sb.AppendLine(PricesDatabase[i].ToString());

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

        for (int i = ReadingsDatabase.Count - 1; i >= 0; i--)
        {
            if (i >= n)
            {
                break;
            }
            sb.AppendLine(ReadingsDatabase[i].ToString());

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
        for (int i = 0; i < TaxesDatabase.Count; i++)
        {
            sb.AppendLine(TaxesDatabase[i].ToString());

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
            ReadingsFileSerializerDeserializer saver = new(ReadingsDatabase, fileName);
            saver.Save();
        }
        {
            PricesFileSerializerDeserializer saver = new(PricesDatabase, fileName);
            saver.Save();
        }
        {
            TaxesFileSerializerDeserializer saver = new(TaxesDatabase, fileName);
            saver.Save();
        }
    }

    public void Load(string fileName)
    {
        {
            ReadingsFileSerializerDeserializer loader = new(ReadingsDatabase, fileName);
            loader.Load();
        }
        {
            PricesFileSerializerDeserializer loader = new(PricesDatabase, fileName);
            loader.Load();
        }
        {
            TaxesFileSerializerDeserializer loader = new(TaxesDatabase, fileName);
            loader.Load();
        }

    }
}