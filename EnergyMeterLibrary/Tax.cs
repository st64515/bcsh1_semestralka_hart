namespace EnergyMonitorLibrary;
public class Tax
{

    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Cost { get; set; }
    public Intervals Interval { get; set; }

    public Tax(string name, DateTime dateStart, DateTime dateEnd, int cost, Intervals interval)
    {
        Description = name;
        StartDate = dateStart;
        EndDate = dateEnd;
        Cost = cost;
        Interval = interval;
    }

    public int CompareTo(Tax? other) => StartDate.CompareTo(other?.StartDate);

    public class Comparer : IComparer<Tax>
    {
        public int Compare(Tax? x, Tax? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }

    override public string ToString()
    {
        return $"{Description}: {StartDate.ToString("dd/MM/yyyy")} - {EndDate.ToString("dd/MM/yyyy")}: {Cost} Kč {Interval}";
    }
}