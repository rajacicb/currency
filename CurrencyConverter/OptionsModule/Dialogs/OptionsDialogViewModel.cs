using CurrencyConverter.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OptionsModule.Dialogs
{
    internal class OptionsDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private ObservableCollection<CurrencyDetailViewModel> currencyList;
        private CurrencyDetailViewModel selectedCurrency;
        private readonly IDialogService dialogService;
        #endregion

        #region Constructor
        public OptionsDialogViewModel(IDialogService dialogService)
        {
            OKCommand = new DelegateCommand(OKCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);
            AddCommand = new DelegateCommand(AddComandExecute);
            EditCommand = new DelegateCommand(EditCommandExecute, CanEditDeleteCommandExecute);
            DeleteCommand = new DelegateCommand(DeleteCommandExecute, CanEditDeleteCommandExecute);
            this.dialogService = dialogService;
            PopulateCurrencyList();
        }
        #endregion

        #region Properties
        public ObservableCollection<CurrencyDetailViewModel> CurrencyList
        {
            get { return currencyList; }
            set { SetProperty(ref currencyList, value); }
        }

        public CurrencyDetailViewModel SelectedCurrency
        {
            get { return selectedCurrency; }
            set
            {
                SetProperty(ref selectedCurrency, value);
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand OKCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public DelegateCommand AddCommand { get; private set; }

        public DelegateCommand EditCommand { get; private set; }

        public DelegateCommand DeleteCommand { get; private set; }
        #endregion

        #region Command Methods
        private void AddComandExecute()
        {
        }

        private void EditCommandExecute()
        {
        }

        private void DeleteCommandExecute()
        {
            // can't remove what are static default values
            CurrencyList.Remove(SelectedCurrency);
        }

        private bool CanEditDeleteCommandExecute()
        {
            return SelectedCurrency != null;
        }

        private void OKCommandExecute()
        {
            // Save to XML file

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        private void CancelCommandExecute()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }
        #endregion

        #region IDialogAware Implementation
        public string Title => "Currency Settings";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        #endregion

        #region Private Methods
        private void PopulateCurrencyList()
        {
            List<CurrencyDetailViewModel> currencies = new List<CurrencyDetailViewModel>();

            foreach (Currency item in CurrencyConverter.Core.Helpers.StaticCurrencies.GetDefaultCurrencies())
            {
                currencies.Add(new CurrencyDetailViewModel(item));
            }

            CurrencyList = new ObservableCollection<CurrencyDetailViewModel>(currencies);
        }
        #endregion
    }
}
