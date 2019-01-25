using System;

namespace Alten.CarTracker.Simulation.Simulator.Data
{
	public class Rootobject
	{
		public Gpx Gpx { get; set; }
	}

	public class Gpx
	{
		public Metadata Metadata { get; set; }
		public Wpt[] Wpt { get; set; }
		public Rte Rte { get; set; }
	}

	public class Metadata
	{
		public Link Link { get; set; }
		public DateTime Time { get; set; }
	}

	public class Link
	{
		public string Text { get; set; }
		public string _href { get; set; }
	}

	public class Rte
	{
		public string Name { get; set; }
		public Rtept[] Rtept { get; set; }
	}

	public class Rtept
	{
		public string Name { get; set; }
		public string _lat { get; set; }
		public string _lon { get; set; }
	}

	public class Wpt
	{
		public string Name { get; set; }
		public string Desc { get; set; }
		public string _lat { get; set; }
		public string _lon { get; set; }
	}

	public class Car
	{
		public string VinCode { get; set; }

		public Rootobject Rootobject { get; set; }
	}
}
