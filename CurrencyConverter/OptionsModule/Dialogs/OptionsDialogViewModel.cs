using CurrencyConverter.Core.Helpers;
using CurrencyConverter.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OptionsModule.Dialogs
{
    internal class OptionsDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private ObservableCollection<CurrencyItemViewModel> currencyList;
        private CurrencyItemViewModel selectedCurrency;
        private readonly IDialogService dialogService;
        #endregion

        #region Constructor
        public OptionsDialogViewModel(IDialogService dialogService)
        {
            OKCommand = new DelegateCommand(OKCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);
            AddCommand = new DelegateCommand(AddComandExecute);
            EditCommand = new DelegateCommand(EditCommandExecute, CanEditCommandExecute);
            DeleteCommand = new DelegateCommand(DeleteCommandExecute, CanDeleteCommandExecute);
            this.dialogService = dialogService;
            PopulateCurrencyList();
        }
        #endregion

        #region Properties
        public ObservableCollection<CurrencyItemViewModel> CurrencyList
        {
            get { return currencyList; }
            set { SetProperty(ref currencyList, value); }
        }

        public CurrencyItemViewModel SelectedCurrency
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
            var paramaters = new DialogParameters();
            paramaters.Add("selectedCurrency", new CurrencyItemViewModel() { Name = "New Currency" });

            dialogService.ShowDialog("EditCurrencyDialog", paramaters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    if (result.Parameters.TryGetValue("editedModel", out CurrencyItemViewModel currency))
                    {
                        CurrencyList.Add(currency);
                    }
                }
            });
        }

        private void EditCommandExecute()
        {
            var paramaters = new DialogParameters();
            paramaters.Add("selectedCurrency", SelectedCurrency.Clone());

            dialogService.ShowDialog("EditCurrencyDialog", paramaters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    if (result.Parameters.TryGetValue("editedModel", out CurrencyItemViewModel currency))
                    {
                        SelectedCurrency.CopyFrom(currency);
                    }
                }
            });
        }

        private void DeleteCommandExecute()
        {
            CurrencyList.Remove(SelectedCurrency);
        }

        private bool CanEditCommandExecute()
        {
            return SelectedCurrency != null;
        }

        // Only allow removal of user added currencies
        private bool CanDeleteCommandExecute()
        {
            return SelectedCurrency != null && !StaticCurrencies.GetDefaultCurrencies().Any(x => x.Code == SelectedCurrency.Code);
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
            List<CurrencyItemViewModel> currencies = new List<CurrencyItemViewModel>();

            foreach (Currency item in StaticCurrencies.GetDefaultCurrencies())
            {
                currencies.Add(new CurrencyItemViewModel(item));
            }

            CurrencyList = new ObservableCollection<CurrencyItemViewModel>(currencies);
        }
        #endregion
    }
}
