using Turismo_V1.Models.Collections.Interfaces;
using Turismo_V1.Models.Entities;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Interface ICheckInRepository que define os métodos para manipulação de check-ins.
*/

namespace Turismo_V1.Models.Repository.Interfaces
{
 public   interface ICheckInRepository : IRepository<CheckIN>
    {
        /// <summary>
        /// Creates a new check-in.
        /// </summary>
        /// <param name="bookingInfo"></param>
        /// <param name="dateDone"></param>
        /// <returns></returns>
        public bool CreateCheckIn(Booking bookingInfo, DateTime dateDone);

        /// <summary>
        /// Gets all check-ins made by a specific client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public List<CheckIN> GetCheckINByClient(Register client);
    }
}
