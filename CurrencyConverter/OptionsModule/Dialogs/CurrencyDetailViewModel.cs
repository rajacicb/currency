using CurrencyConverter.Core.Models;
using Prism.Mvvm;

namespace OptionsModule.Dialogs
{
    internal class CurrencyDetailViewModel : BindableBase
    {
        private string name = string.Empty;
        private string symbol = string.Empty;
        private string displayCode = string.Empty;

        public CurrencyDetailViewModel(Currency model)
        {
            name = model.Name;
            symbol = model.Symbol;
            displayCode = model.DisplayCode;
        }

        public CurrencyDetailViewModel()
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

        public CurrencyDetailViewModel Clone()
        {
            return new CurrencyDetailViewModel()
            {
                Name = this.Name,
                Symbol = this.Symbol,
                DisplayCode = this.DisplayCode
            };
        }

        public void CopyFrom(CurrencyDetailViewModel from)
        {
            this.Name = from.Name;
            this.Symbol = from.Symbol;
            this.DisplayCode = from.DisplayCode;
        }
    }
}
