using Turismo_V1.Models.Collections.Interfaces;
using Turismo_V1.Models.Entities;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Interface IRegisterRepository que define os métodos para manipulação de registos de clientes.
*/
namespace Turismo_V1.Models.Repository.Interfaces
{
    public interface IRegisterRepository : IRepository<Register>
    {
        #region Methods

        /// <summary>
        /// Get a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Register? GetUserByUsername(string username);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="cellPhoneNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool CreateUser(string username, string password, string email, string cellPhoneNumber, string name);

        #endregion

        


    }
}
