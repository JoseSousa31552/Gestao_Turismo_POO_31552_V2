using System.Collections.ObjectModel;
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
* HomeLodgingViewModel que gere a lógica para a visualização de alojamentos.
*/

namespace Turismo_V1.ViewModels
{
    public class HomeLodgingViewModel : BaseViewModel
    {
        #region Fields

        public Action? HideWindowAction { get; set; }
        private readonly IHomeLodgingRepository _repository;
        private readonly IViewFactory _viewFactory;
        private readonly Register _currentUser;
        public ObservableCollection<HomeLodging> Lodgings { get; set; }

        public ICommand OpenBookingCommand { get; }
        public ICommand GoBackMainCommand { get; }

        #endregion Fields

        #region Constructors

        public HomeLodgingViewModel(IHomeLodgingRepository repository, Register user, IViewFactory viewFactory)
        {
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            _repository = repository;
            _currentUser = user;

            var listaDoFicheiro = _repository.GetAll();

            
            Lodgings = new ObservableCollection<HomeLodging>(listaDoFicheiro);

            OpenBookingCommand = new ViewModelCommand(OpenBookingWindow);

            GoBackMainCommand = new ViewModelCommand(GoBackMain);
        }

        #endregion Constructors


        #region Methods

        /// <summary>
        /// Abre a janela de confirmação de reserva.
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenBookingWindow(object parameter)
        {
            
            var selected = parameter as HomeLodging;

            if (selected == null)
                return;

           
            var dadosParaEnviar = (selected, _currentUser);

            
            Window window = _viewFactory.CreateView(ViewType.BookingConfirmation, dadosParaEnviar);
            
            window.ShowDialog();
        }




        /// <summary>
        /// Volta para a janela principal.
        /// </summary>
        /// <param name="parameter"></param>
        private void GoBackMain(object parameter)
        {
           
           Window window = _viewFactory.CreateView(ViewType.Main, _currentUser);
           HideWindowAction?.Invoke();
           window.ShowDialog();
           
            
        }

        #endregion Methods

    }
}