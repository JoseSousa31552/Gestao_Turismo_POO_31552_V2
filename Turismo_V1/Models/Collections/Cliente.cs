using Turismo_V1.Models.Entities;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe Cliente que representa um cliente, classe abstrata. 
*/
namespace Turismo_V1.Models.Collections
{
    
    public abstract class Cliente : IdClass
    {
        

        #region Private Fields

        private string _fullName;
        private string _phoneNumber;
        private string _email;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the full name of the client.
        /// </summary>
        public string Name {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.", nameof(Name));
                if (value.Length > 100)
                    throw new ArgumentException("Name cannot be longer than 100 characters.", nameof(Name));
                _fullName = value;
            }
        }
        /// <summary>
        /// Gets or sets the email of the client.
        /// </summary>
        public string Email {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email cannot be null or empty.", nameof(Email));
                if (value.Length > 100)
                    throw new ArgumentException("Email cannot be longer than 100 characters.", nameof(Email));
                _email = value;
            }
        }
        /// <summary>
        /// Gets or sets the cell phone number of the client.
        /// </summary>
        public string CellPhoneNumber {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be null or empty.", nameof(CellPhoneNumber));
                if (value.Length > 20)
                    throw new ArgumentException("Phone number cannot be longer than 12 characters.", nameof(CellPhoneNumber));
                _phoneNumber = value;
            }
        }

        /// <summary>
        /// Construtor da classe Cliente.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="cellPhoneNumber"></param>
        public Cliente(string name, string email, string cellPhoneNumber)
        {
            Name = name;
            Email = email;
            CellPhoneNumber = cellPhoneNumber;
        }

        #endregion  Properties
    }
}
