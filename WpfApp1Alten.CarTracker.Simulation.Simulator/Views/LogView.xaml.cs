using System.Windows.Controls;
using WpfApp1Alten.CarTracker.Simulation.Simulator.ViewModels;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.Views
{
	/// <summary>
	/// Interaction logic for LogView.xaml
	/// </summary>
	public partial class LogView : UserControl
	{
		LogViewModel LogViewModel = new LogViewModel();

		public LogView()
		{
			InitializeComponent();
			DataContext = LogViewModel;
		}
	}
}
