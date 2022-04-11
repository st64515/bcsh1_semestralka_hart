namespace EnergyMonitorLibrary;
public class Tax
{
    public Tax(string name, DateTime dateStart, DateTime dateEnd, int cost, Intervals interval)
    {
        Name = name;
        StartDate = dateStart;
        EndDate = dateEnd;
        Cost = cost;
        Interval = interval;
    }

    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Cost { get; set; }
    public Intervals Interval { get; set; }

    override public string ToString()
    {
        return $"{Name}: {StartDate.ToString("dd/MM/yyyy")} - {EndDate.ToString("dd/MM/yyyy")}: {Cost} Kč {Interval}";
    }
}