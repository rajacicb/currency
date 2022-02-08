using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core.Interfaces
{
    public interface ICurrencyRepository
    {
        public Task<Dictionary<string, float>> GetRates(string from, List<string> to);

        public List<Currency> GetAllCurrencies();
    }
}
