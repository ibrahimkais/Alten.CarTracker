using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Commands;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Data;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.ViewModels
{
	public class CarViewModel
	{
		public CarViewModel()
		{
			LoadCars();
		}

		public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

		public void LoadCars() => DataLoader.Instance.Cars.ToList().ForEach(car =>
		{
			Cars.Add(car);
		});
	}
}
