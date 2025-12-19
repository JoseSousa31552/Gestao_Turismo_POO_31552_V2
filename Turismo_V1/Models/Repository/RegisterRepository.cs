using Turismo_V1.Models.Collections;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe RegisterRepository que implementa os métodos para manipulação de registos de clientes.
*/

namespace Turismo_V1.Models.Repository
{
    public class RegisterRepository: Repository<Register> , IRegisterRepository
    {
        /// <summary>
        /// Gets a <see cref="User"/> by username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The <see cref="User"/> if found; otherwise, <c>null</c>.</returns>
       
        #region Constructors
        public RegisterRepository() : base("users.json")
        {
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="cellPhoneNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateUser(string username, string password, string email, string cellPhoneNumber, string name)
        {
            
            var existingUser = Find(user => user.User == username);

            if (existingUser != null)
            {
                return false; 
            }
            Register newUser = new Register(name, email, cellPhoneNumber, username, password);
          
            Add(newUser);

            return true;
        }


        /// <summary>
        /// Get User By Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Register? GetUserByUsername(string username)
        {
            return GetUserUsername(username);
        }


        /// <summary>
        /// Get User By Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private Register? GetUserUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            return Find(x => x.User == username);
        }

        #endregion Methods

    }

}


