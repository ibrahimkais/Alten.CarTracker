using Newtonsoft.Json;
using System;
using System.IO;

namespace Alten.CarTracker.Simulation.Simulator.Data
{
	public sealed class DataLoader
	{
		private static readonly Lazy<DataLoader> lazy = new Lazy<DataLoader>(() => new DataLoader());

		public static DataLoader Instance => lazy.Value;

		private DataLoader()
		{
		}

		public Car[] Cars { get; set; } = LoadCars();

		private static Car[] LoadCars()
		{
			
			Car[] cars = JsonConvert.DeserializeObject<Car[]>(File.ReadAllText($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}JsonData{Path.DirectorySeparatorChar}cars.json"));
			foreach (Car car in cars)
			{
				car.Rootobject = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}JsonData{Path.DirectorySeparatorChar}{car.VinCode}_Short.json"));
			}
			return cars;
		}
	}
}