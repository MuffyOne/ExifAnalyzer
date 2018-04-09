
using MainModule;
using MainMenu;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using ExifAnalyzer.Common.Models;
using System;

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
            InitializeContainer();
            Application.Current.MainWindow = mainWindowShell;
            Application.Current.MainWindow.Show();
        }

        private void InitializeContainer()
        {
            Container.RegisterType<IResultSet, ResultSet>(new ContainerControlledLifetimeManager());
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(MainMenuModule));
            moduleCatalog.AddModule(typeof(MainModuleModule));
           

        }
    }
}
