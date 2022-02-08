using ConversionModule.ViewModels;
using ConversionModule.Views;
using CurrencyConverter.Core.DataProviders;
using CurrencyConverter.Core.DataProviders.Frankfurter;
using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Core.Repository;
using CurrencyConverter.Core.Serializaton.XML;
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
            containerRegistry.RegisterSingleton<IDefaultCurrencyProvider, DefaultCurrencyProvider>();
            containerRegistry.RegisterSingleton<ISerializationProvider, XMLSerializationProvider>();
            containerRegistry.RegisterSingleton<IDataProvider, FrankfurterDataProvider>();
            containerRegistry.RegisterSingleton<ICurrencyRepository, CurrencyRepository>();
        }
    }
}
