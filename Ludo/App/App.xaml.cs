using Ludo.Service;
using Ludo.View;
using Ludo.ViewModels;
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
            ServiceCollection serviceCollection = InitializeServices();
            InitializeWindow(serviceCollection);

            base.OnStartup(e);
        }

        private ServiceCollection InitializeServices()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(serviceCollection);

            serviceCollection.AddSingleton<NavigationService>();
            serviceCollection.AddSingleton<MainWindow>();

            serviceCollection.AddSingleton<StartingPageVM>();
            serviceCollection.AddSingleton<StartingPage>();

            serviceCollection.AddScope<BoardPage>();
            serviceCollection.AddScope<BoardVM>();

            return serviceCollection;
        }

        public void InitializeWindow(ServiceCollection serviceCollection)
        {
            NavigationService navService = serviceCollection.GetService<NavigationService>();
            StartingPage startingPage = serviceCollection.GetService<StartingPage>();
            navService.NavigateTo(startingPage);

            this.MainWindow = serviceCollection.GetService<MainWindow>();
            this.MainWindow.Show();
        }
    }
}
