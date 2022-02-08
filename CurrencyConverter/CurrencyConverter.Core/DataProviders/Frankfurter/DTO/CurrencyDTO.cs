using System.Text.Json.Serialization;

namespace CurrencyConverter.Core.DataProviders.Frankfurter.DTO
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
