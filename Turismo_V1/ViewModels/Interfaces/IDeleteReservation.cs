using Turismo_V1.Models.Entities;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Interface IDeleteReservation que define o método para cancelar uma reserva.
*/

namespace Turismo_V1.ViewModels.Interfaces
{
  public  interface IDeleteReservation
    {
        /// <summary>
        /// Cancels an existing booking.
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public bool CancelBooking(Booking booking);
    }
}
