namespace EnergyMonitorLibrary;
/// <summary>
/// Třída reprezentuje odečet z měřiče.
/// </summary>
public class Reading : IComparable<Reading>
{
    public int StateOfGauge { get; init; }
    public DateTime Date { get; init; }

    public Reading(DateTime date, int stateOfGauge)
    {
        StateOfGauge = stateOfGauge;
        this.Date = date;
    }
    override public string ToString()
    {
        return ($"{Date.ToString("dd/MM/yyyy")}: {StateOfGauge} kWh");
    }

    public int CompareTo(Reading? other) => Date.CompareTo(other?.Date);

    public class Comparer : IComparer<Reading>
    {
        public int Compare(Reading? x, Reading? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            else
            {
                return x.Date.CompareTo(y.Date);
            }
        }
    }
}
