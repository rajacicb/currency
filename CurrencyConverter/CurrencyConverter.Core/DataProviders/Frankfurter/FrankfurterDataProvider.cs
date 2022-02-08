using CurrencyConverter.Core.Interfaces;

namespace CurrencyConverter.Core.DataProviders.Frankfurter
{
    public class FrankfurterDataProvider : IDataProvider
    {
        private static string API_URL = "https://api.frankfurter.app/latest";

        public async Task<string> GetRates(string from, List<string> to)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync($"{API_URL}?from={from}&&to={string.Join(",", to)}");
                    result.EnsureSuccessStatusCode();
                    return await result.Content.ReadAsStringAsync();
                }      
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
