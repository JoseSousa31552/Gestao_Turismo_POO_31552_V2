using Turismo_V1.Models.Collections.Interfaces;
using Turismo_V1.Models.Entities;
/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Interface IBookingRepository que define os métodos para manipulação de reservas.
*/

namespace Turismo_V1.Models.Repository.Interfaces 
{ 
  public interface IBookingRepository : IRepository<Booking>
    {
        #region Methods
        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="lodging"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public bool CreateBooking(Register client, HomeLodging lodging, DateTime checkIn, DateTime checkOut);

        /// <summary>
        /// Gets all bookings made by a specific client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<Booking> GetBookingsByClient(Register client);

        /// <summary>
        /// Cancels an existing booking.
        /// </summary>
        /// <param name="booking"></param>
        public void Cancel(Booking booking);


        #endregion

    }
}
