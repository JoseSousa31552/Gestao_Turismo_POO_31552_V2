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
* CheckInViewModel que implementa a ViewModel para a funcionalidade de Check-In.
*/

namespace Turismo_V1.ViewModels
{

    public class CheckInViewModel : BaseViewModel
    {
        #region Fields

        private readonly Register _currentUser;
        private readonly ICheckInRepository _checkinRepo;
        private readonly IViewFactory _viewFactory;
        public ICommand GoBackMainCommand { get; }
        public Action? HideWindowAction { get; set; }
        public ObservableCollection<CheckIN> _checkIns;

        public ObservableCollection<CheckIN> CheckIns
        {
            get => _checkIns;
            set
            {
                _checkIns = value;
              
                OnPropertyChanged(nameof(CheckIns));
            }
        }
        #endregion Fields


        #region Constructors

        public CheckInViewModel(ICheckInRepository checkinRepo, Register currentUser,IViewFactory viewFactory)
        {
            _checkinRepo = checkinRepo;
            _currentUser = currentUser;
            _viewFactory = viewFactory;

            var lista = _checkinRepo.GetCheckINByClient(_currentUser);

            CheckIns = new ObservableCollection<CheckIN>(lista);
            GoBackMainCommand = new ViewModelCommand(ExecuteGoBack);
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Volta à janela principal.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteGoBack(object obj)
        {
           
            Window window = _viewFactory.CreateView(ViewType.Main, _currentUser);
            HideWindowAction?.Invoke();
            window.Show();
        }
        #endregion Methods
    }
}





