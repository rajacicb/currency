using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core.Interfaces
{
    public interface IDefaultCurrencyProvider
    {
        List<Currency> GetDefaultCurrencies();
    }
}