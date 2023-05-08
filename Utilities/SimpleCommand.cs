using System;
using System.Windows.Input;

namespace BowlingScoreCalculator.Utilities
{
    public class SimpleCommand : ICommand
    {
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Properties

        #region Public Properties
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }
        #endregion

        #region Protected Properties
        #endregion

        #region Private Properties
        #endregion

        #endregion

        #region Constructors
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Methods

        #region Public Methods
        /// <summary>
        /// Checks if parameter is executable.
        /// </summary>
        /// <param name="parameter">Parameter to check.</param>
        /// <returns>Whether or not the parameter is executable.</returns>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null) return CanExecuteDelegate(parameter);

            return true;
        }

        /// <summary>
        /// Executes a parameter.
        /// </summary>
        /// <param name="parameter">The parameter to execute</param>
        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #endregion
    }
}
