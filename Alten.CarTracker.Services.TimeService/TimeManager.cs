using Alten.CarTracker.Infrastructure.Common.Events;
using Alten.CarTracker.Infrastructure.Messaging;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alten.CarTracker.Services.TimeService
{
	public class TimeManager
	{
		DateTime _lastCheck;
		CancellationTokenSource _cancellationTokenSource;
		Task _task;
		IMessagePublisher _messagePublisher;


		public TimeManager(IMessagePublisher messagePublisher)
		{
			_cancellationTokenSource = new CancellationTokenSource();
			_lastCheck = DateTime.Now;
			_messagePublisher = messagePublisher;
		}

		public void Start()
		{
			_task = Task.Run(() => Worker(), _cancellationTokenSource.Token);
		}

		public void Stop()
		{
			_cancellationTokenSource.Cancel();
		}

		private async void Worker()
		{
			while (true)
			{
				if (DateTime.Now.Subtract(_lastCheck).Minutes > 0)
				{
					Log.Information($"Minute has passed!");
					_lastCheck = DateTime.Now;
					DateTime passedDay = _lastCheck.AddMinutes(-1);
					MinuteHasPassed e = new MinuteHasPassed(Guid.NewGuid());
					await _messagePublisher.PublishMessageAsync(e.MessageType, e, "MinuteHasPassed");
				}
				Thread.Sleep(59999);
			}
		}
	}
}
