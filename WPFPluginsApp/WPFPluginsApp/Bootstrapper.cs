using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using WPFPluginsApp.ViewModels;
using WPFPluginsApp.Views;
using WPFPluginsInfrastructure;

namespace WPFPluginsApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        private const int MetricsApplicationId = 3;
        private const int ADDITIONALMODULES = 2;

        /// <summary>
        /// Creates the catalog.
        /// </summary>
        /// <returns>Module catalog.</returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var configurationCatalog = new ConfigurationModuleCatalog();
            ((AggregateModuleCatalog)this.ModuleCatalog).AddCatalog(configurationCatalog);

            return;
        }

        protected override DependencyObject CreateShell()
        {
            var menuItems = new ObservableCollection<MenuItem>();
            this.Container.RegisterInstance(menuItems);
            var queryList = new List<Func<bool>>();
            this.Container.RegisterInstance<IList<Func<bool>>>(queryList);

            var shellViewModel = new ShellViewModel();
            shellViewModel.MenuItems = menuItems;

            this.Container.RegisterInstance(shellViewModel);

            // Use the container to create an instance of the shell.
            var view = this.Container.TryResolve<ShellView>();
            //IApplicationPermissions applicationPermissions = new ApplicationPermissions(false);
            //// if (System.Diagnostics.Debugger.IsAttached) { applicationPermissions = new AllowAllPermissions(); }
            //applicationPermissions.Initialize(MetricsApplicationId, Environment.UserName);

            //this.Container.RegisterInstance(applicationPermissions);

            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeModules();

            var menuItems = this.Container.TryResolve<ObservableCollection<MenuItem>>();

            if (menuItems.All(s => s.IsEnabled == false))
            {
                MessageBox.Show("Brak modułów do których masz uprawnienia", "Uprawnienia", MessageBoxButton.OK, MessageBoxImage.Warning);

                Application.Current.Shutdown();
            }

            Application.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            var menuItems = this.Container.TryResolve<ObservableCollection<MenuItem>>();

          menuItems.Add(new MenuItem
            {
                IsEnabled = true,
             
                Header = "Ustawienia",
                Command = ShellViewModel.ShowSettingsCommand
            });
            
        }
        
    }
}
