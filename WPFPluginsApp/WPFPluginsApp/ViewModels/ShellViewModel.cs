using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPluginsInfrastructure;

namespace WPFPluginsApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Linq;
    using System.Windows.Input;
  
    using Prism.Interactivity.InteractionRequest;
    using Prism.Mvvm;

    public class ShellViewModel : BindableBase
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        
    }
}
