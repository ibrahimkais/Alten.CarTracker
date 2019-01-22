using System;
using WpfApp1Alten.CarTracker.Simulation.Simulator.Commands;

namespace WpfApp1Alten.CarTracker.Simulation.Simulator.Data
{
	public class Rootobject
	{
		public Gpx gpx { get; set; }
	}

	public class Gpx
	{
		public Metadata metadata { get; set; }
		public Wpt[] wpt { get; set; }
		public Rte rte { get; set; }
	}

	public class Metadata
	{
		public Link link { get; set; }
		public DateTime time { get; set; }
	}

	public class Link
	{
		public string text { get; set; }
		public string _href { get; set; }
	}

	public class Rte
	{
		public string name { get; set; }
		public Rtept[] rtept { get; set; }
	}

	public class Rtept
	{
		public string name { get; set; }
		public string _lat { get; set; }
		public string _lon { get; set; }
	}

	public class Wpt
	{
		public string name { get; set; }
		public string desc { get; set; }
		public string _lat { get; set; }
		public string _lon { get; set; }
	}

	public class Car
	{
		public string pkCarId { get; set; }

		public Rootobject Rootobject { get; set; }

		public Command ConnectCommand { get; set; }

		public Command StatusCommand { get; set; }
	}

	public class Log
	{
		public string Message { get; set; }
	}
}
