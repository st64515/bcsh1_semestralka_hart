namespace EnergyMonitorLibrary
{
    public interface IReadingsSaveableLoadable
    {
        public Reading[] Save();
        public void Load(Reading?[] loadedReadings);
    }
}
