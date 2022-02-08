using CurrencyConverter.Core.DataProviders.Frankfurter.DTO;
using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Core.Models;
using System.Text.Json;

namespace CurrencyConverter.Core.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        #region Fields
        private readonly IDataProvider dataProvider;
        private readonly ISerializationProvider serializationProvider;
        #endregion

        #region Constructor
        public CurrencyRepository(IDataProvider dataProvider, ISerializationProvider serializationProvider)
        {
            this.dataProvider = dataProvider;
            this.serializationProvider = serializationProvider;
        }

        public List<Currency> GetAllCurrencies()
        {
            return serializationProvider.ReadData();
        }
        #endregion

        #region Public Methods
        public async Task<Dictionary<string, float>> GetRates(string from, List<string> to)
        {
            CurrencyDTO currency = await GetFromApi(from, to);
            Dictionary<string, float> result = new Dictionary<string, float>();

            if (currency != null)
            {
                foreach (string code in to)
                {
                    if (currency.rates.TryGetValue(code, out float value))
                    {
                        result[code] = value;
                    }
                    else
                    {
                        result[code] = 1;
                    }
                }
            }

            return result;
        }
        #endregion

        #region Private Methods
        private async Task<CurrencyDTO> GetFromApi(string from, List<string> to)
        {
            try
            {
                var responseTask = dataProvider.GetRates(from, to);

                string content = await responseTask;

                if (String.IsNullOrEmpty(content))
                {
                    return new CurrencyDTO()
                    {
                        Amount = 1,
                        Base = from,
                        Date = DateTime.Now,
                        rates = new Dictionary<string, float>()
                    };
                }

                return JsonSerializer.Deserialize<CurrencyDTO>(content);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
