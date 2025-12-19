using System.Windows;
using System.Windows.Input;
using Turismo_V1.Models.Repository.Interfaces;
using Turismo_V1.Views.Enums;
using Turismo_V1.Views.Interfaces;
using Turismo_V1.ValidationsRegister;

/**
 * Trabalho de POO
 * @author José Sousa - 31552
 * @version 1.0
 * Classe RegistryViewModel que implementa a ViewModel para o registo de utilizadores.
 */

namespace Turismo_V1.ViewModels
{
   public class RegistryViewModel: BaseViewModel
    {

        #region Fields
        private readonly IRegisterRepository _repository;
        private readonly IViewFactory _viewFactory;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _phoneNumber = string.Empty;
        #endregion

        #region Properties

        /// <summary>
        /// Action to hide the associated window. Typically set by the view.
        /// </summary>
        public Action? HideWindowAction { get; set; }

        /// <summary>
        /// Command for executing the registry action.
        /// </summary>
        public ICommand RegistryCommand { get; }

        /// <summary>
        /// Gets or sets the username entered by the user.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        /// <summary>
        /// Gets or sets the password entered by the user.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        /// <summary>
        /// Gets or sets the name entered by the user.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Gets or sets the email entered by the user.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        /// <summary>
        /// Gets or sets the phone number entered by the user.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }
        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryViewModel"/> class.
        /// </summary>
        /// <param name="viewFactory"></param>
        /// <param name="repository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RegistryViewModel(IViewFactory viewFactory, IRegisterRepository repository)
        {
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            _repository =  repository;
            RegistryCommand = new ViewModelCommand(ExecuteRegistryCommand);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the registry command.
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteRegistryCommand(object parameter)
        {

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber))
            {
                MessageBox.Show("Please fill everything ", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            if (!RegisterValidator.IsValidEmail(Email))
            {
                MessageBox.Show("Email inválido! Tem de ter '@' e um ponto '.'", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            
            if (!RegisterValidator.IsValidPhoneNumber(PhoneNumber))
            {
                MessageBox.Show("Número de telemóvel inválido! Deve ter 9 dígitos.", "Validation error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            bool sucesso = _repository.CreateUser(Username, Password, Email, PhoneNumber, Name);

            if (sucesso)
            {
                MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                try
                {
                    Window window = _viewFactory.CreateView(ViewType.Login); 
                    HideWindowAction?.Invoke(); 
                    window.Show();              
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when trying to go to the login: " + ex.Message);
                }
            }
            else
            {
                
                MessageBox.Show("This username is already in use.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion


    }




















}
