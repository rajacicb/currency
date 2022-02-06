using ConversionModule.Helpers;
using ConversionModule.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConversionModule.ViewModels
{
    internal class ConversionViewModel : BindableBase
    {
        #region Fields
        private float amount = 0;
        private string conversionResult = string.Empty;
        private Currency selectedFrom;
        private Currency selectedTo;
        #endregion

        #region Constructor
        public ConversionViewModel()
        {
            ToCollection = new ObservableCollection<Currency>(StaticCurrencies.GetDefaultCurrencies());
            FromCollection = new ObservableCollection<Currency>(StaticCurrencies.GetDefaultCurrencies());
            SelectedTo = ToCollection.FirstOrDefault(x => x.Code.Equals("EUR"));
            SelectedFrom = FromCollection.FirstOrDefault(x => x.Code.Equals("USD"));
        }
        #endregion

        #region Properties
        public float Amount
        {
            get { return amount; }
            set
            {
                SetProperty(ref amount, value);
                SetConversionResult();
            }
        }

        public ObservableCollection<Currency> FromCollection { get; set; }

        public Currency SelectedFrom
        {
            get { return selectedFrom; }
            set
            {
                SetProperty(ref selectedFrom, value);
                // Call async API
                SetConversionResult();
            }
        }
        public ObservableCollection<Currency> ToCollection { get; set; }

        public Currency SelectedTo
        {
            get { return selectedTo; }
            set
            {
                SetProperty(ref selectedTo, value);
                SetConversionResult();
            }
        }

        public string ConversionResult
        {
            get { return conversionResult; }
            set { SetProperty(ref conversionResult, value); }
        }
        #endregion

        #region Private Methods
        private void SetConversionResult()
        {

        }
        #endregion
    }
}
