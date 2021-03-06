using ConversionModule.Helpers;
using ConversionModule.Models;
using CurrencyConverter.Core.Events;
using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Core.Models;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionModule.ViewModels
{
    public class ConversionViewModel : BindableBase
    {
        #region Fields
        private ICurrencyRepository repository;
        private double amount = 0;
        private string symbol = string.Empty;
        private string conversionResult = string.Empty;
        private ObservableCollection<Currency> fromCollection;
        private ObservableCollection<Currency> toCollection;
        private Currency selectedFrom;
        private Currency selectedTo;
        private Dictionary<string, float> codeToRate = new Dictionary<string, float>();
        #endregion

        #region Constructor
        public ConversionViewModel(ICurrencyRepository repository, IEventAggregator eventAggregator)
        {
            this.repository = repository;
            var options = OptionsSerializer.LoadOptions();

            ToCollection = new ObservableCollection<Currency>(repository.GetAllCurrencies());
            FromCollection = new ObservableCollection<Currency>(repository.GetAllCurrencies());
            SelectedTo = SelectWithCodeOrFirst(ToCollection, options.First().To);
            SelectedFrom = SelectWithCodeOrFirst(FromCollection, options.First().From);

            eventAggregator.GetEvent<SaveOptionsEvent>().Subscribe(OnSaveOptionsEvent);
            eventAggregator.GetEvent<UpdateUIEvent>().Subscribe(OnUpdateUIEvent);
        }
        #endregion

        #region Properties
        public double Amount
        {
            get { return amount; }
            set
            {
                SetProperty(ref amount, value);
                SetConversionResult();
            }
        }

        public string Symbol
        {
            get { return symbol; }
            set { SetProperty(ref symbol, value); }
        }

        public ObservableCollection<Currency> FromCollection
        {
            get { return fromCollection; }
            set { SetProperty(ref fromCollection, value); }
        }

        public Currency SelectedFrom
        {
            get { return selectedFrom; }
            set
            {
                SetProperty(ref selectedFrom, value);
                if (value != null)
                {
                    GetRatesAndRecalculate();
                    Symbol = value.Symbol;
                }                         
            }
        }

        public ObservableCollection<Currency> ToCollection 
        { 
            get { return toCollection; }
            set { SetProperty(ref toCollection, value); }   
        }

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
        private void GetRatesAndRecalculate()
        {
            new Task(async () =>
            {
                await GetRatesFromRepository(SelectedFrom.Code, ToCollection.Select(currency => currency.Code).ToList());
                SetConversionResult();
            }).Start();
        }
        private async Task GetRatesFromRepository(string from, List<string> to)
        {
            codeToRate = await repository.GetRates(from, to);
        }

        private void SetConversionResult()
        {
            if (selectedFrom == null || selectedTo == null || !codeToRate.ContainsKey(selectedTo.Code))
            {
                return;
            }
            ConversionResult = $"{Math.Round(Amount, 2)} {selectedFrom.Name} = {ConvertAmount()} {selectedTo.Name}";
        }

        private double ConvertAmount()
        {
            if (codeToRate.TryGetValue(selectedTo.Code, out float rate))
            {
                return Math.Round(amount * rate, 2);
            }
            else
            {
                return amount;
            }
        }

        private void OnSaveOptionsEvent(string message)
        {
            OptionsSerializer.SaveOptions(new List<CurrencyOption>(1) { new CurrencyOption() { From = SelectedFrom.Code, To = SelectedTo.Code } });
        }

        private void OnUpdateUIEvent(string obj)
        {
            string toCode = SelectedTo.Code;
            string fromCode = SelectedFrom.Code;

            ToCollection = new ObservableCollection<Currency>(repository.GetAllCurrencies());
            FromCollection = new ObservableCollection<Currency>(repository.GetAllCurrencies());

            SelectedTo = SelectWithCodeOrFirst(ToCollection, toCode);
            SelectedFrom = SelectWithCodeOrFirst(FromCollection, fromCode);
        }

        private Currency SelectWithCodeOrFirst(ObservableCollection<Currency> collection, string code)
        {
            var selected = collection.FirstOrDefault(x => x.Code.Equals(code));

            return selected != null ? selected : collection.First();
        }
        #endregion
    }
}
