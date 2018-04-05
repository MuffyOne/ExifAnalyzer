using ExifAnalyzer.Common;
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
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(DirectoryAnalyzerView));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(FileAnalyzerView));
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ResultsView));
        }
    }
}
