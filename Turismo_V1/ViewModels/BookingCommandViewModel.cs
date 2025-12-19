using System.Windows;
using System.Windows.Input;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* ViewModel para o comando de reserva.
*/

namespace Turismo_V1.ViewModels
{
    public class BookingCommandViewModel : BaseViewModel
    {
        #region Private Fields
        private readonly IBookingRepository _bookingRepository;

        private readonly HomeLodging _lodging;

        private readonly Register _currentUser;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public HomeLodging Lodging => _lodging;
        public Action? HideWindowAction { get; set; }
        public ICommand ConfirmCommand { get; }
        #endregion Private Fields

        #region Constructors
        public BookingCommandViewModel(HomeLodging lodging, Register User, IBookingRepository BookingRepository)
        {
            _lodging = lodging;
            _currentUser = User;
            _bookingRepository = BookingRepository;

            CheckIn = DateTime.Today;
            CheckOut = DateTime.Today.AddDays(1);

            ConfirmCommand = new ViewModelCommand(Confirm);
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Confirma a reserva.
        /// </summary>
        /// <param name="parametro"></param>
        private void Confirm(object parametro)
        {
            bool ok = _bookingRepository.CreateBooking(_currentUser, _lodging, CheckIn, CheckOut);

            if (!ok)
            {
                MessageBox.Show("Reserva inválida ou em conflito com outra.");
                return;
            }

            MessageBox.Show("Reserva criada com sucesso!");
            HideWindowAction?.Invoke();
        }
        #endregion
    }

}