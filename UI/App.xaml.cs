using Application.Services;
using Infrastruture.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;
using UI.View;
using UI.View.Tarefa;
using UI.ViewModel.NavigationService;
using UI.ViewModel.Tarefa;
using static UI.ViewModel.Tarefa.TarefaPesquisaViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        public static ServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            Services = serviceCollection.BuildServiceProvider();

            //var mainWindow = Services.GetRequiredService<MainWindow>();
            
            //mainWindow.Show();

            base.OnStartup(e);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // DAL
            services.AddScoped<TarefaDAL>();
            services.AddScoped<EtapaDAL>();
            // Services
            services.AddScoped<TarefaService>();
            services.AddScoped<EtapaService>();
            // Navigation
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModels
            services.AddTransient<TarefaPesquisaViewModel>();
            

            
            // Views
            services.AddTransient<TarefaCadastroView>();
            services.AddTransient<TarefaPesquisaView>();
            services.AddTransient<MainWindow>();
        }

    }

}
