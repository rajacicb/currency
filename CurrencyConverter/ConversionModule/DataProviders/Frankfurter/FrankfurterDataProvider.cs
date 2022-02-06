using ConversionModule.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConversionModule.DataProviders.Frankfurter
{
    internal class FrankfurterDataProvider : IDataProvider
    {
        private static string API_URL = "https://api.frankfurter.app/latest";

        public async Task<string> GetRates(string from, List<string> to)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{API_URL}?from={from}&&to={string.Join(",", to)}");
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
