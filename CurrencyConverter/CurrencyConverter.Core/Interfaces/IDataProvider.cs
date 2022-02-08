namespace CurrencyConverter.Core.Interfaces
{
    public interface IDataProvider
    {
        Task<string> GetRates(string from, List<string> to);
    }
}
