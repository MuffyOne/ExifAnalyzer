using ExifAnalyzer.Common;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace MainMenu
{
    public class MainMenuModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        public MainMenuModule(IRegionManager regionManager, IUnityContainer unityContainer )
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.SideBar, typeof(MainMenuModuleView));
        }
    }
}
