using CurrencyConverter.Core.Models;
using System.Xml.Serialization;

namespace CurrencyConverter.Core.Serializaton.XML
{
    public class CurrencyWrapper
    {
        [XmlArray("CurrencyList"), XmlArrayItem(typeof(Currency), ElementName = "Currency")]
        public List<Currency> CurrencyList { get; set; }
    }
}
