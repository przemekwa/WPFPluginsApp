using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFPluginsInfrastructure
{
    public class MenuItem
    {
        public string Header { get; set; }
        //public HorizontalAlignment HorizontalAlignment { get; set; }
        public bool IsSeparator { get; set; }
        public ICommand Command { get; set; }
        public bool IsEnabled { get; set; }
    }
}
