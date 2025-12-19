using System.Windows.Input;

namespace Turismo_V1.ViewModels.Commands
{
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// The action to be executed by the command.
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter) => true;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter) => _execute((T)parameter!);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;
    }
}
