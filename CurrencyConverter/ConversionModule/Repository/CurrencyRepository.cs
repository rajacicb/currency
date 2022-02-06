using ConversionModule.DataProviders.Frankfurter.DTO;
using ConversionModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConversionModule.Repository
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        #region Fields
        private IDataProvider dataProvider;
        #endregion

        #region Constructor
        public CurrencyRepository(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
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
