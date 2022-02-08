namespace CurrencyConverter.Core.Models
{
    public class Currency
    {
        #region Constructors
        public Currency(string code)
        {
            this.Code = code;
        }

        public Currency(string name, string code, string symbol) :
            this(code)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.DisplayCode = code;
        }
        #endregion

        #region Properties
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public string DisplayCode { get; set; } = string.Empty;
        public string DisplayName => $"{DisplayCode} - {Name}";
        #endregion
    }
}
