namespace ConversionModule.Models
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
        }
        #endregion

        #region Properties
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public string DisplayName => $"{Code} - {Name}";
        #endregion
    }
}
