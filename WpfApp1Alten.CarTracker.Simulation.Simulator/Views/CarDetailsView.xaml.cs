using System.Windows;
using System.Windows.Controls;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Data;
using WpfApp1Alten.CarTracker.Simulation.Simulator.ViewModels;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.Views
{
	/// <summary>
	/// Interaction logic for CarDetailsView.xaml
	/// </summary>
	public partial class CarDetailsView : UserControl
	{
		CarDetailsViewModel carDetailsViewModel;
		public CarDetailsView()
		{
			InitializeComponent();
			carDetailsViewModel = new CarDetailsViewModel();
		}

		public static readonly DependencyProperty CarProperty = DependencyProperty.Register("Car", typeof(Car), typeof(CarDetailsView), new FrameworkPropertyMetadata(null));
		public Car Car
		{
			get => GetValue(CarProperty) as Car;
			set
			{
				carDetailsViewModel.Vehicle = value;
				SetValue(CarProperty, value);
			}
		}
	}
}
