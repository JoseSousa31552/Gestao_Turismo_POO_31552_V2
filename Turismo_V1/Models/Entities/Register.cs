
using Turismo_V1.Models.Collections;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe Register que representa um registo de cliente, herda da classe cliente.
*/
namespace Turismo_V1.Models.Entities
{
    public class Register : Cliente
    {
        #region Private Properties
        private string _user;
        private string _password;
        #endregion Private Properties

        #region Public Properties
        /// <summary>
        /// Nome de utilizador do registo.
        /// </summary>
        public string User {
            get => _user;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("User cannot be null or empty.", nameof(User));
                if (value.Length > 100)
                    throw new ArgumentException("User cannot be longer than 100 characters.", nameof(User));
                _user = value;
            }
        }
        /// <summary>
        /// Password do registo.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Password cannot be null or empty.", nameof(Password));
                if (value.Length > 100)
                    throw new ArgumentException("Password cannot be longer than 100 characters.", nameof(Password));
                _password = value;
            }
        }


        /// <summary>
        /// Construtor da classe Register.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="cellPhoneNumber"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Register(string name, string email, string cellPhoneNumber, string user, string password)
            : base(name, email, cellPhoneNumber)
        {

            User = user;
            Password = password;
        }


        #endregion Public Properties

       
    }
}
