using Turismo_V1.Models.Collections;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* BookingRepository que implementa os métodos para manipulação de reservas.
*/

namespace Turismo_V1.Models.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        #region Fields

        private readonly ICheckInRepository _checkInRepository;

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingRepository"/> class.
        /// </summary>
        public BookingRepository(ICheckInRepository checkInRepository): base("Booking.json")
        {
            _checkInRepository = checkInRepository;
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="lodging"></param>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <returns></returns>
        public bool CreateBooking(Register client, HomeLodging lodging, DateTime checkIn, DateTime checkOut)
        {
            
            if(client == null || lodging == null)
            {
                return false;
            }

            if (checkIn == default || checkOut == default || checkOut <= checkIn)
            {
                return false;
            }

            if(lodging.DateStart.Date > checkIn.Date || lodging.DateEnd.Date < checkOut.Date )
            {
                return false;
            }

            bool conflitoCheckIn = _checkInRepository.GetAll().Any(c => c.BookingInfo.Lodging.ID == lodging.ID && c.BookingInfo.CheckIn.Date < checkOut.Date &&c.BookingInfo.CheckOut.Date > checkIn.Date);

            if (conflitoCheckIn)
                return false;

            var conflitoBooking = Find(b =>b.Lodging.ID == lodging.ID && b.State &&b.CheckIn.Date < checkOut.Date && b.CheckOut.Date > checkIn.Date);

            if (conflitoBooking != null)
                return false;

            Booking newBooking = new Booking(client, lodging, checkIn, checkOut);
          
            Add(newBooking);

            return true;
        }

        /// <summary>
        /// Gets all bookings made by a specific client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<Booking> GetBookingsByClient(Register client)
        {
            if (client == null) return new List<Booking>();

            
            return GetAll()
                   .Where(b => b.Client.ID == client.ID)
                   .ToList();
        }

        /// <summary>
        /// Cancela uma reserva existente.
        /// </summary>
        /// <param name="booking"></param>
        public void Cancel(Booking booking)
        {
            
            Remove(booking);            
            booking.State = false;
            Add(booking);
        }

        #endregion Methods

    }
}


