using Alten.CarTracker.Infrastructure.Common.DTOs;
using Alten.CarTracker.Infrastructure.Messaging;
using Alten.CarTracker.Simulation.Simulator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alten.CarTracker.Simulation.Simulator
{
	public class TimeManager
	{
		private DateTime _lastCheck;
		private List<Rtept> points = new List<Rtept>();
		private int pointsIndex = 0;
		private bool stop, disconnect;
		public Car Car { get; set; }
		private HttpClient _client;
		private readonly string _controller, _action;

		public TimeManager(Car car, HttpClient client, string contoller, string action)
		{
			_controller = contoller;
			_action = action;
			Car = car;
			points = Car.Rootobject.Gpx.Rte.Rtept.ToList();
			_lastCheck = DateTime.Now;
			_client = client;
		}

		public async Task Start(string vinCode)
		{
			if (Car.VinCode == vinCode)
			{
				stop = false;
				disconnect = false;
				if (pointsIndex == 0)
				{
					await Worker();
				}
			}
		}

		public void Stop(string vinCode)
		{
			if (Car.VinCode == vinCode)
			{
				stop = true;
			}
		}

		public void Disconnect(string vinCode)
		{
			if (Car.VinCode == vinCode)
			{
				disconnect = true;
			}
		}

		private async Task Worker()
		{
			while (pointsIndex < points.Count)
			{
				if (DateTime.Now.Subtract(_lastCheck).Minutes > 0)
				{
					_lastCheck = DateTime.Now;
					DateTime passedDay = _lastCheck.AddMinutes(-1);
					Rtept currentPoint = points[pointsIndex];
					if (!stop)
					{
						pointsIndex++;
					}

					StatusReceivedDTO status = new StatusReceivedDTO()
					{
						VinCode = Car.VinCode,
						ReceivedDate = DateTime.Now,
						X = Convert.ToDouble(currentPoint._lon),
						Y = Convert.ToDouble(currentPoint._lat),
						StatusId = stop ? (int)CarStatuses.Stopped : (int)CarStatuses.Moving
					};

					if (!disconnect)
					{
						string statusSerialized = MessageSerializer.Serialize(status);
						byte[] buffer = Encoding.UTF8.GetBytes(statusSerialized);
						ByteArrayContent content = new ByteArrayContent(buffer);
						content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
						Console.WriteLine(_client.BaseAddress);
						HttpResponseMessage response = await _client.PostAsync($"{_controller}/{_action}", content);
						response.EnsureSuccessStatusCode();
					}

					Console.WriteLine($"Message Sent form {Car.VinCode}");
				}
				Thread.Sleep(59999);
			}
		}
	}
}
