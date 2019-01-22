using System.Collections.ObjectModel;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Data;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.ViewModels
{
	public class LogViewModel
	{
		public ObservableCollection<Log> Logs { get; set; } = new ObservableCollection<Log>();

		public void AddLog(Log log)
		{
			Logs.Add(log);
		}
	}
}
