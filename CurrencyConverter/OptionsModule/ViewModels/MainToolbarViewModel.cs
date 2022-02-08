using CurrencyConverter.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace OptionsModule.ViewModels
{
    public class MainToolbarViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        private readonly IEventAggregator eventAggregator;

        public MainToolbarViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            OpenOptionsCommand = new DelegateCommand(OpenOptionsExecute);
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;
        }

        public DelegateCommand OpenOptionsCommand { get; private set; }

        private void OpenOptionsExecute()
        {
            dialogService.ShowDialog("OptionsDialog", result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    eventAggregator.GetEvent<UpdateUIEvent>().Publish("UpdateViews");
                }
            });
        }
    }
}
