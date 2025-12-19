using System.Windows;
using System.Windows.Input;
using Turismo_V1.Models.Entities;
using Turismo_V1.Models.Repository.Interfaces;
using Turismo_V1.Views.Enums;
using Turismo_V1.Views.Interfaces;

/**
 * Trabalho de POO
 * @author José Sousa - 31552
 * @version 1.0
 * Logic de LoginViewModel
 */

namespace Turismo_V1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private readonly IRegisterRepository _repository;
        private readonly IViewFactory _viewFactory;

        private string _username = string.Empty;
        private string _password = string.Empty;

        public Action? HideWindowAction { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegistryCommand { get; }

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

        #endregion Fields

        #region Constructors

        public LoginViewModel(IViewFactory viewFactory, IRegisterRepository repository)
        {
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));

            
            _repository = repository;

            LoginCommand = new ViewModelCommand(ExecuteLoginCommand);

            
            RegistryCommand = new ViewModelCommand(ExecuteRegistryCommand);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Executes the login command.
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteLoginCommand(object parameter)
        {
            Register? user = _repository.GetUserByUsername(Username);

            if (user != null && user.Password == Password)
            {
                try
                {
                    Window window = _viewFactory.CreateView(ViewType.Main, user);
                    HideWindowAction?.Invoke();
                    window.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Login OK, mas erro ao abrir janela Main: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Dados incorretos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Executes the registry command.
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteRegistryCommand(object param)
        {
            Window window = _viewFactory.CreateView(ViewType.Registry);
            HideWindowAction?.Invoke();
            window?.Show();
        }


        #endregion
    }
}