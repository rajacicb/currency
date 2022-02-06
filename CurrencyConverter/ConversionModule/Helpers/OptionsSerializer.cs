using ConversionModule.Models;
using ConversionModule.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConversionModule.Helpers
{
    internal class OptionsSerializer
    {
        public static void SaveOptions(List<CurrencyOption> options)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, options);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                CurrencySettings.Default.CurrencyOptions = Convert.ToBase64String(buffer);
                CurrencySettings.Default.Save();
            }
        }

        public static List<CurrencyOption> LoadOptions()
        {
            if (CurrencySettings.Default.CurrencyOptions.Equals(string.Empty))
            {
                return new List<CurrencyOption>(1)
                {
                    new CurrencyOption(){ From = "EUR", To = "USD" }
                };
            }
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(CurrencySettings.Default.CurrencyOptions)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<CurrencyOption>)bf.Deserialize(ms);
            }
        }
    }
}
