using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversionModule.Interfaces
{
    internal interface ICurrencyRepository
    {
        public Task<Dictionary<string, float>> GetRates(string from, List<string> to);
    }
}
