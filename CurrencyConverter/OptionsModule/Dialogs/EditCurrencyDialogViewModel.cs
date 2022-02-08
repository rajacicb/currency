using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace OptionsModule.Dialogs
{
    internal class EditCurrencyDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private CurrencyItemViewModel currencyItem;
        #endregion

        #region Constructor
        public EditCurrencyDialogViewModel()
        {
            OKCommand = new DelegateCommand(OKCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);
        }
        #endregion

        #region Properties
        public CurrencyItemViewModel CurrencyItem
        {
            get { return currencyItem; }
            set { SetProperty(ref currencyItem, value); }
        }

        public DelegateCommand OKCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }
        #endregion

        #region Command Methods
        private void OKCommandExecute()
        {
            var result = ButtonResult.OK;
            var parameters = new DialogParameters();
            parameters.Add("editedModel", CurrencyItem);

            RequestClose?.Invoke(new DialogResult(result, parameters));
        }

        private void CancelCommandExecute()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }
        #endregion

        #region IDialogAware
        public string Title => "Edit Currency";

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
            if (!parameters.TryGetValue("selectedCurrency", out CurrencyItemViewModel currency))
            {
                return;
            }

            CurrencyItem = currency;
        }
        #endregion
    }
}
