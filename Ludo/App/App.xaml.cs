using Ludo.Service;
using System;
using System.Windows;

namespace Ludo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(serviceCollection);
            
            base.OnStartup(e);
        }
    }
}
