using Turismo_V1.Models.Collections;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;
/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe CheckInRepository que implementa os métodos para manipulação de check-ins.
*/

namespace Turismo_V1.Models.Repository
{
    public class CheckInRepository : Repository<CheckIN>, ICheckInRepository
    {
        #region Constructors
        public CheckInRepository() : base("CheckIn.json")
        {
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Creates a new CheckIN
        /// </summary>
        /// <param name="bookingInfo"></param>
        /// <param name="dateDone"></param>
        /// <returns></returns>
        public bool CreateCheckIn(Booking bookingInfo, DateTime dateDone)
        {
            
            if(bookingInfo == null)
            {
                return false;
            }

            if (dateDone == default || dateDone.Date > bookingInfo.CheckOut.Date|| dateDone.Date < bookingInfo.CheckIn.Date)
            {
                return false;
            }

            var existingCheckIn = Find(c => c.BookingInfo.ID == bookingInfo.ID); 
            if (existingCheckIn != null)
            {
                return false;
            }

            CheckIN newCheckIN = new CheckIN(bookingInfo,dateDone);
          
            Add(newCheckIN);

            return true;
        }


        /// <summary>
        /// Gets all check-ins made by a specific client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<CheckIN> GetCheckINByClient(Register client)
        {
            if (client == null)
                return new List<CheckIN>();

            return GetAll()
                   .Where(c => c.BookingInfo.Client.ID == client.ID)
                   .ToList();
        }
        #endregion Methods
    }
}


