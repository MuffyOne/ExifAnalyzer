
using DirectoryAnalyzer;
using MainMenu;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace ExifAnalyzer
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Shell mainWindowShell = (Shell)Shell;

            Application.Current.MainWindow = mainWindowShell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(MainModule));
            moduleCatalog.AddModule(typeof(MainMenuModule));
        }
    }
}
