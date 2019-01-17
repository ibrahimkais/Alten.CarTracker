﻿using Alten.CarTracker.Infrastructure.Common.Domain.Model;
using System;

namespace Alten.CarTracker.Services.StatusReceivedService.Domain.Model
{
	public class Status : BaseEntity<int>
	{
		public DateTime ReceivedDate { get; set; }

		public string Location { get; set; }

		public string CarId { get; set; }

		public int StatusId { get; set; }

		public CarStatusLookup CarStatus { get; set; }

		public Car Car { get; set; }
	}
}
