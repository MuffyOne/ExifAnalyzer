using ExifAnalyzer.Common;
using MainModule.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace MainModule
{
    public class MainModuleModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public MainModuleModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            container.RegisterType<DirectoryAnalyzerViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<FileAnalyzerViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<ResultsViewModel>(new ContainerControlledLifetimeManager());
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(DirectoryAnalyzerView));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FileAnalyzerView));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ResultsView));
        }
    }
}
