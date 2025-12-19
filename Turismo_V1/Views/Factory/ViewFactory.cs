using System.Windows;
using Turismo_V1.Views.Interfaces;
using Turismo_V1.Views.Enums;
using Microsoft.Extensions.DependencyInjection;
using Turismo_V1.ViewModels;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;

namespace Turismo_V1.Views.Factory
{
    /// <summary>
    /// Factory class responsible for creating WPF Windows and their associated ViewModels.
    /// </summary>
    public class ViewFactory : IViewFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Public Methods

        /// <summary>
        /// Creates a WPF Window based on the specified <see cref="ViewType"/> and optional parameter.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Window CreateView(ViewType type, object? parameter = null)
        {
            Window window = type switch
            {
                ViewType.Login => _serviceProvider.GetRequiredService<LoginWindow>(),
                ViewType.Main => _serviceProvider.GetRequiredService<MainWindow>(),
                ViewType.Registry => _serviceProvider.GetRequiredService<RegistryWindow>(),
                ViewType.HomeLodging => _serviceProvider.GetRequiredService<HomeLodgingView>(),
                ViewType.Booking => _serviceProvider.GetRequiredService<BookingView>(),
                ViewType.CheckIn => _serviceProvider.GetRequiredService<CheckInView>(),
                ViewType.BookingConfirmation => _serviceProvider.GetRequiredService<BookingCommand>(),
                _ => throw new NotImplementedException()
            }; 

            object viewModel = type switch
            {
               ViewType.Login => _serviceProvider.GetRequiredService<LoginViewModel>(),
               ViewType.Registry => _serviceProvider.GetRequiredService<RegistryViewModel>(),
               
               ViewType.HomeLodging => ActivatorUtilities.CreateInstance<HomeLodgingViewModel>(_serviceProvider, parameter!),
               ViewType.Main => ActivatorUtilities.CreateInstance<MainViewModel>(_serviceProvider, parameter!),
               ViewType.Booking => ActivatorUtilities.CreateInstance<BookingViewModel>(_serviceProvider, parameter!),
               ViewType.CheckIn => ActivatorUtilities.CreateInstance<CheckInViewModel>(_serviceProvider, parameter!),
               ViewType.BookingConfirmation => CreateBookingViewModelManually(_serviceProvider, parameter),
                _ => throw new NotImplementedException()
            };

            window.DataContext = viewModel;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (viewModel is LoginViewModel vmLogin)
                vmLogin.HideWindowAction = window.Close;

            if (viewModel is RegistryViewModel vmReg)
                vmReg.HideWindowAction = window.Close;

            if (viewModel is MainViewModel vmHome)
            {
                vmHome.HideWindowAction = window.Close;
            }
            if (viewModel is BookingViewModel vmBook)
            {
                vmBook.HideWindowAction = window.Close;
            }

            if (viewModel is BookingCommandViewModel vmBookConf)
            {
                vmBookConf.HideWindowAction = window.Close;
            }

            if (viewModel is HomeLodgingViewModel vmHomeLodging)
                vmHomeLodging.HideWindowAction = window.Close;


            if (viewModel is CheckInViewModel vmCheck)
                vmCheck.HideWindowAction = window.Close;

            return window;
        }

        /// <summary>
        /// Creates the BookingCommandViewModel manually due to its specific constructor requirements.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private object CreateBookingViewModelManually(IServiceProvider provider, object? parameter)
        {
           
            if (parameter is ValueTuple<HomeLodging, Register> dados)
            {
                var repo = provider.GetRequiredService<IBookingRepository>();

                
                return new BookingCommandViewModel(dados.Item1, dados.Item2, repo);
            }

            throw new ArgumentException("Parâmetros errados para BookingConfirmation");
        }
    }




    #endregion
}


