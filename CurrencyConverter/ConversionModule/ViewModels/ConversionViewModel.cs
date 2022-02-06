﻿using ConversionModule.Helpers;
using ConversionModule.Interfaces;
using ConversionModule.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionModule.ViewModels
{
    internal class ConversionViewModel : BindableBase
    {
        #region Fields
        private ICurrencyRepository repository;
        private float amount = 0;
        private string conversionResult = string.Empty;
        private Currency selectedFrom;
        private Currency selectedTo;
        private Dictionary<string, float> codeToRate = new Dictionary<string, float>();
        #endregion

        #region Constructor
        public ConversionViewModel(ICurrencyRepository repository)
        {
            this.repository = repository;
            ToCollection = new ObservableCollection<Currency>(StaticCurrencies.GetDefaultCurrencies());
            FromCollection = new ObservableCollection<Currency>(StaticCurrencies.GetDefaultCurrencies());
            SelectedTo = ToCollection.FirstOrDefault(x => x.Code.Equals("GBP"));
            SelectedFrom = FromCollection.FirstOrDefault(x => x.Code.Equals("EUR"));
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
                GetRatesAndRecalculate();
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
            ConversionResult = $"{Amount} {selectedFrom.Name} = {Calculate()} {selectedTo.Name}";
        }

        private float Calculate()
        {
            if (codeToRate.TryGetValue(selectedTo.Code, out float rate))
            {
                return amount * rate;
            }
            else
            {
                return amount;
            }
        }
        #endregion
    }
}
