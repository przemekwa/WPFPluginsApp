using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace WPFPluginsInfrastructure
{
    public abstract class Modularity : IModule, IDisposable
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        protected string ViewName { get; set; }
        public Type MainWindowType { get; set; }
        public ObservableCollection<MenuItem> MenuItems { get; private set; }

        protected Modularity(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.MenuItems = this.container.TryResolve<ObservableCollection<MenuItem>>();
        }

        public abstract MenuItem GetMenuItem();

        public abstract bool CheckModuleActive();

        public virtual void Initialize()
        {
            MenuItem menuItem;

            menuItem = this.GetMenuItem();

            menuItem.IsEnabled = this.CheckModuleActive();

            if (menuItem.Command == null)
            {
                menuItem.Command = new Prism.Commands.DelegateCommand(this.Execute, this.CanExecute);
            }

            this.MenuItems.Add(menuItem);
            if (this.ViewName == null)
            {
                this.ViewName = this.MainWindowType + "View";
            }

            this.container.RegisterType(typeof(object), this.MainWindowType, this.ViewName);
        }

        public virtual bool CanExecute()
        {
            return this.CheckModuleActive();
        }

        public virtual void Execute()
        {
            this.regionManager.RequestNavigate("ClientRegion", this.ViewName);
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.container.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this.disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Modularity() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}
