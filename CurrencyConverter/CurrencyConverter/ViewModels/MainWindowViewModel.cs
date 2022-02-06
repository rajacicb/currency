using Prism.Mvvm;

namespace CurrencyConverter.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Currency Converter Application";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public MainWindowViewModel()
        {
        }
    }
}
