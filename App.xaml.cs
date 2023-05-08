using BowlingScoreCalculator.UI;
using BowlingScoreCalculator.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BowlingScoreCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            InjectorProvider.SetServiceProvider(serviceCollection.BuildServiceProvider());
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>()
                    .AddSingleton<MainWindowViewModel>()
                    .AddSingleton<GameView>()
                    .AddSingleton<GameViewViewModel>()
                    .AddSingleton<NewGame>()
                    .AddSingleton<NewGameViewModel>()
            ;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
                var mainWindow = InjectorProvider.Get<MainWindow>();
                mainWindow.Show();
        }
    }
}
