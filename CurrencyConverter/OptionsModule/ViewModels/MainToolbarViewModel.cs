using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace OptionsModule.ViewModels
{
    public class MainToolbarViewModel : BindableBase
    {
        private readonly IDialogService dialogService;

        public MainToolbarViewModel(IDialogService dialogService)
        {
            OpenOptionsCommand = new DelegateCommand(OpenOptionsExecute);
            this.dialogService = dialogService;
        }

        public DelegateCommand OpenOptionsCommand { get; private set; }

        private void OpenOptionsExecute()
        {
            dialogService.ShowDialog("OptionsDialog", result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    // Saved to XML, Publish event to refresh all views
                }
            });
        }
    }
}
