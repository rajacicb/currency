using System;

namespace ConversionModule.Models
{
    [Serializable()]
    public class CurrencyOption
    {
       public string From { get; set; } = String.Empty;
       public string To { get; set; } = String.Empty;
    }
}
