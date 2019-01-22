using System;
using System.Windows.Input;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.Commands
{
	public class Command : ICommand
	{
		readonly Action<object> _TargetExecuteMethod;
		readonly Func<bool> _TargetCanExecuteMethod;

		public Command(Action<object> executeMethod) => _TargetExecuteMethod = executeMethod;

		public Command(Action<object> executeMethod, Func<bool> canExecuteMethod)
		{
			_TargetExecuteMethod = executeMethod;
			_TargetCanExecuteMethod = canExecuteMethod;
		}

		public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);

		bool ICommand.CanExecute(object parameter)
		{
			if (_TargetCanExecuteMethod != null)
			{
				return _TargetCanExecuteMethod();
			}
			if (_TargetExecuteMethod != null)
			{
				return true;
			}
			return false;
		}

		public event EventHandler CanExecuteChanged = delegate { };

		void ICommand.Execute(object parameter) => _TargetExecuteMethod?.Invoke(parameter);
	}
}