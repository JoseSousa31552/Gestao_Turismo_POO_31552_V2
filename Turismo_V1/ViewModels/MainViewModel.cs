using System.Windows;
using System.Windows.Input;
using Turismo_V1.Models.Entities;
using Turismo_V1.Views.Enums;
using Turismo_V1.Views.Interfaces;

/**
 * Trabalho de POO
 * @author José Sousa - 31552
 * @version 1.0
 * MainViewModel que gerencia a navegação principal da aplicação.
 */

namespace Turismo_V1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields

        private readonly IViewFactory _viewFactory;
        private readonly Register _currentUser;

        public Action? HideWindowAction { get; set; }
        public ICommand OpenCheckInCommand { get; }
        public ICommand OpenHomeLodgingCommand { get; }

        public ICommand OpenBookingCommand { get; }

        public ICommand ExecuteGoBackCommand { get; }

        #endregion Fields

        #region Constructors
        public MainViewModel(IViewFactory viewFactory, Register currentUser)
        {
            _viewFactory = viewFactory;
            _currentUser = currentUser;

            OpenHomeLodgingCommand = new ViewModelCommand(OpenHomeLodging);
            OpenBookingCommand = new ViewModelCommand(OpenBooking);
            OpenCheckInCommand = new ViewModelCommand(OpenCheckIn);
            ExecuteGoBackCommand = new ViewModelCommand(ExecuteGoBack);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Abre a janela de Alojamentos.
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenHomeLodging(object parameter)
        {
            Window window = _viewFactory.CreateView(ViewType.HomeLodging, _currentUser);
          HideWindowAction?.Invoke();
            window.Show();
        }


        /// <summary>
        /// Abre a janela de Reservas.
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenBooking(object parameter)
        {
            Window window = _viewFactory.CreateView(ViewType.Booking, _currentUser);
           HideWindowAction?.Invoke();
            window.Show();
        }

        /// <summary>
        /// Abre a janela de Check-In.
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenCheckIn(object parameter)
        {
            Window window = _viewFactory.CreateView(ViewType.CheckIn, _currentUser);
            HideWindowAction?.Invoke();
            window.Show();
        }

        /// <summary>
        /// Executa a ação de fechar o programa.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteGoBack(object obj)
        {


            HideWindowAction?.Invoke();

        }

        #endregion Methods
    }
}