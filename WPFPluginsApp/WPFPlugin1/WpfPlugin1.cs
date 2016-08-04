using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Regions;
using WPFPluginsInfrastructure;

namespace WPFPlugin1
{
    public class WpfPlugin1 : Modularity
    {
        public WpfPlugin1(IUnityContainer container, IRegionManager regionManager ) : base(container, regionManager)
        {
            this.MainWindowType = typeof(Views.Main);
        }

        /// <summary>
        /// Checks the module active.
        /// </summary>
        /// <returns>True if there are any rights to the module.</returns>
        public override bool CheckModuleActive()
        {
            return true;
        }

        public override MenuItem GetMenuItem() => new MenuItem
        {
            Header = "Plugin1"
        };
    }
}
