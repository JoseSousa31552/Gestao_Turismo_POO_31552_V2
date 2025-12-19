using Turismo_V1.Models.Collections.Interfaces;
using Turismo_V1.Models.Entities;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Interface IHomeLodgingRepository que define os métodos para manipulação de alojamentos locais. 
*/
namespace Turismo_V1.Models.Repository.Interfaces 
{ 
  public interface IHomeLodgingRepository : IRepository<HomeLodging>
    {
        #region Methods

        /// <summary>
        /// Get a home lodging by its name
        /// </summary>
        /// <param name="nameHomeLodging"></param>
        /// <returns></returns>
        public HomeLodging? GetHomeLodgingByname(string nameHomeLodging);

        /// <summary>
        /// Creates a new home lodging.
        /// </summary>
        /// <param name="nameHomeLodging"></param>
        /// <param name="address"></param>
        /// <param name="pricePerNight"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public bool CreateHomeLodging(string nameHomeLodging, string address, double pricePerNight, DateTime dateStart, DateTime dateEnd);

        #endregion

    }
}
