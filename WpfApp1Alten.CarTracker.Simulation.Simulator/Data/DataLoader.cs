using Newtonsoft.Json;
using System;
using System.IO;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.Data
{
	public class DataLoader
	{
		private static readonly Lazy<DataLoader> lazy = new Lazy<DataLoader>(() => new DataLoader());

		public static DataLoader Instance => lazy.Value;

		public Car[] Cars { get; set; } = LoadCars();

		private DataLoader()
		{

		}

		private static Car[] LoadCars()
		{
			Car[] cars = JsonConvert.DeserializeObject<Car[]>(File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Data\\JsonData\\cars.json"));
			foreach (Car car in cars)
			{
				car.Rootobject = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Data\\JsonData\\{car.pkCarId}_Short.json"));
			}
			return cars;
		}
	}
}