using CurrencyConverter.Core.Interfaces;
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
        private readonly ISerializationProvider serializationProvider;
        private readonly IDefaultCurrencyProvider defaultCurrencyProvider;
        private readonly ICurrencyRepository currencyRepository;
        #endregion

        #region Constructor
        public OptionsDialogViewModel(IDialogService dialogService, 
                                      ISerializationProvider serializationProvider, 
                                      IDefaultCurrencyProvider defaultCurrencyProvider,
                                      ICurrencyRepository currencyRepository)
        {
            OKCommand = new DelegateCommand(OKCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);
            AddCommand = new DelegateCommand(AddComandExecute);
            EditCommand = new DelegateCommand(EditCommandExecute, CanEditCommandExecute);
            DeleteCommand = new DelegateCommand(DeleteCommandExecute, CanDeleteCommandExecute);
            this.dialogService = dialogService;
            this.serializationProvider = serializationProvider;
            this.defaultCurrencyProvider = defaultCurrencyProvider;
            this.currencyRepository = currencyRepository;
            PopulateCurrencyList();
        }
        #endregion

        #region Properties
        public ObservableCollection<CurrencyItemViewModel> CurrencyCollection
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
                        currency.Code = currency.DisplayCode;
                        CurrencyCollection.Add(currency);
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
            CurrencyCollection.Remove(SelectedCurrency);
        }

        private bool CanEditCommandExecute()
        {
            return SelectedCurrency != null;
        }

        // Only allow removal of user added currencies
        private bool CanDeleteCommandExecute()
        {
            return SelectedCurrency != null && !defaultCurrencyProvider.GetDefaultCurrencies().Any(x => x.Code == SelectedCurrency.Code);
        }

        private void OKCommandExecute()
        {
            List<Currency> currenciesToPersist = new List<Currency>(CurrencyCollection.Count);

            foreach(var item in CurrencyCollection)
            {
                currenciesToPersist.Add(item.ToModel());
            }

            serializationProvider.WriteData(currenciesToPersist);
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

            foreach (Currency item in currencyRepository.GetAllCurrencies())
            {
                currencies.Add(new CurrencyItemViewModel(item));
            }

            CurrencyCollection = new ObservableCollection<CurrencyItemViewModel>(currencies);
        }
        #endregion
    }
}
