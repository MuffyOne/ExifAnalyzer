using ExifAnalyzer.Common;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DirectoryAnalyzer
{
    public class MainModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public MainModule(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.container = container;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(DirectoryAnalyzerView));
        }
    }
}
