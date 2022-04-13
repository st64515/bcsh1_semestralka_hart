namespace EnergyMonitorLibrary
{
    public interface ITaxesSaveableLoadable
    {
        public Tax[] Save();
        public void Load(Tax? [] loadedTaxes);
    }
}
