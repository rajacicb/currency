using ConversionModule.DataProviders.Frankfurter;
using ConversionModule.Interfaces;
using ConversionModule.Repository;
using ConversionModule.ViewModels;
using ConversionModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace ConversionModule
{
    public class ConversionModule : IModule
    {
        private readonly IRegionManager regionManager;

        public ConversionModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            var view = containerProvider.Resolve<ConversionView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<ConversionView, ConversionViewModel>();
            containerRegistry.Register<IDataProvider, FrankfurterDataProvider>();
            containerRegistry.Register<ICurrencyRepository, CurrencyRepository>();
        }
    }
}
