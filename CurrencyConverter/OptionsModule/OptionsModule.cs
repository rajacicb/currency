using OptionsModule.Dialogs;
using OptionsModule.ViewModels;
using OptionsModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace OptionsModule
{
    public class OptionsModule : IModule
    {
        private readonly IRegionManager regionManager;

        public OptionsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
           regionManager.Regions["MainToolbarRegion"].Add(containerProvider.Resolve<MainToolbarView>());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           ViewModelLocationProvider.Register<MainToolbarView, MainToolbarViewModel>();
           containerRegistry.RegisterDialog<OptionsDialog, OptionsDialogViewModel>();
           containerRegistry.RegisterDialog<EditCurrencyDialog, EditCurrencyDialogViewModel>();
        }
    }
}
