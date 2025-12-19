using Turismo_V1.Models.Collections;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;
/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe HomeLodgingRepository que implementa os métodos para manipulação de alojamentos locais.
*/

namespace Turismo_V1.Models.Repository
{
   public class HomeLodgingRepository : Repository<HomeLodging>, IHomeLodgingRepository
    {
         #region Constructors
        public HomeLodgingRepository() : base("HomeLodging.json")
        {
        }
        #endregion Constructors


        #region Methods
        /// <summary>
        /// Creates a new home lodging.
        /// </summary>
        /// <param name="nameHomeLodging"></param>
        /// <param name="address"></param>
        /// <param name="pricePerNight"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public bool CreateHomeLodging(string nameHomeLodging, string address, double pricePerNight, DateTime dateStart, DateTime dateEnd)
        {
            var existingHomeLodging = Find(name => name.NameHomeLodging == nameHomeLodging);

           if (existingHomeLodging != null)
            {
                return false;
            }
            HomeLodging newHomeLodging = new HomeLodging(nameHomeLodging, address, pricePerNight, dateStart, dateEnd);

           Add(newHomeLodging);

            return true;
        }


        /// <summary>
        /// Get HomeLodging By Name
        /// </summary>
        /// <param name="nameHomeLodging"></param>
        /// <returns></returns>
        public HomeLodging? GetHomeLodgingByname(string nameHomeLodging)
        {
            return GetHomeLodgingname(nameHomeLodging);
        }


        /// <summary>
        /// Get HomeLodging By Name
        /// </summary>
        /// <param name="nameHomeLodging"></param>
        /// <returns></returns>
        private HomeLodging? GetHomeLodgingname(string nameHomeLodging)
        {
            if (string.IsNullOrEmpty(nameHomeLodging))
            {
                return null;
            }


            return Find(x => x.NameHomeLodging == nameHomeLodging);
        }
        #endregion Methods
    }
}

