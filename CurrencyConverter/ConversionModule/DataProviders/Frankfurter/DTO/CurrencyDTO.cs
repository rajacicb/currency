using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConversionModule.DataProviders.Frankfurter.DTO
{
    internal class CurrencyDTO
    {
        [JsonPropertyName("amount")]
        public float Amount { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        public Dictionary<string, float>? rates { get; set; }
    }
}
