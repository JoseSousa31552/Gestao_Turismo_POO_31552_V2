using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;
using Turismo_V1.ViewModels.Interfaces;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe DeleteReservation que implementa o método para cancelar uma reserva.
*/

namespace Turismo_V1.ViewModels.Services
{

    public class DeleteReservation: IDeleteReservation
    {
        #region Private Fields
        private readonly IBookingRepository _bookingRepository;
        private readonly ICheckInRepository _checkInRepository;
        #endregion Private Fields

        #region Constructor
        public DeleteReservation(IBookingRepository bookingRepo, ICheckInRepository checkInRepo)
        {
            _bookingRepository = bookingRepo;
            _checkInRepository = checkInRepo;
        }
        #endregion Constructor

        #region Methods

        /// <summary>
        /// Cancela uma reserva se não houver check-in associado.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public bool CancelBooking(Booking booking)
        {

            
            if (booking == null) return false;

            
            var checkIns = _checkInRepository.GetAll();
            bool HaveCheckIn = checkIns.Any(c => c.BookingInfo.ID == booking.ID);

            if (HaveCheckIn)
            {
                return false; 
            }
            
           
            _bookingRepository.Cancel(booking);

            return true;
        }
        #endregion

    }
}
