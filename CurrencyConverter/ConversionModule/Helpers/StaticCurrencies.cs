using ConversionModule.Models;
using System.Collections.Generic;

namespace ConversionModule.Helpers
{
    internal static class StaticCurrencies
    {
        private static List<Currency> currencies = new List<Currency>(34)
        {
            new Currency("Australia Dollar", "AUD", "$"),
            new Currency("Bulgaria Lev", "BGN", "лв"),
            new Currency("Brazil Real", "BRL", "R$"),
            new Currency("Canadian Dollar", "CAD","$"),
            new Currency("Swiss Franc", "CHF", "CHF"),
            new Currency("China Yuan Renminbi", "CNY", "¥"),
            new Currency("Czech Koruna", "CZK", "Kč"),
            new Currency("Denmark Krone", "DKK", "kr"),
            new Currency("Canadian Dollar", "CAD","$"),
            new Currency("Euro", "EUR", "€"),
            new Currency("British Pound", "GBP", "£"), 
            new Currency("Hong Kong Dollar", "HKD", "$"), 
            new Currency("Croatian Kuna", "HRK", "kn"), 
            new Currency("Hungarian Forint", "HUF", "Ft"), 
            new Currency("Indonesian Rupiah", "IDR", "Rp"), 
            new Currency("Israeli New Sheqel", "ILS", "₪"), 
            new Currency("Indian Rupee", "INR", "₹"), 
            new Currency("Icelandic Króna", "ISK", "kr"), 
            new Currency("Japanese Yen", "JPY", "¥"), 
            new Currency("South Korean Won", "KRW", "₩"),
            new Currency("Mexican Peso", "MXN", "$"),
            new Currency("Malaysian Ringgit", "MYR", "RM"),
            new Currency("Norwegian Krone", "NOK", "kr"),
            new Currency("New Zealand Dollar", "NZD", "$"),
            new Currency("Philippine Peso", "PHP", "₱"),
            new Currency("Polish Złoty", "PLN", "zł"),
            new Currency("Romanian Leu", "RON", "lei"),
            new Currency("Russian Ruble", "RUB", "₽"),
            new Currency("Swedish Krona", "SEK", "kr"),
            new Currency("Singapore Dollar", "SGD", "$"),
            new Currency("Thai Baht", "THB", "฿"),
            new Currency("Turkish Lira", "TRY", "₺"),
            new Currency("United States Dollar", "USD", "$"),
            new Currency("South African Rand", "ZAR", "R")
        };

        public static List<Currency> GetDefaultCurrencies()
        {
            return currencies;
        }
    }
}
