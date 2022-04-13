namespace EnergyMonitorLibrary
{
    public interface IPricesSaveableLoadable
    {
        public Price[] Save();
        public void Load(Price? [] loadedPrices);
    }
}
