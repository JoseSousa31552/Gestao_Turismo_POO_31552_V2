using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;
using Turismo_V1.ViewModels.Interfaces;
using Turismo_V1.Views.Enums;
using Turismo_V1.Views.Interfaces;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* BookingViewModel que implementa a lógica para a gestão de reservas na interface de utilizador.
*/

namespace Turismo_V1.ViewModels
{
    public class BookingViewModel : BaseViewModel
    {
        #region  Fields

        private readonly IBookingRepository _bookingRepo;
        private readonly Register _currentUser;
        private readonly IDeleteReservation _deleteService; 
        private readonly IViewFactory _viewFactory;
        private readonly ICheckInRepository _checkInRepo;
       
        public Action? HideWindowAction { get; set; }

        private ObservableCollection<Booking> _bookings;
        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand GoBackMainCommand { get; }

        public ICommand CheckInCommand { get; }

        #endregion Fields

        #region Constructors
        public BookingViewModel(IBookingRepository bookingRepo, Register currentUser, IDeleteReservation deleteService, ICheckInRepository checkInRepo, IViewFactory viewFactory)
        {
            _bookingRepo = bookingRepo;
            _currentUser = currentUser;
            _deleteService = deleteService;
            _viewFactory = viewFactory;
            _checkInRepo = checkInRepo;

            var listaCompleta = _bookingRepo.GetBookingsByClient(_currentUser);
            var listaAtiva = listaCompleta.Where(b => b.State == true).ToList();
            Bookings = new ObservableCollection<Booking>(listaAtiva);

            CancelCommand = new ViewModelCommand(ExecuteCancel);
            GoBackMainCommand = new ViewModelCommand(ExecuteGoBack);
            CheckInCommand = new ViewModelCommand(ExecuteCheckIn);
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Executes the cancel command.
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteCancel(object parameter)
        {
            var bookingParaApagar = parameter as Booking;
            if (bookingParaApagar == null) return;

            var result = MessageBox.Show($"Deseja cancelar a reserva em {bookingParaApagar.Lodging.NameHomeLodging}?",
                                         "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                
                bool sucesso = _deleteService.CancelBooking(bookingParaApagar);

                if (sucesso)
                {
                    
                    Bookings.Remove(bookingParaApagar);
                    MessageBox.Show("Reserva cancelada.");
                }
                else
                {
                    MessageBox.Show("Não pode cancelar: Já existe Check-In!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Executes the check-in command.
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteCheckIn(object parameter)
        {
            var bookingParaCheckIn = parameter as Booking;
            if (bookingParaCheckIn == null) return;

            var result = MessageBox.Show($"Confirmar Check-In em {bookingParaCheckIn.Lodging.NameHomeLodging}?",
                                         "Check-In", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                
                bool sucesso = _checkInRepo.CreateCheckIn(bookingParaCheckIn, DateTime.Now);

                if (sucesso)
                {
                    _bookingRepo.Cancel(bookingParaCheckIn);
                    Bookings.Remove(bookingParaCheckIn);
                    MessageBox.Show("Check-In realizado com sucesso!");
                }
                else
                {
                    
                    MessageBox.Show("Erro: Não pode fazer Check-In hoje (fora das datas) ou já foi realizado.",
                                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Executes the go back to main command.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteGoBack(object obj)
        {
            Window window = _viewFactory.CreateView(ViewType.Main, _currentUser);
            HideWindowAction?.Invoke();
            window.Show();
        }
        #endregion Methods
    }
}