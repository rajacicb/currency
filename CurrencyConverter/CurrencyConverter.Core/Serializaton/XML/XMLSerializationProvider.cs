using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Core.Models;
using System.Xml.Serialization;

namespace CurrencyConverter.Core.Serializaton.XML
{
    public class XMLSerializationProvider : ISerializationProvider
    {
        private static readonly string filename = $@"{AppDomain.CurrentDomain.BaseDirectory}\CurrencyList.xml";
        private readonly IDefaultCurrencyProvider defaultCurrencyProvider;

        public XMLSerializationProvider(IDefaultCurrencyProvider defaultCurrencyProvider)
        {
            this.defaultCurrencyProvider = defaultCurrencyProvider;
        }

        public List<Currency> ReadData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CurrencyWrapper));
            CurrencyWrapper currencyWrapper;

            try
            {
                using (Stream reader = new FileStream(filename, FileMode.Open))
                {
                    currencyWrapper = (CurrencyWrapper)serializer.Deserialize(reader);
                }
            }
            catch
            {
                currencyWrapper = new CurrencyWrapper();
                currencyWrapper.CurrencyList = defaultCurrencyProvider.GetDefaultCurrencies();
            }

            return currencyWrapper.CurrencyList;
        }

        public void WriteData(List<Currency> currencies)
        {
            CurrencyWrapper currencyWrapper = new CurrencyWrapper() { CurrencyList = currencies };
            XmlSerializer serializer = new XmlSerializer(typeof(CurrencyWrapper));

            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    serializer.Serialize(fs, currencyWrapper);
                }
            }
            catch
            {
            }
        }
    }
}
