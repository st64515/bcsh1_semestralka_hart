namespace EnergyMonitorLibrary;
public class Price : IComparable<Price>
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double Cost { get; init; }

    public Price(DateTime startDate, DateTime endDate, double cost)
    {
        StartDate = startDate;
        EndDate = endDate;
        Cost = cost;
    }
    public int CompareTo(Price? other) => StartDate.CompareTo(other?.StartDate);

    public class PriceTagIntervalComparer : IComparer<Price>
    {
        public int Compare(Price? x, Price? y)
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
    new public string ToString()
    {
        return $"- od {StartDate} do {EndDate}: {Cost}Kč/kWh";
    }
}