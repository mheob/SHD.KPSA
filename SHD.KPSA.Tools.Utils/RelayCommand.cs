namespace SHD.KPSA.Tools.Utils
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    /// The default return value for the CanExecutemethod is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion Constructors

        #region ICommand Members
        /// <summary>
        /// Query whether the execution is possible.
        /// </summary>
        /// <param name="parameters">The command that is to be checked.</param>
        /// <returns>The possibility of the execution.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return canExecute?.Invoke(parameters) ?? true;
        }

        /// <summary>
        /// Event that checks whether the possibility of execution has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameters">The command to be executed.</param>
        public void Execute(object parameters)
        {
            execute(parameters);
        }
        #endregion ICommand Members
    }
}