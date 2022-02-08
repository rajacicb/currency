using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core.Interfaces
{
    public interface ISerializationProvider
    {
        List<Currency> ReadData();
        void WriteData(List<Currency> currencies);
    }
}
