using CurrencyConverter.Core.Models;
using Prism.Mvvm;

namespace OptionsModule.Dialogs
{
    internal class CurrencyItemViewModel : BindableBase
    {
        private string name = string.Empty;
        private string symbol = string.Empty;
        private string displayCode = string.Empty;

        public CurrencyItemViewModel(Currency model)
        {
            Name = model.Name;
            Symbol = model.Symbol;
            DisplayCode = model.DisplayCode;
            Code = model.Code;
        }

        public CurrencyItemViewModel()
        {
        }

        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string Symbol
        {
            get { return symbol; }
            set { SetProperty(ref symbol, value); }
        }

        public string DisplayCode
        {
            get { return displayCode; }
            set
            {
                SetProperty(ref displayCode, value);
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName => $"{DisplayCode} - {Name}";

        public string Code { get; set; }

        public CurrencyItemViewModel Clone()
        {
            return new CurrencyItemViewModel()
            {
                Name = this.Name,
                Symbol = this.Symbol,
                DisplayCode = this.DisplayCode
            };
        }

        public void CopyFrom(CurrencyItemViewModel from)
        {
            this.Name = from.Name;
            this.Symbol = from.Symbol;
            this.DisplayCode = from.DisplayCode;
        }

        public Currency ToModel()
        {
            return new Currency()
            {
                Code = this.Code,
                Name = this.Name,
                Symbol = this.Symbol,
                DisplayCode = this.DisplayCode
            };
        }
    }
}
