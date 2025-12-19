/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe CheckIN que representa um check-in.  
*/
namespace Turismo_V1.Models.Entities
{

    public class CheckIN : IdClass
    {
        #region Private Fields
        
        private Booking _bookingInfo;
        private DateTime _dateDone;
        #endregion Private Fields

        #region Properties 

        /// <summary>
        /// Informações da reserva associada ao check-in.
        /// </summary>
        public Booking BookingInfo
        {
            get => _bookingInfo;
            set
            {
                if (value == null)
                    throw new ArgumentException("Booking information cannot be null.", nameof(BookingInfo));

                _bookingInfo = value;
            }
        }

        /// <summary>
        /// Data em que o check-in foi realizado.
        /// </summary>
        public DateTime DateDone
        {
            get => _dateDone;
            set
            {
                
                if (value == default)
                    throw new ArgumentException("DateDone cannot be default.", nameof(DateDone));

                
                if (BookingInfo != null)
                {
                    
                    if (value.Date < BookingInfo.CheckIn.Date) //
                        throw new ArgumentException("DateDone cannot be before the booking check-in date.");

                    
                    if (value.Date > BookingInfo.CheckOut.Date)
                        throw new ArgumentException("DateDone cannot be after the booking check-out date.");
                }

                _dateDone = value;
            }
        }

        #endregion Properties

        #region Constructor
        public CheckIN(Booking bookingInfo, DateTime dateDone)
        {
            BookingInfo = bookingInfo;
            DateDone = dateDone;
        }
        #endregion
    }
}
