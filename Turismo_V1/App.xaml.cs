using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Turismo_V1.Models.Repository;
using Turismo_V1.ViewModels;
using Turismo_V1.Views;
using Turismo_V1.Views.Factory;
using Turismo_V1.Views.Interfaces;
using Turismo_V1.Views.Enums;
using Turismo_V1.Models.Repository.Interfaces;
using Turismo_V1.Models.Entities;
using Turismo_V1.ViewModels.Interfaces;
using Turismo_V1.ViewModels.Services;

namespace Turismo_V1
{

    
    public partial class App : Application
    {
        
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Construtor da Aplicação. É chamado antes de tudo.
        /// </summary>
        public App()
        {
            
            ServiceCollection services = new ServiceCollection();

           
            ConfigureServices(services);

            
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Configura os serviços para injeção de dependência.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServices(IServiceCollection services)
        {
            // --- REPOSITÓRIOS ---
            
            services.AddSingleton<IRegisterRepository, RegisterRepository>();
            services.AddSingleton<IHomeLodgingRepository, HomeLodgingRepository>();
            services.AddSingleton<IBookingRepository, BookingRepository>();
            services.AddSingleton<ICheckInRepository, CheckInRepository>();
            services.AddTransient<IDeleteReservation, DeleteReservation>();
            // --- FACTORY ---

            services.AddSingleton<IViewFactory, ViewFactory>();

            // --- VIEWMODELS ---
            
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistryViewModel>();
            services.AddTransient<HomeLodgingViewModel>();
            services.AddTransient<BookingViewModel>();
            services.AddTransient<CheckInViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<BookingCommandViewModel>();

            // --- VIEWS ---
           
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();
            services.AddTransient<BookingView>();
            services.AddTransient<RegistryWindow>();
            services.AddTransient<HomeLodgingView>();
            services.AddTransient<CheckInView>();
            services.AddTransient<BookingCommand>();

        }

        /// <summary>
        /// Método chamado na inicialização da aplicação.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var viewFactory = _serviceProvider.GetRequiredService<IViewFactory>();
                Window loginWindow = viewFactory.CreateView(ViewType.Login);
                loginWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro Fatal: {ex.Message}");
            }
          }
        }
}
    