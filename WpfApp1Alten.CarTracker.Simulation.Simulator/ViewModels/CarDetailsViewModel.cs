using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Commands;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Data;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.ViewModels
{
	public class CarDetailsViewModel
	{
		public Car Vehicle { get; set; }
		public Command ConnectCommand { get; set; }

		public Command StatusCommand { get; set; }

		public CarDetailsViewModel()
		{
			ConnectCommand = new Command(OnConnectChecked);
			StatusCommand = new Command(OnStatusChecked);
		}

		private void OnConnectChecked(object obj)
		{

		}

		private void OnStatusChecked(object obj)
		{

		}
	}
}
