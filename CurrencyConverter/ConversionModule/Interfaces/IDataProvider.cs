using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConversionModule.Interfaces
{
    internal interface IDataProvider
    {
        Task<string> GetRates(string from, List<string> to);
    }
}
