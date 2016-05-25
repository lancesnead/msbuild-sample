using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //time to demo some C# 6 features
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var name = $"{this.MainWindow.Name} - WpfApplication";
            var title = this.MainWindow?.Title;
        }
    }
}
