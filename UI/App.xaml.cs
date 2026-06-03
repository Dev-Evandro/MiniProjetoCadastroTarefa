using Application.Services;
using Infrastruture.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;
using UI.View;
using UI.View.Tarefa;
using UI.ViewModel.Etapa;
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

        public static ServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();

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
            
            services.AddTransient<EtapaService>();

            // Navigation
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModels
            services.AddTransient<TarefaPesquisaViewModel>();
            services.AddTransient<EtapaCadastroViewModel>();
            

            // Views
            services.AddTransient<TarefaCadastroView>();
            services.AddTransient<TarefaPesquisaView>();
            services.AddTransient<MainWindow>();
        }

    }

}
