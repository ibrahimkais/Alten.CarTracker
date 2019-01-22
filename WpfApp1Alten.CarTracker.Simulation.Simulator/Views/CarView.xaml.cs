using System.Windows.Controls;
using WpfApp1Alten.CarTracker.Simulation.Simulator.ViewModels;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.Views
{
	/// <summary>
	/// Interaction logic for CarView.xaml
	/// </summary>
	public partial class CarView : UserControl
	{
		CarViewModel carViewModelObject = new CarViewModel();
		public CarView()
		{
			InitializeComponent();
			DataContext = carViewModelObject;
		}
	}
}
