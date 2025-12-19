/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe Booking que representa uma reserva.  
*/
namespace Turismo_V1.Models.Entities
{

    public class Booking : IdClass
    {
        #region Private Fields
        private Register _client;
        private HomeLodging _lodging;
        private DateTime _checkIn;
        private DateTime _checkOut;
        private bool _state;
        #endregion

        #region Public Properties

        /// <summary>
        /// Cliente que realizou a reserva.
        /// </summary>
        public Register Client
        {
            get => _client;
            set
            {
                if (value == null)
                    throw new ArgumentException("Client cannot be null.", nameof(Client));

                _client = value;
            }
        }

        /// <summary>
        /// Alojamento reservado.
        /// </summary>
        public HomeLodging Lodging
        {
            get => _lodging;
            set
            {
                if (value == null)
                    throw new ArgumentException("Lodging cannot be null.", nameof(Lodging));

                _lodging = value;
            }
        }

        /// <summary>
        /// Data de entrada.
        /// </summary>
        public DateTime CheckIn
        {
            get => _checkIn;
            set
            {
                if (value == default)
                    throw new ArgumentException("CheckIn date is invalid.", nameof(CheckIn));

                _checkIn = value;
            }
        }

        /// <summary>
        /// Data de saída.
        /// </summary>
        public DateTime CheckOut
        {
            get => _checkOut;
            set
            {
                if (value <= CheckIn)
                    throw new ArgumentException("CheckOut must be after CheckIn.", nameof(CheckOut));

                _checkOut = value;
            }
        }

        /// <summary>
        /// true = reserva ativa, false = check-in realizado
        /// </summary>
        public bool State
        {
            get => _state;
            set => _state = value;
        }

        #endregion

        #region Constructor
        public Booking(Register client, HomeLodging lodging, DateTime checkIn, DateTime checkOut)
        {
            Client = client;
            Lodging = lodging;
            CheckIn = checkIn;
            CheckOut = checkOut;

            State = true; // reserva ativa ao ser criada
        }
        #endregion
    }
}